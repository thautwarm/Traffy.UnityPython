using System;
using System.Collections.Generic;
using System.Text;
using InlineHelper;
using Traffy.Annotations;

namespace Traffy.Objects
{
    public static class TrObjectFromString
    {
        public static TrStr ToTr(this string self) => MK.Str(self);

        public static string AsStr(this TrObject self)
        {
            var o_str = self as TrStr;
            if (o_str == null)
            {
                throw new TypeError($"Expected a string, got a {self.Class.Name}");
            }
            return o_str.value;
        }

        public static bool IsStr(this TrObject self) => self is TrStr;
    }

    [Serializable]
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Comparable), typeof(Traffy.Interfaces.Sequence))]
    public partial class TrStr : TrObject, IComparable<TrObject>
    {
        public string value;
        internal int s_ContentCount => value.Length;

        public bool isInterned = false;
        int IComparable<TrObject>.CompareTo(TrObject other)
        {
            if (other is TrStr s)
            {
                return IronPython.Runtime.Operations.StringOps.Compare(value, s.value);
            }
            throw new TypeError($"unsupported comparison for '{CLASS.Name}' and '{other.Class.Name}'");
        }

        public override string __repr__() => IronPython.Runtime.Operations.StringOps.__repr__(value);
        public override string __str__() => value;
        public override bool __bool__() => value.Length != 0;
        public override List<TrObject> __array__ => null;
        public override IEnumerator<TrObject> __iter__()
        {
            for (int i = 0; i < value.Length; i++)
            {
                yield return MK.Str(value[i].ToString());
            }
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public TrStr Interned() => isInterned ? this : MK.IStr(value);
        public InternedString GetInternedString() => InternedString.Unsafe(Interned().value);

        public InternedString AsIString() => isInterned ?
            InternedString.Unsafe(this.value) :
            InternedString.FromString(value);

        public override int __hash__() => value.GetHashCode();

        public override bool __le__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for <=: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqLtE<FString, FString, char>(b.value, out var _);
        }
        public override bool __lt__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqLt<FString, FString, char>(b.value);
        }

        public override bool __gt__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for >: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqGt<FString, FString, char>(b.value);
        }


        public override bool __ge__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for >=: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqLtE<FString, FString, char>(b.value, out var _);
        }


        public override bool __ne__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                return true;
            }
            if (isInterned && b.isInterned)
                return !object.ReferenceEquals(value, b.value);
            return value != b.value;
        }

        public override bool __eq__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                return false;
            }
            if (isInterned && b.isInterned)
                return object.ReferenceEquals(value, b.value);
            return value == b.value;
        }

        public override bool __contains__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"'in {Class.__repr__()}' requires string as left operand, not '{other.Class.__repr__()}'");
            }
            return value.Contains(b.value);
        }

        public override TrObject __len__()
        {
            return MK.Int(value.Length);
        }

        public override TrObject __getitem__(TrObject item)
        {
            switch (item)
            {
                case TrInt o_i:
                {
                    return MK.Str(this.value[(int)o_i.value].ToString());
                }
                case TrSlice o_slice:
                {
                    return MK.Str(IronPython.Runtime.Operations.StringOps.GetSlice(value, o_slice));
                }
                default:
                    throw new TypeError($"str indices must be integers, not '{item.Class.Name}'");
            }
        }

        public override TrObject __mul__(TrObject a)
        {
            if (a is TrInt o_times)
            {
                return MK.Str(IronPython.Runtime.Operations.StringOps.Multiply(value, (int)o_times.value));
            }
            throw new TypeError($"can't multiply sequence by non-int of type '{a.Class.Name}'");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrStr>("str");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("str.__new__", TrStr.datanew);
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override object Native => value;

        public override TrObject __add__(TrObject other)
        {
            if (other is TrStr s)
                return MK.Str(value + s.value);
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{other.Class.Name}'");
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Str("");
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                if (arg is TrStr)
                    return arg;
                return MK.Str(arg.__str__());
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        [PyBind]
        public string capitalize()
        {
            return IronPython.Runtime.Operations.StringOps.capitalize(value);
        }

        [PyBind]
        public string casefold()
        {
            return IronPython.Runtime.Operations.StringOps.lower(value);
        }

        [PyBind]
        public string center(int width, string fillchar = null)
        {
            if (fillchar == null)
                return IronPython.Runtime.Operations.StringOps.center(value, width);
            if (fillchar.Length != 1)
            {
                throw new TypeError($"center() argument 2 must be a single character, not {fillchar.Length}-character string");
            }
            return IronPython.Runtime.Operations.StringOps.center(value, width, fillchar[0]);
        }

        [PyBind]
        public int count(string element, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            return IronPython.Runtime.Operations.StringOps.count(value, element, start, end);
            // return value.Inline().CountSubSeqGenericSimple<FString, FString, char>(element, start, end);
        }

        [PyBind]
        public byte[] encode(string encoding = "utf-8")
        {
            switch (IronPython.Runtime.Operations.StringOps.lower(encoding))
            {
                case "utf8":
                case "utf-8":
                    return Encoding.UTF8.GetBytes(value);
                case "utf16":
                case "utf-16":
                    return Encoding.Unicode.GetBytes(value);
                case "utf32":
                case "utf-32":
                    return Encoding.UTF32.GetBytes(value);
                case "latin1":
                case "latin-1":
                    return Encoding.GetEncoding("iso-8859-1").GetBytes(value);
                case "ascii":
                    return Encoding.ASCII.GetBytes(value);
                default:
                    throw new TypeError($"unknown encoding: {encoding}");
            }
        }

        [PyBind]
        public bool endswith(TrObject suffix, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            if (suffix is TrStr string_pattern)
            {
                if (start == 0 && end == s_ContentCount)
                    return IronPython.Runtime.Operations.StringOps.endswith(value, string_pattern.value);
                return IronPython.Runtime.Operations.StringOps.endswith(value, string_pattern.value, start, end);
            }
            else if (suffix is TrTuple tuple)
            {
                if (start == 0 && end == s_ContentCount)
                    return IronPython.Runtime.Operations.StringOps.endswith(value, tuple);
                return IronPython.Runtime.Operations.StringOps.endswith(value, tuple, start, end);
            }
            else
            {
                throw new ValueError("endswith first argument must be str or tuple");
            }
        }

        [PyBind]
        public string expandtabs(int tabsize = 8)
        {
            return IronPython.Runtime.Operations.StringOps.expandtabs(value, tabsize);
        }

        [PyBind]
        public int find(string element, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            if (start == 0)
            {
                if (end == s_ContentCount)
                    return IronPython.Runtime.Operations.StringOps.find(value, element);
                Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(ref end, s_ContentCount);
            }
            else
            {
                Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(ref start, s_ContentCount);
                if (end == s_ContentCount)
                    return IronPython.Runtime.Operations.StringOps.find(value, element, start);
                Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(ref end, s_ContentCount);
            }
            return IronPython.Runtime.Operations.StringOps.find(value, element, start, end);
        }

        [PyBind]
        public TrObject format(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            return MK.Str(IronPython.Runtime.NewStringFormatter.FormatString(
                Traffy.Compatibility.IronPython.PythonContext.Current, value, args.ToArray(), MK.Dict(kwargs)));
        }

        [PyBind]
        public string format_map(Dictionary<TrObject, TrObject> mapping)
        {
            return IronPython.Runtime.NewStringFormatter.FormatString(
                Traffy.Compatibility.IronPython.PythonContext.Current, value, MK.Tuple().elts, MK.Dict(mapping));
        }

        [PyBind]
        public int index(string element, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0, [PyBind.Keyword(Only = true)] bool noraise = false)
        {
            int result;
            if (start == 0)
            {
                if (end == s_ContentCount)
                {
                    result = IronPython.Runtime.Operations.StringOps.find(value, element);
                    goto returning;
                }
                Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(ref end, s_ContentCount);
            }
            else
            {
                Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(ref start, s_ContentCount);
                if (end == s_ContentCount)
                {
                    result = IronPython.Runtime.Operations.StringOps.find(value, element, start);
                    goto returning;
                }

                Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(ref end, s_ContentCount);
            }
            result = IronPython.Runtime.Operations.StringOps.find(value, element, start, end);
            returning:
            if (result == -1 && !noraise)
                throw new ValueError("substring not found");
            return result;
        }

        [PyBind]
        public bool isalnum()
        {
            return IronPython.Runtime.Operations.StringOps.isalnum(value);
        }

        [PyBind]
        public bool isalpha()
        {
            return IronPython.Runtime.Operations.StringOps.isalpha(value);
        }

        [PyBind]
        public bool isascii()
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] > 127)
                    return false;
            }
            return true;
        }

        [PyBind]
        public bool isdecimal()
        {
            return IronPython.Runtime.Operations.StringOps.isdecimal(value);
        }

        [PyBind]
        public bool isdigit()
        {
            return IronPython.Runtime.Operations.StringOps.isdigit(value);
        }

        [PyBind]
        public bool isidentifier()
        {
            return IronPython.Runtime.Operations.StringOps.isidentifier(value);
        }

        [PyBind]
        public bool islower()
        {
            return IronPython.Runtime.Operations.StringOps.islower(value);
        }

        [PyBind]
        public bool isnumeric()
        {
            return IronPython.Runtime.Operations.StringOps.isnumeric(value);
        }

        [PyBind]
        public bool isprintable()
        {
            return IronPython.Runtime.Operations.StringOps.isprintable(value);
        }
        [PyBind]
        public bool isspace()
        {
            return IronPython.Runtime.Operations.StringOps.isspace(value);
        }
        [PyBind]
        public bool istitle()
        {
            return IronPython.Runtime.Operations.StringOps.istitle(value);
        }

        [PyBind]
        public bool isupper()

        {
            return IronPython.Runtime.Operations.StringOps.isupper(value);
        }

        [PyBind]
        public string join(TrObject sequence)
        {
            if (sequence is TrList list)
                return IronPython.Runtime.Operations.StringOps.join(value, list);
            return IronPython.Runtime.Operations.StringOps.join(value, sequence);
        }

        [PyBind]
        public string ljust(int width, string fillchar = null)
        {
            if (fillchar == null)
                return IronPython.Runtime.Operations.StringOps.ljust(value, width);
            if (fillchar.Length != 1)
                throw new ValueError("fillchar must be exactly one character long");
            return IronPython.Runtime.Operations.StringOps.ljust(value, width, fillchar[0]);
        }

        [PyBind]
        public string lower()
        {
            return IronPython.Runtime.Operations.StringOps.lower(value);
        }
        [PyBind]
        public string lstrip(string chars = null)
        {
            if (chars == null)
                return IronPython.Runtime.Operations.StringOps.lstrip(value);
            return IronPython.Runtime.Operations.StringOps.lstrip(value, chars);
        }

        [PyBind]
        public TrObject partition(string sep)
        {
            var (l1, l2, l3) = IronPython.Runtime.Operations.StringOps.partition(value, sep);
            return MK.NTuple(MK.Str(l1), MK.Str(l2), MK.Str(l3));
        }

        [PyBind]
        public string removeprefix(string prefix)
        {
            if (prefix.Length > value.Length)
            {
                return value;
            }
            for (int i = 0; i < prefix.Length; i++)
            {
                if (value[i] != prefix[i])
                {
                    return value;
                }
            }
            return value.Substring(prefix.Length, value.Length - prefix.Length);
        }

        [PyBind]
        public string removesuffix(string suffix)
        {
            if (suffix.Length > value.Length)
            {
                return value;
            }
            for (int i = 0; i < suffix.Length; i++)
            {
                if (value[value.Length - suffix.Length + i] != suffix[i])
                {
                    return value;
                }
            }
            return value.Substring(0, value.Length - suffix.Length);
        }

        [PyBind]
        public string replace(string old, string new_, int count = -1)
        {
            return IronPython.Runtime.Operations.StringOps.replace(value, old, new_, count);
        }

        [PyBind]
        public int rfind(string pattern, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            return IronPython.Runtime.Operations.StringOps.rfind(value, pattern, start, end);
        }

        [PyBind]
        public int rindex(string pattern, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            return IronPython.Runtime.Operations.StringOps.rindex(value, pattern, start, end);
        }

        [PyBind]
        public string rjust(int width, string fillchar = null)
        {
            if (fillchar == null)
                return IronPython.Runtime.Operations.StringOps.rjust(value, width);
            if (fillchar.Length != 1)
                throw new ValueError("fillchar must be exactly one character long");
            return IronPython.Runtime.Operations.StringOps.rjust(value, width, fillchar[0]);
        }

        [PyBind]
        public TrObject rpartition(string sep)
        {
            var (l1, l2, l3) = IronPython.Runtime.Operations.StringOps.rpartition(value, sep);
            return MK.NTuple(MK.Str(l1), MK.Str(l2), MK.Str(l3));
        }
        [PyBind]
        public TrObject rsplit([PyBind.Keyword] string sep = null, [PyBind.Keyword] int maxsplit = -1)
        {
            return IronPython.Runtime.Operations.StringOps.rsplit(value, sep, maxsplit);
        }
        [PyBind]
        public string rstrip(string chars = null)
        {
            if (chars == null)
                return IronPython.Runtime.Operations.StringOps.rstrip(value);
            return IronPython.Runtime.Operations.StringOps.rstrip(value, chars);
        }
        [PyBind]
        public TrObject split([PyBind.Keyword] string sep = null, [PyBind.Keyword] int maxsplit = -1)
        {
            return IronPython.Runtime.Operations.StringOps.split(value, sep, maxsplit);
        }

        [PyBind]
        public TrObject splitlines([PyBind.Keyword] bool keepends = false)
        {
            return IronPython.Runtime.Operations.StringOps.splitlines(value, keepends);
        }

        [PyBind]
        public bool startswith(TrObject prefix, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            if (prefix is TrStr string_pattern)
            {
                if (start == 0 && end == s_ContentCount)
                    return IronPython.Runtime.Operations.StringOps.startswith(value, string_pattern.value);
                return IronPython.Runtime.Operations.StringOps.startswith(value, string_pattern.value, start, end);
            }
            else if (prefix is TrTuple tuple)
            {
                if (start == 0 && end == s_ContentCount)
                    return IronPython.Runtime.Operations.StringOps.startswith(value, tuple);
                return IronPython.Runtime.Operations.StringOps.startswith(value, tuple, start, end);
            }
            else
            {
                throw new ValueError("startswith first argument must be str or tuple");
            }
        }

        [PyBind]
        public string strip(string chars = null)
        {
            return IronPython.Runtime.Operations.StringOps.strip(value, chars);
        }

        [PyBind]
        public string swapcase()
        {
            return IronPython.Runtime.Operations.StringOps.swapcase(value);
        }
        [PyBind]
        public string title()
        {
            return IronPython.Runtime.Operations.StringOps.title(value);
        }
        [PyBind]
        public string translate(Dictionary<TrObject, TrObject> table)
        {
            return IronPython.Runtime.Operations.StringOps.translate(value, table);
        }
        [PyBind]
        public string upper()
        {
        return IronPython.Runtime.Operations.StringOps.upper(value);
        }
        [PyBind]
        public string zfill(int width)
        {
            return IronPython.Runtime.Operations.StringOps.zfill(value, width);
        }
    }

}