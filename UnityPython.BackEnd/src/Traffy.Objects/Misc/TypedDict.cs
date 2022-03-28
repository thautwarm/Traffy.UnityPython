using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [PyBuiltin]
    public partial class TrTypedDict : TrObject
    {
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        public static TrObject typed_dict_instance_new(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (args.Count != 1)
                throw new TypeError("creating TypedDict takes no positional arguments");
            return MK.Dict(kwargs);
        }

        [PyBind]
        public static void __init_subclass__(TrObject _, TrObject newclsobj)
        {
            if (newclsobj is TrClass newcls)
            {
                newcls[MagicNames.i___new__] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("TypedDict.__init_subclass__", typed_dict_instance_new));
            }
            else
            {
                throw new TypeError("__init_subclass__ must be called with a class as first argument");
            }
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrTypedDict>("TypedDict");
            CLASS.IsSealed = false;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("TypedDict.__new__", datanew);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError("TypedDict should be not used directly");
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
    }

}