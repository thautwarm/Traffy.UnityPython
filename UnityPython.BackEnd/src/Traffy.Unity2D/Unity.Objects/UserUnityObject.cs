using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    [PyInherit(typeof(TrTraffyBehaviour))]
    [UnitySpecific]
    public sealed partial class TrUnityUserComponent: TrUnityComponent
    {
        private TrUnityUserComponent(TrUnityObject uo, TrClass cls): base(uo)
        {
            CLASS = cls;
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ { get; } = new List<TrObject>();
        public static TrUnityUserComponent Create(TrUnityObject uo, TrClass cls)
        {
            return new TrUnityUserComponent(uo, cls);
        }

        public override bool TryGetNativeUnityObject(out Object o)
        {
            o = null;
            return false;
        }
    }
}
#endif