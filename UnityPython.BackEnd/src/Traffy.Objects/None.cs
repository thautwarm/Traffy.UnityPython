using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [Serializable]
    [PyBuiltin]
    public partial class TrNone : TrObject
    {

        const int NoneHash = 1225283;
        public override List<TrObject> __array__ => null;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public static TrNone Unique = new TrNone();
        public static bool unique_set = false;

        public override int __hash__() => NoneHash;

        public override bool __eq__(TrObject other) => Object.ReferenceEquals(this, other);

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrNone>("NoneType");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NoneType.__new__", TrNone.datanew);
            CLASS.IsSealed = true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }

        [OnDeserialized]
        TrNone _Singleton()
        {
            return Unique;
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.None();
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

}