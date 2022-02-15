using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    [Serializable]
    public partial class TrStr: TrObject
    {
        public string value;

        public Dictionary<TrObject, TrObject> __dict__ => throw new NotImplementedException();

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TrStr>();
            CLASS.Name = "str";
            CLASS.__new = TrStr.datanew;
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