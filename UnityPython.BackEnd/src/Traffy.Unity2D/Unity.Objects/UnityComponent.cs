using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;
using UnityEngine.EventSystems;

namespace Traffy.Unity2D
{
    [UnitySpecific]
    public abstract class TrUnityComponent : TrUserObjectBase
    {
        // internal static TrObject cannot_inst_component(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        // {
        //     throw new TypeError($"Cannot instantiate a component {cls.Name} with __new__");
        // }

        public override List<TrObject> __array__ => null;

        protected TrUnityComponent(TrGameObject baseObject)
        {
            this.baseObject = baseObject;
        }

        public TrGameObject baseObject;

        public abstract void RemoveComponent();

        [PyBind(Name = "gameObject")]
        internal TrObject _baseObject
        {
            get
            {
                return baseObject;
            }
        }

        [PyBind]
        internal TrObject x
        {
            get => baseObject.x;
            set => baseObject.x = value;
        }

        [PyBind]
        internal TrObject y
        {
            get => baseObject.y;
            set => baseObject.y = value;
        }

        [PyBind]
        internal TrObject z
        {
            get => baseObject.z;
            set => baseObject.z = value;
        }

        [PyBind]
        internal TrObject on(TrEventTriggerType o_ev) => baseObject.on(o_ev);

        [PyBind(Name = nameof(TrGameObject.requireComponents))]
        internal TrObject _RequireComponents(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) => baseObject._RequireComponents(args, kwargs);

        [PyBind]
        internal void destory() => RemoveComponent();
    }
}

#endif