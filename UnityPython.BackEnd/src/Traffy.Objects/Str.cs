using System;
using System.Collections.Generic;
using InlineHelper;

namespace Traffy.Objects
{
    public static class TrObjectFromString
    {
        public static TrStr ToTr(this string self) => MK.Str(self);

        public static string AsStr(this TrObject self) => ((TrStr)self).value;

        public static bool IsStr(this TrObject self) => self is TrStr;
    }

    [Serializable]
    public partial class TrStr : TrObject
    {
        public string value;
        public bool isInterned = false;

        int IComparable<TrObject>.CompareTo(TrObject other)
        {
            if (other is TrStr s)
            {
                return value.CompareTo(s.value);
            }
            throw new TypeError($"unsupported comparison for '{CLASS.Name}' and '{other.Class.Name}'");
        }

        public string __repr__() => value.Escape();
        public string __str__() => value;
        public bool __bool__() => value.Length != 0;
        public List<TrObject> __array__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;
        public TrStr Interned() => isInterned ? this : MK.IStr(value);
        public InternedString GetInternedString() => InternedString.Unsafe(Interned().value);

        public InternedString AsIString() => isInterned ?
            InternedString.Unsafe(this.value) :
            InternedString.FromString(value);

        public int __hash__() => value.GetHashCode();

        public bool __contains__(TrObject other) => value.Contains(other.AsStr());

        bool TrObject.__le__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for <=: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqLtE<FString, FString, char>(b.value, out var _);
        }
        bool TrObject.__lt__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqLt<FString, FString, char>(b.value);
        }

        bool TrObject.__gt__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for >: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqGt<FString, FString, char>(b.value);
        }


        bool TrObject.__ge__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for >=: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqLtE<FString, FString, char>(b.value, out var _);
        }


        bool TrObject.__ne__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for !=: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqNe<FString, FString, char>(b.value);
        }

        bool TrObject.__eq__(TrObject other)
        {
            if (!(other is TrStr b))
            {
                throw new TypeError($"unsupported operand type(s) for ==: '{CLASS.Name}' and '{other.Class.Name}'");
            }
            return value.Inline().SeqEq<FString, FString, char>(b.value);
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrStr>("str");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("str.__new__", TrStr.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrStr)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrStr))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public object Native => value;

        public TrObject __add__(TrObject other)
        {
            if (other is TrStr s)
                return MK.Str(value + s.value);
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{other.Class.Name}'");
        }

        public bool __eq__(TrObject other)
        {
            return
                other is TrStr s &&
                (isInterned
                    ? object.ReferenceEquals(s.value, value)
                    : s.value == value);
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
    }

}