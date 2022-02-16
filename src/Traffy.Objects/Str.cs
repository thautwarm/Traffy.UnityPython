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

        public Dictionary<TrObject, TrObject> __dict__ => null;
        public string __repr__() => value.Escape();
        public string __str__() => value;
        public bool __bool__() => value.Length != 0;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.ClasInitToken)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrStr>();
            CLASS.Name = "str";
            CLASS.IsFixed = true;
            CLASS.IsSealed = true;
            CLASS.__new = TrStr.datanew;
            TrClass.TypeDict[typeof(TrStr)] = CLASS;
        }

        [Mark(typeof(TrStr))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

        public object Native => value;

        public bool __eq__(TrObject other)
        {
            return other is TrStr s && s.value == value;
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