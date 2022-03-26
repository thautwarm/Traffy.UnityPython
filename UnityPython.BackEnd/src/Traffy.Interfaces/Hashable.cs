using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class Hashable
    {
        [AbsMember]
        public static int __hash__(TrObject self)
        {
            return TrObject.__hash__(self);
        }
    }
}
        