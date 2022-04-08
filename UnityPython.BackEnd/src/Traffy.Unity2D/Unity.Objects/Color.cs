using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;

#if !NOT_UNITY

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrColor : TrObject
    {

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrColor>("Color");
        }
        
        
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrClass CLASS;
        public override List<TrObject> __array__ => null;
        public override TrClass Class => CLASS;

        public Color color;
        public TrColor(Color color)
        {
            this.color = color;
        }
        public override bool __eq__(TrObject other)
        {
            if (other is TrColor otherColor)
            {
                return color.Equals(otherColor.color);
            }
            return base.__eq__(other);
        }

        public override int __hash__()
        {
            return color.GetHashCode();
        }

        [PyBind]
        public static TrColor __new__(TrClass _, float r, float g, float b, float a)
        {
            return new TrColor(new Color(r, g, b, a));
        }

        [PyBind]
        public TrObject r
        {
            get => MK.Float(color.r);
            set => color.r = value.AsFloat();
        }
        [PyBind]
        public TrObject g
        {
            get => MK.Float(color.g);
            set => color.g = value.AsFloat();
        }
        [PyBind]
        public TrObject b
        {
            get => MK.Float(color.b);
            set => color.b = value.AsFloat();
        }
        [PyBind]
        public TrObject a
        {
            get => MK.Float(color.a);
            set => color.a = value.AsFloat();
        }
    }
}

#endif