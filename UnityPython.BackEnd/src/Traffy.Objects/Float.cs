using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    [Serializable]
    public partial class TrFloat : TrObject
    {
        public float value;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public object Native => value;

        public List<TrObject> __array__ => null;
        public bool __bool__() => value != 0.0f;

        string TrObject.__repr__() => value.ToString();

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
            throw new TypeError($"{clsobj.AsClass.Name}.__new__() takes 1 or 2 positional argument(s) but {narg} were given");
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrFloat>("float");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("float.__new__", TrFloat.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrFloat)] = CLASS;
        }
        [Traffy.Annotations.Mark(typeof(TrFloat))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
    }
}