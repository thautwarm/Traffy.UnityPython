using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
#endif

namespace Traffy.Unity2D
{
    [PyBuiltin]
    public sealed partial class TrUnityObject : TrUnityComponent
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("unity");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc(".__new__", cannot_inst_component));
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = false;
            Initialization.Prelude(CLASS);
        }
        TraffyBehaviour traffy;
        public TraffyBehaviour Raw => traffy;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool IsUserObject() => true;
#if UNITY_VERSION
        public override GameObject gameObject => traffy.gameObject;
#endif
        public override List<TrObject> __array__ => traffy.TraffyObjects;

        public TrUnityObject(TraffyBehaviour traffy)
        {
            this.traffy = traffy;
        }

        [PyBind]
        public TrObject name
        {
            set
            {
#if UNITY_VERSION
                traffy.name = value.AsStr();
#endif
            }

            get
            {
#if UNITY_VERSION
                return MK.Str(traffy.name ?? "");
#else
                return MK.Str("");
#endif
            }
        }

        [PyBind]
        public TrObject x
        {
            set
            {
#if UNITY_VERSION
                var pos = traffy.transform.localPosition;
                pos.x = value.NumToFloat();
                traffy.transform.localPosition = pos;
#endif
            }

            get
            {
#if UNITY_VERSION
                return MK.Float(traffy.transform.localPosition.x);
#else
                return MK.Float(0.0);
#endif

            }
        }

        [PyBind]
        public TrObject y
        {
            set
            {
#if UNITY_VERSION
                var pos = traffy.transform.localPosition;
                pos.y = value.NumToFloat();
                traffy.transform.localPosition = pos;
#endif
            }

            get
            {
#if UNITY_VERSION
                return MK.Float(traffy.transform.localPosition.y);
#else
                return MK.Float(0.0);
#endif
            }
        }

        [PyBind]
        public TrObject z
        {
            set
            {
#if UNITY_VERSION
                var pos = traffy.transform.localPosition;
                pos.z = value.NumToFloat();
                traffy.transform.localPosition = pos;
#endif
            }

            get
            {
#if UNITY_VERSION
                return MK.Float(traffy.transform.localPosition.z);
#else
                return MK.Float(0.0);
#endif
            }
        }
    }

}