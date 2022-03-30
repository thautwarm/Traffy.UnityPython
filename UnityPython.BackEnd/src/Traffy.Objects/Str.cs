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
                return value.CompareTo(s.value);
            }
            throw new TypeError($"unsupported comparison for '{CLASS.Name}' and '{other.Class.Name}'");
        }

        public override string __repr__() => value.Escape();
        public override string __str__() => value;
        public override bool __bool__() => value.Length != 0;
        public override List<TrObject> __array__ => null;
        public override IEnumerator<TrObject> __iter__()
        {
            for(int i = 0; i < value.Length; i++)
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

        public override bool __contains__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"'in {Class.__repr__()}' requires string as left operand, not '{other.Class.__repr__()}'");
            }
            return value.Contains(b.value);
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

        public override TrObject __len__()
        {
            return MK.Int(value.Length);
        }

        public override TrObject __getitem__(TrObject item)
        {
            switch(item)
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
                return MK.Str(value.Repeat(unchecked((int)o_times.value)));
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
        public int count(string element, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            return IronPython.Runtime.Operations.StringOps.count(value, element, start, end);
            // return value.Inline().CountSubSeqGenericSimple<FString, FString, char>(element, start, end);
        }
    }

}