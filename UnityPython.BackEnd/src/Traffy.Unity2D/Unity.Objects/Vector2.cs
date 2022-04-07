using System;
using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]

    public sealed partial class TrVector2 : TrObject
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrVector2>("Vector2");
            CLASS.IsSealed = true;
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
        public static TrClass CLASS;

        public override TrClass Class => CLASS;

        [PyBind]
        public static TrObject __new__(float x, float y)
        {

            var data = new Vector2(x, y);
            return new TrVector2(data);


        }
        public override List<TrObject> __array__ => null;

        Vector2 vec2;

        public static TrVector2 Create(Vector2 data)
        {
            return new TrVector2(data);
        }
        private TrVector2(Vector2 v)
        {
            vec2 = v;
        }


        public override IEnumerator<TrObject> __iter__()
        {

            yield return new TrFloat(vec2.x);
            yield return new TrFloat(vec2.y);

        }

        public override TrObject __add__(TrObject other)
        {


            switch (other)
            {
                case TrVector2 otherVec2:
                    return new TrVector2(vec2 + otherVec2.vec2);
                case TrInt num:
                {
                    var data = vec2;
                    data.x += num.value;
                    data.y += num.value;
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x += num.value;
                    data.y += num.value;
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }

        public override TrObject __radd__(TrObject a)
        {
            return __add__(a);
        }

        public override TrObject __sub__(TrObject a)
        {


            switch (a)
            {
                case TrVector2 otherVec2:
                    return new TrVector2(vec2 - otherVec2.vec2);
                case TrInt num:
                {
                    var data = vec2;
                    data.x -= num.value;
                    data.y -= num.value;
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x -= num.value;
                    data.y -= num.value;
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }

        public override TrObject __rsub__(TrObject a)
        {

            switch (a)
            {
                case TrVector2 otherVec2:
                    return new TrVector2(otherVec2.vec2 - vec2);
                case TrInt num:
                {
                    var data = vec2;
                    data.x = num.value - data.x;
                    data.y = num.value - data.y;
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x = num.value - data.x;
                    data.y = num.value - data.y;
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }
        public override TrObject __mul__(TrObject a)
        {


            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    var data = vec2;
                    data.x *= otherVec2.vec2.x;
                    data.y *= otherVec2.vec2.y;
                    return new TrVector2(data);
                }
                case TrInt num:
                {
                    var data = vec2;
                    data.x *= num.value;
                    data.y *= num.value;
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x *= num.value;
                    data.y *= num.value;
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }
        }

        public override TrObject __rmul__(TrObject a)
        {
            return __mul__(a);
        }

        public override TrObject __matmul__(TrObject a)
        {
            
            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    return MK.Float(Vector2.Dot(vec2, otherVec2.vec2));
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }

        public override TrObject __truediv__(TrObject a)
        {

            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    var data = vec2;
                    data.x /= otherVec2.vec2.x;
                    data.y /= otherVec2.vec2.y;
                    return new TrVector2(data);
                }
                case TrInt num:
                {
                    var data = vec2;
                    data.x /= num.value;
                    data.y /= num.value;
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x /= num.value;
                    data.y /= num.value;
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }

        public override TrObject __rtruediv__(TrObject a)
        {

            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    var data = vec2;
                    data.x = otherVec2.vec2.x / data.x;
                    data.y = otherVec2.vec2.y / data.y;
                    return new TrVector2(data);
                }
                case TrInt num:
                {
                    var data = vec2;
                    data.x = num.value / data.x;
                    data.y = num.value / data.y;
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x = num.value / data.x;
                    data.y = num.value / data.y;
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }


        public override TrObject __mod__(TrObject a)
        {

            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    var data = vec2;
                    data.x = NumberMethods.s_floatmod(data.x, otherVec2.vec2.x);
                    data.y = NumberMethods.s_floatmod(data.y, otherVec2.vec2.y);
                    return new TrVector2(data);
                }
                case TrInt num:
                {
                    var data = vec2;
                    data.x = NumberMethods.s_floatmod(data.x, num.value);
                    data.y = NumberMethods.s_floatmod(data.y, num.value);
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x = NumberMethods.s_floatmod(data.x, num.value);
                    data.y = NumberMethods.s_floatmod(data.y, num.value);
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }

        public override TrObject __rmod__(TrObject a)
        {

            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    var data = vec2;
                    data.x = NumberMethods.s_floatmod(otherVec2.vec2.x, data.x);
                    data.y = NumberMethods.s_floatmod(otherVec2.vec2.y, data.y);
                    return new TrVector2(data);
                }
                case TrInt num:
                {
                    var data = vec2;
                    data.x = NumberMethods.s_floatmod(num.value, data.x);
                    data.y = NumberMethods.s_floatmod(num.value, data.y);
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x = NumberMethods.s_floatmod(num.value, data.x);
                    data.y = NumberMethods.s_floatmod(num.value, data.y);
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }

        public override TrObject __pow__(TrObject a)
        {

            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    var data = vec2;
                    data.x = Mathf.Pow(data.x, otherVec2.vec2.x);
                    data.y = Mathf.Pow(data.y, otherVec2.vec2.y);
                    return new TrVector2(data);
                }
                case TrInt num:
                {
                    var data = vec2;
                    data.x = Mathf.Pow(data.x, num.value);
                    data.y = Mathf.Pow(data.y, num.value);
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x = Mathf.Pow(data.x, num.value);
                    data.y = Mathf.Pow(data.y, num.value);
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }

        public override TrObject __rpow__(TrObject a)
        {

            switch (a)
            {
                case TrVector2 otherVec2:
                {
                    var data = vec2;
                    data.x = Mathf.Pow(otherVec2.vec2.x, data.x);
                    data.y = Mathf.Pow(otherVec2.vec2.y, data.y);
                    return new TrVector2(data);
                }
                case TrInt num:
                {
                    var data = vec2;
                    data.x = Mathf.Pow(num.value, data.x);
                    data.y = Mathf.Pow(num.value, data.y);
                    return new TrVector2(data);
                }
                case TrFloat num:
                {
                    var data = vec2;
                    data.x = Mathf.Pow(num.value, data.x);
                    data.y = Mathf.Pow(num.value, data.y);
                    return new TrVector2(data);
                }
                default:
                    return TrNotImplemented.Unique;
            }

        }
        public override TrObject __abs__()
        {

            var data = vec2;
            data.x = Mathf.Abs(data.x);
            data.y = Mathf.Abs(data.y);
            return new TrVector2(data);

        }

        public override TrObject __neg__()
        {

            var data = vec2;
            data.x = -data.x;
            data.y = -data.y;
            return new TrVector2(data);

        }


        [PyBind]
        public TrObject x
        {
            get
            {

                return MK.Float(vec2.x);

            }

            set
            {

                vec2.x = value.ToFloat();

            }
        }

        [PyBind]
        public TrObject y
        {
            get
            {

                return MK.Float(vec2.y);


            }

            set
            {

                vec2.y = value.ToFloat();

            }

        }

        [PyBind]
        public TrObject tovec3()
        {

            return TrVector3.Create(vec2);

        }
    }
}
#endif