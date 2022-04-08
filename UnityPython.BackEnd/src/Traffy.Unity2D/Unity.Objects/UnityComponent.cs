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

        [PyBind(Name = "base")]
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

        [PyBind(Name = nameof(TrGameObject.AddComponent))]
        internal TrObject _AddComponent(TrObject componentType, TrObject gameState, TrObject parameter = null) =>
            baseObject.AddComponent(componentType, gameState, parameter);

        [PyBind(Name = nameof(TrGameObject.TryGetComponent))]
        internal bool _TryGetComponent(TrObject componentType, TrRef refval) =>
            baseObject.TryGetComponent(componentType, refval);

        [PyBind(Name = nameof(TrGameObject.TryGetComponents))]
        internal bool _TryGetComponents(TrObject componentType, TrRef refval) =>
            baseObject.TryGetComponents(componentType, refval);

        [PyBind(Name = nameof(TrGameObject.GetComponent))]
        internal TrObject _GetComponent(TrObject componentType) =>
            baseObject.GetComponent(componentType);

        [PyBind]
        internal void Destroy() => RemoveComponent();
    }
}

#endif