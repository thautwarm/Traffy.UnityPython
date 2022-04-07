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
    [PyInherit(typeof(TrMonoBehaviour))]
    public abstract class TrUnityComponent : TrUserObjectBase
    {
        // internal static TrObject cannot_inst_component(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        // {
        //     throw new TypeError($"Cannot instantiate a component {cls.Name} with __new__");
        // }

        public override List<TrObject> __array__ => null;
        // public static TrClass CLASS;

        protected TrUnityComponent(TrGameObject baseObject)
        {
            this.baseObject = baseObject;
        }

        public override bool __eq__(TrObject other) => other.Native.Equals(Native);

        public override bool __ne__(TrObject other) => !other.Native.Equals(Native);

        public override int __hash__() => Native.GetHashCode();

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

        [PyBind(Name = nameof(RemoveComponent))]
        internal void _RemoveComponent() => RemoveComponent();
    }
}

#endif