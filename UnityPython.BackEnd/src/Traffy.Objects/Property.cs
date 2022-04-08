using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    public partial class TrProperty : TrObject
    {
        public string name;
        Func<TrObject, TrObject> _getter;
        Action<TrObject, TrObject> _setter = null;
        public override List<TrObject> __array__ => null;
        public override string __repr__() => $"<property {name}>";
        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        TrObject bind_setter(TrObject o)
        {
            _setter = (self, x) => o.Call(self, x);
            return this;
        }

        TrObject bind_getter(TrObject o)
        {
            _getter = self => o.Call(self);
            return this;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrProperty>("property");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("property.__new__", datanew);
            CLASS.IsSealed = true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        [PyBind]
        TrObject getter
        {
            get {
                return TrSharpFunc.FromFunc("property.getter", bind_getter);
            }
        }

        [PyBind]
        TrObject setter
        {
            get {
                return TrSharpFunc.FromFunc("property.setter", bind_setter);
            }
        }

        internal TrObject Get(TrObject o)
        {
            if (_getter == null)
            {
                if (setter != null)
                    throw new AttributeError("write-only property");
                throw new AttributeError("cannot read property");
            }
            return _getter(o);
        }

        internal void Set(TrObject trObject, TrObject value)
        {
            if (_setter == null)
            {
                if (getter != null)
                    throw new AttributeError($"readonly property");
                throw new AttributeError($"cannot set property");
            }
            _setter(trObject, value);
        }

        public static TrProperty Create(string name, Func<TrObject, TrObject> s_getter, Action<TrObject, TrObject> s_setter)
        {
            var prop = new TrProperty();
            prop.name = name;
            if (s_getter != null)
                prop._getter = s_getter;
            if (s_setter != null)
                prop._setter = s_setter;
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

                var prop = new TrProperty();
                var getter = args[1];
                prop.name = getter.__repr__();
                prop._getter = (self) => getter.Call(self);
                return prop;
                // var pyfunc = args[1];
                // Func<TrObject, TrObject> getter = self => pyfunc.Call(self);
                // return TrProperty.Create(getter, null);
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }


    }

}