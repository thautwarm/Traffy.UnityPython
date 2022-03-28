using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Modules
{
    [PyBuiltin]
    public sealed partial class TrModule_types : TrObject
    {
        public override List<TrObject> __array__ => null;

        public override TrClass Class => CLASS;

        public static TrClass CLASS;

        [PyBind]
        public static TrObject BuiltinFunctionType => TrSharpFunc.CLASS;

        [PyBind]
        public static TrObject FunctionType => TrFunc.CLASS;

        [PyBind]
        public static TrObject ModuleType => TrModule.CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_abc>("module_types");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("module_types.__new__", TrClass.new_notallow));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;

            ModuleSystem.Modules["types"] = CLASS;
        }
    }
}