using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    [Serializable]
    public partial class TrInt : TrObject
    {
        public Int64 value;
        public object Native => value;
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Int(0L);
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                switch (arg)
                {
                    case TrInt _: return arg;
                    case TrFloat v: return MK.Int((int)v.value);
                    case TrStr v: return RTS.parse_int(v.value);
                    case TrBool v: return MK.Int(v.value ? 1L : 0L);
                    default:
                        throw new InvalidCastException($"cannot cast {arg.Class.Name} objects to {clsobj.AsClass.Name}");
                }
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TrInt>();
            CLASS.Name = "int";
            CLASS.__new = TrInt.datanew;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }
}