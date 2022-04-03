using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [Serializable]
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Comparable))]
    public sealed partial class TrFloat : TrObject
    {
        public float value;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override object Native => value;
        public override List<TrObject> __array__ => null;

        public TrFloat(){ }
        public TrFloat(float floatValue)
        {
            value = floatValue;
        }
        public override TrObject __int__() => MK.Int((long)value);
        public override TrObject __float__() => this;

        public override TrObject __round__(TrObject ndigits)
        {
            if (ndigits.IsNone())
                return MK.Int(System.Convert.ToInt64(value));
            return MK.Float(Math.Round(value, ndigits.AsInt(), MidpointRounding.AwayFromZero));
        }
        public override bool __bool__() => value != 0.0f;

        public override string __repr__() => value.ToString();

        public override int __hash__() => value.GetHashCode();

        [PyBind]
        public static TrObject __new__(TrObject clsobj, TrObject value = null)
        {
            if (value == null)
                return MK.Float(0.0);
            return value.__float__();
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrFloat>("float");
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
    }
}