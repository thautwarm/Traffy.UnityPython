using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Traffy.Objects
{
    [Serializable]
    public partial class TrNone : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public object Native => this;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("NoneType");
            CLASS.Name = "NoneType";
            CLASS.__new = TrNone.datanew;
            CLASS.Fixed = true;
            CLASS.IsSealed = true;
        }

        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

        public static bool unique_set = false;
        public static TrNone Unique = new TrNone();

        [OnDeserialized]
        TrNone _Singleton()
        {
            return Unique;
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.None();
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

}