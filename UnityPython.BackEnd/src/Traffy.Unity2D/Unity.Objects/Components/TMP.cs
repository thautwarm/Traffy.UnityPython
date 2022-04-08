using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Traffy.Unity2D
{

    [PyBuiltin]
    [UnitySpecific]
    [PyInherit(typeof(TrMonoBehaviour))]
    public sealed partial class TrText : TrUIComponent
    {
        internal TextMeshProUGUI native;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrText(TrGameObject uo, TextMeshProUGUI component) : base(uo)
        {
            this.native = component;
        }
        internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
        {
            var native_comp = uo.gameObject.AddComponent<TextMeshProUGUI>();
            if (native_comp == null)
                throw new ValueError("Text.AddComponent: TextMeshProUGUI has been added!");
            return FromRaw(uo, native_comp);
        }
        internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
        {
            var native_comp = uo.gameObject.GetComponent<TextMeshProUGUI>();
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
            var rects = uo.gameObject.GetComponents<TextMeshProUGUI>();
            if (rects == null || rects.Length == 0)
            {
                components = null;
                return false;
            }
            components = uo.gameObject.GetComponents<TextMeshProUGUI>().Select(x => FromRaw(uo, x));
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
        }
        public static TrText FromRaw(TrGameObject uo, TextMeshProUGUI component)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(component, out var allocation))
            {
                return allocation as TrText;
            }
            var result = new TrText(uo, component);
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
        public TrObject size
        {
            get => MK.Float(native.fontSize);
            set
            {
                native.fontSize = value.ToFloat();
            }
        }

        [PyBind]
        public TrObject AlignMiddle => MK.Int((int)TMPro.TextAlignmentOptions.Midline);

        [PyBind]
        public TrObject AlignCenter => MK.Int((int)TMPro.TextAlignmentOptions.Center);

        [PyBind]
        public TrObject AlignLeft => MK.Int((int)TMPro.TextAlignmentOptions.Left);

        [PyBind]
        public TrObject AlignRight => MK.Int((int)TMPro.TextAlignmentOptions.Right);

        [PyBind]
        public TrObject AlignTop => MK.Int((int)TMPro.TextAlignmentOptions.Top);

        [PyBind]
        public TrObject AlignBottom => MK.Int((int)TMPro.TextAlignmentOptions.Bottom);


        [PyBind]
        public TrObject alignment
        {
            get => MK.Int((int)native.alignment);
            set
            {
                native.alignment = (TMPro.TextAlignmentOptions)value.AsInt();
            }
        }

        [PyBind]
        public TrObject autoSize
        {
            get => MK.Bool(native.autoSizeTextContainer);
            set
            {
                native.autoSizeTextContainer = value.AsBool();
            }
        }

        [PyBind]
        public TrObject color
        {
            get => new TrColor(native.color);
            set
            {
                if (value is TrColor c)
                    native.color = c.color;
                else
                    throw new ValueError($"{TrText.CLASS.Name}.{nameof(color)}: expected {TrColor.CLASS.Name}");
            }
        }

        [PyBind]
        public TrObject overflow
        {
            get
            {
                switch (native.overflowMode)
                {
                    case TMPro.TextOverflowModes.Ellipsis:
                        return MK.Str("...");
                    case TMPro.TextOverflowModes.Page:
                        return MK.Str("page");
                    case TMPro.TextOverflowModes.ScrollRect:
                        return MK.Str("scroll");
                    default:
                        throw new ValueError($"{TrText.CLASS.Name}.{nameof(overflow)}: overflow mode {native.overflowMode} is not supported.");
                }
            }

            set
            {
                if (value is TrStr s)
                {
                    switch (s.value)
                    {
                        case "...":
                            native.overflowMode = TMPro.TextOverflowModes.Ellipsis;
                            return;
                        case "page":
                            native.overflowMode = TMPro.TextOverflowModes.Page;
                            return;
                        case "scroll":
                            native.overflowMode = TMPro.TextOverflowModes.ScrollRect;
                            return;
                        default:
                            throw new ValueError($"{TrText.CLASS.Name}.{nameof(overflow)}: {s.value} is not a valid overflow mode.");
                    }
                }
                throw new TypeError($"{TrText.CLASS.Name}.{nameof(overflow)}: expected {TrStr.CLASS.Name}, got {value.Class.Name}");
            }
        }

        [PyBind]
        public TrObject pageCount
        {
            get => MK.Int(native.textInfo.pageCount);

        }

        [PyBind]
        public TrObject characterCount
        {
            get => MK.Int(native.textInfo.characterCount);
        }

        [PyBind]
        public TrObject currentPage
        {
            get => MK.Int(native.pageToDisplay);
            set => native.pageToDisplay = System.Math.Max(1, value.AsInt());
        }

        [PyBind]
        public TrObject maxVisibleCharacters
        {
            get => MK.Int(native.maxVisibleCharacters);
            set => native.maxVisibleCharacters = value.AsInt();
        }



        [PyBind]
        public TrObject contents
        {
            get => MK.Str(native.text);
            set
            {
                native.GetTextInfo(value.AsStr());
            }
        }

        [PyBind]
        public void ForceMeshUpdate([PyBind.Keyword] bool ignoreActiveState = false, [PyBind.Keyword] bool forceTextReparsing = false)
        {
            native.ForceMeshUpdate(ignoreActiveState: ignoreActiveState, forceTextReparsing: forceTextReparsing);
        }


        [PyBind]
        public IEnumerator<TrObject> playText([PyBind.Keyword] string text, [PyBind.Keyword] float speed = 5f)
        {
            native.maxVisibleCharacters = 0;
            var textInfo = native.GetTextInfo(text);
            var pages = textInfo.pageInfo;
            TrObject none = MK.None();
            native.pageToDisplay = 1;

            while (native.pageToDisplay < pages.Length)
            {
                int pageIndex = native.pageToDisplay;
                var first = pages[pageIndex - 1].firstCharacterIndex;
                float cnt = pages[pageIndex - 1].lastCharacterIndex + 0.5f;
                float t = first;
                native.maxVisibleCharacters = first;
                while (true)
                {
                    if (native.pageToDisplay != pageIndex)
                        break;
                    if (t < cnt)
                    {
                        t += Time.deltaTime * speed;
                        native.maxVisibleCharacters = (int)Mathf.Clamp(t, 0, cnt);
                    }
                    yield return none;
                }
                if (native.pageToDisplay >= pages.Length)
                {
                    native.maxVisibleCharacters = 0;
                    yield break;
                }
            }
            native.maxVisibleCharacters = 0;
            yield break;
        }
    }
}
#endif