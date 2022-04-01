using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Modules
{
    [PyBuiltin]
    public partial class TrModule_enum : TrObject
    {
        public static TrClass CLASS;

        public override List<TrObject> __array__ => null;

        public override TrClass Class => CLASS;

        private static TrObject _auto_ann;

        [PyBind]
        public static TrObject auto()
        {
            if (_auto_ann != null)
                return _auto_ann;
            _auto_ann = TrRawObject.CLASS.Call();
            return _auto_ann;
        }


        [PyBind]
        public static TrObject Enum => TrEnum.CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_enum>("module_enum");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("module_enum.__new__", TrClass.new_notallow));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;

            ModuleSystem.Modules["enum"] = CLASS;
        }
    }

}