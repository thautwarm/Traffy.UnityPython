using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Traffy.Objects
{
    public partial class TrRef : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;
        public TrObject value;

        public object Native => this;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("ref");
            CLASS.Name = "ref";
            CLASS.Fixed = true;
            CLASS.IsSealed = true;
            CLASS.__new = TrRef.datanew;
        }

                [InitSetup(InitOrder.SetupClassObjects)]
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
                return MK.Ref();
            if (narg == 2)
                return MK.Ref(args[1]);
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

}