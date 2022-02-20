using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public static class TrObjectFromInt
    {
        public static TrObject ToTr(this int self) => MK.Int(self);

        public static int AsInt(this TrObject self) => (int)((TrInt)self).value;

        public static int AsIntUnchecked(this TrObject self) => unchecked((int)((TrInt)self).value);
    }
    [Serializable]
    public partial class TrInt : TrObject
    {
        public Int64 value;
        public object Native => value;
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

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
            throw new TypeError($"{clsobj.AsClass.Name}.__new__() takes 1 or 2 positional argument(s) but {narg} were given");
        }

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrInt>();
            CLASS.Name = "int";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("int.__new__", TrInt.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrInt)] = CLASS;
        }

        [Mark(typeof(TrInt))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
    }
}