using Traffy;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [PyInherit(typeof(Iterable))]
    [AbstractClass]
    public static partial class Reversible
    {
        [AbsMember]
        public static TrObject __reversed__(TrObject self)
        {
            return TrObject.__reversed__(self);
        }
    }

}
