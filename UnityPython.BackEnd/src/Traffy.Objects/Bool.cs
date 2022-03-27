using System;
using System.Collections.Generic;


namespace Traffy.Objects
{
    public static class TrObjectFromBool
    {
        public static TrObject ToTr(this bool b)
        {
            return b ? TrBool.TrBool_True : TrBool.TrBool_False;
        }

        public static bool AsBool(this TrObject o)
        {
            return ((TrBool)o).value;
        }
    }

    [Serializable]
    [Traffy.Annotations.PyBuiltin]
    public sealed partial class TrBool : TrObject
    {
        public bool value;

        public override bool __bool__() => value;

        public override string __repr__() => value ? "True" : "False";
        public override object Native => value;
        internal static TrBool TrBool_True = new TrBool(true);
        internal static TrBool TrBool_False = new TrBool(false);

        public TrBool(){ }
        private TrBool(bool v)
        {
            value = v;
        }

        public static TrClass CLASS = null;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Bool(false);
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

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrBool>("bool");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("bool.__new__", TrBool.datanew));
            TrClass.TypeDict[typeof(TrBool)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
    }

}