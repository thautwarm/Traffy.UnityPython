using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;
using UnityEngine.UI;


namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrRawImage : TrUnityComponent
    {

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrRawImage>("RawImage");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrRawImage New(TrUnityObject uo)
        {
            var render = uo.gameObject.AddComponent<RawImage>();
            return new TrRawImage(uo, render);
        }
        [PyBind]
        public TrObject __new__(TrClass _, TrUnityObject uo)
        {
            return New(uo);
        }


        RawImage rawImage;
        public static TrRawImage FromExisting(RawImage rawImage)
        {
            var o = TrUnityObject.FromRaw(rawImage.gameObject);
            return new TrRawImage(o, rawImage);
        }
        public TrRawImage(TrUnityObject uo, RawImage rawImage): base(uo)
        {
            this.rawImage = rawImage;
        }
        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override bool TryGetNativeUnityObject(out Object o)
        {
            o = rawImage;
            return true;
        }

        [PyBind]
        public TrObject alpha
        {
            get => MK.Float(rawImage.color.a);
            set
            {
                var color = rawImage.color;
                color.a = value.ToFloat();
                rawImage.color = color;
            }
        }
    }
}

#endif