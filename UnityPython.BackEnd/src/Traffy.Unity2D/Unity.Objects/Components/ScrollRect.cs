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
    public sealed partial class TrScrollRect : TrUIComponent
    {
        ScrollRect native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrScrollRect(TrGameObject uo, ScrollRect component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<ScrollRect>();
            if (native_comp == null)
                throw new ValueError("ScrollRect.AddComponent: ScrollRect has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<ScrollRect>();
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
            var rects = uo.gameObject.GetComponents<ScrollRect>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<ScrollRect>().Select(x => FromRaw(uo, x));
            return true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrScrollRect>("ScrollRect");
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
        public static TrScrollRect FromRaw(TrGameObject uo, ScrollRect component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrScrollRect;
            }
            var result = new TrScrollRect(uo, component);
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
        public float elasticity
        {
            get => native.elasticity;
            set => native.elasticity = value;
        }
        [PyBind]
        public float decelerationRate
        {
            get => native.decelerationRate;
            set => native.decelerationRate = value;
        }

        [PyBind]
        public TrObject verticalScrollbarSpacing
        {
            get => MK.Float(native.verticalScrollbarSpacing);
            set => native.verticalScrollbarSpacing = value.ToFloat();
        }

        [PyBind]
        public TrObject horizontalScrollbarSpacing
        {
            get => MK.Float(native.horizontalScrollbarSpacing);
            set => native.horizontalScrollbarSpacing = value.ToFloat();
        }

        [PyBind]
        public void onValueChanged(TrObject callback)
        {
            native.onValueChanged.AddListener((x) => callback.Call(TrVector2.Create(x)));
        }

        [PyBind]
        public TrObject movementType
        {
            get
            {
                switch (native.movementType)
                {
                    case ScrollRect.MovementType.Unrestricted:
                        return MK.Str("Unrestricted");
                    case ScrollRect.MovementType.Elastic:
                        return MK.Str("Elastic");
                    case ScrollRect.MovementType.Clamped:
                        return MK.Str("Clamped");
                    default:
                        throw new ValueError($"Unknown MovementType {native.movementType}");
                }
            }
            set
            {
                if (!(value is TrStr s))
                {
                    throw new TypeError($"Expected str, got {value.Class.Name}");
                }
                switch (s.value)
                {
                    case "Unrestricted":
                        native.movementType = ScrollRect.MovementType.Unrestricted;
                        break;
                    case "Elastic":
                        native.movementType = ScrollRect.MovementType.Elastic;
                        break;
                    case "Clamped":
                        native.movementType = ScrollRect.MovementType.Clamped;
                        break;
                    default:
                        throw new ValueError($"Unknown MovementType {s.value}");
                }
            }
        }

        [PyBind]
        public bool inertia
        {
            get => native.inertia;
            set => native.inertia = value;
        }
        [PyBind]
        public TrObject content
        {
            get => TrUI.FromRaw(baseObject, native.content);
            set
            {
                if (value is TrUI ui_comp_sealed)
                {
                    native.content = ui_comp_sealed.native;
                }
                else if (value is TrUnityComponent comp)
                {
                    var rectTransform = comp.baseObject.gameObject.GetComponent<RectTransform>();
                    if (rectTransform == null)
                        throw new TypeError("ScrollRect.content: RectTransform needs to be attached!");
                    native.content = rectTransform;
                }
                else
                {
                    throw new TypeError("ScrollRect.content: expected UI or other UIComponent");
                }
            }
        }
        
    }
}

#endif