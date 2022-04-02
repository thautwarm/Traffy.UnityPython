using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    public sealed partial class TrUI : TrUnityComponent
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrUI>("ui");
        }
        
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("ui.__new__", cannot_inst_component));
            CLASS.IsSealed = true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
        public static TrUI GetComponent(TrUnityObject uo)
        {
            var rect = uo.Raw.GetComponent<RectTransform>();
            if (rect == null)
                return new TrUI { rect = rect };
            return null;
        }
        public static TrUI AddComponent(TrUnityObject uo)
        {
            var rect = uo.Raw.gameObject.AddComponent<RectTransform>();
            if (rect == null)
                return new TrUI { rect = rect };
            return null;
        }
        RectTransform rect;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool IsUserObject() => false;
        public override GameObject gameObject => rect.gameObject;
        public override List<TrObject> __array__
        {
            get
            {
                var tb = rect.GetComponent<TraffyBehaviour>();
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
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value.NumToFloat());
            }

            get
            {
                return MK.Float(rect.rect.width);
            }
        }

        [PyBind]
        public TrObject height
        {
            set
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value.NumToFloat());
            }

            get
            {
                return MK.Float(rect.rect.height);
            }
        }
    }
}