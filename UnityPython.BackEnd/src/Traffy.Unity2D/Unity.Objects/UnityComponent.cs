using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
using UnityEngine.EventSystems;
#endif

namespace Traffy.Unity2D
{

    public abstract class TrUnityComponent : TrUserObjectBase
    {
#if UNITY_VERSION
        public override object Native => gameObject;
        public abstract GameObject gameObject { get; }
#endif
        public override List<TrObject> __array__
        {
            get
            {
#if UNITY_VERSION
                var tb = gameObject.GetComponent<TraffyBehaviour>();
                if (tb != null)
                    return tb.TraffyObjects;
#endif                
                return null;
            }
        }


        bool _equals(TrObject x)
        {
            if (x is TrUnityComponent comp)
            {

#if UNITY_VERSION
                return GameObject.ReferenceEquals(gameObject, comp.gameObject);
#else
                    return object.ReferenceEquals(this, x);
#endif
            }
            return false;
        }

        int _hash()
        {
#if UNITY_VERSION
            return gameObject.GetHashCode();
#else
                return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
#endif
        }

        public override int GetHashCode() => _hash();

        public override bool Equals(object x) => x is TrObject obj && _equals(obj);
        public override int __hash__() => _hash();
        public override bool __eq__(TrObject o) => _equals(o);


        public abstract bool IsUserObject();
        public static TrObject cannot_inst_component(TrClass clsobj, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"cannot directory instantiate class {clsobj.Name}. \n" +
                                $"Maybe you mean 'gameObject.AddComponent({clsobj.Name})'?");
        }

        [PyBind]
        public TrObject baseobject
        {
            get
            {
#if UNITY_VERSION
                if (IsUserObject())
                {
                    var tb = gameObject.GetComponent<TraffyBehaviour>();
                    if (tb == null)
                    {
                        tb = gameObject.AddComponent<TraffyBehaviour>();
                        tb.TraffyObjects = tb.TraffyObjects ?? new List<TrObject>();
                    }
                    return new TrUnityObject(tb);
                }
                else
#endif
                    return MK.None();
            }
        }

        [PyBind]
        public TrUnityComponent GetComponent(TrObject componentType)
        {
            throw new System.NotImplementedException();
        }

        [PyBind]
        public IEnumerable<TrUnityComponent> GetComponents(TrObject componentType)
        {
            throw new System.NotImplementedException();
        }

        [PyBind]
        public TrUnityComponent AddComponent(TrObject componentType)
        {
            throw new System.NotImplementedException();
        }

        [PyBind]
        public TrObject on(TrEventTriggerType o_ev)
        {
#if UNITY_VERSION            
            var ev = o_ev.eventID;
            TrObject register(TrObject callback)
            {
                var trigger = this.gameObject.GetComponent<EventTrigger>();
                if (trigger == null)
                {
                    var component = this.GetComponent(TrUI.CLASS);
                    if (component == null)
                        component.gameObject.AddComponent<Collider2D>();
                    trigger = component.gameObject.AddComponent<EventTrigger>();
                }
                var entry = new EventTrigger.Entry();
                entry.eventID = ev;
                entry.callback.AddListener(x => callback.Call(TrEventData.FromRaw((PointerEventData)x)));
                trigger.triggers.Add(entry);
                return callback;
            }
            return TrSharpFunc.FromFunc($"<{ev} register>", register);
#else
            return MK.None();
#endif

        }


    }
}