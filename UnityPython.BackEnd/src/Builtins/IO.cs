// builtin functions in traffy.unitypython
using Traffy.Objects;
using Traffy.Annotations;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace Traffy
{
    public static partial class Builtins
    {
        static readonly TrObject _sep = MK.Str(" ");
        static readonly TrObject _newline = MK.Str("\n");

        [PyBuiltin]
        static TrObject print(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var sb = new System.Text.StringBuilder();
            var enumerator = args.GetEnumerator();
            if (kwargs == null || !kwargs.TryGetValue(MK.Str("sep"), out TrObject sep))
            {
                sep = _sep;
            }
            if (kwargs == null || !kwargs.TryGetValue(MK.Str("end"), out TrObject end))
            {
                end = _newline;
            }

            bool flush = kwargs == null || !kwargs.TryGetValue(MK.Str("flush"), out TrObject flush_) || flush_.AsBool();

            if (enumerator.MoveNext())
            {
                sb.Append(enumerator.Current.__str__());
                while (enumerator.MoveNext())
                {
                    sb.Append(sep.__str__());
                    sb.Append(enumerator.Current.__str__());
                }
            }
            sb.Append(end.__str__());
#if NUNITY
            System.Console.Write(sb.ToString());
            if (flush)
                System.Console.Out.Flush();
#else
            Debug.Log(sb.ToString());
#endif
            return MK.None();
        }

        [PyBuiltin]
        static TrObject stacktrace(TrObject exception)
        {
            var exc = (TrExceptionBase) exception;
            return MK.Str(exc.GetStackTrace());
        }
    }

}