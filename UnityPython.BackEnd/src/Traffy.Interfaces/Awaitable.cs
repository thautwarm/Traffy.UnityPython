using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class Awaitable
    {
        [AbsMember]
        public static Awaitable<TrObject> __await__(TrObject self)
        {
            return TrObject.__await__(self);
        }

    }
}