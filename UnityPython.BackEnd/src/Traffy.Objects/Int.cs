using System;
using System.Collections.Generic;
using Traffy.Annotations;

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
        object TrObject.Native => value;

        string TrObject.__repr__() => value.ToString();

        public static TrClass CLASS;
        TrClass TrObject.Class => CLASS;

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

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrInt>("int");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("int.__new__", TrInt.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrInt)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrInt))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        [PyBind]
        public static TrObject from_bytes(byte[] bytes, string byteorder)
        {
            long x = 0;
            switch (byteorder)
            {
                case "little":
                    {
                        for (int i = 0, j = 0; i < bytes.Length; i++, j += 8)
                        {
                            x |= (long)bytes[i] << j;
                        }
                        return MK.Int(x);
                    }
                case "big":
                    {
                        for (int i = 0, j = 8 * (bytes.Length - 1); i < bytes.Length; i++, j -= 8)
                        {
                            x |= (long)bytes[i] << j;
                        }
                        return MK.Int(x);
                    }
                default:
                    throw new ValueError($"invalid byteorder: {byteorder}");
            }
        }

    }
}