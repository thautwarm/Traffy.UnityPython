using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    public static class TrObjectFromInt
    {
        public static TrObject ToTr(this int self) => MK.Int(self);

        public static int AsInt(this TrObject self)
        {
            var i = self as TrInt;
            if (i == null)
            {
                throw new TypeError($"'{self.Class.Name}' object cannot be interpreted as an integer");
            }
            return unchecked((int) i.value);
        }
    }
    [Serializable]
    [PyBuiltin]
    public sealed partial class TrInt : TrObject
    {
        public Int64 value;
        public override object Native => value;

        public override string __repr__() => value.ToString();

        // XXX: CPython behavior: check 'ndigit' should be int
        public override TrObject __round__(TrObject _) => this;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;


        [PyBind]
        public static TrObject __new__(TrObject clsobj, TrObject value)
        {
            switch (value)
            {
                case TrInt _: return value;
                case TrFloat v: return MK.Int((int)v.value);
                case TrStr v: return RTS.parse_int(v.value);
                case TrBool v: return MK.Int(v.value ? 1L : 0L);
                default:
                    throw new InvalidCastException($"cannot cast {value.Class.Name} objects to {clsobj.AsClass.Name}");
            }
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrInt>("int");
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