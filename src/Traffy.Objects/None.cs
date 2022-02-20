using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Traffy.Objects
{
    [Serializable]
    public partial class TrNone : TrObject
    {

        public List<TrObject> __array__ => null;

        public object Native => this;

        public static TrClass CLASS;
        public TrClass Class => CLASS;
        public static TrNone Unique = new TrNone();
        public static bool unique_set = false;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrNone>();
            CLASS.Name = "NoneType";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NoneType.__new__", TrNone.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrNone)] = CLASS;
        }
        [Mark(typeof(TrNone))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
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