using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public static class TrObjectFromString
    {
        public static TrStr ToTr(this string self) => MK.Str(self);

        public static string AsString(this TrObject self) => ((TrStr)self).value;

        public static bool IsStr(this TrObject self) => self is TrStr;
    }

    [Serializable]
    public partial class TrStr : TrObject
    {
        public string value;
        public bool isInterned = false;
        public string __repr__() => value.Escape();
        public string __str__() => value;
        public bool __bool__() => value.Length != 0;
        public List<TrObject> __array__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;
        public TrStr Interned() => isInterned ? this : MK.IStr(value);

        public InternedString AsIString() => isInterned ?
            InternedString.Unsafe(this.value) :
            InternedString.FromString(value);

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrStr>();
            CLASS.Name = "str";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("str.__new__", TrStr.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrStr)] = CLASS;
        }

        [Mark(typeof(TrStr))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            ModuleInit.Prelude(CLASS);
        }

        public object Native => value;

        public bool __eq__(TrObject other)
        {
            return
                other is TrStr s &&
                (isInterned
                    ? object.ReferenceEquals(s.value, value)
                    : s.value == value);
        }

        public bool __lt__(TrObject other)
        {
            return other is TrStr s && value.SequenceLessThan(s.value);
        }

        public bool __le__(TrObject other)
        {
            return other is TrStr s && value.SequenceLessEqualThan(s.value);
        }

        public int __hash__()
        {
            return value.GetHashCode();
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