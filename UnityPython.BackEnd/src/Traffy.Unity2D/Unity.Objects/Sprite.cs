using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
#endif


namespace Traffy.Unity2D
{
    [PyBuiltin]
    public sealed partial class TrSprite : TrUnityComponent
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSprite>("Sprite");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("Sprite.__new__", cannot_inst_component));
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
        public static TrSprite GetComponent(TrUnityObject uo)
        {
#if UNITY_VERSION
            var render = uo.Raw.GetComponent<SpriteRenderer>();
            if (render == null)
                return new TrSprite { render = render };
#endif            
            return null;
        }
        public static TrSprite AddComponent(TrUnityObject uo)
        {
#if UNITY_VERSION
            var render = uo.Raw.gameObject.AddComponent<SpriteRenderer>();
            if (render == null)
                return new TrSprite { render = render };
#endif
            return null;
        }
#if UNITY_VERSION
        SpriteRenderer render;
        public override GameObject gameObject => render.gameObject;
#endif
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool IsUserObject() => false;
        
        [PyBind]
        public TrObject width
        {
            set
            {
#if UNITY_VERSION
                var scale = render.transform.localScale;
                var width_origin = render.sprite.texture.width;
                scale.x = value.ToFloat() / width_origin;
                render.transform.localScale = scale;
#endif
            }
            get
            {
#if UNITY_VERSION
                var scale = render.transform.localScale;
                var width_origin = render.sprite.texture.width;
                return MK.Float(width_origin * scale.x);
#else
                return MK.Float(0.0);
#endif
            }
        }

        [PyBind]
        public TrObject height
        {
            set
            {
#if UNITY_VERSION
                var scale = render.transform.localScale;
                var height_origin = render.sprite.texture.height;
                scale.y = value.ToFloat() / height_origin;
                render.transform.localScale = scale;
#endif
            }

            get
            {
#if UNITY_VERSION
                var scale = render.transform.localScale;
                var width_origin = render.sprite.texture.height;
                return MK.Float(width_origin * scale.y);
#else
                return MK.Float(0.0);
#endif
            }
        }

        public TrObject alpha
        {
            get
            {
#if UNITY_VERSION
                return MK.Float(render.material.color.a);
#else
                return MK.Float(0.0);
#endif                
            }

            set
            {
#if UNITY_VERSION
                if (!(value is TrFloat floating))
                {
                    throw new TypeError($"alpha must be float, not {value.Class.Name}");
                }
                var alpha = floating.value;
                RuntimeValidation.invalidate_int_range("color alpha", alpha, high: 255);
                var color = render.material.color;
                color.a = alpha;
                render.material.color = color;
#endif                
            }
        }

    }
}