using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TrClassMethod : TrObject
    {
        public TrObject func;
        public Dictionary<TrObject, TrObject> __dict__ => null;
        public static TrClass CLASS;
        public TrClass Class => CLASS;
        public List<TrObject> __array__ => null;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrClassMethod>();
            CLASS.Name = "classmethod";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("classmethod.__new__", TrClassMethod.datanew));
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrClassMethod)] = CLASS;
        }

        [Mark(typeof(TrClassMethod))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            ModuleInit.Prelude(CLASS);
        }


        // call types.MethodType

        public static TrClassMethod Bind(TrObject func)
        {
            return new TrClassMethod { func = func };
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_only(args, 2);
            return Bind(args[1]);
        }
    }
}