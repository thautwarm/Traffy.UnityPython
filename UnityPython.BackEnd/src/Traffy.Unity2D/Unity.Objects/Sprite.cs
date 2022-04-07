using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrSprite : TrUnityComponent
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSprite>("Sprite");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        [PyBind]
        public TrObject __new__(TrClass _, TrUnityObject uo)
        {
            return New(uo);
        }

        public static TrSprite New(TrUnityObject uo)
        {
            var render = uo.gameObject.AddComponent<SpriteRenderer>();
            return new TrSprite(uo, render);

        }
        SpriteRenderer render;
        public static TrSprite FromExisting(SpriteRenderer render)
        {
            var o = TrUnityObject.FromRaw(render.gameObject);
            return new TrSprite(o, render);
        }
        public TrSprite(TrUnityObject uo, SpriteRenderer render) : base(uo)
        {
            this.render = render;
        }
        public override bool TryGetNativeUnityObject(out Object o)
        {
            o = render;
            return true;
        }
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