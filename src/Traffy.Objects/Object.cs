using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Traffy.Objects
{

    public class TraffyComparer : IEqualityComparer<TrObject>
    {
        public bool Equals(TrObject x, TrObject y)
        {
            return x.__eq__(y);
        }

        public int GetHashCode([DisallowNull] TrObject obj)
        {
            return obj.__hash__();
        }
    }

    public interface TrObject : IEquatable<TrObject>
    {
        bool IEquatable<TrObject>.Equals(TrObject other)
        {
            return __eq__(other);
        }

        public TrClass AsClass => (TrClass)this;
        public Dictionary<TrObject, TrObject> __dict__ { get; }
        public object Native => this;
        public TrClass Class { get; }
        public string __str__() =>
            (Class.__str != null) ? Class.__str(this) : __repr__();
        public string __repr__() =>
            (Class.__repr != null) ? Class.__repr(this) : Native.ToString();

        Exception unsupported(string op) =>
            throw new TypeError($"{Class.Name} has no {op} method");
        public TrObject __next__() =>
            (Class.__next != null) ? Class.__next(this) : throw unsupported(nameof(__next__));

        // Arithmetic ops
        public TrObject __add__(TrObject a)
        {
            if (Class.__add != null)
                return Class.__add(this, a);
            throw unsupported(nameof(__add__));
        }
        public TrObject __sub__(TrObject a)
        {
            if (Class.__sub != null)
                return Class.__sub(this, a);
            throw unsupported(nameof(__sub__));
        }

        public TrObject __mul__(TrObject a)
        {
            if (Class.__mul != null)
                return Class.__mul(this, a);
            throw unsupported(nameof(__mul__));
        }

        public TrObject __matmul__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__matmul;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__matmul__));
        }

        public TrObject __floordiv__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__floordiv;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__floordiv__));
        }

        public TrObject __truediv__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__truediv;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__truediv__));
        }

        public TrObject __mod__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__mod;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__mod__));
        }

        public TrObject __pow__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__pow;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__pow__));
        }

        // Bitwise logic operations

        public TrObject __bitand__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__bitand;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__bitand__));
        }

        public TrObject __bitor__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__bitor;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__bitor__));
        }

        public TrObject __bitxor__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__bitxor;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__bitxor__));
        }

        // bit shift
        public TrObject __lshift__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__lshift;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__lshift__));
        }

        public TrObject __rshift__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__rshift;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__rshift__));
        }

        // Object protocol
        public int __hash__()
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__hash;
                if (meth != null)
                    return meth(this);
            }
            return Native.GetHashCode();
        }
        public TrObject Call(params TrObject[] objs)
        {
            var xs = new BList<TrObject>();
            foreach (var e in objs)
            {
                xs.Add(e);
            }
            return __call__(xs, null);
        }

        public TrObject Call()
        {
            var xs = new BList<TrObject>();
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1)
        {
            var xs = new BList<TrObject> { a1 };
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1, TrObject a2)
        {
            var xs = new BList<TrObject> { a1, a2 };
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1, TrObject a2, TrObject a3)
        {
            var xs = new BList<TrObject> { a1, a2, a3 };
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1, TrObject a2, TrObject a3, TrObject a4)
        {
            var xs = new BList<TrObject> { a1, a2, a3, a4 };
            return __call__(xs, null);
        }


        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__call;
                if (meth != null)
                    return meth(this, args, kwargs);
            }
            throw unsupported(nameof(__call__));
        }

        public bool __contains__(TrObject a)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__contains;
                if (meth != null)
                    return meth(this, a);
            }
            throw unsupported(nameof(__contains__));
        }

        public bool __getitem__(TrObject item, out TrObject found)
        {
            var reference = MK.Ref();
            var res = __getitem__(item, reference);
            found = reference.value;
            return res;
        }
        public bool __getitem__(TrObject item, TrRef found)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__getitem;
                if (meth != null)
                    return meth(this, item, found);
            }
            throw unsupported(nameof(__getitem__));
        }

        public void __setitem__(TrObject item, TrObject value)
        {
            for (int i = 0; i < Class.__mro.Length; i++)
            {
                var meth = Class.__mro[i].__setitem;
                if (meth != null)
                {
                    meth(this, item, value);
                    return;
                }
            }
            throw unsupported(nameof(__setitem__));
        }

        public bool __getattr__(TrObject s, out TrObject found)
        {
            var reference = MK.Ref();
            var res = __getattr__(s, reference);
            found = reference.value;
            return res;
        }
        public bool __getattr__(TrObject s, TrRef found)
        {
            if (Class.__getattr != null)
                return Class.__getattr(this, s, found);

            TrObject getter;
            if (this.__dict__ != null)
            {
                if (RTS.baredict_get_noerror(__dict__, s, out found.value))
                {
                    return true;
                }
            }
            Dictionary<TrObject, TrObject> MAGIC_METHODS = this.Class.__dict__;
            if (MAGIC_METHODS.TryGetValue(s, out getter))
            {
                // TODO: check proper type
                found.value = Objects.TrSharpMethod.BindOrUnwrap(getter, this);
                return true;
            }
            if (this.Class.__base.Length != 0)
            {
                var mro = this.Class.__mro;
                for (int i = 0; i < mro.Length; i++)
                {
                    var get = mro[i].__dict__.TryGetValue(s, out getter);
                    if (get)
                    {
                        // TODO: check proper type
                        found.value = Objects.TrSharpMethod.BindOrUnwrap(getter, this);
                        return true;
                    }
                }
            }
            return false;
        }

        public void __setattr__(TrObject s, TrObject value)
        {
            if (Class.__setattr != null)
            {
                Class.__setattr(this, s, value);
                return;
            }

            if (this.__dict__ != null)
            {
                RTS.baredict_set(__dict__, s, value);
                return;
            }
            throw new AttributeError(this, s, $"cannot set attribute {s.__repr__()}.");
        }

        public IEnumerator<TrObject> __iter__()
        {
            if (Class.__iter != null)
                return Class.__iter(this);

            throw unsupported(nameof(__iter__));
        }

        public TrObject __len__()
        {
            if (Class.__len != null)
                return Class.__len(this);
            throw unsupported(nameof(__len__));
        }

        // Comparators
        public bool __eq__(TrObject o)
        {
            if (Class.__eq != null)
                return Class.__eq(this, o);
            return Object.ReferenceEquals(o.Native, this.Native);
        }

        public bool __lt__(TrObject o)
        {
            if (Class.__lt != null)
                return Class.__lt(this, o);
            throw unsupported(nameof(__lt__));
        }


        // Unary ops
        public TrObject __neg__()
        {
            if (Class.__neg != null)
                return Class.__neg(this);
            throw unsupported(nameof(__neg__));
        }

        public TrObject __inv__()
        {
            if (Class.__inv != null)
                return Class.__inv(this);
            throw unsupported(nameof(__inv__));
        }

        public TrObject __pos__()
        {
            if (Class.__pos != null)
                return Class.__pos(this);
            throw unsupported(nameof(__pos__));
        }

        public bool __bool__() =>
            (Class.__bool != null) ? Class.__bool(this) : true;
    }

    public class TrRawObject : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitMeta)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("object");
            CLASS.Name = "object";
            CLASS.__new = TrRawObject.datanew;
            CLASS.Fixed = true;
        }

        [InitSetup(InitOrder.InitClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

        static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.RawObject();
            throw new TypeError($"{clsobj.AsClass.Name}.__new__() takes 1 positional argument but {narg} were given");
        }
    }
}