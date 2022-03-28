using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Modules
{
    [PyBuiltin]
    public sealed partial class TrModule_typing: TrObject
    {
        public override List<TrObject> __array__ => null;

        public override TrClass Class => CLASS;

        public static TrClass CLASS;

        [PyBind]
        public static TrObject runtime_checkable(TrObject f)
        {
            return f;
        }

        [PyBind]
        public static TrObject TypeVar(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (args.Count < 1)
            {
                throw new TypeError("TypeVar() requires at least 1 positional argument(s), got " + args.Count);
            }
            return args[0];
        }

        static TrObject _cache_AnyStr = null;
        [PyBind]
        public static TrObject AnyStr =>
            _cache_AnyStr ?? TypeVar(new BList<TrObject> { MK.IStr("AnyStr") }, null);


        [PyBind]
        public static TrObject Annotated => new TrAnnotatedType();


        // so that 'Generic' can be inherited without restrictions.
        [PyBind]
        public static TrObject Generic => TrRawObject.CLASS;

        [PyBind]
        public static TrObject TypedDict => TrTypedDict.CLASS;

    
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_abc>("module_typing");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("module_typing.__new__", TrClass.new_notallow));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;

            ModuleSystem.Modules["typing"] = CLASS;
        }
    }
}