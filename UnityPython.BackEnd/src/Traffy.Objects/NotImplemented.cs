using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Traffy.Annotations;
using Traffy.InlineCache;

namespace Traffy.Objects
{

    public static class Ext_NotImplemented
    {
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static bool IsNotImplemented(this TrObject self)
        {
            return object.ReferenceEquals(self, TrNotImplemented.Unique);
        }
    }
    [PyBuiltin]
    public partial class TrNotImplemented : TrObject
    {
        public static TrClass CLASS;
        public static TrNotImplemented Unique = new TrNotImplemented();
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ { get; }
        public override string __repr__() => "NotImplemented";
        
        [PyBind]
        public static TrObject __new__(TrObject _, TrObject cls, TrObject args)
        {
            return Unique;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrNotImplemented>("NotImplementedType");
            CLASS.IsSealed = true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
            Initialization.Prelude("NotImplemented", Unique);
        }
    }

}