using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public interface TrUserObjectBase : TrObject
    {
        object TrObject.Native => this;
        string TrObject.__str__() =>
            (Class.__str != null) ? Class.__str(this) : TrObject.__raw_str__(this);
        string TrObject.__repr__() =>
            (Class.__repr != null) ? Class.__repr(this) : TrObject.__raw_repr__(this);
        TrObject TrObject.__next__() =>
            (Class.__next != null) ? Class.__next(this) : TrObject.__raw_next__(this);

        // Arithmetic ops
        TrObject TrObject.__add__(TrObject a)
        {
            if (Class.__add != null)
                return Class.__add(this, a);
            return TrObject.__raw_add__(this, a);
        }
        TrObject TrObject.__sub__(TrObject a)
        {
            if (Class.__sub != null)
                return Class.__sub(this, a);
            return TrObject.__raw_sub__(this, a);
        }

        TrObject TrObject.__mul__(TrObject a)
        {
            if (Class.__mul != null)
                return Class.__mul(this, a);
            return TrObject.__raw_mul__(this, a);
        }

        TrObject TrObject.__matmul__(TrObject a)
        {
            if (Class.__matmul != null)
                return Class.__matmul(this, a);
            return TrObject.__raw_matmul__(this, a);
        }

        TrObject TrObject.__floordiv__(TrObject a)
        {
            if (Class.__floordiv != null)
                return Class.__floordiv(this, a);
            return TrObject.__raw_floordiv__(this, a);
        }

        TrObject TrObject.__truediv__(TrObject a)
        {
            if (Class.__truediv != null)
                return Class.__truediv(this, a);
            return TrObject.__raw_truediv__(this, a);
        }

        TrObject TrObject.__mod__(TrObject a)
        {
            if (Class.__mod != null)
                return Class.__mod(this, a);
            return TrObject.__raw_mod__(this, a);
        }

        TrObject TrObject.__pow__(TrObject a)
        {
            if (Class.__pow != null)
                return Class.__pow(this, a);
            return TrObject.__raw_pow__(this, a);
        }

        // Bitwise logic operations

        TrObject TrObject.__bitand__(TrObject a)
        {
            if (Class.__bitand != null)
                return Class.__bitand(this, a);
            return TrObject.__raw_bitand__(this, a);
        }

        TrObject TrObject.__bitor__(TrObject a)
        {
            if (Class.__bitor != null)
                return Class.__bitor(this, a);
            return TrObject.__raw_bitor__(this, a);
        }

        TrObject TrObject.__bitxor__(TrObject a)
        {
            if (Class.__bitxor != null)
                return Class.__bitxor(this, a);
            return TrObject.__raw_bitxor__(this, a);
        }

        // bit shift
        TrObject TrObject.__lshift__(TrObject a)
        {
            if (Class.__lshift != null)
                return Class.__lshift(this, a);
            return TrObject.__raw_lshift__(this, a);
        }

        TrObject TrObject.__rshift__(TrObject a)
        {
            if (Class.__rshift != null)
                return Class.__rshift(this, a);
            return TrObject.__raw_rshift__(this, a);
        }

        // Object protocol
        int TrObject.__hash__()
        {
            if (Class.__hash != null)
                return Class.__hash(this);
            return TrObject.__raw_hash__(this);
        }

        TrObject TrObject.__call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (Class.__call != null)
                return Class.__call(this, args, kwargs);
            return TrObject.__raw_call__(this, args, kwargs);
        }

        bool TrObject.__contains__(TrObject a)
        {
            if (Class.__contains != null)
                return Class.__contains(this, a);
            return TrObject.__raw_contains__(this, a);
        }


        bool TrObject.__getitem__(TrObject item, TrRef found)
        {
            if (Class.__getitem != null)
                return Class.__getitem(this, item, found);
            return TrObject.__raw_getitem__(this, item, found);
        }

        void TrObject.__setitem__(TrObject key, TrObject value)
        {
            if (Class.__setitem != null)
                Class.__setitem(this, key, value);
            else
                TrObject.__raw_setitem__(this, key, value);
        }
        bool TrObject.__getattr__(TrObject s, TrRef found)
        {
            if (Class.__getattr != null)
                return Class.__getattr(this, s, found);

            return TrObject.__raw_getattr__(this, s, found);
        }

        void TrObject.__setattr__(TrObject s, TrObject value)
        {
            if (Class.__setattr != null)
            {
                Class.__setattr(this, s, value);
                return;
            }
            TrObject.__raw_setattr__(this, s, value);
        }

        IEnumerator<TrObject> TrObject.__iter__()
        {
            if (Class.__iter != null)
                return Class.__iter(this);

            return TrObject.__raw_iter__(this);
        }

        TrObject TrObject.__len__()
        {
            if (Class.__len != null)
                return Class.__len(this);
            return TrObject.__raw_len__(this);
        }

        // Comparators
        bool TrObject.__eq__(TrObject o)
        {
            if (Class.__eq != null)
                return Class.__eq(this, o);
            return TrObject.__raw_eq__(this, o);
        }

        bool TrObject.__lt__(TrObject o)
        {
            if (Class.__lt != null)
                return Class.__lt(this, o);
            return TrObject.__raw_lt__(this, o);
        }


        // Unary ops
        TrObject TrObject.__neg__()
        {
            if (Class.__neg != null)
                return Class.__neg(this);
            return TrObject.__raw_neg__(this);
        }

        TrObject TrObject.__inv__()
        {
            if (Class.__inv != null)
                return Class.__inv(this);
            return TrObject.__raw_inv__(this);
        }

        TrObject TrObject.__pos__()
        {
            if (Class.__pos != null)
                return Class.__pos(this);
            return TrObject.__raw_pos__(this);
        }

        bool TrObject.__bool__() =>
            (Class.__bool != null) ? Class.__bool(this) : TrObject.__raw_bool__(this);
    }
}
