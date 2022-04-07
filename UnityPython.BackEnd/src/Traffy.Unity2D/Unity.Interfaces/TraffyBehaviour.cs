using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Interfaces;
using Traffy.Objects;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public partial class TrTraffyBehaviour : TrObject
    {
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        [PyBind]
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (args.Count != 2)
            {
                throw new TypeError($"TraffyBehaviour.__new__ takes exactly 2 arguments ({args.Count} given)");
            }
            var u1 = args[0] as TrClass;
            if (RTS.issubclassof(u1, CLASS))
            {
                throw new TypeError($"TraffyBehaviour.__new__: argument 1 must be a subclass of {CLASS.Name}");
            }
            var uo = args[1] as TrUnityObject;
            if (uo == null)
            {
                throw new TypeError($"TraffyBehaviour.__new__: expected a TrUnityObject, got {args[1].Class.Name}");
            }            
            return TrUnityUserComponent.Create(uo, u1);
        }

        [PyBind]
        public static void __init_subclass__(TrObject _, TrObject newclsobj)
        {
            if (newclsobj is TrClass newcls)
            {
                newcls[MagicNames.i___new__] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("TraffyBehaviour.__init_subclass__", datanew));
            }
            else
            {
                throw new TypeError("__init_subclass__ must be called with a class as first argument");
            }
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrTypedDict>("TraffyBehaviour");
            CLASS.IsSealed = false;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("TraffyBehaviour.__new__", datanew);
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
    }
}
#endif