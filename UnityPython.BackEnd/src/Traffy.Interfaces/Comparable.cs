using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class Comparable
    {
        
        [AbsMember]
        public static bool __lt__(TrObject self, TrObject other)
        {
            return TrObject.__lt__(self, other);
        }

        [MixinMember]
        public static bool __ge__(TrObject self, TrObject other)
        {
            return !__lt__(self, other);
        }

        [MixinMember]
        public static bool __gt__(TrObject self, TrObject other)
        {
            return !self.__eq__(other) && !__lt__(self, other);
        }

        [MixinMember]
        public static bool __le__(TrObject self, TrObject other)
        {
            return self.__eq__(other) || __lt__(other, self);
        }
    }

}