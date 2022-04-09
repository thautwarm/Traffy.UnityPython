using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Modules
{
    [PyBuiltin]
    public partial class TrModule_future: TrObject
    {
        public static TrClass CLASS;
        
        public override List<TrObject> __array__ => null;

        public override TrClass Class => CLASS;

        [PyBind]
        public static TrObject annotations => TrBool.TrBool_True;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_future>("module_future");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("module_future.__new__", TrClass.new_notallow));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;

            ModuleSystem.Modules["__future__"] = CLASS;
        }
    }

}