using System;
using System.Collections.Generic;
using System.Linq;
using InlineHelper;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Sequence))]
    public sealed partial class TrList : TrObject, IComparable<TrObject>
    {
        public List<TrObject> container;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        [PyBind(Name = "compare")]
        int IComparable<TrObject>.CompareTo(TrObject other)
        {
            if (other is TrList lst)
            {
                if (container.Inline().SeqLtE<FList<TrObject>, FList<TrObject>, TrObject>(lst.container, out bool isEqual))
                    return isEqual ? 0 : -1;
                return 1;
            }
            throw new TypeError($"unsupported comparison for '{CLASS.Name}' and '{other.Class.Name}'");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrList>("list");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("list.__new__", TrList.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrList)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.List();
            if (narg == 2 && kwargs == null)
            {
                return MK.List(RTS.object_to_list(args[1]));
            }
            throw new TypeError($"{clsobj.AsClass.Name}.__new__() takes 1 or 2 positional argument(s) but {narg} were given");
        }


        public override TrObject __add__(TrObject a)
        {
            if (a is TrList lst)
            {
                var xs = container.Copy();
                xs.AddRange(lst.container);
                return MK.List(xs);
            }
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{a.Class.Name}'");
        }

        public override TrObject __mul__(TrObject a)
        {
            if (a is TrInt integer)
            {
                var xs = RTS.barelist_create();
                for (int i = 0; i < integer.value; i++)
                {
                    xs.AddRange(container);
                }
                return MK.List(xs);
            }
            throw new TypeError($"unsupported operand type(s) for *: '{CLASS.Name}' and '{a.Class.Name}'");
        }


        public override string __repr__() => "[" + String.Join(",", container.Select((i) => i.__str__())) + "]";

        public override string __str__() => this.AsObject().__repr__();

        #region MutableSequence

        #region Sequence
        #region Collection

        #region Iterable
        public override IEnumerator<TrObject> __iter__()
        {
            return container.GetEnumerator();
        }
        #endregion Iterable

        #region Container
        public override bool __contains__(TrObject a) => container.Contains(a);
        #endregion Container
        public override TrObject __len__() => MK.Int(container.Count);
        #endregion Collection

        #region Reversible
        static IEnumerator<TrObject> _list_reverse(List<TrObject> container)
        {
            for (int i = container.Count - 1; i >= 0; i--)
            {
                yield return container[i];
            }
        }

        public override TrObject __reversed__() => MK.Iter(_list_reverse(container));

        #endregion Reversible

        public override TrObject __getitem__(TrObject item)
        {
            switch (item)
            {
                case TrInt ith:
                    {
                        var i = unchecked((int)ith.value);
                        if (i < 0)
                            i += container.Count;
                        if (i < 0 || i >= container.Count)
                            throw new IndexError($"list index out of range");
                        return container[i];
                    }
                case TrSlice slice:
                    {
                        var (istart, istep, nstep) = slice.mkslice(container.Count);
                        var newcontainer = RTS.barelist_create();
                        for (int i = 0, x = istart; i < nstep; i++, x += istep)
                        {
                            RTS.barelist_add(newcontainer, container[x]);
                        }
                        return MK.List(newcontainer);
                    }
                default:
                    throw new TypeError($"list indices must be integers, not '{item.Class.Name}'");
            }
        }

        public override void __setitem__(TrObject item, TrObject value)
        {
            switch (item)
            {
                case TrInt oitem:
                    {
                        var i = unchecked((int)oitem.value);
                        if (i < 0)
                            i += container.Count;
                        if (i < 0 || i >= container.Count)
                            throw new IndexError($"list assignment index out of range");
                        container[i] = value;
                        return;
                    }
                case TrSlice slice:
                    {
                        var (istart, istep, nstep) = slice.mkslice(container.Count);
                        if (istep == 1 && istart == 0 && nstep == container.Count)
                        {
                            container.Clear();
                            var itr = value.__iter__();
                            while (itr.MoveNext())
                            {
                                RTS.barelist_add(container, itr.Current);
                            }
                            return;
                        }
                        else
                        {
                            var seq = value.__iter__().ToList();
                            if (seq.Count != nstep)
                                throw new ValueError($"attempt to assign sequence of size {seq.Count} to extended slice of size {nstep}");
                            for (int x = istart, i = 0; i < nstep; i++, x += istep)
                            {
                                container[x] = seq[i];
                            }
                            return;
                        }
                    }
                default:
                    throw new TypeError($"list indices must be integers, not '{item.Class.Name}'");
            }
        }

        [PyBind]
        public TrObject index(TrObject x, int start = 0, int end = -1)
        {
            if (end == -1)
                end = container.Count;
            var index = container.IndexOf(x, start, end - start);
            if (index == -1)
                throw new ValueError($"list.index(x): x not in list");
            return MK.Int(index);
        }

        [PyBind]
        public long count(TrObject x)
        {
            long cnt = 0;
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].__eq__(x))
                    cnt++;
            }
            return cnt;
        }

        #endregion Sequence


        public override void __delitem__(Traffy.Objects.TrObject item)
        {
            switch (item)
            {
                case TrInt oitem:
                    {
                        var i = unchecked((int)oitem.value);
                        if (i < 0)
                            i += container.Count;
                        if (i < 0 || i >= container.Count)
                            throw new IndexError($"list assignment index out of range");
                        container.RemoveAt((int)i);
                        return;
                    }
                case TrSlice slice:
                    {
                        var (istart, istep, nstep) = slice.mkslice(container.Count);
                        // XXX: can optimize to O(n)
                        // we may iterate the list, remove the items in the slice, and add the remaining items to the new list
                        if (istep < 0)
                        {
                            for (int x = istart, i = 0; i < nstep; i++, x += istep)
                            {
                                container.RemoveAt(x);
                            }
                        }
                        else
                        {
                            istart += (nstep - 1) * istep;
                            for (int x = istart, i = 0; i < nstep; i++, x -= istep)
                            {
                                container.RemoveAt(x);
                            }
                        }
                        return;
                    }
                default:
                    throw new TypeError($"list indices must be integers, not '{item.Class.Name}'");
            }
        }

        [PyBind]
        public TrObject append(TrObject elt)
        {
            container.Add(elt);
            return MK.None();
        }

        [PyBind]
        public TrObject extend(TrObject other)
        {
            if (other is TrList lst)
            {
                container.AddRange(lst.container);
            }
            else
            {
                var itr = other.__iter__();
                while (itr.MoveNext())
                {
                    container.Add(itr.Current);
                }
            }
            return MK.None();
        }

        [PyBind]
        public TrObject insert(TrObject index, TrObject elt)
        {
            if (index is TrInt ith)
            {
                var i = unchecked((int)ith.value);
                if (i < 0)
                    i += container.Count;
                if (i < 0 || i > container.Count)
                    throw new IndexError($"list assignment index out of range");
                container.Insert(i, elt);
            }
            else
            {
                throw new TypeError($"list indices must be integers, not '{index.Class.Name}'");
            }

            // read file 'a.txt';
            return MK.None();
        }

        [PyBind]
        public TrObject remove(TrObject value)
        {
            var index = container.IndexOf(value);
            if (index == -1)
                throw new ValueError($"list.remove(x): x not in list");
            container.RemoveAt(index);
            return MK.None();
        }

        [PyBind]
        public TrObject pop(TrObject index = null)
        {
            if (index == null)
            {
                if (container.Count == 0)
                    throw new IndexError($"pop from empty list");
                var ret = container[container.Count - 1];
                container.RemoveAt(container.Count - 1);
                return ret;
            }
            else if (index is TrInt ith)
            {
                var i = unchecked((int)ith.value);
                if (i < 0)
                    i += container.Count;
                if (i < 0 || i >= container.Count)
                    throw new IndexError($"list assignment index out of range");
                var ret = container[i];
                container.RemoveAt(i);
                return ret;
            }
            else
            {
                throw new TypeError($"list indices must be integers, not '{index.Class.Name}'");
            }
        }


        [PyBind]
        public TrObject clear()
        {
            container.Clear();
            return MK.None();
        }

        [PyBind]
        public TrObject reverse()
        {
            container.Reverse();
            return MK.None();
        }


        #endregion


        #region Comparable
        public override bool __eq__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqEq<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            return false;
        }

        public override bool __ne__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqNe<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            return true;
        }

        public override bool __gt__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqGt<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'>' not supported between instances of '{Class.Name}' and '" + other.Class.Name + "'");
        }

        public override bool __ge__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqGtE<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'>=' not supported between instances of '{Class.Name}' and '" + other.Class.Name + "'");
        }

        public override bool __lt__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqLt<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'<' not supported between instances of '{Class.Name}' and '" + other.Class.Name + "'");
        }

        public override bool __le__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqLtE<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'<=' not supported between instances of '{Class.Name}' and '" + other.Class.Name + "'");
        }

        #endregion

        [PyBind]
        public TrObject find(TrObject x, int start = 0, int end = -1)
        {
            if (end == -1)
                end = container.Count;
            var index = container.IndexOf(x, start, end - start);
            return MK.Int(index);
        }

        static Comparison<TrObject> _rev_cmp = (TrObject a, TrObject b) =>
        {
            if (a.__eq__(b))
                return 0;
            if (a.__lt__(b))
                return 1;
            return -1;
        };

        static Func<TrObject, Comparison<TrObject>> _normal_cmp_by = (TrObject key) => (TrObject a, TrObject b) =>
        {
            var ka = key.Call(a);
            var kb = key.Call(b);
            if (ka.__eq__(kb))
                return 0;
            if (ka.__lt__(kb))
                return -1;
            return 1;
        };

        static Func<TrObject, Comparison<TrObject>> _rev_cmp_by = (TrObject key) => (TrObject a, TrObject b) =>
        {
            var ka = key.Call(a);
            var kb = key.Call(b);
            if (ka.__eq__(kb))
                return 0;
            if (ka.__lt__(kb))
                return 1;
            return -1;
        };

        [PyBind]
        public TrObject sort(
            [PyBind.Keyword(Only = true)] TrObject key = null,
            [PyBind.Keyword(Only = true)] bool reverse = false)
        {

            if (key == null)
            {
                if (reverse)
                {
                    container.Sort(_rev_cmp);
                }
                else
                {
                    container.Sort();
                }
            }
            else
            {
                if (reverse)
                {
                    container.Sort(_rev_cmp_by(key));
                }
                else
                {
                    container.Sort(_normal_cmp_by(key));
                }
            }
            return MK.None();
        }


        [PyBind]
        public TrObject copy()
        {
            return MK.List(container.Copy());
        }

    }

}