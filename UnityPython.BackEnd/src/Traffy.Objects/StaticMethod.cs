using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TrStaticMethod : TrObject
    {
        public TrObject func;

        string TrObject.__repr__() => $"<staticmethod {func.__repr__()}>";

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        List<TrObject> TrObject.__array__ => null;

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrStaticMethod>("staticmethod");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("staticmethod.__new__", TrStaticMethod.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrStaticMethod)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrStaticMethod))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        TrObject TrObject.__call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
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
        public static TrStaticMethod Bind(string name, Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            return new TrStaticMethod { func = TrSharpFunc.FromFunc(name, func) };
        }

        public static TrStaticMethod Bind(string name, Func<TrClass, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
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