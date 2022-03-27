using System.Collections.Generic;
using Traffy;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [PyInherit(typeof(Sized), typeof(Iterable), typeof(Container))]
    [AbstractClass]
    public static partial class Collection
    {

    }
}
