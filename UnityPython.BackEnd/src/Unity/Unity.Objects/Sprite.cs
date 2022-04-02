using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
using UnityEngine;

namespace Traffy.Unity2D
{
    public class TrSprite : TrUnityComponent
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSprite>("sprite");
        }
        
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("sprite.__new__", cannot_inst_component));
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
        SpriteRenderer render;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool IsUserObject() => false;
        public override GameObject gameObject => render.gameObject;
        public override List<TrObject> __array__
        {
            get
            {
                var tb = render.GetComponent<TraffyBehaviour>();
                if (tb == null)
                    return null;
                return tb.TraffyObjects;
            }
        }

        [PyBind]
        public TrObject width
        {
            set
            {
                var scale = render.transform.localScale;
                var width_origin = render.sprite.texture.width;
                scale.x = value.NumToFloat() / width_origin;
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
                scale.y = value.NumToFloat() / height_origin;
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