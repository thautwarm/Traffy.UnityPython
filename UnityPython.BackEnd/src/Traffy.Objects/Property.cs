using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public partial class TrProperty : TrObject
    {
        public Func<TrObject, TrObject> getter = null;
        public Action<TrObject, TrObject> setter = null;
        public List<TrObject> __array__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        TrObject bind_setter(TrObject o)
        {
            setter = (self, v) => o.Call(self, v);
            return this;
        }

        TrObject bind_getter(TrObject o)
        {
            getter = (self) => o.Call(self);
            return this;
        }

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrProperty>();
            CLASS.Name = "property";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("property.__new__", datanew);
            CLASS.InstanceUseInlineCache = false;
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrProperty)] = CLASS;
        }
        [Mark(typeof(TrProperty))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public bool __getattr__(TrObject s, TrRef found)
        {
            var attr = s.AsString();
            switch (attr)
            {
                case "setter":
                    found.value = TrSharpFunc.FromFunc("property.setter", bind_setter);
                    return true;
                case "getter":
                    found.value = TrSharpFunc.FromFunc("property.getter", bind_getter);
                    return true;
                default:
                    throw new Exception($"property has no attribute {attr}");
            }
        }

        internal TrObject Get(TrObject o)
        {
            if (getter == null)
                throw new Exception("property has no getter");
            return getter(o);
        }

        internal void Set(TrObject trObject, TrObject value)
        {
            if (setter == null)
                throw new Exception("property has no setter");
            setter(trObject, value);
        }

        public static TrProperty Create(Func<TrObject, TrObject> getter, Action<TrObject, TrObject> setter)
        {
            var prop = new TrProperty();
            prop.getter = getter;
            prop.setter = setter;
            return prop;
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return new TrProperty();

            if (narg == 2 && kwargs == null)
            {

                var pyfunc = args[1];
                Func<TrObject, TrObject> getter = self => pyfunc.Call(self);
                return TrProperty.Create(getter, null);
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }


    }

}