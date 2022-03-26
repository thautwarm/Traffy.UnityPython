using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass(typeof(Iterable))]
    public static partial class Reversible
    {
        [AbsMember]
        public static TrObject __reversed__(TrObject self)
        {
            return TrObject.__reversed__(self);
        }
    }

}
