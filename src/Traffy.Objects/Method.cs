using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TrSharpMethod : TrObject
    {
        public TrObject func;

        public TrObject self;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSharpMethod>();
            CLASS.Name = "method";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("method.__new__", TrSharpMethod.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrSharpMethod)] = CLASS;
        }

        [Mark(typeof(TrSharpMethod))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            args.AddLeft(self);
            var o = func.__call__(args, kwargs);
            args.PopLeft();
            return o;
        }

        // call types.MethodType

        public static TrObject Bind(TrObject func, TrObject self)
        {
            return new TrSharpMethod { func = func, self = self };
        }

        public static TrObject BindOrUnwrap(TrObject func, TrObject self)
        {
            return new TrSharpMethod { func = func, self = self };
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_only(args, 3);
            return Bind(args[1], args[2]);
        }
    }
}