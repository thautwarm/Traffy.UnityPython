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
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<SpriteRenderer>();
            if (native_comp == null)
                throw new ValueError("TrSprite.AddComponent: RectTransform has been added!");
            return New(uo, native_comp);
        }

        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<SpriteRenderer>();
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
            var rects = uo.gameObject.GetComponents<SpriteRenderer>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<SpriteRenderer>().Select(x => New(uo, x));
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
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        [PyBind]
        public TrObject __new__(TrClass _, TrGameObject uo)
        {
            return __add_component__(CLASS, uo);
        }

        public static TrSprite New(TrGameObject uo, SpriteRenderer render)
        {
            return new TrSprite(uo, render);
        }

        SpriteRenderer render;
        public static TrSprite FromExisting(SpriteRenderer render)
        {
            var o = TrGameObject.FromRaw(render.gameObject);
            return new TrSprite(o, render);
        }
        public TrSprite(TrGameObject uo, SpriteRenderer render) : base(uo)
        {
            this.render = render;
        }

        public override void RemoveComponent() => Object.Destroy(render);

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        

        [PyBind]
        public TrObject width
        {
            set
            {

                var scale = render.transform.localScale;
                var width_origin = render.sprite.texture.width;
                scale.x = value.ToFloat() / width_origin;
                render.transform.localScale = scale;
            }
            get
            {
                var scale = render.transform.localScale;
                var width_origin = render.sprite.texture.width;
                return MK.Float(width_origin * scale.x);
            }
        }

        [PyBind]
        public TrObject height
        {
            set
            {
                var scale = render.transform.localScale;
                var height_origin = render.sprite.texture.height;
                scale.y = value.ToFloat() / height_origin;
                render.transform.localScale = scale;
            }

            get
            {
                var scale = render.transform.localScale;
                var width_origin = render.sprite.texture.height;
                return MK.Float(width_origin * scale.y);
            }
        }

        public TrObject alpha
        {
            get
            {
                return MK.Float(render.material.color.a);
            }

            set
            {
                if (!(value is TrFloat floating))
                {
                    throw new TypeError($"alpha must be float, not {value.Class.Name}");
                }
                var alpha = floating.value;
                RuntimeValidation.invalidate_int_range("color alpha", alpha, high: 255);
                var color = render.material.color;
                color.a = alpha;
                render.material.color = color;
            }
        }

    }
}

#endif