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
    public sealed partial class TrFont : TrUnityComponent
    {

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrFont>("Font");
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

        [PyBind]
        public TrObject __new__(TrClass _, TrUnityObject uo)
        {
            return New(uo);
        }
        internal static TrFont New(TrUnityObject uo)
        {

            var text = uo.gameObject.AddComponent<TMPro.TextMeshProUGUI>();
            return new TrFont(uo, text);
        }

        public override bool TryGetNativeUnityObject(out Object o)
        {
            o = textTMP;
            return true;
        }
        TMPro.TextMeshProUGUI textTMP;
        public TrFont(TrUnityObject o, TMPro.TextMeshProUGUI textTMP): base(o)
        {
            this.textTMP = textTMP;
        }
        public static TrFont FromExisting(TMPro.TextMeshProUGUI textMeshProUGUI)
        {
            var o = TrUnityObject.FromRaw(textMeshProUGUI.gameObject);
            return new TrFont(o, textMeshProUGUI);
        }

        [PyBind]
        public TrObject size
        {
            get =>
                MK.Float(textTMP.fontSize);
            set
            {
                textTMP.fontSize = value.ToFloat();
            }
        }

        [PyBind]
        public TrObject contents
        {
            get =>
                MK.Str(textTMP.text);
            set
            {
                textTMP.text = value.AsStr();
            }
        }
        
    }
}

#endif