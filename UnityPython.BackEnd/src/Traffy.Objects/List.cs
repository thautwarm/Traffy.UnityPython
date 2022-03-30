using System;
using System.Collections.Generic;
using System.Linq;
using InlineHelper;
using Traffy.Annotations;
using static Traffy.SeqUtils;

namespace Traffy.Objects
{
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Comparable), typeof(Traffy.Interfaces.Sequence))]
    public sealed partial class TrList : TrObject, IComparable<TrObject>
    {
        public List<TrObject> container;
        internal int s_ContentCount => container.Count;

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
                return MK.List(IronPython.Runtime.Operations.ListOps.Add(container, lst.container));
            }
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{a.Class.Name}'");
        }

        public override TrObject __mul__(TrObject a)
        {
            if (a is TrInt integer)
            {
                return MK.List(IronPython.Runtime.Operations.ListOps.Multiply(container, checked((int) integer.value)));
            }
            throw new TypeError($"unsupported operand type(s) for *: '{CLASS.Name}' and '{a.Class.Name}'");
        }


        public override string __repr__() => "[" + String.Join(",", container.Select((i) => i.__str__())) + "]";

        public override string __str__() => __repr__();

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
                        var i = checked((int)ith.value);
                        if (Traffy.Compatibility.IronPython.PythonOps.TryFixIndex(ref i, container.Count))
                        {
                            return container[i];
                        }
                        throw new IndexError($"list index out of range");
                    }
                case TrSlice slice:
                    {
                        return MK.List(IronPython.Runtime.Operations.ListOps.GetSlice(container, slice));
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
                        var i = checked((int)oitem.value);
                        if (Traffy.Compatibility.IronPython.PythonOps.TryFixIndex(ref i, container.Count))
                        {
                            container[i] = value;
                            return;
                        }
                        throw new IndexError($"list assignment index out of range");
                    }
                case TrSlice slice:
                    {
                        List<TrObject> rhs;
                        if (value is TrList lst)
                        {
                            rhs = lst.container;
                        }
                        else
                        {
                            rhs = RTS.object_to_list(value);
                        }
                        IronPython.Runtime.Operations.ListOps.SetItem(
                            container, slice, rhs
                        );
                        return;
                    }
                default:
                    throw new TypeError($"list indices must be integers or slices, not '{item.Class.Name}'");
            }
        }


        [PyBind]
        public TrObject index(TrObject x, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0, [PyBind.Keyword(Only = true)] bool noraise = false)
        {
            var index = container.Inline().IndexEltGenericSimple<FList<TrObject>, TrObject>(x, start, end);
            if (index == -1 && !noraise)
                throw new ValueError($"list.index(x): x not in list");
            return MK.Int(index);
        }

        [PyBind]
        public long count(
            TrObject x, int start = 0,
            [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            return container.Inline().CountGenericSimple<FList<TrObject>, TrObject>(x, start, end);
        }

        #endregion Sequence


        public override void __delitem__(Traffy.Objects.TrObject item)
        {
            // use List instead of FList here, because delete functions check it
            DeleteItemsSupportSlice(container, item, CLASS);
        }

        [PyBind]
        public void append(TrObject elt)
        {
            container.Add(elt);
            return;
        }

        [PyBind]
        public void extend(TrObject other)
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
            return;
        }

        [PyBind]
        public void insert(TrObject index, TrObject elt)
        {
            if (index is TrInt ith)
            {
                int i = checked((int)ith.value);
                if (Traffy.Compatibility.IronPython.PythonOps.TryFixIndex(ref i, container.Count))
                {
                    container.Insert(i, elt);
                    return;
                }
                throw new IndexError($"list assignment index out of range");
            }
            else
            {
                throw new TypeError($"list indices must be integers, not '{index.Class.Name}'");
            }
        }

        [PyBind]
        public void remove(TrObject value)
        {
            var index = container.IndexOf(value);
            if (index == -1)
                throw new ValueError($"list.remove(x): x not in list");
            container.RemoveAt(index);
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
                var i = checked((int)ith.value);
                if (Traffy.Compatibility.IronPython.PythonOps.TryFixIndex(ref i, container.Count))
                {
                    var ret = container[i];
                    container.RemoveAt(i);
                    return ret;
                }
                throw new IndexError($"list assignment index out of range");
            }
            else
            {
                throw new TypeError($"list indices must be integers, not '{index.Class.Name}'");
            }
        }


        [PyBind]
        public void clear()
        {
            container.Clear();
        }

        [PyBind]
        public void reverse()
        {
            container.Reverse();
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
        public TrObject find(TrObject x, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            start = Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(start, container.Count);
            end = Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(end, container.Count);
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