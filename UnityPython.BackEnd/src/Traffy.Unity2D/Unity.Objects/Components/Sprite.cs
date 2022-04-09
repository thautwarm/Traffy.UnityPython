using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    [PyInherit(typeof(TrMonoBehaviour))]
    public sealed partial class TrSprite : TrUnityComponent
    {
        internal SpriteRenderer native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrSprite(TrGameObject uo, SpriteRenderer component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<SpriteRenderer>();
            if (native_comp == null)
                throw new ValueError("Sprite.AddComponent: SpriteRenderer has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<SpriteRenderer>();
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
            var rects = uo.gameObject.GetComponents<SpriteRenderer>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<SpriteRenderer>().Select(x => FromRaw(uo, x));
            return true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSprite>("Sprite");
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
        public static TrSprite FromRaw(TrGameObject uo, SpriteRenderer component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrSprite;
            }
            var result = new TrSprite(uo, component);
            allocations[component] = result;
            return result;
        }
        public override void RemoveComponent()
        {
            UnityRTS.Get.allocations.Remove(native);
            Object.Destroy(native);
        }
        [PyBind]
        public TrObject width
        {
            set
            {
                var scale = native.transform.localScale;
                var width_origin = native.sprite.texture.width;
                scale.x = value.ToFloat() / width_origin;
                native.transform.localScale = scale;
            }
            get
            {
                var scale = native.transform.localScale;
                var width_origin = native.sprite.texture.width;
                return MK.Float(width_origin * scale.x);
            }
        }

        [PyBind]
        public TrObject height
        {
            set
            {
                var scale = native.transform.localScale;
                var height_origin = native.sprite.texture.height;
                scale.y = value.ToFloat() / height_origin;
                native.transform.localScale = scale;
            }

            get
            {
                var scale = native.transform.localScale;
                var width_origin = native.sprite.texture.height;
                return MK.Float(width_origin * scale.y);
            }
        }

        [PyBind]
        public TrObject alpha
        {
            get
            {
                return MK.Float(native.material.color.a);
            }

            set
            {
                if (!(value is TrFloat floating))
                {
                    throw new TypeError($"alpha must be float, not {value.Class.Name}");
                }
                var alpha = floating.value;
                RuntimeValidation.invalidate_int_range("color alpha", alpha, high: 1.0f);
                var color = native.material.color;
                color.a = alpha;
                native.material.color = color;
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