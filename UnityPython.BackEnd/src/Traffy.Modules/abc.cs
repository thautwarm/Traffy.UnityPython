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
        public static TrClass Awaitable => Traffy.Interfaces.Awaitable.CLASS;
        [PyBind]
        public static TrClass Callable => Traffy.Interfaces.Callable.CLASS;
        [PyBind]
        public static TrClass Collection => Traffy.Interfaces.Collection.CLASS;
        [PyBind]
        public static TrClass Comparable => Traffy.Interfaces.Comparable.CLASS;
        [PyBind]
        public static TrClass Container => Traffy.Interfaces.Container.CLASS;
        [PyBind]
        public static TrClass Hashable => Traffy.Interfaces.Hashable.CLASS;
        [PyBind]
        public static TrClass Iterable => Traffy.Interfaces.Iterable.CLASS;
        [PyBind]
        public static TrClass Reversible => Traffy.Interfaces.Reversible.CLASS;
        [PyBind]
        public static TrClass Sequence => Traffy.Interfaces.Sequence.CLASS;
        [PyBind]
        public static TrClass Sized => Traffy.Interfaces.Sized.CLASS;

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
            CLASS.IsFixed = true;

            ModuleSystem.Modules["abc"] = CLASS;
        }
    }
}