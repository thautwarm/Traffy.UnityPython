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
            CLASS.IsFixed = false;
            Initialization.Prelude(CLASS);
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        public override bool __eq__(TrObject other) => gameObject.Equals(other);
        public GameObject gameObject;
        public MemLessIntMap<List<TrUnityComponent>> Components =
            new MemLessIntMap<List<TrUnityComponent>>();


        public TrGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public static TrGameObject FromRaw(GameObject o)
        {
            return new TrGameObject(o);
        }

        public void RequireComponents(params TrClass[] klasses)
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
        public TrObject name
        {
            set
            {
                gameObject.name = value.AsStr();
            }

            get
            {
                return MK.Str(gameObject.name ?? "");
            }
        }

        [PyBind]
        public TrObject x
        {
            set
            {
                var pos = gameObject.transform.localPosition;
                pos.x = value.ToFloat();
                gameObject.transform.localPosition = pos;
            }

            get
            {
                return MK.Float(gameObject.transform.localPosition.x);

            }
        }

        [PyBind]
        public TrObject y
        {
            set
            {
                var pos = gameObject.transform.localPosition;
                pos.y = value.ToFloat();
                gameObject.transform.localPosition = pos;
            }

            get
            {
                return MK.Float(gameObject.transform.localPosition.y);

            }
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

            get
            {
                return MK.Float(gameObject.transform.localPosition.z);

            }
        }

        [PyBind]
        internal bool TryGetComponents(TrObject componentType, TrRef refval)
        {
            if (componentType is TrClass cls && cls.__get_components__ != null)
            {
                if(cls.__get_components__(cls, this, out var components))
                {
                    refval.value = MK.Iter(components.GetEnumerator());
                    return true;
                }
                return false;
            }
            throw new TypeError($"{componentType.Class.Name} is not a component type");
        }

        [PyBind]
        internal bool TryGetComponent(TrObject componentType, TrRef refval)
        {
            if (componentType is TrClass cls && cls.__get_component__ != null)
            {
                if (cls.__get_component__(cls, this, out var component))
                {
                    refval.value = component;
                    return true;
                }
                return false;
            }
            throw new TypeError($"{componentType.Class.Name} is not a component type");
        }

        [PyBind]
        internal TrObject GetComponent(TrObject componentType)
        {
            if (componentType is TrClass cls && cls.__get_component__ != null)
            {
                if (cls.__get_component__(cls, this, out var component))
                {
                    return component;
                }
                return MK.None();
            }
            throw new TypeError($"{componentType.Class.Name} is not a component type");
        }

        [PyBind]
        internal TrUnityComponent AddComponent(TrObject componentType, TrObject gameState, TrObject parameter = null)
        {
            if (componentType is TrClass cls && cls.__add_component__ != null)
            {
                var component = cls.__add_component__(cls, this);
                if (cls.__getic__(cls.ic__init, out var cls_init))
                {
                    cls_init.Call(component, gameState, parameter ?? MK.None());
                }
                return component;
            }
            throw new TypeError($"Cannot add component {componentType.Class.Name} to {this.Class.Name}: {componentType.__repr__()} is not a class");
        }

        [PyBind(Name = nameof(RequireComponents))]
        internal void _RequireComponents(BList<TrObject> componentTypes, Dictionary<TrObject, TrObject> _)
        {
            RequireComponents(componentTypes.Select(x => x.AsClass).ToArray());
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
    }

}

#endif