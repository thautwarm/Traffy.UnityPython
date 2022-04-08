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
        PolygonCollider2D native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrPolygonCollider2D(TrGameObject uo, PolygonCollider2D component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<PolygonCollider2D>();
            if (native_comp == null)
                throw new ValueError("PolygonCollider2D.AddComponent: PolygonCollider2D has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<PolygonCollider2D>();
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
            var rects = uo.gameObject.GetComponents<PolygonCollider2D>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<PolygonCollider2D>().Select(x => FromRaw(uo, x));
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
        }
        public static TrPolygonCollider2D FromRaw(TrGameObject uo, PolygonCollider2D component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrPolygonCollider2D;
            }
            var result = new TrPolygonCollider2D(uo, component);
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
            if (!object.ReferenceEquals(cls, CLASS)) throw new TypeError($"{CLASS.Name}.__new__(): the first argument is class {cls.Name} but expects class {CLASS.Name}");
            return __add_component__(CLASS, uo);
        }

        [PyBind]
        public void ResetShape(TrSprite obj_sprite, float tolerance = 0.05f)
        {
            List<Vector2> points = new List<Vector2>();
            List<Vector2> simplifiedPoints = new List<Vector2>();
            var sprite = obj_sprite.native.sprite;
            if (sprite == null)
                throw new ValueError("PolygonCollider2D.ResetShape(): SpriteRenderer has no sprite!");
            var pathCount = native.pathCount = sprite.GetPhysicsShapeCount();
            for (int i = 0; i < pathCount; i++)
            {
                sprite.GetPhysicsShape(i, points);
                LineUtility.Simplify(points, tolerance, simplifiedPoints);
                native.SetPath(i, simplifiedPoints.ToArray());

                points.Clear();
                simplifiedPoints.Clear();
            }
        }
    }
}
#endif