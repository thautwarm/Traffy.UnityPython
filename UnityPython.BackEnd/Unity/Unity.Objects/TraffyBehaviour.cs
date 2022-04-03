using System.Collections.Generic;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
#endif

namespace Traffy.Unity2D
{
    public class TraffyBehaviour
#if UNITY_VERSION
        : MonoBehaviour
#endif
    {
        public List<TrObject> TraffyObjects = new List<TrObject>();
    }
}