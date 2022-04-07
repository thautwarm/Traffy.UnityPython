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
    public sealed partial class TrPolygonCollider2D : TrUnityComponent
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrPolygonCollider2D>("PolygonCollider2D");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrPolygonCollider2D New(TrUnityObject uo)
        {
            var collider = uo.gameObject.AddComponent<PolygonCollider2D>();
            return new TrPolygonCollider2D(uo, collider);
        }

        PolygonCollider2D collider;
        public TrPolygonCollider2D(TrUnityObject uo, PolygonCollider2D collider) : base(uo)
        {
            this.collider = collider;
        }
        public override bool TryGetNativeUnityObject(out Object o)
        {
            o = collider;
            return true;
        }
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
    }
}
#endif