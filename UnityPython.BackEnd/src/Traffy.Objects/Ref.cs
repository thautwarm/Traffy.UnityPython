using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Traffy.Objects
{
    public partial class TrRef : TrObject
    {
        public TrObject value;
        static string s_attrValue = String.Intern("value");
        public object Native => this;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrRef>("ref");
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ref.__new__", TrRef.datanew);
            CLASS.InstanceUseInlineCache = false;
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrRef)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrRef))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public bool __getattr__(TrObject s, TrRef found)
        {
            TrStr attr = (TrStr)s;
            if (attr.isInterned && object.ReferenceEquals(attr.value, s_attrValue))
            {
                found.value = value;
                return true;
            }
            switch (attr.value)
            {
                case "value":
                    if (value == null)
                        value = RTS.object_none;
                    found.value = value;
                    return true;
                default:
                    return false;
            }
        }

        public void __setattr__(TrObject s, TrObject m_value)
        {
            TrStr attr = (TrStr)s;
            if (attr.isInterned && object.ReferenceEquals(attr.value, s_attrValue))
            {
                value = m_value;
                return;
            }
            switch (attr.value)
            {
                case "value":
                    value = m_value;
                    return;
                default:
                    throw new AttributeError(this, s, $"ref has no attribute '{attr.value}'");
            }
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Ref();
            if (narg == 2)
                return MK.Ref(args[1]);
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

}