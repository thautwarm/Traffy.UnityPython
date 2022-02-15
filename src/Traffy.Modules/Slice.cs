using System;
using System.Collections.Generic;

namespace Traffy
{
    public class TrSlice : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;
        public TrObject low;
        public TrObject high;
        public TrObject step;
        public TrClass Class => TrClass.SliceClass;
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
    }

}