using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endif

namespace Traffy.Unity2D
{
    [PyBuiltin]
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

#if UNITY_VERSION
        public PointerEventData eventData;
        public static TrEventData FromRaw(PointerEventData data)
        {
            return new TrEventData { eventData = data };
        }
#endif
        public TrObject ui_hits
        {
            get
            {
#if UNITY_VERSION
                var pos = eventData.pointerCurrentRaycast.worldPosition;
                List<RaycastResult> results = new List<RaycastResult>();
                var raycaster = UnityRTS.Get.MainCanvas.GetComponent<GraphicRaycaster>();
                if (raycaster == null)
                    throw new RuntimeError("No GraphicRaycaster found in the main camera set to UnityRTS");
                raycaster.Raycast(eventData, results);
                return MK.List(results.Select(x =>
                    {
                        TrObject obj = TrUnityObject.FromRaw(x.gameObject);
                        return obj;
                    }
                ).ToList());
#else
                return MK.List();
#endif                
            }
        }

        public TrObject physical_hits
        {
            get
            {
#if UNITY_VERSION
                var pos = eventData.pointerCurrentRaycast.worldPosition;
                List<RaycastResult> results = new List<RaycastResult>();
                var raycaster = UnityRTS.Get.MainCamera.GetComponent<Physics2DRaycaster>();
                if (raycaster == null)
                    throw new RuntimeError("No GraphicRaycaster found in the main camera set to UnityRTS");
                raycaster.Raycast(eventData, results);
                return MK.List(results.Select(x =>
                    {
                        TrObject obj = TrUnityObject.FromRaw(x.gameObject);
                        return obj;
                    }
                ).ToList());
#else
                return MK.List();
#endif                
            }
        }

        public TrObject world_pos
        {
            get
            {
#if UNITY_VERSION
                var pos = eventData.pointerCurrentRaycast.worldPosition;
                return TrVector3.Create(pos);
#else
                return MK.None();
#endif
            }
        }

        public TrObject screen_pos
        {
            get
            {
#if UNITY_VERSION
                var pos = eventData.position;
                return MK.NTuple(MK.Float(pos.x), MK.Float(pos.y));
#else
                return MK.None();
#endif

            }
        }

        

    }
}