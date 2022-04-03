using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    public static class TrObjectFromInt
    {
        public static TrObject ToTr(this int self) => MK.Int(self);
    }
    [PyBuiltin]
    [Serializable]
    [PyInherit(typeof(Traffy.Interfaces.Comparable))]
    public sealed partial class TrInt : TrObject
    {

        public Int64 value;
        public override object Native => value;

        public TrInt(){ }
        public TrInt(long longValue)
        {
            value = longValue;
        }

        public override string __repr__() => value.ToString();

        public override int __hash__() => value.GetHashCode();

        public override bool __bool__() => value != 0;

        public override TrObject __int__() => this;

        public override TrObject __float__() => MK.Float(value);

        // XXX: CPython behavior: check 'ndigit' should be int
        public override TrObject __round__(TrObject _) => this;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        [PyBind]
        public static TrObject __new__(TrObject clsobj, TrObject value = null)
        {
            if (value == null)
                return MK.IntZero;
            return value.__int__();
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrInt>("int");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
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