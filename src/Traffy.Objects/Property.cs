using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public partial class TrProperty : TrObject
    {
        public Func<TrObject, TrObject> getter = null;
        public Action<TrObject, TrObject> setter = null;
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;
        [Mark(ModuleInit.ClasInitToken)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrProperty>();
            CLASS.Name = "property";
            CLASS.IsFixed = true;
            CLASS.IsSealed = true;
            CLASS.__new = TrProperty.datanew;
            TrClass.TypeDict[typeof(TrProperty)] = CLASS;
        }
        [Mark(typeof(TrProperty))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

        public bool __getattr__(TrObject s, TrRef found)
        {
            var attr = s.AsString();
            switch (attr)
            {
                case "setter":
                    TrObject bind_setter(TrObject o)
                    {
                        setter = (self, v) => o.Call(self, v);
                        return RTS.object_none;
                    }
                    found.value = TrSharpFunc.FromFunc("property.setter", bind_setter);
                    return true;
                case "getter":
                    TrObject bind_getter(TrObject o)
                    {
                        getter = (self) => o.Call(self);
                        return RTS.object_none;
                    }
                    found.value = TrSharpFunc.FromFunc("property.getter", bind_getter);
                    return true;
                default:
                    return TrObject.__raw_getattr__(this, s, found);
            }
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
                return new TrProperty { getter = getter };
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

}