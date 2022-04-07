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
    public sealed partial class TrUnityObject: TrObject
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("unity");
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
        public MemLessIntMap<List<TrUnityComponent>> Components =
            new MemLessIntMap<List<TrUnityComponent>>();

        public GameObject gameObject;

        public TrUnityObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public static TrUnityObject FromRaw(GameObject o)
        {
            return new TrUnityObject(o);
        }

        public void PreInitComponents(params TrClass[] klasses)
        {       
            var components = Components.GetOrUpdateMany(
                klasses.Select(x => x.ClassId).ToArray(), (_) => new List<TrUnityComponent>(1));
        }

        public bool TryGetComponents(TrClass klass, out List<TrUnityComponent> components)
        {
            return Components.TryGetValue(klass.ClassId, out components);
        }

        public bool TryGetComponent(TrClass klass, out TrUnityComponent value)
        {
            if (TryGetComponents(klass, out var components) && components.Count > 0)
            {
                value = components[0];
                return true;
            }
            value = null;
            return false;
        }

        public TrUnityComponent AddComponent(TrClass klass, TrObject gameState)
        {
            if (!klass.__getic__(klass.ic__new, out var cls_new))
            {
                throw new TypeError($"Cannot add component {klass.Name} to {this.Class.Name}: class {klass.Name} does not have a __new__ from MonoBehaviour");
            }
            var components = Components.GetOrUpdate(
                klass.ClassId,
                () => new List<TrUnityComponent>(1));
            var obj = cls_new.Call(klass, this);
            if (!(obj is TrUnityComponent component))
                throw new TypeError($"Cannot add component {klass.Name} to {this.Class.Name}: __new__ returned {obj.Class.Name} but it is not a component in .NET side.");
            if (klass.__getic__(klass.ic__init, out var cls_init))
            {
                cls_init.Call(obj, gameState);
            }
            components.Add(component);
            return component;
        }

        public List<TrUnityComponent> TryGetComponentsOrAdd(TrClass klass, TrObject gameState, ref bool isNew)
        {
            if (!klass.__getic__(klass.ic__new, out var cls_new))
            {
                throw new TypeError($"Cannot add component {klass.Name} to {this.Class.Name}: class {klass.Name} does not have a __new__ from MonoBehaviour");
            }
            var components = Components.GetOrUpdate(
                klass.ClassId, () => new List<TrUnityComponent>(1));
            if (components.Count == 0)
            {
                var obj = cls_new.Call(klass, this);
                if (!(obj is TrUnityComponent component))
                    throw new TypeError($"Cannot add component {klass.Name} to {this.Class.Name}: __new__ returned {obj.Class.Name} but it is not a component in .NET side.");
                if (klass.__getic__(klass.ic__init, out var cls_init))
                {
                    cls_init.Call(obj, gameState);
                }
                isNew = true;
                components.Add(component);
            }
            return components;
        }

        public TrUnityComponent TryGetComponentOrAdd(TrClass klass, TrObject gameState, ref bool isNew)
        {
            var components = TryGetComponentsOrAdd(klass, gameState, ref isNew);
            return components[0];
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

        [PyBind(Name = nameof(TryGetComponents))]
        internal bool _TryGetComponents(TrObject componentType, TrRef refval)
        {
            if (componentType is TrClass cls)
            {
                if (TryGetComponents(cls, out var components))
                {
                    refval.value = MK.Iter(components.GetEnumerator());
                    return true;
                }
            }
            return false;
        }

        [PyBind(Name = nameof(TryGetComponent))]
        internal bool _TryGetComponent(TrObject componentType, TrRef refval)
        {
            if (componentType is TrClass cls)
            {
                if (TryGetComponent(cls, out var component))
                {
                    refval.value = component;
                    return true;
                }
            }
            return false;
        }

        [PyBind(Name = nameof(AddComponent))]
        internal TrObject _AddComponent(TrObject componentType, TrObject gameState)
        {
            if (componentType is TrClass cls)
            {
                return AddComponent(cls, gameState);
            }
            throw new TypeError($"Cannot add component {componentType.Class.Name} to {this.Class.Name}: {componentType.__repr__()} is not a class");
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
                if (!this.TryGetComponent(TrUI.CLASS, out var component) && gameObject.GetComponent<Collider2D>() == null)
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