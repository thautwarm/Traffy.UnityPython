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
    public sealed partial class TrText : TrUnityComponent
    {
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<TMPro.TextMeshProUGUI>();
            if (native_comp == null)
                throw new ValueError("TrText.AddComponent: TextMeshProUGUI has been added!");
            return New(uo, native_comp);
        }

        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
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
            var rects = uo.gameObject.GetComponents<TMPro.TextMeshProUGUI>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<TMPro.TextMeshProUGUI>().Select(x => New(uo, x));
            return true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrText>("Text");
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

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [PyBind]
        public TrObject __new__(TrClass _, TrGameObject uo)
        {
            return __add_component__(CLASS, uo);
        }
        internal static TrText New(TrGameObject uo, TMPro.TextMeshProUGUI text)
        {
            return new TrText(uo, text);
        }


        TMPro.TextMeshProUGUI textTMP;
        public TrText(TrGameObject o, TMPro.TextMeshProUGUI textTMP): base(o)
        {
            this.textTMP = textTMP;
        }
        public static TrText FromExisting(TMPro.TextMeshProUGUI textMeshProUGUI)
        {
            var o = TrGameObject.FromRaw(textMeshProUGUI.gameObject);
            return new TrText(o, textMeshProUGUI);
        }

        public override void RemoveComponent()
        {
            Object.Destroy(textTMP);
        }

        [PyBind]
        public TrObject size
        {
            get => MK.Float(textTMP.fontSize);
            set
            {
                textTMP.fontSize = value.ToFloat();
            }
        }

        [PyBind]
        public TrObject contents
        {
            get => MK.Str(textTMP.text);
            set
            {
                textTMP.text = value.AsStr();
            }
        }
        
    }
}

#endif