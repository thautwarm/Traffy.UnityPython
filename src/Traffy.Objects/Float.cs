using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    [Serializable]
    public partial class TrFloat : TrObject
    {
        public float value;
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public object Native => value;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Float(0.0f);
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                switch (arg)
                {
                    case TrFloat _: return arg;
                    case TrInt v: return MK.Float(v.value);
                    case TrStr v: return RTS.parse_float(v.value);
                    case TrBool v: return MK.Float(v.value ? 1.0f : 0.0f);
                    default:
                        throw new InvalidCastException($"cannot cast {arg.Class.Name} objects to {clsobj.AsClass.Name}");
                }
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TrFloat>();
            CLASS.Name = "float";
            CLASS.__new = TrFloat.datanew;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }
}