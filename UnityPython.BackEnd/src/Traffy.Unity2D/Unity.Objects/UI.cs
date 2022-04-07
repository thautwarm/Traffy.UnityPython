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
    public sealed partial class TrUI : TrUnityComponent
    {

        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<RectTransform>();
            if (native_comp == null)
                throw new ValueError("TrUI.AddComponent: RectTransform has been added!");
            return New(uo, native_comp);
        }

        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<RectTransform>();
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
            var rects = uo.gameObject.GetComponents<RectTransform>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<RectTransform>().Select(x => New(uo, x));
            return true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrUI>("ui");
            CLASS.UnityKind = TrClass.UnityComponentClassKind.BuiltinComponent;
            CLASS.__add_component__ = __add_component__;
            CLASS.__get_component__ = __get_component__;
            CLASS.__get_components__ = __get_components__;
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

        internal static TrUI New(TrGameObject uo, RectTransform rect)
        {
            return new TrUI(uo, rect);
        }

        [PyBind]
        public TrObject __new__(TrClass _, TrGameObject uo)
        {
            return __add_component__(CLASS, uo);
        }

        public override void RemoveComponent()
        {
            Object.Destroy(rect);
        }

        RectTransform rect;
        public override object Native => rect;

        private TrUI(TrGameObject uo, RectTransform rect): base(uo)
        {
            this.rect = rect;
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [PyBind]
        public TrObject width
        {
            set
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value.ToFloat());
            }

            get => MK.Float(rect.rect.width);
        }

        [PyBind]
        public TrObject height
        {
            set
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value.ToFloat());
            }

            get => MK.Float(rect.rect.height);
        }
    }
}
#endif