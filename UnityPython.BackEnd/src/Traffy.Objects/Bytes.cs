using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using InlineHelper;

namespace Traffy.Objects
{
    [Serializable]
    public class TrBytes : TrObject
    {
        public byte[] contents;
        int IComparable<TrObject>.CompareTo(TrObject other)
        {
            bool isEqual = false;
            if (other is TrBytes b)
            {
                if (contents.Inline().SeqLtE<FArray<byte>, FArray<byte>, byte>(b.contents, out isEqual))
                    return isEqual ? 0 : -1;
                return 1;
            }
            if (other is TrByteArray byteArray)
            {
                if (contents.Inline().SeqLtE<FArray<byte>, FList<byte>, byte>(byteArray.contents, out isEqual))
                    return isEqual ? 0 : -1;
                return 1;
            }
            throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");
        }


        string TrObject.__repr__() => contents.Select(x => $"\\x{x:X}").Prepend("b'").Append("'").By(String.Concat);
        bool TrObject.__bool__() => contents.Length != 0;
        List<TrObject> TrObject.__array__ => null;
        static TrClass CLASS;
        TrClass TrObject.Class => CLASS;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrBytes>();
            CLASS.Name = "bytes";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("bytes.__new__", TrBytes.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrBytes)] = CLASS;
        }

        [Mark(typeof(TrBytes))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        object TrObject.Native => contents;

        TrObject TrObject.__add__(TrObject other)
        {
            if (other is TrBytes b)
            {

                return MK.Bytes(contents.ConcatArray(b.contents));
            }
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{other.Class.Name}'");
        }


        bool TrObject.__le__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqLtE<FArray<byte>, FArray<byte>, byte>(b.contents, out var _)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqLtE<FArray<byte>, FList<byte>, byte>(byteArray.contents, out var _)
            : throw new TypeError($"unsupported operand type(s) for <=: '{CLASS.Name}' and '{other.Class.Name}'");

        bool TrObject.__lt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqLt<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqLt<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");

        bool TrObject.__gt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqGt<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqGt<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >: '{CLASS.Name}' and '{other.Class.Name}'");


        bool TrObject.__ge__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqGtE<FArray<byte>, FArray<byte>, byte>(b.contents, out var _)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqGtE<FArray<byte>, FList<byte>, byte>(byteArray.contents, out var _)
            : throw new TypeError($"unsupported operand type(s) for >=: '{CLASS.Name}' and '{other.Class.Name}'");


        bool TrObject.__ne__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqNe<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqNe<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for !=: '{CLASS.Name}' and '{other.Class.Name}'");


        bool __eq__(TrObject other) =>
                (other is TrBytes b)
                ? contents.Inline().SeqEq<FArray<byte>, FArray<byte>, byte>(b.contents)
                : (other is TrByteArray byteArray)
                ? contents.Inline().SeqEq<FArray<byte>, FList<byte>, byte>(byteArray.contents)
                : throw new TypeError($"unsupported operand type(s) for ==: '{CLASS.Name}' and '{other.Class.Name}'");


        int TrObject.__hash__()
        {
            return contents.Inline().ByteSequenceHash<FArray<byte>>(
                Initialization.HashConfig.BYTE_HASH_SEED,
                Initialization.HashConfig.BYTE_HASH_PRIME
            );
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Bytes();
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                if (arg is TrBytes)
                    return arg;
                if (arg is TrByteArray byteArray)
                    return MK.Bytes(byteArray.contents.ToArray());
                throw new TypeError($"bytes(x) argument 1 must be bytes or bytearray, not {arg.Class.Name}");
            }
            if (narg == 3)
            {
                var arg1 = args[1] as TrStr;
                if (arg1 == null)
                    throw new TypeError($"bytes(str, encoding) argument 1 must be str, not {args[1].Class.Name}");

                var arg2 = args[2] as TrStr;
                if (arg2 == null)
                    throw new TypeError($"bytes(str, encoding) argument 2 must be str, not {args[2].Class.Name}");
                switch (arg2.value)
                {
                    case "utf8":
                    case "utf-8":
                        return MK.Bytes(System.Text.Encoding.UTF8.GetBytes(arg1.value));
                    case "utf16":
                    case "utf-16":
                        return MK.Bytes(System.Text.Encoding.Unicode.GetBytes(arg1.value));
                    case "utf32":
                    case "utf-32":
                        return MK.Bytes(System.Text.Encoding.UTF32.GetBytes(arg1.value));
                    case "latin1":
                    case "latin-1":
                        return MK.Bytes(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(arg1.value));
                    case "ascii":
                        return MK.Bytes(System.Text.Encoding.ASCII.GetBytes(arg1.value));
                    default:
                        throw new ValueError($"unknown encoding: {arg2.value}");
                }
            }
            throw new TypeError($"bytes() takes at most 3 arguments ({args.Count} given)");
        }


    }

}