using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endif
namespace Traffy.Unity2D
{
    [PyBuiltin]

    public sealed partial class TrVector3 : TrObject
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrVector3>("Vector3");
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
        public static TrObject __new__(float x, float y, float z)
        {
            #if UNITY_VERSION
                var data = new Vector3(x, y, z);
                return new TrVector3(data);
            #else
                return new TrVector3();
            #endif
            
        }
        public override List<TrObject> __array__ => null;
#if UNITY_VERSION
        Vector3 vec3;
        public TrVector3(Vector3 v)
        {
            vec3 = v;
        }
#endif

        
        public override TrObject __add__(TrObject other)
        {

#if UNITY_VERSION
            switch (other)
            {
                case TrVector3 otherVec3:
                    return new TrVector3(vec3 + otherVec3.vec3);
                case TrInt num:
                {
                    var data = vec3;
                    data.x += num.value;
                    data.y += num.value;
                    data.z += num.value;
                    return new TrVector3(data);
                }
                case TrFloat num:
                {
                    var data = vec3;
                    data.x += num.value;
                    data.y += num.value;
                    data.z += num.value;
                    return new TrVector3(data);
                }
                default:
                    throw new TypeError($"Cannot add {other.Class.Name} to Vector3");
            }
#else
            throw new NotImplementedException();
#endif

        }

        public override TrObject __sub__(TrObject a)
        {

#if UNITY_VERSION
            switch (a)
            {
                case TrVector3 otherVec3:
                    return new TrVector3(vec3 - otherVec3.vec3);
                case TrInt num:
                {
                    var data = vec3;
                    data.x -= num.value;
                    data.y -= num.value;
                    data.z -= num.value;
                    return new TrVector3(data);
                }
                case TrFloat num:
                {
                    var data = vec3;
                    data.x -= num.value;
                    data.y -= num.value;
                    data.z -= num.value;
                    return new TrVector3(data);
                }
                default:
                    throw new TypeError($"Cannot subtract {a.Class.Name} from Vector3");
            }
#else
        throw new NotImplementedException();
#endif
        }

        public override TrObject __mul__(TrObject a)
        {

#if UNITY_VERSION
            switch (a)
            {
                case TrVector3 otherVec3:
                {
                    var data = vec3;
                    data.x *= otherVec3.vec3.x;
                    data.y *= otherVec3.vec3.y;
                    data.z *= otherVec3.vec3.z;
                    return new TrVector3(data);
                }
                case TrInt num:
                {
                    var data = vec3;
                    data.x *= num.value;
                    data.y *= num.value;
                    data.z *= num.value;
                    return new TrVector3(data);
                }
                case TrFloat num:
                {
                    var data = vec3;
                    data.x *= num.value;
                    data.y *= num.value;
                    data.z *= num.value;
                    return new TrVector3(data);
                }
                default:
                    throw new TypeError($"Cannot multiply {a.Class.Name} to Vector3");
            }
#else
        throw new NotImplementedException();
#endif
        }

        public override TrObject __matmul__(TrObject a)
        {
#if UNITY_VERSION            
            switch (a)
            {
                case TrVector3 otherVec3:
                {
                    return MK.Float(Vector3.Dot(vec3, otherVec3.vec3));
                }
                default:
                    throw new TypeError($"Cannot do matrix multiplication for {a.Class.Name} to Vector3");
            }
#else
        throw new NotImplementedException();
#endif
        }
        public override TrObject __truediv__(TrObject a)
        {
#if UNITY_VERSION
            switch (a)
            {
                case TrVector3 otherVec3:
                {
                    var data = vec3;
                    data.x /= otherVec3.vec3.x;
                    data.y /= otherVec3.vec3.y;
                    data.z /= otherVec3.vec3.z;
                    return new TrVector3(data);
                }
                case TrInt num:
                {
                    var data = vec3;
                    data.x /= num.value;
                    data.y /= num.value;
                    data.z /= num.value;
                    return new TrVector3(data);
                }
                case TrFloat num:
                {
                    var data = vec3;
                    data.x /= num.value;
                    data.y /= num.value;
                    data.z /= num.value;
                    return new TrVector3(data);
                }
                default:
                    throw new TypeError($"Cannot divide {a.Class.Name} to Vector3");
            }
#else
        throw new NotImplementedException();
#endif
        }

        public override TrObject __abs__()
        {
#if UNITY_VERSION
            var data = vec3;
            data.x = Mathf.Abs(data.x);
            data.y = Mathf.Abs(data.y);
            data.z = Mathf.Abs(data.z);
            return new TrVector3(data);
#else
            throw new NotImplementedException();
#endif
        }

        [PyBind]
        public TrObject x
        {
            get
            {
#if UNITY_VERSION
                return MK.Float(vec3.x);
#else
                return MK.Float(0.0);
#endif
            }

            set
            {
#if UNITY_VERSION
                vec3.x = value.ToFloat();
#endif
            }
        }

        [PyBind]
        public TrObject y
        {
            get
            {
#if UNITY_VERSION
                return MK.Float(vec3.y);
#else
                return MK.Float(0.0);
#endif

            }

            set
            {
#if UNITY_VERSION
                vec3.y = value.ToFloat();
#endif
            }

        }
        [PyBind]
        public TrObject z
        {
            get
            {
#if UNITY_VERSION
                return MK.Float(vec3.z);
#else
                return MK.Float(0.0);
#endif
            }
            set
            {
#if UNITY_VERSION
                vec3.z = value.ToFloat();
#endif
            }
        }
    }
}