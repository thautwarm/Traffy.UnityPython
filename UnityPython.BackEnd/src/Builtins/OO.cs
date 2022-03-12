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
        static TrObject bin(TrObject a)
        {
            if (a is TrInt i)
                return MK.Str("0b" + Convert.ToString(i.value, 2));
            throw new TypeError("bin() argument must be an integer");
        }

        [PyBuiltin]
        static TrObject chr(TrObject a)
        {
            if (a is TrInt i)
                return MK.Str(new string(new char[] { Convert.ToChar(i.value) }));
            throw new TypeError("chr() argument must be an integer");
        }

        [PyBind]
        static TrObject ord(TrObject a)
        {
            if (a is TrStr s)
                return MK.Int(Convert.ToInt32(s.value[0]));
            throw new TypeError("ord() argument must be a string of length 1");
        }

        [PyBind]
        static TrObject oct(TrObject a)
        {
            if (a is TrInt i)
                return MK.Str("0o" + Convert.ToString(i.value, 8));
            throw new TypeError("oct() argument must be an integer");
        }

        [PyBuiltin]
        static TrObject hex(TrObject a)
        {
            if (a is TrInt i)
                return MK.Str("0x" + Convert.ToString(i.value, 16));
            throw new TypeError("hex() argument must be an integer");
        }
    }
}