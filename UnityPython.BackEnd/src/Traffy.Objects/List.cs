using System;
using System.Collections.Generic;
using System.Linq;
using InlineHelper;
using Traffy.Annotations;

namespace Traffy.Objects
{
    public sealed partial class TrList : TrObject
    {
        public List<TrObject> container;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        List<TrObject> TrObject.__array__ => null;

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

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrList>("list");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("list.__new__", TrList.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrList)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrList))]
        static void _SetupClasses()
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

        IEnumerator<TrObject> TrObject.__iter__()
        {
            return container.GetEnumerator();
        }

        TrObject TrObject.__len__() => MK.Int(container.Count);

        string TrObject.__repr__() => "[" + String.Join(",", container.Select((i) => i.__str__())) + "]";

        string TrObject.__str__() => this.AsObject().__repr__();

        bool TrObject.__eq__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqEq<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            return false;
        }

        bool TrObject.__ne__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqNe<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            return true;
        }

        bool TrObject.__gt__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqGt<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'>' not supported between instances of '{Class.Name}' and '" + other.AsClass.Name + "'");
        }

        bool TrObject.__ge__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqGtE<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'>=' not supported between instances of '{Class.Name}' and '" + other.AsClass.Name + "'");
        }

        bool TrObject.__lt__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqLt<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'<' not supported between instances of '{Class.Name}' and '" + other.AsClass.Name + "'");
        }

        bool TrObject.__le__(Traffy.Objects.TrObject other)
        {
            if (other is TrList lst)
            {
                return container.Inline().SeqLtE<FList<TrObject>, FList<TrObject>, TrObject>(lst.container.Inline());
            }
            throw new TypeError($"'<=' not supported between instances of '{Class.Name}' and '" + other.AsClass.Name + "'");
        }

        [PyBind]
        TrObject copy()
        {
            return MK.List(container.Copy());
        }

        TrObject TrObject.__getitem__(TrObject item)
        {
            switch (item)
            {
                case TrInt ith:
                    {
                        var i = ith.value;
                        if (i < 0)
                            i += container.Count;
                        if (i < 0 || i >= container.Count)
                            throw new IndexError($"list index out of range");
                        return container[unchecked((int)i)];
                    }
                case TrSlice slice:
                    {
                        var (istart, istop, istep) = slice.mkslice(container.Count);
                        var newcontainer = RTS.barelist_create();
                        for (int i = istart; i < istop; i += istep)
                        {
                            RTS.barelist_add(newcontainer, container[i]);
                        }
                        return MK.List(newcontainer);
                    }
                default:
                    throw new TypeError($"list indices must be integers, not '{item.AsClass.Name}'");
            }
        }

        void TrObject.__setitem__(TrObject item, TrObject value)
        {
            switch(item)
            {
                case TrInt oitem:
                {
                    var i = oitem.value;
                    if (i < 0)
                        i += container.Count;
                    if (i < 0 || i >= container.Count)
                        throw new IndexError($"list assignment index out of range");
                    container[unchecked((int)i)] = value;
                    return;
                }
                case TrSlice slice:
                {
                    var (istart, istop, istep) = slice.mkslice(container.Count);
                    if (istep == 1 && istart == 0 && istop == container.Count)
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
                        if (seq.Count != (istop - istart) / istep)
                            throw new ValueError($"attempt to assign sequence of size {seq.Count} to extended slice of size {(istop - istart) / istep}");
                        for (int i = istart, j = 0; i < istop; i += istep, j ++)
                        {
                            container[i] = seq[j];
                        }
                        return;
                    }
                }
                default:
                        throw new TypeError($"list indices must be integers, not '{item.AsClass.Name}'");
            }
        }

        void TrObject.__delitem__(Traffy.Objects.TrObject item)
        {
            switch (item)
            {
                case TrInt oitem:
                {
                    var i = oitem.value;
                    if (i < 0)
                        i += container.Count;
                    if (i < 0 || i >= container.Count)
                        throw new IndexError($"list assignment index out of range");
                    container.RemoveAt(unchecked((int)i));
                    return;
                }
                case TrSlice slice:
                {
                    var (istart, istop, istep) = slice.mkslice(container.Count);
                    // XXX: can optimize to O(n)
                    // we may iterate the list, remove the items in the slice, and add the remaining items to the new list
                    for (int i = istart, j = 0; i < istop; i += istep, j++)
                    {
                        container.RemoveAt(i);
                    }
                    return;
                }
                default:
                    throw new TypeError($"list indices must be integers, not '{item.AsClass.Name}'");
            }
        }

        [PyBind]
        public TrObject append(TrObject elt)
        {
            container.Add(elt);
            return MK.None();
        }
    }

}