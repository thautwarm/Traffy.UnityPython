using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

#if !NOT_UNITY

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
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
        public EventTriggerType eventID;
        public static TrClass CLASS;
        public override List<TrObject> __array__ => null;
        public override TrClass Class => CLASS;

        [PyBind]
        public static TrObject PointerEnter =
            new TrEventTriggerType { eventID = EventTriggerType.PointerEnter };


        [PyBind]
        public static TrObject PointerExit =
            new TrEventTriggerType { eventID = EventTriggerType.PointerExit };

        [PyBind]
        public static TrObject PointerDown =
            new TrEventTriggerType { eventID = EventTriggerType.PointerDown };

        [PyBind]
        public static TrObject PointerUp =
            new TrEventTriggerType { eventID = EventTriggerType.PointerUp };


        [PyBind]
        public static TrObject PointerClick =
            new TrEventTriggerType { eventID = EventTriggerType.PointerClick };

        [PyBind]
        public static TrObject Drag =
            new TrEventTriggerType { eventID = EventTriggerType.Drag };

        [PyBind]
        public static TrObject Drop =
            new TrEventTriggerType { eventID = EventTriggerType.Drop };

        [PyBind]
        public static TrObject Scroll =
            new TrEventTriggerType { eventID = EventTriggerType.Scroll };


        [PyBind]
        public static TrObject UpdateSelected =
            new TrEventTriggerType { eventID = EventTriggerType.UpdateSelected };

        [PyBind]
        public static TrObject Select =
            new TrEventTriggerType { eventID = EventTriggerType.Select };

        [PyBind]
        public static TrObject Deselect =
            new TrEventTriggerType { eventID = EventTriggerType.Deselect };

        [PyBind]
        public static TrObject Move =
            new TrEventTriggerType { eventID = EventTriggerType.Move };

        [PyBind]
        public static TrObject InitializePotentialDrag =
            new TrEventTriggerType { eventID = EventTriggerType.InitializePotentialDrag };

        [PyBind]
        public static TrObject BeginDrag =
            new TrEventTriggerType { eventID = EventTriggerType.BeginDrag };

        [PyBind]
        public static TrObject EndDrag =
            new TrEventTriggerType { eventID = EventTriggerType.EndDrag };



        [PyBind]
        public static TrObject Submit =
            new TrEventTriggerType { eventID = EventTriggerType.Submit };


        [PyBind]
        public static TrObject Cancel =
            new TrEventTriggerType { eventID = EventTriggerType.Cancel };
    }
}

#endif