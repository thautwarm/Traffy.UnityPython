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
    public sealed partial class TrCanvasGroup : TrUnityComponent
    {
        internal CanvasGroup native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrCanvasGroup(TrGameObject uo, CanvasGroup component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<CanvasGroup>();
            if (native_comp == null)
                throw new ValueError("CanvasGroup.AddComponent: CanvasGroup has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<CanvasGroup>();
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
            var rects = uo.gameObject.GetComponents<CanvasGroup>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<CanvasGroup>().Select(x => FromRaw(uo, x));
            return true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrCanvasGroup>("CanvasGroup");
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
        public static TrCanvasGroup FromRaw(TrGameObject uo, CanvasGroup component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrCanvasGroup;
            }
            var result = new TrCanvasGroup(uo, component);
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
        public bool blocksRaycasts
        {
            set
            {
                native.blocksRaycasts = value;
            }

            get => native.blocksRaycasts;
        }

        [PyBind]
        public float alpha
        {
            set
            {
                RuntimeValidation.invalidate_float_range("canvasgroup alpha", value, 0, 1);
                native.alpha = value;
            }

            get => native.alpha;
        }

        [PyBind]
        public bool ignoreParentGroups
        {
            set
            {
                native.ignoreParentGroups = value;
            }

            get => native.ignoreParentGroups;
        }

        [PyBind]
        public bool interactable
        {
            set
            {
                native.interactable = value;
            }

            get => native.interactable;
        }
    }
}

#endif