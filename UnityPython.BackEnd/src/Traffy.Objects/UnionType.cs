using System;
using System.Collections.Generic;
using System.Linq;

namespace Traffy.Objects
{

    public partial class TrUnionType : TrObject
    {
        public TrClass left;
        public TrClass right;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;
        public override string __repr__() => left.__repr__() + "|" + right.__repr__();

        public TrUnionType(TrClass left, TrClass right)
        {
            this.left = left;
            this.right = right;
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrUnionType>("UnionType");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("tuple.__new__", TrUnionType.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrUnionType)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrUnionType))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {CLASS.Name}");
        }
    }

}