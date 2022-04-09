using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;
using UnityEngine.EventSystems;


namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrGameObject: TrObject
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrGameObject>("GameObject");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = false;
            Initialization.Prelude(CLASS);
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        public override TrObject __getitem__(TrObject item)
        {
            if (item is TrClass cls && cls.UnityKind != TrClass.UnityComponentClassKind.NotUnity)
            {
                return new TrComponentGroup(this, cls);
            }
            throw new TypeError($"{item.__repr__()} is not a component class");
        }

        public override bool __eq__(TrObject other) => gameObject.Equals(other);
        public GameObject gameObject;
        public MemLessIntMap<List<TrUnityComponent>> Components =
            new MemLessIntMap<List<TrUnityComponent>>();


        private TrGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public static TrGameObject FromRaw(GameObject o)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(o, out var uo))
                return uo as TrGameObject;
            allocations[o] = uo = new TrGameObject(o);
            return uo as TrGameObject;
        }

        public void requireComponents(params TrClass[] klasses)
        {       
            var userComponentClasses =
                    klasses
                        .Where(x => x.UnityKind == TrClass.UnityComponentClassKind.UserComponent)
                        .Select(x => x.ClassId).ToArray();
            
            if (userComponentClasses.Length == 0)
            {
                /* donothing */
            }
            else if (userComponentClasses.Length == 1)
            {
                Components.GetOrUpdate(userComponentClasses[0], () => new List<TrUnityComponent>(1));
            }
            else
            {
                Components
                    .GetOrUpdateMany(
                        userComponentClasses,
                        (_) => new List<TrUnityComponent>(1)
                    );
            }
        }

        [PyBind]
        public static TrGameObject __new__(TrClass cls, string name = "")
        {
            if (!object.ReferenceEquals(cls, CLASS))
                throw new TypeError($"GameObject.__new__(): the first argument must be class GameObject, not {cls.Name}");
            var go = new GameObject(name);
            return FromRaw(go);
        }

        [PyBind]
        public TrObject name
        {
            set
            {
                gameObject.name = value.AsStr();
            }

            get => MK.Str(gameObject.name ?? "");
        }

        [PyBind]
        public TrObject x
        {
            set
            {
                var pos = gameObject.transform.localPosition;
                pos.x = value.ToFloat() / UnityRTS.PixelPerUnit;
                gameObject.transform.localPosition = pos;
            }

            get => MK.Float(gameObject.transform.localPosition.x * UnityRTS.PixelPerUnit);
        }

        [PyBind]
        public TrObject y
        {
            set
            {
                var pos = gameObject.transform.localPosition;
                pos.y = value.ToFloat() / UnityRTS.PixelPerUnit;
                gameObject.transform.localPosition = pos;
            }

            get => MK.Float(gameObject.transform.localPosition.y * UnityRTS.PixelPerUnit);
        }

        [PyBind]
        public TrObject z
        {
            set
            {
                var pos = gameObject.transform.localPosition;
                pos.z = value.ToFloat();
                gameObject.transform.localPosition = pos;
            }

            get => MK.Float(gameObject.transform.localPosition.z);
        }
        

        [PyBind(Name = nameof(requireComponents))]
        internal TrObject _RequireComponents(BList<TrObject> componentTypes, Dictionary<TrObject, TrObject> _)
        {
            requireComponents(componentTypes.Select(x => x.AsClass).ToArray());
            return MK.None();
        }

        [PyBind]
        internal void destory()
        {
            UnityRTS.Get.allocations.Remove(gameObject);
            Object.Destroy(gameObject);
        }

        [PyBind]
        internal TrObject on(TrEventTriggerType o_ev)
        {
            var ev = o_ev.eventID;
            var entry = new EventTrigger.Entry();
            entry.eventID = ev;
            var trigger = gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                if (this.gameObject.GetComponent<RectTransform>() == null && gameObject.GetComponent<Collider2D>() == null)
                    throw new TypeError($"non-UI component needs a collider to accept pointer event!");
                
                trigger = gameObject.AddComponent<EventTrigger>();
            }
            trigger.triggers.Add(entry);
            TrObject register(TrObject callback)
            {
                entry.callback.AddListener(x => callback.Call(TrEventData.FromRaw((PointerEventData)x)));
                return callback;
            }
            return TrSharpFunc.FromFunc($"<{ev} register>", register);
        }

        [PyBind]
        internal TrObject parent
        {
            set
            {
                if (value is TrGameObject go)
                {
                    gameObject.transform.SetParent(go.gameObject.transform, false);
                }
                else if (value.IsNone())
                {
                    gameObject.transform.SetParent(null);
                }
                else
                {
                    throw new TypeError($"parent must be a GameObject or None");
                }

            }

            get
            {
                var parent = gameObject.transform.parent;
                if (parent == null)
                    return MK.None();
                return TrGameObject.FromRaw(parent.gameObject);
            }
        }
    }

}

#endif