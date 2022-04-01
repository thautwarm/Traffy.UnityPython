using System.Collections.Generic;
using Traffy;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    public static partial class Sized
    {
        [AbsMember]
        public static TrObject __len__(TrObject self)
        {
            return TrObject.__len__(self);
        }

        [MixinMember]
        public static bool __bool__(TrObject self)
        {
            return self.__len__().AsInt() != 0;
        }
    }
    

}
