using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [PyBuiltin]
    public partial class TrABC : TrObject
    {

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrABC>("abc");
            CLASS.IsSealed = false;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ABC.__new__", TrABC.new_abstract_class_instance);
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
        }
        public static TrObject new_abstract_class_instance(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"cannot initialized abstract class {cls.__repr__()}");
        }


        [PyBind]
        public static TrObject __init_subclass__(TrClass clsobj, TrClass newcls)
        {
            var bases = newcls.__base;
            if (bases.Contains(CLASS))
            {
                // directly inherit ABC
                newcls.IsAbstract = true;
                newcls[newcls.ic__new] = CLASS[CLASS.ic__new];
            }
            return MK.None();
        }

    }

}