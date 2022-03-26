using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class Container
    {
        [AbsMember]
        public static bool __contains__(TrObject self, TrObject element)
        {
            return TrObject.__contains__(self, element);
        }
    }
}
        