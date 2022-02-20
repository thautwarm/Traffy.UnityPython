// builtin functions in traffy.unitypython
using Traffy.Objects;
namespace Traffy
{
    public static class Builtins
    {
        [Mark(Initialization.TokenBuiltinInit)]
        public static void InitRuntime()
        {
            TrObject stacktrace(TrObject exception)
            {
                var exc = (TrExceptionBase) exception;
                return MK.Str(exc.GetStackTrace());
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("stacktrace", stacktrace));
        }
    }

}