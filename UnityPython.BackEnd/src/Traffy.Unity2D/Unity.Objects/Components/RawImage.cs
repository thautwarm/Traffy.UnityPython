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
    public sealed partial class TrRawImage : TrUIComponent
    {
        RawImage native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrRawImage(TrGameObject uo, RawImage component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<RawImage>();
            if (native_comp == null)
                throw new ValueError("RawImage.AddComponent: RawImage has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<RawImage>();
            if (native_comp != null)
            {
                component = FromRaw(uo, native_comp);
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
            components = uo.gameObject.GetComponents<RawImage>().Select(x => FromRaw(uo, x));
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
        }
        public static TrRawImage FromRaw(TrGameObject uo, RawImage component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrRawImage;
            }
            var result = new TrRawImage(uo, component);
            allocations[component] = result;
            return result;
        }
        public override void RemoveComponent()
        {
            UnityRTS.Get.allocations.Remove(native);
            Object.Destroy(native);
        }
        [PyBind]
        public static TrObject __new__(TrClass cls, TrGameObject uo)
        {
            if (!object.ReferenceEquals(cls, CLASS))
                throw new TypeError($"{CLASS.Name}.__new__(): the first argument is class {cls.Name} but expects class {CLASS.Name}");
            return __add_component__(CLASS, uo);
        }
        [PyBind]
        public TrObject alpha
        {
            get => MK.Float(native.color.a);
            set
            {
                var color = native.color;
                color.a = value.ToFloat();
                native.color = color;
            }
        }

        [PyBind]
        public TrObject image
        {
            get
            {
                if (native.texture == null)
                    return MK.None();
                if (!(native.texture is Texture2D tex))
                {
                    throw new ValueError($"{CLASS.Name}: UnityPython so far only supports Texture2D as image!");
                }
                return TrImageResource.FromRaw(tex);
            }

            set
            {
                if (value is TrImageResource image_resource)
                {
                    native.texture = image_resource.native;
                }
                else
                {
                    throw new TypeError($"image must be {TrImageResource.CLASS.Name}, not {value.Class.Name}");
                }
            }
        }
    }
}

#endif