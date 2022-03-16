using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [PyBuiltin]
    public sealed partial class TrClassMethod : TrObject
    {
        public TrObject func;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        [PyBind]
        public TrObject __func__
        {
            get
            {
                return func;
            }

            set
            {
                func = value;
            }
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrClassMethod>("classmethod");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("classmethod.__new__", TrClassMethod.datanew));
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrClassMethod)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrClassMethod))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
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