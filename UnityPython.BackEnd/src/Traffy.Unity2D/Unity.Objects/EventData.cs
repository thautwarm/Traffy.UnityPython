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
    public sealed partial class TrEventData : TrObject
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

        // public TrObject ui_hits
        // {
        //     get
        //     {
        //         List<RaycastResult> results = new List<RaycastResult>();
        //         var raycaster = UnityRTS.Get.MainCanvas.GetComponent<GraphicRaycaster>();
        //         if (raycaster == null)
        //             throw new RuntimeError("No GraphicRaycaster found in the main canvas set to UnityRTS");
        //         raycaster.Raycast(eventData, results);
        //         return MK.List(results.Select(x =>
        //             {
        //                 TrObject obj = TrGameObject.FromRaw(x.gameObject);
        //                 return obj;
        //             }
        //         ).ToList());
        //     }
        // }

        // public TrObject physical2d_hits
        // {
        //     get
        //     {
        //         List<RaycastResult> results = new List<RaycastResult>();
        //         var raycaster = UnityRTS.Get.MainCamera.GetComponent<Physics2DRaycaster>();
        //         if (raycaster == null)
        //             throw new RuntimeError("No Physics2DRaycaster found in the main camera set to UnityRTS");
        //         raycaster.Raycast(eventData, results);
        //         return MK.List(results.Select(x =>
        //             {
        //                 TrObject obj = TrGameObject.FromRaw(x.gameObject);
        //                 return obj;
        //             }
        //         ).ToList());

        //     }
        // }

        // public TrObject world_pos => TrVector3.Create(eventData.pointerCurrentRaycast.worldPosition);
        public TrObject screen_pos => TrVector2.Create(eventData.position);
        public TrObject delta =>
                TrVector2.Create(eventData.delta);

        [PyBind]
        public int clickCount => eventData.clickCount;

        [PyBind]
        public float clickTime => eventData.clickTime;

        [PyBind]
        public TrObject hovered => MK.Iter(eventData.hovered.Select(TrGameObject.FromRaw).GetEnumerator());

        [PyBind]
        public bool is_dragging =>
                eventData.dragging;
        
        [PyBind]
        public bool is_scrolling =>
                eventData.IsScrolling();
        
        [PyBind]
        public float scroll_delta_y => eventData.scrollDelta.y;
        
        [PyBind]
        public TrObject is_pointer_moving =>
                MK.Bool(eventData.IsPointerMoving());

    }
}

#endif