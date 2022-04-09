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
    public sealed partial class TrSpriteImage : TrUIComponent
    {
        internal Image native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrSpriteImage(TrGameObject uo, Image component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<Image>();
            if (native_comp == null)
                throw new ValueError("SpriteImage.AddComponent: Image has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<Image>();
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
            var rects = uo.gameObject.GetComponents<Image>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<Image>().Select(x => FromRaw(uo, x));
            return true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSpriteImage>("SpriteImage");
            CLASS.UnityKind = TrClass.UnityComponentClassKind.BuiltinComponent;
            CLASS.__add_component__ = __add_component__;
            CLASS.__get_component__ = __get_component__;
            CLASS.__get_components__ = __get_components__;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
        }
        public static TrSpriteImage FromRaw(TrGameObject uo, Image component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrSpriteImage;
            }
            var result = new TrSpriteImage(uo, component);
            allocations[component] = result;
            return result;
        }
        public override void RemoveComponent()
        {
            UnityRTS.Get.allocations.Remove(native);
            Object.Destroy(native);
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
                if (native.sprite == null)
                    return MK.None();
                return TrImageResource.FromRaw(native.sprite.texture);
            }

            set
            {
                if (value is TrImageResource image_resource)
                {
                    native.sprite = image_resource.sprite;
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