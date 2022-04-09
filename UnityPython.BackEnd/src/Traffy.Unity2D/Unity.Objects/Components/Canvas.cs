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
    public sealed partial class TrCanvas : TrUnityComponent
    {
        internal Canvas native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrCanvas(TrGameObject uo, Canvas component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<Canvas>();
            if (native_comp == null)
                throw new ValueError("Canvas.AddComponent: Canvas has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<Canvas>();
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
            var rects = uo.gameObject.GetComponents<Canvas>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<Canvas>().Select(x => FromRaw(uo, x));
            return true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrCanvas>("Canvas");
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
        public static TrCanvas FromRaw(TrGameObject uo, Canvas component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrCanvas;
            }
            var result = new TrCanvas(uo, component);
            result.native.renderMode = RenderMode.ScreenSpaceCamera;
            result.native.worldCamera = UnityRTS.Get.MainCamera;
            allocations[component] = result;
            return result;
        }
        public override void RemoveComponent()
        {
            UnityRTS.Get.allocations.Remove(native);
            Object.Destroy(native);
        }
        
        [PyBind]
        public TrObject rootCanvas
        {
            get
            {
                var root = native.rootCanvas;
                return TrCanvas.FromRaw(baseObject, root);
            }
        }

        [PyBind]
        public int sortingOrder
        {
            get => native.sortingOrder;
            set => native.sortingOrder = value;
        }
    }
}

#endif