using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TrStaticMethod : TrObject
    {
        public TrObject func;
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public string __repr__() => $"<staticmethod {func.__repr__()}>";

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrStaticMethod>();
            CLASS.Name = "staticmethod";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("staticmethod.__new__", TrStaticMethod.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrStaticMethod)] = CLASS;
        }

        [Mark(typeof(TrStaticMethod))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            return func.__call__(args, kwargs);
        }

        // call types.MethodType

        public static TrStaticMethod Bind(TrObject func)
        {
            return new TrStaticMethod { func = func };
        }

        public static TrStaticMethod Bind(string name, Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            return new TrStaticMethod { func = TrSharpFunc.FromFunc(name, func) };
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_only(args, 2);
            return Bind(args[1]);
        }
    }
}