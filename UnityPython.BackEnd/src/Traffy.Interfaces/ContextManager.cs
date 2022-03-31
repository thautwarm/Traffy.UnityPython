using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class ContextManager
    {
        [AbsMember]
        public static TrObject __enter__(TrObject self)
        {
            return TrObject.__enter__(self);
        }

        [AbsMember]
        public static TrObject __exit__(TrObject self, TrObject exc_type, TrObject exc_value, TrObject traceback)
        {
            return TrObject.__exit__(self, exc_type, exc_value, traceback);
        }
    }
}
