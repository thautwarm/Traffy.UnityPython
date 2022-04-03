using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
using UnityEngine.UI;
#endif

namespace Traffy.Unity2D
{
    [PyBuiltin]
    public sealed partial class TrRawImage : TrUnityComponent
    {

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrRawImage>("RawImage");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("RawImage.__new__", cannot_inst_component));
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

#if UNITY_VERSION
        RawImage rawImage;
        public override GameObject gameObject => rawImage.transform.gameObject;
#endif

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool IsUserObject() => false;

        [PyBind]
        public TrObject alpha
        {
            get =>
#if UNITY_VERSION
                MK.Float(rawImage.color.a);
#else
                MK.Float(0.0);
#endif
            set
            {
#if UNITY_VERSION
                var color = rawImage.color;
                color.a = value.ToFloat();
                rawImage.color = color;
#endif
            }
        }
    }
}