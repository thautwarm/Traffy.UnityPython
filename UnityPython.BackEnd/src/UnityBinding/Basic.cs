#if NUNITY
#else
using UnityEngine;

namespace Traffy.Unity
{
    public static class Bind
    {
        public static void f()
        {
            Debug.Assert(false);
        }
    }
}

#endif