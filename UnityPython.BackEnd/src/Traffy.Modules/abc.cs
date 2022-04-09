using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Modules
{
    [PyBuiltin]
    public sealed partial class TrModule_abc : TrObject
    {
        public override List<TrObject> __array__ => null;

        public override TrClass Class => CLASS;

        public static TrClass CLASS;

        [PyBind]
        public static TrClass ABC => TrABC.CLASS;
        [PyBind]
        // 'ABCMeta' does nothing at runtime!
        // This is useful at compile time, so please use an IDE (especially, VSCode Pylance)!
        public static TrClass ABCMeta => TrRawObject.CLASS;

        [PyBind]
        public static TrObject abstractmethod(TrObject func)
        {
            // 'abstractmethod' does nothing at runtime!
            // This is useful at compile time, so please use an IDE (especially, VSCode Pylance)!
            return func;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_abc>("module_abc");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("module_abc.__new__", TrClass.new_notallow));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;

            ModuleSystem.Modules["abc"] = CLASS;
        }
    }
}