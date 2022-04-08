using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Callable))]
    public sealed partial class TrStaticMethod : TrObject
    {
        public TrObject func;

        public override string __repr__() => $"<staticmethod {func.__repr__()}>";

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrStaticMethod>("staticmethod");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("staticmethod.__new__", TrStaticMethod.datanew);
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            return func.__call__(args, kwargs);
        }

        // call types.MethodType

        public static TrStaticMethod Bind(TrObject func)
        {
            return new TrStaticMethod { func = func };
        }

        public static TrStaticMethod Bind(string name, Func<TrClass, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            return new TrStaticMethod { func = TrSharpFunc.FromFunc(name, func) };
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

        [PyBind]
        public TrObject __func__ => func;
    }
}