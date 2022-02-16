using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TrSharpMethod : TrObject
    {
        public Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func;

        public TrObject self;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;
        [Mark(ModuleInit.ClasInitToken)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSharpMethod>();
            CLASS.Name = "method";
            CLASS.__new = TrSharpMethod.datanew;
            CLASS.IsFixed = true;
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrSharpMethod)] = CLASS;
        }

        [Mark(typeof(TrSharpMethod))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            args.AddLeft(self);
            var o = func(args, kwargs);
            args.PopLeft();
            return o;
        }

        // call types.MethodType

        public static TrObject Bind(TrObject func, TrObject self)
        {
            return new TrSharpMethod { func = func.__call__, self = self };
        }

        public static TrObject BindOrUnwrap(TrObject func, TrObject self)
        {
            return new TrSharpMethod { func = func.__call__, self = self };
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_only(args, 3);
            return Bind(args[1], args[2]);
        }
    }
}