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

        public Dictionary<TrObject, TrObject> __dict__ => throw new NotImplementedException();

        public static TrClass CLASS;
        public TrClass Class => CLASS;
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("tuple");
            CLASS.Name = "tuple";
            CLASS.Fixed = true;
            CLASS.IsSealed = true;
            CLASS.__new = TrTuple.datanew;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
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