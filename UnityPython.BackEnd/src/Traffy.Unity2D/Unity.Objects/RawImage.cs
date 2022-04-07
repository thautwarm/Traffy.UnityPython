using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;
using UnityEngine.UI;


namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    [PyInherit(typeof(TrMonoBehaviour))]
    public sealed partial class TrRawImage : TrUnityComponent
    {

        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<RawImage>();
            if (native_comp == null)
                throw new ValueError("TrRawImage.AddComponent: RawImage has been added!");
            return New(uo, native_comp);
        }

        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<RawImage>();
            if (native_comp != null)
            {
                component = New(uo, native_comp);
                return true;
            }
            component = null;
            return false;
        }

        internal static bool __get_components__(TrClass _, TrGameObject uo, out IEnumerable<TrUnityComponent> components)
        {
            var rects = uo.gameObject.GetComponents<RawImage>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<RawImage>().Select(x => New(uo, x));
            return true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrRawImage>("RawImage");
            CLASS.UnityKind = TrClass.UnityComponentClassKind.BuiltinComponent;
            CLASS.__add_component__ = __add_component__;
            CLASS.__get_component__ = __get_component__;
            CLASS.__get_components__ = __get_components__;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrRawImage New(TrGameObject uo, RawImage image)
        {
            return new TrRawImage(uo, image);
        }
        [PyBind]
        public TrObject __new__(TrClass _, TrGameObject uo)
        {
            return __add_component__(CLASS, uo);
        }


        RawImage rawImage;
        public static TrRawImage FromExisting(RawImage rawImage)
        {
            var o = TrGameObject.FromRaw(rawImage.gameObject);
            return new TrRawImage(o, rawImage);
        }

        public override void RemoveComponent()
        {
            Object.Destroy(rawImage);
        }

        public TrRawImage(TrGameObject uo, RawImage rawImage): base(uo)
        {
            this.rawImage = rawImage;
        }
        public static TrClass CLASS;
        public override TrClass Class => CLASS;

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