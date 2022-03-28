using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [PyBuiltin]
    public partial class TrAnnotatedType : TrObject
    {
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;
        public override TrObject __getitem__(TrObject item)
        {
            if (item is TrTuple tuple)
            {
                if(tuple.elts.Count <= 1)
                {
                    throw new TypeError("Annotated[...]  Annotated[...] should be used with at least two arguments (a type and an annotation)");
                }
                return tuple.elts[0];
            }
            return item;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrAnnotatedType>("AnnotatedType");
            CLASS.IsSealed = false;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ABC.__new__", datanew);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var self = new TrAnnotatedType();
            return self;
        
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
    }

}