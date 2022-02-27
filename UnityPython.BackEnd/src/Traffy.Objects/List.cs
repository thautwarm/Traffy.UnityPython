using System.Collections.Generic;
using System;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{
    public sealed partial class TrList : TrObject
    {
        public List<TrObject> container;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

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

        public IEnumerator<TrObject> __iter__()
        {
            return container.GetEnumerator();
        }

        public TrObject __len__() => MK.Int(container.Count);

        public string __repr__() => "[" + String.Join(",", container.Select((i) => i.__str__())) + "]";

        public string __str__() => __repr__();

        TrObject TrObject.__getitem__(TrObject item)
        {
            var oitem = item as TrInt;
            if ((object)oitem != null)
            {
                var i = oitem.value;
                if (i < 0)
                    i += container.Count;
                if (i < 0 || i >= container.Count)
                    throw new IndexError($"list index out of range");
                return container[unchecked((int)i)];
            }
            throw new TypeError($"list indices must be integers, not {item.Class.Name}");
        }

        // bool TrObject.__finditem__(TrObject item, TrRef found)
        // {
        //     var oitem = item as TrInt;
        //     if ((object)oitem != null)
        //     {
        //         var i = oitem.value;
        //         if (i < 0)
        //             i += container.Count;
        //         if (i < 0 || i >= container.Count)
        //             return false;
        //         found.value = container[unchecked((int)i)];
        //         return true;
        //     }
        //     throw new TypeError($"list indices must be integers, not {item.Class.Name}");
        // }

        void TrObject.__setitem__(TrObject item, TrObject value)
        {
            var oitem = item as TrInt;
            if ((object)oitem != null)
            {
                var i = oitem.value;
                if (i < 0)
                    i += container.Count;
                if (i < 0 || i >= container.Count)
                    throw new IndexError($"list assignment index out of range");
                container[unchecked((int)i)] = value;
                return;
            }
            throw new TypeError($"list indices must be integers, not {item.Class.Name}");
        }

        [PyBind]
        public TrObject append(TrObject elt)
        {
            container.Add(elt);
            return MK.None();
        }
    }

}