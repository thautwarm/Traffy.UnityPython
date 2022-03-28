using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class SupportAbs
    {
        [AbsMember]
        public static TrObject __abs__(TrObject self)
        {
            return TrObject.__abs__(self);
        }
    }
}
        