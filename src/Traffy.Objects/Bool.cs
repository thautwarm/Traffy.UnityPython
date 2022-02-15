using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    [Serializable]
    public partial class TrBool: TrObject
    {
        public bool value;

        public bool __bool__() => value;
        public object Native => value;

        public static TrBool TrBool_True;
        public static TrBool TrBool_False;

        private TrBool(bool v)
        {
            value = v;
        }

        static TrBool()
        {
            TrBool_True = new TrBool(true);
            TrBool_False = new TrBool(false);
        }

        public Dictionary<TrObject, TrObject> __dict__ => throw new NotImplementedException();

        public static TrClass CLASS = null;
        public TrClass Class => Class;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Bool(false);
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                switch(arg)
                {
                    case TrFloat _: return arg;
                    case TrInt v: return MK.Float(v.value);
                    case TrStr v: return RTS.parse_float(v.value);
                    case TrBool v: return MK.Float(v.value ? 1.0f: 0.0f);
                    default:
                        throw new InvalidCastException($"cannot cast {arg.Class.Name} objects to {clsobj.AsClass.Name}");
                }
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TrBool>();
            CLASS.Name = "bool";
            CLASS.__new = TrBool.datanew;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

}