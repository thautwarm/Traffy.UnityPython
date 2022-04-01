// builtin functions in traffy.unitypython
using System;
using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

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

        [PyBuiltin]
        static TrObject ord(TrObject a)
        {
            if (a is TrStr s)
                return MK.Int(Convert.ToInt32(s.value[0]));
            throw new TypeError("ord() argument must be a string of length 1");
        }

        [PyBuiltin]
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

        [PyBuiltin]
        static bool hasattr(TrObject obj, TrObject attr)
        {
            if (!(attr is TrStr s))
            {
                throw new TypeError("hasattr() arg 2 must be a string");
            }
            if (obj is TrObject o)
            {
                return o.__getic_refl__(s, out var _);
            }
            throw new TypeError("hasattr() arg 1 must be an object");
        }

        [PyBuiltin]
        static TrObject getattr(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject obj, found;
            int narg = args.Count;
            switch (narg)
            {
                case 2:
                    {
                        obj = args[0];
                        if (args[1] is TrStr attr)
                            return obj.__getic_refl__(attr, out found)
                            ? found
                            : throw new AttributeError(obj, args[1], $"{obj.Class.Name} has no attribute '{attr.__repr__()}'");
                        else
                            throw new TypeError("getattr(): attribute name must be a string");
                    }
                case 3:
                    {
                        obj = args[0];
                        if (args[1] is TrStr attr)
                            return obj.__getic_refl__(attr, out found) ? found : args[2];
                        else
                            throw new TypeError("getattr(): attribute name must be a string");
                    }
                default:
                    throw new TypeError($"getattr() takes at least 2 arguments and at most 3, got {narg}");

            }
        }

        [PyBuiltin]
        static TrObject setattr(TrObject self, TrObject attr, TrObject value)
        {
            if (attr is TrStr s)
                self.__setic_refl__(s, value);
            else
                throw new TypeError("setattr(): attribute name must be a string");
            return MK.None();
        }

        [PyBuiltin]
        static TrObject len(TrObject x)
        {
            return x.__len__();
        }

        [PyBuiltin]
        static int hash(TrObject x)
        {
            return x.__hash__();
        }

        [PyBuiltin]
        static TrObject pow(TrObject x, TrObject y)
        {
            return x.__pow__(y);
        }

        [PyBuiltin]
        static TrObject repr(TrObject x)
        {
            return MK.Str(x.__repr__());
        }

        [PyBuiltin]
        static TrObject round(TrObject x, TrObject n = null)
        {
            return x.__round__(n ?? MK.None());
        }

        [PyBuiltin]
        static bool isinstance(TrObject x, TrObject type)
        {
            return RTS.isinstanceof(x, type);
        }

        [PyBuiltin]
        static bool issubclass(TrObject x, TrObject type)
        {
            return RTS.issubclassof(x, type);
        }

        [PyBuiltin]
        static TrObject next(TrObject supportNext, TrObject __default = null)
        {
            var r = MK.Ref();
            if(supportNext.__trynext__(r))
            {
                return r.value;
            }
            if (__default == null)
            {
                throw new StopIteration();
            }
            return __default;
        }

    }
}