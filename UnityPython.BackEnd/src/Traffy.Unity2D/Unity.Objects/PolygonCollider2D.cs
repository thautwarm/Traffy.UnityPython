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
    public sealed partial class TrPolygonCollider2D : TrUnityComponent
    {

        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<PolygonCollider2D>();
            if (native_comp == null)
                throw new ValueError("TrPolygonCollider2D.AddComponent: PolygonCollider2D has been added!");
            return New(uo, native_comp);
        }

        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<PolygonCollider2D>();
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
            var rects = uo.gameObject.GetComponents<PolygonCollider2D>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<PolygonCollider2D>().Select(x => New(uo, x));
            return true;
        }
        
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrPolygonCollider2D>("PolygonCollider2D");
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

        public static TrPolygonCollider2D New(TrGameObject uo, PolygonCollider2D collider2D)
        {
            return new TrPolygonCollider2D(uo, collider2D);
        }

        public override void RemoveComponent()
        {
            Object.Destroy(collider);
        }

        PolygonCollider2D collider;
        public TrPolygonCollider2D(TrGameObject uo, PolygonCollider2D collider) : base(uo)
        {
            this.collider = collider;
        }
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
    }
}
#endif