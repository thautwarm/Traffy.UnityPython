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
        public static TrObject BuiltinMethodType => TrSharpFunc.CLASS;

        [PyBind]
        public static TrObject FunctionType => TrFunc.CLASS;

        [PyBind]
        public static TrObject ModuleType => TrModule.CLASS;

        [PyBind]
        public static TrObject MethodType => TrSharpMethod.CLASS;

        [PyBind]
        public static TrObject __dict__
        {
            get
            {
                var d = RTS.baredict_create();
                foreach(var (k, v) in CLASS.GetDictItems())
                {
                    d[k] = v;
                }
                return MK.Dict(d);
            }
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_types>("module_types");
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
            CLASS.IsClassFixed = true;

            ModuleSystem.Modules["types"] = CLASS;
        }
    }
}