using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    public partial class TrSharpMethod : TrObject
    {
        public TrObject func;

        [PyBind]
        public TrObject __func__
        {
            get { return func; }
            set { func = value; }
        }
        public TrObject self;

        [PyBind]
        public TrObject __self__
        {
            get { return self; }
            set { self = value; }
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        public override string __repr__() => $"<bound method {func.__repr__()} at {self.__repr__()}>";

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSharpMethod>("method");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("method.__new__", TrSharpMethod.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrSharpMethod)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrSharpMethod))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
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

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_only(args, 3);
            return Bind(args[1], args[2]);
        }
    }
}