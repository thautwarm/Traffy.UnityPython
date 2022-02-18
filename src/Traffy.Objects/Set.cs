using System.Collections.Generic;

namespace Traffy.Objects
{
    public partial class TrSet : TrObject
    {
        public HashSet<TrObject> container;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSet>();
            CLASS.Name = "set";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("set.__new__", TrSet.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrSet)] = CLASS;
        }
        [Mark(typeof(TrSet))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
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