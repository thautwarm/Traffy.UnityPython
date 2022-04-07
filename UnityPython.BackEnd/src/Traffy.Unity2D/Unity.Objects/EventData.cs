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
    public sealed class TrEventData : TrObject
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrEventData>("EventData");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("EventData.__new__", cannot_inst_component));
        }

        public static TrObject cannot_inst_component(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"cannot call {CLASS.Name}.__new__()");
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
        public override List<TrObject> __array__ => null;

        public PointerEventData eventData;
        public static TrEventData FromRaw(PointerEventData data)
        {
            return new TrEventData { eventData = data };
        }

        public TrObject ui_hits
        {
            get
            {
                var pos = eventData.pointerCurrentRaycast.worldPosition;
                List<RaycastResult> results = new List<RaycastResult>();
                var raycaster = UnityRTS.Get.MainCanvas.GetComponent<GraphicRaycaster>();
                if (raycaster == null)
                    throw new RuntimeError("No GraphicRaycaster found in the main canvas set to UnityRTS");
                raycaster.Raycast(eventData, results);
                return MK.List(results.Select(x =>
                    {
                        TrObject obj = TrUnityObject.FromRaw(x.gameObject);
                        return obj;
                    }
                ).ToList());

            }
        }

        public TrObject physical_hits
        {
            get
            {
                var pos = eventData.pointerCurrentRaycast.worldPosition;
                List<RaycastResult> results = new List<RaycastResult>();
                var raycaster = UnityRTS.Get.MainCamera.GetComponent<Physics2DRaycaster>();
                if (raycaster == null)
                    throw new RuntimeError("No Physics2DRaycaster found in the main camera set to UnityRTS");
                raycaster.Raycast(eventData, results);
                return MK.List(results.Select(x =>
                    {
                        TrObject obj = TrUnityObject.FromRaw(x.gameObject);
                        return obj;
                    }
                ).ToList());

            }
        }

        public TrObject world_pos => TrVector3.Create(eventData.pointerCurrentRaycast.worldPosition);

        public TrObject screen_pos => TrVector2.Create(eventData.position);

        public TrObject delta =>
                TrVector2.Create(eventData.delta);


        public TrObject is_dragging =>
                MK.Bool(eventData.dragging);
        public TrObject is_scrolling =>
                MK.Bool(eventData.IsScrolling());
        public TrObject scroll_delta_y =>
                MK.Float(eventData.scrollDelta.y);

        public TrObject is_pointer_moving =>
                MK.Bool(eventData.IsPointerMoving());

    }
}

#endif