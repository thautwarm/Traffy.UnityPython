using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InlineHelper;
using Traffy.Annotations;
using static Traffy.BytesUtils;

namespace Traffy.Objects
{
    [Serializable]
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Comparable), typeof(Traffy.Interfaces.Sequence))]
    public sealed partial class TrBytes : TrObject, IComparable<TrObject>
    {
        public FArray<byte> contents;
        internal int s_ContentCount => contents.Count;
        int IComparable<TrObject>.CompareTo(TrObject other)
        {
            bool isEqual;
            if (other is TrBytes b)
            {
                if (contents.SeqLtE<FArray<byte>, FArray<byte>, byte>(b.contents, out isEqual))
                    return isEqual ? 0 : -1;
                return 1;
            }
            if (other is TrByteArray byteArray)
            {
                if (contents.SeqLtE<FArray<byte>, FList<byte>, byte>(byteArray.contents, out isEqual))
                    return isEqual ? 0 : -1;
                return 1;
            }
            throw new TypeError($"unsupported comparison for '{CLASS.Name}' and '{other.Class.Name}'");
        }


        public override string __repr__() =>
            IronPython.Runtime.Operations.IListOfByteOps.BytesRepr(contents.UnList);
        public override bool __bool__() => contents.Count != 0;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        public override TrObject __getitem__(TrObject item)
        {
            switch (item)
            {
                case TrInt ith:
                {
                    var i = checked((int)ith.value);
                    if (Traffy.Compatibility.IronPython.PythonOps.TryFixIndex(ref i, contents.Count))
                    {
                        return MK.Int(contents[i]);
                    }
                    throw new IndexError($"{Class.Name} index out of range");
                }
                case TrSlice slice:
                {
                    return MK.Bytes(IronPython.Runtime.Operations.ArrayOps.GetItem(contents.UnList, slice));
                }
                default:
                    throw new TypeError($"{Class.Name} indices must be integers, not '{item.Class.Name}'");
            }
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrBytes>("bytes");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override object Native => contents.UnList;

        public override TrObject __add__(TrObject other)
        {
            if (other is TrBytes b)
            {
                return MK.Bytes(IronPython.Runtime.Operations.ArrayOps.Add(contents.UnList, b.contents.UnList));
            }
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{other.Class.Name}'");
        }

        public override TrObject __mul__(TrObject other)
        {
            if (other is TrInt i)
            {
                return MK.Bytes(IronPython.Runtime.Operations.ArrayOps.Multiply(contents.UnList, checked((int)i.value)));
            }
            else
            {
                throw new TypeError($"unsupported operand type(s) for *: '{CLASS.Name}' and '{other.Class.Name}'");
            }
        }

        public override bool __le__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqLtE<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqLtE<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <=: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __lt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqLt<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqLt<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __gt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqGt<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqGt<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ge__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqGtE<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqGtE<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ne__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqNe<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqNe<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for !=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __eq__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqEq<FArray<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqEq<FArray<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for ==: '{CLASS.Name}' and '{other.Class.Name}'");


        public override int __hash__()
        {
            return contents.ByteSequenceHash<FArray<byte>>(
                Initialization.HashConfig.BYTE_HASH_SEED,
                Initialization.HashConfig.BYTE_HASH_PRIME
            );
        }

        public override IEnumerator<TrObject> __iter__()
        {
            for (int i = 0; i < contents.Count; i++)
            {
                yield return MK.Int(contents[i]);
            }
        }

        public override bool __contains__(TrObject a)
        {
            switch (a)
            {
                case TrInt o_i:
                {
                    var i = o_i.value;
                    if (i < 0 || i > 255)
                    {
                        throw new ValueError($"{Class.Name} must be in range(0, 256)");
                    }
                    return contents.UnList.Contains(unchecked((byte)i));
                }
                case TrBytes b:
                {
                    return IronPython.Runtime.Operations.IListOfByteOps.IndexOf(contents.UnList, b.contents.UnList, 0, contents.Count) != -1;
                    // return contents.IndexSubSeqGenericSimple<FList<byte>, FArray<byte>, byte>(b.contents) != -1;
                }
                case TrByteArray b:
                {
                    return IronPython.Runtime.Operations.IListOfByteOps.IndexOf(contents.UnList, b.contents.UnList, 0, contents.Count) != -1;
                    // return contents.IndexSubSeqGenericSimple<FList<byte>, FList<byte>, byte>(b.contents) != -1;
                }
                default:
                    throw new TypeError($"a bytes-like object is required, not '{a.Class.Name}'");
            }
        }

        public override TrObject __len__() => MK.Int(contents.Count);

        static IEnumerator<TrObject> _bytes_reverse(byte[] container)
        {
            for (int i = container.Length - 1; i >= 0; i--)
            {
                yield return MK.Int(container[i]);
            }
        }

        public override TrObject __reversed__() => MK.Iter(_bytes_reverse(contents));

        [PyBind]
        public long count(
            TrObject o_elements, int start = 0,
            [PyBind.SelfProp(nameof(s_ContentCount))] int end = /* pseudo */ 0)
        {
            switch (o_elements)
            {
                case TrByteArray b:
                {
                    return IronPython.Runtime.Operations.IListOfByteOps.CountOf(contents.UnList, b.contents.UnList, start, end);
                    // return contents.CountSubSeqGenericSimple<FArray<byte>, FList<byte>, byte>(b.contents, start, end);
                }
                case TrBytes b:
                {
                    return IronPython.Runtime.Operations.IListOfByteOps.CountOf(contents.UnList, b.contents.UnList, start, end);
                    // return contents.CountSubSeqGenericSimple<FArray<byte>, FArray<byte>, byte>(b.contents, start, end);
                }

                case TrInt b:
                {
                    return contents.CountGenericSimple<FArray<byte>, byte>(CheckByte(b), start, end);
                }
                default:
                    throw new TypeError($"{Class.Name}.count() argument must be 'bytes' or 'bytearray' or 'int', not '{o_elements.Class.Name}'");
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
                    int n = (int)b.value;
                    var xs = new byte[n];
                    for (int i = 0; i < n; i++)
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
                    if (RTS.isinstanceof(buffer, Traffy.Interfaces.Iterable.CLASS))
                    {
                        var xs = new List<byte>();
                        var itr = buffer.__iter__();
                        while (itr.MoveNext())
                        {
                            var elt = itr.Current;
                            if (elt is TrInt o_i)
                            {
                                xs.Add(CheckByte(o_i));
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

        [PyBind]
        public int index(
            TrObject x, int start = 0,
            [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0,
            [PyBind.Keyword(Only = true)] bool noraise = false)
        {
            start = Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(start, contents.Count);
            end = Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(end, contents.Count);
            int index;
            switch (x)
            {
                case TrByteArray b:
                {
                    index = IronPython.Runtime.Operations.IListOfByteOps.IndexOf(contents.UnList, b.contents.UnList, 0, contents.Count);
                    // index = contents.IndexSubSeqGenericSimple<FList<byte>, FList<byte>, byte>(b.contents, start, end);
                    break;
                }
                case TrBytes b:
                {
                    index = IronPython.Runtime.Operations.IListOfByteOps.IndexOf(contents.UnList, b.contents.UnList, 0, contents.Count);
                    // index = contents.IndexSubSeqGenericSimple<FList<byte>, FArray<byte>, byte>(b.contents, start, end);
                    break;
                }
                case TrInt b:
                {
                    index = contents.IndexEltGenericSimple<FArray<byte>, byte>(CheckByte(b), start, end);
                    break;
                }
                default:
                    throw new TypeError($"{Class.Name}.index(): a bytes-like object is required, not '{x.Class.Name}'");
            }

            if (index == -1 && !noraise)
                throw new ValueError($"{Class.Name}.index(x): x not in {Class.Name}");
            return index;
        }

        [PyBind]
        public byte[] upper()
        {
            var newcontents = new byte[contents.Count];
            for (int i = 0; i < contents.Count; i++)
            {
                newcontents[i] = (
                    contents[i] >= 'a' && contents[i] <= 'z'
                    ? (byte)(contents[i] - 'a' + 'A')
                    : contents[i]
                );
            }
            return newcontents;
        }

        [PyBind]
        public byte[] lower()
        {
            var newcontents = new byte[contents.Count];
            for (int i = 0; i < contents.Count; i++)
            {
                newcontents[i] = (
                    contents[i] >= 'A' && contents[i] <= 'Z'
                    ? (byte)(contents[i] - 'A' + 'a')
                    : contents[i]
                );
            }
            return newcontents;
        }

        [PyBind]
        public string decode(string encoding = "utf-8")
        {
            switch (encoding.ToLowerInvariant())
            {
                case "utf8":
                case "utf-8":
                    return Encoding.UTF8.GetString(contents.UnList);
                case "utf16":
                case "utf-16":
                    return Encoding.Unicode.GetString(contents.UnList);
                case "utf32":
                case "utf-32":
                    return Encoding.UTF32.GetString(contents.UnList);
                case "latin1":
                case "latin-1":
                    return Encoding.ASCII.GetString(contents.UnList);
                case "ascii":
                    return Encoding.ASCII.GetString(contents.UnList);
                default:
                    throw new ValueError($"unknown encoding: {encoding}");
            }
        }
        [PyBind]
        public bool islower()
        {
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i] >= 'a' && contents[i] <= 'z')
                    continue;
                return false;
            }
            return true;
        }

        [PyBind]
        public bool isupper()
        {
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i] >= 'A' && contents[i] <= 'Z')
                    continue;
                return false;
            }
            return true;
        }

        [PyBind]
        public byte[] capitalize()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.Capitalize(contents.UnList).ToArray();
        }

        [PyBind]
        public byte[] center(int width, IList<byte> fillchar)
        {
            if (fillchar.Count != 1)
            {
                throw new TypeError("center() argument 2 must be a byte string of length 1, not bytes");
            }
            return IronPython.Runtime.Operations.IListOfByteOps.TryCenter(contents.UnList, width, fillchar[0]).ToArray();
        }

        [PyBind]
        public byte[] expandtabs(int tabsize = 8)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.ExpandTabs(contents.UnList, tabsize).ToArray();
        }
        [PyBind]
        public int find(IList<byte> sub, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.Find(
                contents.UnList,
                sub,
                start,
                end
            );
        }
        [PyBind]
        public static byte[] fromhex(string s)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.FromHex(s).ToArray();
        }

        /*
        Create a str of hexadecimal numbers from a bytearray object.
        sep
            An optional single character or byte to separate hex bytes.
        bytes_per_sep
            How many bytes between separators.  Positive values count from the
            right, negative values count from the left. */
        [PyBind]
        public string hex(TrObject sep = null, int bytes_per_sep = 0)
        {
            return BytesUtils.Hex(contents, sep, bytes_per_sep);
        }

        [PyBind]
        public bool endswith(TrObject suffix, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {

            static bool compute(byte[] seq, IList<byte> sub, int start, int end, int count)
            {
                if (start == 0)
                {
                    if (end == count)
                    {
                        return IronPython.Runtime.Operations.IListOfByteOps
                            .EndsWith(seq, sub);
                    }
                }
                else if (end == count)
                {
                    return IronPython.Runtime.Operations.IListOfByteOps
                        .EndsWith(seq, sub, start);
                }
                return IronPython.Runtime.Operations.IListOfByteOps
                    .EndsWith(seq, sub, start, end);
            }
            switch (suffix)
            {
                case TrTuple tuple:
                {
                    if (start == 0)
                    {
                        if (end == s_ContentCount)
                            return IronPython.Runtime.Operations.IListOfByteOps
                            .EndsWith(contents.UnList, tuple);
                    }
                    else if (end == s_ContentCount)
                    {
                        return IronPython.Runtime.Operations.IListOfByteOps
                            .EndsWith(contents.UnList, tuple, start);
                    }
                    return IronPython.Runtime.Operations.IListOfByteOps
                            .EndsWith(contents.UnList, tuple, start, end);
                }
                case TrByteArray b:
                    return compute(contents.UnList, b.contents.UnList, start, end, s_ContentCount);
                case TrBytes b:
                    return compute(contents.UnList, b.contents.UnList, start, end, s_ContentCount);
                default:
                    throw new TypeError("endswith first arg must be a byte string or a tuple of byte strings");
            }
        }

        [PyBind]
        public bool isalnum()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.IsAlphaNumeric(contents.UnList);
        }

        [PyBind]
        /*
        Return True if all characters in B are alphabetic
        and there is at least one character in B, False otherwise.
        */
        public bool isalpha()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.IsLetter(contents.UnList);
        }

        [PyBind]
        public bool isascii()
        {
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i] > 127)
                    return false;
            }
            return true;
        }

        [PyBind]
        public bool isspace()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.IsWhiteSpace(contents.UnList);
        }

        [PyBind]
        public bool istitle()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.IsTitle(contents.UnList);
        }

        [PyBind]
        public bool isdigit()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.IsDigit(contents.UnList);
        }

        [PyBind]
        public byte[] join(IEnumerator<TrObject> iterable_of_bytes)
        {
            var newbytearray = new List<byte>();
            int index = 0;
            if (iterable_of_bytes.MoveNext())
            {
                IronPython.Runtime.Operations.ByteOps.AppendJoin(
                    iterable_of_bytes.Current,
                    index++,
                    newbytearray
                );
                while (iterable_of_bytes.MoveNext())
                {
                    newbytearray.AddRange(contents.UnList);
                    IronPython.Runtime.Operations.ByteOps.AppendJoin(
                        iterable_of_bytes.Current,
                        index++,
                        newbytearray
                    );
                }
            }
            return newbytearray.ToArray();

        }
        [PyBind]
        public byte[] lstrip(IList<byte> chars = null)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.LeftStrip(contents.UnList, chars).ToArray();
        }

        [PyBind]
        public byte[] ljust(int width, IList<byte> fillchar = null)
        {
            if (fillchar == null)
                fillchar = new byte[] { (byte)' ' };

            if (fillchar.Count != 1)
            {
                throw new ValueError("fillchar must be a single character");
            }

            var spaces = width - contents.Count;
            if (spaces <= 0)
            {
                return contents.UnList;
            }
            List<byte> ret = new List<byte>(width);
            ret.AddRange(contents.UnList);
            var fillchar_first = fillchar[0];
            for (int i = 0; i < spaces; i++)
            {
                ret.Add(fillchar_first);
            }
            return ret.ToArray();
        }

        [PyBind]
        public byte[] rjust(int width, IList<byte> fillchar = null)
        {
            if (fillchar == null)
                fillchar = new byte[] { (byte)' ' };

            if (fillchar.Count != 1)
            {
                throw new ValueError("fillchar must be a single character");
            }

            var spaces = width - contents.Count;
            if (spaces <= 0)
            {
                return contents.UnList;
            }
            List<byte> ret = new List<byte>(width);
            for (int i = 0; i < spaces; i++)
            {
                ret.Add(fillchar[0]);
            }
            ret.AddRange(contents.UnList);
            return ret.ToArray();
        }

        [PyBind]
        public byte[] removeprefix(IList<byte> prefix)
        {
            if (prefix.Count > contents.Count)
            {
                return contents.UnList;
            }
            for (int i = 0; i < prefix.Count; i++)
            {
                if (contents[i] != prefix[i])
                {
                    return contents.UnList;
                }
            }
            var ret = new List<byte>(contents.Count - prefix.Count);
            for (int i = prefix.Count; i < contents.Count; i++)
            {
                ret.Add(contents[i]);
            }
            return ret.ToArray();
        }

        [PyBind]
        public byte[] removesuffix(IList<byte> suffix)
        {
            if (suffix.Count > contents.Count)
            {
                return contents.UnList;
            }

            for (int i = 0; i < suffix.Count; i++)
            {

                if (contents[contents.Count - 1 - i] != suffix[suffix.Count - 1 - i])
                {
                    return contents.UnList;
                }
            }
            var ret = new List<byte>(contents.Count - suffix.Count);
            for (int i = 0; i < contents.Count - suffix.Count; i++)
            {
                ret.Add(contents[i]);
            }
            return ret.ToArray();
        }


        [PyBind]
        public TrObject replace(IList<byte> old, IList<byte> new_, int count = -1)
        {
            return MK.ByteArray(IronPython.Runtime.Operations.IListOfByteOps.Replace(contents.UnList, old, new_, count));
        }

        // TODO: support find integer
        [PyBind]
        public int rfind(IList<byte> sub, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.ReverseFind(contents.UnList, sub, start, end);
        }

        // TODO: support find integer
        [PyBind]
        public int rindex(IList<byte> sub, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            var ret = IronPython.Runtime.Operations.IListOfByteOps.ReverseFind(contents.UnList, sub, start, end);
            if (ret == -1)
            {
                throw new ValueError("substring not found");
            }
            return ret;
        }

        [PyBind]
        public TrObject partition(IList<byte>/*!*/ sep)
        {
            if (sep.Count == 0)
            {
                throw new ValueError("empty separator");
            }

            TrObject[] obj = new TrObject[3] { MK.Bytes(), MK.Bytes(), MK.Bytes() };
            if (contents.Count != 0)
            {
                var bytes_sep = MK.Bytes(sep.ToArray());
                int index = find(sep);
                if (index == -1)
                {
                    obj[0] = this;
                }
                else
                {
                    obj[0] = MK.Bytes(
                        IronPython.Runtime.Operations.IListOfByteOps.Substring(contents.UnList, 0, index).ToArray());
                    obj[1] = bytes_sep;
                    obj[2] = MK.Bytes(
                        IronPython.Runtime.Operations.IListOfByteOps.Substring(contents.UnList, index + sep.Count, contents.Count - index - sep.Count).ToArray());
                }
            }

            return MK.Tuple(obj);
        }

        [PyBind]
        public TrObject rpartition(IList<byte>/*!*/ sep)
        {
            if (sep.Count == 0)
            {
                throw new ValueError("empty separator");
            }

            TrObject[] obj = new TrObject[3] { MK.Bytes(), MK.Bytes(), MK.Bytes() };

            if (contents.Count != 0)
            {
                var bytes_sep = MK.Bytes(sep.ToArray());
                int index = rfind(sep);
                if (index == -1)
                {
                    obj[0] = this;
                }
                else
                {
                    obj[0] = MK.Bytes(
                        IronPython.Runtime.Operations.IListOfByteOps.Substring(contents.UnList, 0, index).ToArray());
                    obj[1] = bytes_sep;
                    obj[2] = MK.Bytes(
                        IronPython.Runtime.Operations.IListOfByteOps.Substring(contents.UnList, index + sep.Count, contents.Count - index - sep.Count).ToArray());
                }
            }

            return MK.Tuple(obj);
        }

        [PyBind]
        public TrObject rsplit([PyBind.Keyword] IList<byte> sep = null, [PyBind.Keyword] int maxsplit = -1)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.RightSplit(
                contents.UnList,
                sep,
                maxsplit,
                x => MK.Bytes(x is byte[] arr ? arr : x.ToArray())
            );
        }

        [PyBind]
        public byte[] rstrip(IList<byte> chars = null)
        {
            if (chars == null)
            {
                return IronPython.Runtime.Operations.IListOfByteOps.RightStrip(contents.UnList).ToArray();
            }
            else
            {
                return IronPython.Runtime.Operations.IListOfByteOps.RightStrip(contents.UnList, chars).ToArray();
            }
        }

        [PyBind]
        public TrObject split([PyBind.Keyword] IList<byte> sep = null, [PyBind.Keyword] int maxsplit = -1)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.Split(
                contents.UnList,
                sep,
                maxsplit,
                x => MK.Bytes(x.ToArray())
            );
        }

        [PyBind]
        public TrObject splitlines([PyBind.Keyword] bool keepends = false)
        {
            return IronPython.Runtime.Operations.IListOfByteOps.SplitLines(
                contents.UnList,
                keepends,
                x => MK.Bytes(x.ToArray())
            );
        }

        [PyBind]
        public bool startswith(TrObject prefix, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            static bool compute(byte[] seq, IList<byte> sub, int start, int end, int count)
            {
                if (start == 0)
                {
                    if (end == count)
                    {
                        return IronPython.Runtime.Operations.IListOfByteOps
                            .StartsWith(seq, sub);
                    }
                }
                return IronPython.Runtime.Operations.IListOfByteOps
                    .StartsWith(seq, sub, start, end);
            }
            switch (prefix)
            {
                case TrTuple prefixes:
                    if (start == 0)
                    {
                        if (end == s_ContentCount)
                        {
                            return IronPython.Runtime.Operations.IListOfByteOps.StartsWith(contents.UnList, prefixes);
                        }
                    }
                    else if (end == s_ContentCount)
                    {
                        return IronPython.Runtime.Operations.IListOfByteOps.StartsWith(contents.UnList, prefixes, start);
                    }
                    return IronPython.Runtime.Operations.IListOfByteOps.StartsWith(contents.UnList, prefixes, start, end);

                case TrByteArray b:
                    return compute(contents.UnList, b.contents.UnList, start, end, s_ContentCount);

                case TrBytes b:
                    return compute(contents.UnList, b.contents.UnList, start, end, s_ContentCount);

                default:
                    throw new TypeError("startswith first arg must be a byte string or a tuple of byte strings");
            }
        }

        [PyBind]
        public byte[] strip(IList<byte> chars = null)
        {
            if (chars == null)
            {
                return IronPython.Runtime.Operations.IListOfByteOps.Strip(contents.UnList).ToArray();
            }
            else
            {
                return IronPython.Runtime.Operations.IListOfByteOps.Strip(contents.UnList, chars).ToArray();
            }
        }

        [PyBind]
        public byte[] swapcase()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.SwapCase(contents.UnList).ToArray();
        }

        [PyBind]
        public byte[] title()
        {
            return IronPython.Runtime.Operations.IListOfByteOps.Title(contents.UnList).ToArray();
        }

        [PyBind]
        public byte[] translate(byte[] table, [PyBind.Keyword] IList<byte> delete = null)
        {
            if (contents.Count == 0)
                return this.contents.UnList;
            return IronPython.Runtime.Operations.IListOfByteOps.Translate(contents.UnList, table, delete).ToArray();
        }

        [PyBind]
        public byte[] zfill(int width)
        {
            int spaces = width - contents.Count;
            if (spaces <= 0)
            {
                return this.contents;
            }
            return IronPython.Runtime.Operations.IListOfByteOps.ZeroFill(
                    contents.UnList,
                    width,
                    spaces).ToArray();
        }

    }

}