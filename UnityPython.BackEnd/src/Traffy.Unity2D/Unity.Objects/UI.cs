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
            CLASS.IsSealed = true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        internal static TrUI New(TrUnityObject uo)
        {
            var rect = uo.gameObject.AddComponent<RectTransform>();
            return new TrUI(uo, rect);
        }

        [PyBind]
        public TrObject __new__(TrClass _, TrUnityObject uo)
        {
            return New(uo);
        }

        RectTransform rect;

        public static TrUI FromExisting(RectTransform rect)
        {
            var o = TrUnityObject.FromRaw(rect.gameObject);
            return new TrUI(o, rect);
        }

        private TrUI(TrUnityObject uo, RectTransform rect): base(uo)
        {
            this.rect = rect;
        }
        private TrUI(TrUnityObject uo): base(uo)
        { }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool TryGetNativeUnityObject(out Object o)
        {
            o = rect;
            return true;
        }

        [PyBind]
        public TrObject width
        {
            set
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value.ToFloat());
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
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value.ToFloat());
            }

            get
            {
                return MK.Float(rect.rect.height);
            }
        }
    }
}
#endif