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
        [PyBuiltin]
        static TrObject abs(TrObject x)
        {
            return x.__abs__();
        }

        [PyBuiltin]
        static TrObject callable(TrObject x)
        {
            return x.__abs__();
        }
    }
}