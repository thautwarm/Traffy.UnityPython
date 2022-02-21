// builtin functions in traffy.unitypython
using Traffy.Objects;
using System.Collections.Generic;
namespace Traffy
{
    public static class Builtins
    {
        static IEnumerator<TrObject> mkrange(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
                yield return MK.Int(i);
        }
        [Mark(Initialization.TokenBuiltinInit)]
        public static void InitRuntime()
        {
            TrObject stacktrace(TrObject exception)
            {
                var exc = (TrExceptionBase) exception;
                return MK.Str(exc.GetStackTrace());
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("stacktrace", stacktrace));
            TrObject range(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                var narg = args.Count;
                switch(narg)
                {
                    case 1:
                        return MK.Iter(mkrange(0, args[0].AsInt(), 1));
                    case 2:
                        return MK.Iter(mkrange(args[0].AsInt(),  args[1].AsInt(), 1));
                    case 3:
                        return MK.Iter(mkrange(args[0].AsInt(),  args[1].AsInt(), args[1].AsInt()));
                    default:
                        throw new TypeError($"range() takes 1 to 3 positional argument(s) but {narg} were given");
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("range", range));
        }
    }

}