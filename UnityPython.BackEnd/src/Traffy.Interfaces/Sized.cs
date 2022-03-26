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
    }
    

}
