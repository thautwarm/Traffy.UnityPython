using System.Collections.Generic;
using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class Iterable
    {
        [AbsMember]
        public static IEnumerator<TrObject> __iter__(TrObject self)
        {
            return TrObject.__iter__(self);
        }
    }
}
        