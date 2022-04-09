using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [PyBuiltin]
    public partial class TrUnionType : TrObject
    {
        public TrObject left;
        public TrObject right;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;
        public override string __repr__() => left.__repr__() + "|" + right.__repr__();

        public override TrObject __or__(TrObject right)
        {
            if (right is TrClass || right is TrNone || right is TrUnionType)
            {
                return MK.UnionType(this, right);
            }
            throw new TypeError($"UnionType can only be used with classes, None, or UnionType, not {right.__repr__()}");
        }

        public TrUnionType(TrObject left, TrObject right)
        {
            this.left = left;
            this.right = right;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrUnionType>("UnionType");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("tuple.__new__", TrUnionType.datanew);
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {CLASS.Name}");
        }
    }

}