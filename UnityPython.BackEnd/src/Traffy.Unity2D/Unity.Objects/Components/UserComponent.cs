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