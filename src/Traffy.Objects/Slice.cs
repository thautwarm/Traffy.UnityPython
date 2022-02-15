using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TrSlice : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;
        public TrObject low;
        public TrObject high;
        public TrObject step;

        public static TrClass CLASS;
        public TrClass Class => CLASS;
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

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TrSlice>();
            CLASS.Name = "slice";
            CLASS.__new = TrSlice.datanew;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

}