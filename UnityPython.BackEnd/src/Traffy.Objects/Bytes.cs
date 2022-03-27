using System;
using System.Collections.Generic;
using System.Linq;
using InlineHelper;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [Serializable]
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Comparable), typeof(Traffy.Interfaces.Sequence))]
    public sealed partial class TrBytes : TrObject, IComparable<TrObject>
    {
        public byte[] contents;
        int IComparable<TrObject>.CompareTo(TrObject other)
        {
            bool isEqual;
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
            throw new TypeError($"unsupported comparison for '{CLASS.Name}' and '{other.Class.Name}'");
        }


        public override string __repr__() => contents.Select(x => $"\\x{x:X}").Prepend("b'").Append("'").By(String.Concat);
        public override bool __bool__() => contents.Length != 0;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrBytes>("bytes");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrBytes)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override object Native => contents;

        public override TrObject __add__(TrObject other)
        {
            if (other is TrBytes b)
            {

                return MK.Bytes(contents.ConcatArray(b.contents));
            }
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{other.Class.Name}'");
        }


        public override bool __le__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqLtE<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqLtE<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <=: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __lt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqLt<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqLt<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __gt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqGt<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqGt<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ge__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqGtE<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqGtE<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ne__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqNe<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqNe<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for !=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __eq__(TrObject other) =>
            (other is TrBytes b)
            ? contents.Inline().SeqEq<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.Inline().SeqEq<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for ==: '{CLASS.Name}' and '{other.Class.Name}'");


        public override int __hash__()
        {
            return contents.Inline().ByteSequenceHash<FArray<byte>>(
                Initialization.HashConfig.BYTE_HASH_SEED,
                Initialization.HashConfig.BYTE_HASH_PRIME
            );
        }

        [PyBind]
        public long count(TrObject o_elements, int start = 0, int end = -1)
        {
            long cnt = 0;
            if (end == -1)
            {
                end = contents.Length;
            }

            switch (o_elements)
            {
                case TrByteArray b:
                {
                    while (start < end)
                    {
                        if (contents.Inline().StartswithI<FArray<byte>, FList<byte>, byte>(b.contents, start))
                        {
                            start += b.contents.Count;
                            cnt++;
                        }
                        else
                        {
                            start++;
                        }
                    }
                    return cnt;
                }
                case TrBytes b:
                {
                    while (start < end)
                    {
                        if (contents.Inline().StartswithI<FArray<byte>, FArray<byte>, byte>(b.contents, start))
                        {
                            start += b.contents.Length;
                            cnt++;
                        }
                        else
                        {
                            start++;
                        }
                    }
                    return cnt;
                }

                case TrInt b:
                {
                    if (b.value < 0 || b.value > 255)
                    {
                        throw new ValueError($"{Class.Name} must be in range(0, 256)");
                    }
                    byte elt = unchecked((byte) b.value);
                    for(int i = start; i < end; i++)
                    {
                        if (contents[i] == elt)
                        {
                            cnt++;
                        }
                    }
                    return cnt;
                }
                default:
                    throw new TypeError($"{Class.Name}.count() argument must be 'bytes' or 'bytearray', not '{o_elements.Class.Name}'");
            }
        }


        [PyBind]
        public static TrBytes __new__(TrObject clsobj, TrObject buffer = null, string encoding = null)
        {
            if ((object)buffer == null)
            {
                return MK.Bytes();
            }
            switch (buffer)
            {
                case TrBytes b:
                {
                    return b;
                }
                case TrByteArray b:
                {
                    return MK.Bytes(b.contents.ToArray());
                }
                case TrInt b:
                {
                    int n = (int) b.value;
                    var xs = new byte[n];
                    for(int i = 0; i < n; i ++)
                    {
                        xs[i] = 0;
                    }
                    return MK.Bytes(xs);
                }
                case TrStr b:
                {
                    if (encoding == null)
                    {
                        throw new TypeError($"{clsobj.AsClass.Name}.__new__(): string argument without an encoding.");
                    }
                    switch (encoding.ToLowerInvariant())
                    {
                        case "utf8":
                        case "utf-8":
                            return MK.Bytes(System.Text.Encoding.UTF8.GetBytes(b.value));
                        case "utf16":
                        case "utf-16":
                            return MK.Bytes(System.Text.Encoding.Unicode.GetBytes(b.value));
                        case "utf32":
                        case "utf-32":
                            return MK.Bytes(System.Text.Encoding.UTF32.GetBytes(b.value));
                        case "latin1":
                        case "latin-1":
                            return MK.Bytes(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(b.value));
                        case "ascii":
                            return MK.Bytes(System.Text.Encoding.ASCII.GetBytes(b.value));
                        default:
                            throw new ValueError($"unknown encoding: {encoding}");
                    }
                }
                default:
                    if(RTS.isinstanceof(buffer, Traffy.Interfaces.Iterable.CLASS))
                    {
                        var xs = new List<byte>();
                        var itr = buffer.__iter__();
                        while (itr.MoveNext())
                        {
                            var elt = itr.Current;
                            if (elt is TrInt o_i)
                            {
                                if (o_i.value > 255 || o_i.value < 0)
                                {
                                    throw new ValueError("bytes must be in range(0, 256)");
                                }
                                xs.Add((byte) o_i.value);
                            }
                            else
                            {
                                throw new TypeError($"{elt.Class.Name} object cannot be interpreted as an integer.");
                            }   
                        }
                        return MK.Bytes(xs.ToArray());
                    }
                    throw new TypeError($"{clsobj.AsClass.Name}.__new__(): argument must be 'bytes' or 'bytearray' or 'str', not '{buffer.Class.Name}'");
            }
        }

    }

}