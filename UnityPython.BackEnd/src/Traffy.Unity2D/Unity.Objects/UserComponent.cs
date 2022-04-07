using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    [UnitySpecific]
    public sealed class TrUnityUserComponent: TrUnityComponent
    {
        private TrUnityUserComponent(TrGameObject uo, TrClass cls): base(uo)
        {
            CLASS = cls;
        }
        public TrClass CLASS;
        public override TrClass Class => CLASS;
        public override object Native => this;
        public override List<TrObject> __array__ { get; } = new List<TrObject>();

        public override int __hash__()
        {
            if (__getic__(Class.ic__hash, out var self_hash))
            {
                return self_hash.Call().AsInt();
            }
            return TrObject.__hash__(this);
        }

        public override bool __eq__(TrObject other)
        {
            if (__getic__(Class.ic__eq, out var self_eq))
            {
                return self_eq.Call(other).AsBool();
            }
            return TrObject.__eq__(this, other);
        }
        public override bool __ne__(TrObject other)
        {
            if (__getic__(Class.ic__ne, out var self_ne))
            {
                return self_ne.Call(other).AsBool();
            }
            return TrObject.__ne__(this, other);
        }

        public static TrUnityUserComponent Create(TrGameObject uo, TrClass cls)
        {
            return new TrUnityUserComponent(uo, cls);
        }

        public override void RemoveComponent()
        {
            if (baseObject.Components.TryGetValue(CLASS.ClassId, out var components) && components.Count != 0)
            {
                components.Remove(this);
            }
        }
    }
}
#endif