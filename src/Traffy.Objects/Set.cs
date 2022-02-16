using System.Collections.Generic;

namespace Traffy.Objects
{
    public partial class TrSet : TrObject
    {
        public HashSet<TrObject> container;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;
        [Mark(ModuleInit.ClasInitToken)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSet>();
            CLASS.Name = "set";
            CLASS.IsFixed = true;
            CLASS.IsSealed = true;
            CLASS.__new = TrSet.datanew;
            TrClass.TypeDict[typeof(TrSet)] = CLASS;
        }
        [Mark(typeof(TrSet))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Set();
            if (narg == 2 && kwargs == null)
            {
                HashSet<TrObject> res = RTS.bareset_create();
                RTS.bareset_extend(res, args[1]);
                return MK.Set(res);
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

}