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
        // public static TrClass CLASS;

        protected TrUnityComponent(TrUnityObject baseObject)
        {
            this.baseObject = baseObject;
        }

        public TrUnityObject baseObject;
        public abstract bool TryGetNativeUnityObject(out UnityEngine.Object o);
        public bool _default_TryGetNativeUnityObject(out UnityEngine.Object o)
        {
            o = null;
            return false;
        }

        public void RemoveComponent()
        {
            if (baseObject.TryGetComponents(Class, out var components))
            {
                components.Remove(this);
                if (TryGetNativeUnityObject(out var uo))
                    UnityEngine.Object.Destroy(uo);
            }
        }


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

        [PyBind(Name = nameof(TrUnityObject.AddComponent))]
        internal TrObject _AddComponent(TrObject componentType, TrObject gameState) =>
            baseObject._AddComponent(componentType, gameState);

        [PyBind(Name = nameof(TrUnityObject.TryGetComponent))]
        internal bool _TryGetComponent(TrObject componentType, TrRef refval) =>
            baseObject._TryGetComponent(componentType, refval);

        [PyBind(Name = nameof(TrUnityObject.TryGetComponents))]
        internal bool _TryGetComponents(TrObject componentType, TrRef refval) =>
            baseObject._TryGetComponents(componentType, refval);

        [PyBind(Name = nameof(RemoveComponent))]
        internal void _RemoveComponent() => RemoveComponent();

    }
}

#endif