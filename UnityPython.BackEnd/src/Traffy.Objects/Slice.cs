using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    [Serializable]
    public class TrSlice : TrObject
    {
        public TrObject low;
        public TrObject high;
        public TrObject step;

        public static TrClass CLASS;
        TrClass TrObject.Class => CLASS;

        List<TrObject> TrObject.__array__ => null;

        string TrObject.__repr__() => $"slice({low.__repr__()}:{high.__repr__()}:{step.__repr__()})";

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_range(args, 1, 4);
            int narg = args.Count;
            if (narg == 1)
            {
                return new TrSlice { low = TrNone.Unique, high = TrNone.Unique, step = TrNone.Unique };
            }
            if (narg == 2)
            {
                return new TrSlice { low = TrNone.Unique, high = args[1], step = TrNone.Unique };
            }
            if (narg == 3)
            {
                return new TrSlice { low = args[1], high = args[2], step = TrNone.Unique };
            }
            if (narg == 4)
            {
                return new TrSlice { low = args[1], high = args[2], step = args[3] };
            }
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }


        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSlice>();
            CLASS.Name = "slice";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("slice.__new__", TrSlice.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrSlice)] = CLASS;
        }

        [Mark(typeof(TrSlice))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
    }

}