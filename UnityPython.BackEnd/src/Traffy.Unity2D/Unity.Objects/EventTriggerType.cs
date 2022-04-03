using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#endif

namespace Traffy.Unity2D
{
    [PyBuiltin]
    public sealed partial class TrEventTriggerType : TrObject
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrEventTriggerType>("EventTriggerType");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("EventTriggerType.__new__", cannot_inst_component));
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject cannot_inst_component(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"cannot call {CLASS.Name}.__new__()");
        }

#if UNITY_VERSION
        public EventTriggerType eventID;
#endif
        public static TrClass CLASS;
        public override List<TrObject> __array__ => null;
        public override TrClass Class => CLASS;

        [PyBind]
        public static TrObject PointerEnter =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.PointerEnter };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject PointerExit =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.PointerExit };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject PointerDown =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.PointerDown };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject PointerUp =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.PointerUp };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject PointerClick =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.PointerClick };
#else
            MK.None();  
#endif

        [PyBind]
        public static TrObject Drag =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Drag };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject Drop =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Drop };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject Scroll =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Scroll };
#else
            MK.None();
#endif


        [PyBind]
        public static TrObject UpdateSelected =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.UpdateSelected };
#else
            MK.None();
#endif
        [PyBind]
        public static TrObject Select =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Select };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject Deselect =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Deselect };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject Move =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Move };
#else
            MK.None();  
#endif

        [PyBind]
        public static TrObject InitializePotentialDrag =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.InitializePotentialDrag };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject BeginDrag =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.BeginDrag };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject EndDrag =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.EndDrag };
#else
            MK.None();
#endif


        [PyBind]
        public static TrObject Submit =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Submit };
#else
            MK.None();
#endif

        [PyBind]
        public static TrObject Cancel =
#if UNITY_VERSION
            new TrEventTriggerType { eventID = EventTriggerType.Cancel };
#else
            MK.None();
#endif
    }
}