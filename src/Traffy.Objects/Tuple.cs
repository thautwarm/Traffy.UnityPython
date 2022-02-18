using System;
using System.Collections.Generic;
using System.Linq;

namespace Traffy.Objects
{

    public static class ConverterTuple
    {
        public static TrObject ToTr(this TrObject[] arr) => RTS.tuple_construct(arr);

        public static TrTuple AsTuple(this TrObject tuple) => (TrTuple)tuple;
    }

    [Serializable]
    public partial class TrTuple : TrObject
    {
        public TrObject[] elts;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrTuple>();
            CLASS.Name = "tuple";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("tuple.__new__", TrTuple.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrTuple)] = CLASS;
        }

        [Mark(typeof(TrTuple))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            ModuleInit.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Tuple();
            if (narg == 2 && kwargs == null)
            {
                return MK.Tuple(RTS.object_as_array(args[1]));
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        public string __repr__() =>
            elts.Length == 1 ? $"({elts[0].__repr__()},)" : "(" + String.Join(", ", elts.Select(x => x.__repr__())) + ")";
    }

}