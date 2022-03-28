using Traffy;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    [PyInherit(typeof(Callable))]
    public static partial class function
    {
        [AbsMember]
        public static bool __call__(TrObject self, TrObject element)
        {
            return TrObject.__contains__(self, element);
        }

        [SetupMark(SetupMarkKind.InitRef)]
        internal static void _PrivateInit()
        {
            Initialization.Prelude("function", CLASS);
        }
    }
}
