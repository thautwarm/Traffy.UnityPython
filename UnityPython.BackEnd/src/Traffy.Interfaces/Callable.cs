using System.Collections.Generic;
using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class Callable
    {
        [AbsMember]
        public static TrObject __call__(TrObject self, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            return TrObject.__call__(self, args, kwargs);
        }
    }
    
}
