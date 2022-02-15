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
        public string AsString => ((TrStr)this).value;
        public int AsInt => unchecked((int)((TrInt)this).value);
        public Dictionary<TrObject, TrObject> __dict__ { get; }
        public object Native => this;
        public TrClass Class { get; }
        public string __str__() => __repr__();
        public string __repr__() => Native.ToString();
        public TrObject __next__() => throw unsupported_op(this, "__next__");
        static Exception unsupported_op(TrObject a, string op) =>
            new TypeError($"{a.Class.Name} does not support '{op}'");

        // Arithmetic ops
        public TrObject __add__(TrObject a)
        {
            throw unsupported_op(this, "+");
        }
        public TrObject __sub__(TrObject a)
        {
            throw unsupported_op(this, "-");
        }

        public TrObject __mul__(TrObject a)
        {
            throw unsupported_op(this, "*");
        }

        public TrObject __matmul__(TrObject a)
        {
            throw unsupported_op(this, "@");
        }

        public TrObject __floordiv__(TrObject a)
        {
            throw unsupported_op(this, "//");
        }

        public TrObject __truediv__(TrObject a)
        {
            throw unsupported_op(this, "/");
        }

        public TrObject __mod__(TrObject a)
        {
            throw unsupported_op(this, "%");
        }

        public TrObject __pow__(TrObject a)
        {
            throw unsupported_op(this, "**");
        }

        // Bitwise logic operations

        public TrObject __bitand__(TrObject a)
        {
            throw unsupported_op(this, "&");
        }

        public TrObject __bitor__(TrObject a)
        {
            throw unsupported_op(this, "|");
        }

        public TrObject __bitxor__(TrObject a)
        {
            throw unsupported_op(this, "^");
        }

        // bit shift
        public TrObject __lshift__(TrObject a)
        {
            throw unsupported_op(this, "<<");
        }

        public TrObject __rshift__(TrObject a)
        {
            throw unsupported_op(this, ">>");
        }

        // Object protocol
        public int __hash__() => Native.GetHashCode();
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
            throw unsupported_op(this, "__call__");
        }

        public bool __contains__(TrObject a)
        {
            throw unsupported_op(this, "__contains__");
        }

        public TrObject __getitem__(TrObject item)
        {
            throw unsupported_op(this, "__getitem__");
        }

        public void __setitem__(TrObject item, TrObject value)
        {
            throw unsupported_op(this, "__getitem__");
        }

        public TrObject __getattr__(TrObject s)
        {
            if (this.__dict__ != null)
            {
                TrObject o = RTS.baredict_get_noerror(__dict__, s);
                if (o != null)
                    return o;
            }
            Dictionary<TrObject, TrObject> MAGIC_METHODS = this.Class.MAGIC_METHOD_GETTERS;
            if (MAGIC_METHODS.TryGetValue(s, out var getter))
            {
                return Objects.TrSharpMethod.BindOrUnwrap(getter, this);
            }
            if (this.Class.__base.Length != 0)
            {
                var bases = this.Class.__base;
                for (int i = 0; i < bases.Length; i++)
                {
                    var get = bases[i].__getattr__(s);
                    if (get != null)
                        return Objects.TrSharpMethod.BindOrUnwrap(get, this);
                }
            }
            return null;
        }

        public void __setattr__(TrObject s, TrObject value)
        {
            if (this.__dict__ != null)
            {
                RTS.baredict_set(__dict__, s, value);
                return;
            }
            throw new AttributeError($"cannot set attribute {s.__repr__()}.");
        }

        public IEnumerator<TrObject> __iter__()
        {
            throw unsupported_op(this, "__iter__");
        }

        public TrObject __len__()
        {
            throw unsupported_op(this, "__len__");
        }

        // Comparators
        public bool __eq__(TrObject o)
        {
            return Object.ReferenceEquals(o.Native, this.Native);
        }

        public bool __lt__(TrObject o)
        {
            throw unsupported_op(this, "<");
        }

        public bool __le__(TrObject o)
        {
            return __lt__(o) || __eq__(o);
        }

        // Unary ops
        public TrObject __neg__()
        {
            throw unsupported_op(this, "__neg__");
        }

        public TrObject __inv__()
        {
            throw unsupported_op(this, "__inv__");
        }

        public TrObject __pos__()
        {
            throw unsupported_op(this, "__pos__");
        }

        public bool __bool__() => true;
    }
}