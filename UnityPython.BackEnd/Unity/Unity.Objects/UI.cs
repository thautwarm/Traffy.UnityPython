using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
using UnityEngine.UI;
#endif


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
#if UNITY_VERSION
            var rect = uo.Raw.GetComponent<RectTransform>();
            if (rect == null)
                return new TrUI { rect = rect };
#endif
            return null;
        }
        public static TrUI AddComponent(TrUnityObject uo)
        {
#if UNITY_VERSION
            var rect = uo.Raw.gameObject.AddComponent<RectTransform>();
            if (rect == null)
                return new TrUI { rect = rect };
#endif            
            return null;
        }
        
#if UNITY_VERSION
        RectTransform rect;
        public override GameObject gameObject => rect.gameObject;
#endif
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool IsUserObject() => false;

        public override List<TrObject> __array__
        {
            get
            {
#if UNITY_VERSION
                var tb = rect.GetComponent<TraffyBehaviour>();
                if (tb != null)
                    return tb.TraffyObjects;
#endif
                return null;
            }
        }

        [PyBind]
        public TrObject width
        {
            set
            {
#if UNITY_VERSION
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value.NumToFloat());
#endif
            }

            get
            {
#if UNITY_VERSION
                return MK.Float(rect.rect.width);
#else
                return MK.Float(0.0);
#endif                
            }
        }

        [PyBind]
        public TrObject height
        {
            set
            {
#if UNITY_VERSION
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value.NumToFloat());
#endif
            }

            get
            {
#if UNITY_VERSION
                return MK.Float(rect.rect.height);
#else
                return MK.Float(0.0);
#endif
            }
        }
    }
}