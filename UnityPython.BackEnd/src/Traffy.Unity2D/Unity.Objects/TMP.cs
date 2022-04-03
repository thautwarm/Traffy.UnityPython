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
    public sealed partial class TrFont : TrUnityComponent
    {

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrFont>("Font");
        }
        
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("Font.__new__", cannot_inst_component));
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override bool IsUserObject() => false;

#if UNITY_VERSION
        public override GameObject gameObject => textTMP.transform.gameObject;
        TMPro.TextMeshProUGUI textTMP;
        public static TrFont FromRaw(TMPro.TextMeshProUGUI textTMP)
        {
            return new TrFont { textTMP = textTMP };
        }
#endif

        [PyBind]
        public TrObject size
        {
            get =>
#if UNITY_VERSION
                MK.Float(textTMP.fontSize);
#else
                MK.Float(0.0);
#endif
            set
            {
#if UNITY_VERSION
                textTMP.fontSize = (int)value.ToFloat();
#endif
            }
        }

        [PyBind]
        public TrObject contents
        {
            get =>
#if UNITY_VERSION
                MK.Str(textTMP.text);
#else
                MK.Str("");
#endif
            set
            {
#if UNITY_VERSION
                textTMP.text = value.AsStr();
#endif
            }
        }
        
    }
}
