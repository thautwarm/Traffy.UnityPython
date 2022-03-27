using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    public partial class TrProperty : TrObject
    {
        TrObject _getter;
        TrObject _setter = null;
        public override List<TrObject> __array__ => null;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        TrObject bind_setter(TrObject o)
        {
            _setter = o;
            return this;
        }

        TrObject bind_getter(TrObject o)
        {
            _getter = o;
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
                throw new Exception("property has no getter");
            return _getter.Call(o);
        }

        internal void Set(TrObject trObject, TrObject value)
        {
            if (_setter == null)
                throw new Exception("property has no setter");
            _setter.Call(trObject, value);
        }

        public static TrProperty Create(TrObject s_getter, TrObject s_setter)
        {
            var prop = new TrProperty();
            prop._getter = s_getter;
            prop._setter = s_setter;
            return prop;
        }

        public static TrProperty Create(string name, Func<TrObject, TrObject> s_getter, Action<TrObject, TrObject> s_setter)
        {
            var prop = new TrProperty();
            if (s_getter != null)
                prop._getter = TrSharpFunc.FromFunc("get " + name, s_getter);
            if (s_setter != null)
                prop._setter = TrSharpFunc.FromFunc("set" + name, s_setter);
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
                prop._getter = args[1];
                return prop;
                // var pyfunc = args[1];
                // Func<TrObject, TrObject> getter = self => pyfunc.Call(self);
                // return TrProperty.Create(getter, null);
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }


    }

}