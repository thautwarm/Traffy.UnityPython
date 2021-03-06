using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Callable))]
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

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSharpMethod>("method");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("method.__new__", TrSharpMethod.datanew);
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
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