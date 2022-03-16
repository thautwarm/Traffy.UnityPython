using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    public partial class TrRef : TrObject
    {
        public TrObject value;
        static string s_attrValue = String.Intern("value");
        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrRef>("ref");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ref.__new__", TrRef.datanew);
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

        bool _slow_read(Traffy.Objects.TrStr s, out Traffy.Objects.TrObject found)
        {
            if (s.value == s_attrValue)
            {
                found = value;
                return true;
            }
            return CLASS.__getic_refl__(s, out found);
        }

        void _slow_write(Traffy.Objects.TrStr s, Traffy.Objects.TrObject s_value)
        {
            if (s.value == s_attrValue)
            {
                value = s_value;
                return;
            }
            throw new AttributeError(this, s, $"'{CLASS.Name}' object has no attribute '" + s.value + "'");
        }

        public override bool __getic_refl__(Traffy.Objects.TrStr s, out Traffy.Objects.TrObject found) => _slow_read(s, out found);
        public override bool __getic__(Traffy.InlineCache.PolyIC ic, out Traffy.Objects.TrObject found)
        {
            if (object.ReferenceEquals(ic.attribute.value, s_attrValue))
            {
                if (value == null)
                {
                    found = null;
                    return false;
                }
                found = value;
                return true;
            }
            return _slow_read(ic.attribute, out found);
        }


        public override void __setic_refl__(Traffy.Objects.TrStr s, Traffy.Objects.TrObject value) => _slow_write(s, value);
        public override void __setic__(Traffy.InlineCache.PolyIC ic, Traffy.Objects.TrObject s_value)
        {
            TrStr attr = ic.attribute;
            if (object.ReferenceEquals(attr.value, s_attrValue))
            {
                value = s_value;
                return;
            }
            _slow_write(attr, s_value);
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