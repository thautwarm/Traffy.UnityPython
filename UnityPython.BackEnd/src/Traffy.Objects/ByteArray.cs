using System;
using System.Collections.Generic;
using System.Linq;
using InlineHelper;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [Traffy.Annotations.PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Comparable), typeof(Traffy.Interfaces.Sequence))]
    public sealed partial class TrByteArray : TrObject
    {
        public FList<byte> contents;
        public override object Native => contents;
        public override string __repr__() => contents.Select(x => $"\\x{x:X}").Prepend("bytearray(b'").Append("')").By(String.Concat);
        public override bool __bool__() => contents.Count != 0;
        public override List<TrObject> __array__ => null;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override bool __le__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqLtE<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqLtE<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <=: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __lt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqLt<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqLt<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __gt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqGt<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqGt<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ge__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqGtE<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqGtE<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ne__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqNe<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqNe<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for !=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __eq__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqEq<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqEq<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for ==: '{CLASS.Name}' and '{other.Class.Name}'");


        [PyBind]
        public long count(TrObject o_elements, int start = 0, int end = -1)
        {
            long cnt = 0;
            if (end == -1)
            {
                end = contents.Count;
            }

            switch (o_elements)
            {
                case TrByteArray b:
                {
                    while (start < end)
                    {
                        if (contents.StartswithI<FList<byte>, FList<byte>, byte>(b.contents, start))
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
                        if (contents.StartswithI<FList<byte>, FArray<byte>, byte>(b.contents, start))
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
                        if (contents.UnList[i] == elt)
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

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrByteArray>("bytearray");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrByteArray)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override TrObject __add__(TrObject other)
        {
            if (other is TrBytes b)
            {
                var res = new List<byte>(contents);
                res.AddRange(b.contents);
                return MK.ByteArray(res);
            }
            else if (other is TrByteArray ba)
            {
                var res = new List<byte>(contents);
                res.AddRange(ba.contents);
                return MK.ByteArray(res);
            }
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{other.Class.Name}'");
        }

        [PyBind]
        public static TrByteArray __new__(TrObject clsobj, TrObject buffer = null, string encoding = null)
        {
            if ((object)buffer == null)
            {
                return MK.ByteArray();
            }
            switch (buffer)
            {
                case TrBytes b:
                {
                    return MK.ByteArray(b.contents.ToList());
                }
                case TrByteArray b:
                {
                    return MK.ByteArray(b.contents.UnList.Copy());
                }
                case TrInt b:
                {
                    var xs = new List<byte>();
                    for (long i = 0; i < b.value; i++)
                    {
                        xs.Add(0);
                    }
                    return MK.ByteArray(xs);
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
                            return MK.ByteArray(System.Text.Encoding.UTF8.GetBytes(b.value).ToList());
                        case "utf16":
                        case "utf-16":
                            return MK.ByteArray(System.Text.Encoding.Unicode.GetBytes(b.value).ToList());
                        case "utf32":
                        case "utf-32":
                            return MK.ByteArray(System.Text.Encoding.UTF32.GetBytes(b.value).ToList());
                        case "latin1":
                        case "latin-1":
                            return MK.ByteArray(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(b.value).ToList());
                        case "ascii":
                            return MK.ByteArray(System.Text.Encoding.ASCII.GetBytes(b.value).ToList());
                        default:
                            throw new ValueError($"unknown encoding: {encoding}");
                    }
                }
                default:
                    if (RTS.isinstanceof(buffer, Traffy.Interfaces.Iterable.CLASS))
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
                                    throw new ValueError("bytearray must be in range(0, 256)");
                                }
                                xs.Add((byte)o_i.value);
                            }
                            else
                            {
                                throw new TypeError($"{elt.Class.Name} object cannot be interpreted as an integer.");
                            }
                        }
                        return MK.ByteArray(xs);
                    }
                    throw new TypeError($"{clsobj.AsClass.Name}.__new__(): argument must be 'bytes' or 'bytearray' or 'str', not '{buffer.Class.Name}'");
            }
        }
    }

}