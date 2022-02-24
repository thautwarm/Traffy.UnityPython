using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public interface TrUserObjectBase : TrObject
    {
        object TrObject.Native => this;


        string TrObject.__str__()
        {
            var self_str = this[Class.ic__str];
            if (self_str != null)
            {
                var ret = self_str.Call();
                return ret.AsString();
            }
            return TrObject.__raw_str__(this);
        }
        string TrObject.__repr__()
        {
            var self_repr = this[Class.ic__repr];
            if (self_repr != null)
            {
                var ret = self_repr.Call();
                return ret.AsString();
            }
            return TrObject.__raw_repr__(this);
        }
        TrObject TrObject.__next__()
        {
            var self_next = this[Class.ic__next];
            if (self_next != null)
            {
                var ret = self_next.Call();
                return ret;
            }
            return TrObject.__raw_next__(this);
        }

        // Arithmetic ops
        TrObject TrObject.__add__(TrObject a)
        {
            var self_add = this[Class.ic__add];
            if (self_add != null)
            {
                var ret = self_add.Call(a);
                return ret;
            }
            return TrObject.__raw_add__(this, a);
        }
        TrObject TrObject.__sub__(TrObject a)
        {
            var self_sub = this[Class.ic__sub];
            if (self_sub != null)
            {
                var ret = self_sub.Call(a);
                return ret;
            }
            return TrObject.__raw_sub__(this, a);
        }

        TrObject TrObject.__mul__(TrObject a)
        {
            var self_mul = this[Class.ic__mul];
            if (self_mul != null)
            {
                var ret = self_mul.Call(a);
                return ret;
            }
            return TrObject.__raw_mul__(this, a);
        }

        TrObject TrObject.__matmul__(TrObject a)
        {
            var self_matmul = this[Class.ic__matmul];
            if (self_matmul != null)
            {
                var ret = self_matmul.Call(a);
                return ret;
            }
            return TrObject.__raw_matmul__(this, a);
        }

        TrObject TrObject.__floordiv__(TrObject a)
        {
            var self_floordiv = this[Class.ic__floordiv];
            if (self_floordiv != null)
            {
                var ret = self_floordiv.Call(a);
                return ret;
            }
            return TrObject.__raw_floordiv__(this, a);
        }

        TrObject TrObject.__truediv__(TrObject a)
        {
            var self_truediv = this[Class.ic__truediv];
            if (self_truediv != null)
            {
                var ret = self_truediv.Call(a);
                return ret;
            }
            return TrObject.__raw_truediv__(this, a);
        }

        TrObject TrObject.__mod__(TrObject a)
        {
            var self_mod = this[Class.ic__mod];
            if (self_mod != null)
            {
                var ret = self_mod.Call(a);
                return ret;
            }
            return TrObject.__raw_mod__(this, a);
        }

        TrObject TrObject.__pow__(TrObject a)
        {
            var self_pow = this[Class.ic__pow];
            if (self_pow != null)
            {
                var ret = self_pow.Call(a);
                return ret;
            }
            return TrObject.__raw_pow__(this, a);
        }

        // Bitwise logic operations

        TrObject TrObject.__bitand__(TrObject a)
        {
            var self_bitand = this[Class.ic__bitand];
            if (self_bitand != null)
            {
                var ret = self_bitand.Call(a);
                return ret;
            }
            return TrObject.__raw_bitand__(this, a);
        }

        TrObject TrObject.__bitor__(TrObject a)
        {
            var self_bitor = this[Class.ic__bitor];
            if (self_bitor != null)
            {
                var ret = self_bitor.Call(a);
                return ret;
            }
            return TrObject.__raw_bitor__(this, a);
        }

        TrObject TrObject.__bitxor__(TrObject a)
        {
            var self_bitxor = this[Class.ic__bitxor];
            if (self_bitxor != null)
            {
                var ret = self_bitxor.Call(a);
                return ret;
            }
            return TrObject.__raw_bitxor__(this, a);
        }

        // bit shift
        TrObject TrObject.__lshift__(TrObject a)
        {
            var self_lshift = this[Class.ic__lshift];
            if (self_lshift != null)
            {
                var ret = self_lshift.Call(a);
                return ret;
            }
            return TrObject.__raw_lshift__(this, a);
        }

        TrObject TrObject.__rshift__(TrObject a)
        {
            var self_rshift = this[Class.ic__rshift];
            if (self_rshift != null)
            {
                var ret = self_rshift.Call(a);
                return ret;
            }
            return TrObject.__raw_rshift__(this, a);
        }

        // Object protocol
        int TrObject.__hash__()
        {
            var self_hash = this[Class.ic__hash];
            if (self_hash != null)
            {
                var ret = self_hash.Call();
                return ret.AsInt();
            }
            return TrObject.__raw_hash__(this);
        }

        TrObject TrObject.__call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var self_call = this[Class.ic__call];
            if (self_call != null)
            {
                args.AddLeft(this);
                var ret = self_call.__call__(args, kwargs);
                args.PopLeft();
                return ret;
            }
            return TrObject.__raw_call__(this, args, kwargs);
        }

        bool TrObject.__contains__(TrObject a)
        {
            var self_contains = this[Class.ic__contains];
            if (self_contains != null)
            {
                var ret = self_contains.Call(a);
                return ret.AsBool();
            }
            return TrObject.__raw_contains__(this, a);
        }


        bool TrObject.__getitem__(TrObject item, TrRef found)
        {
            var self_getitem = this[Class.ic__getitem];
            if (self_getitem != null)
            {
                return self_getitem.Call(item, found).AsBool();
            }
            return TrObject.__raw_getitem__(this, item, found);
        }

        void TrObject.__setitem__(TrObject key, TrObject value)
        {
            var self_setitem = this[Class.ic__setitem];
            if (self_setitem != null)
            {
                self_setitem.Call(key, value);
                return;
            }
            TrObject.__raw_setitem__(this, key, value);
        }
        bool TrObject.__getattr__(TrObject s, TrRef found)
        {
            var self_getattr = this[Class.ic__getattr];
            if (self_getattr != null)
            {
                return self_getattr.Call(s, found).AsBool();
            }
            return TrObject.__raw_getattr__(this, s, found);
        }

        void TrObject.__setattr__(TrObject s, TrObject value)
        {
            var self_setattr = this[Class.ic__setattr];
            if (self_setattr != null)
            {
                self_setattr.Call(s, value);
                return;
            }
            TrObject.__raw_setattr__(this, s, value);
        }

        IEnumerator<TrObject> TrObject.__iter__()
        {
            var self_iter = this[Class.ic__iter];
            if (self_iter != null)
            {
                return self_iter.Call().__iter__();
            }
            return TrObject.__raw_iter__(this);
        }

        TrObject TrObject.__len__()
        {
            var self_len = this[Class.ic__len];
            if (self_len != null)
            {
                return self_len.Call();
            }
            return TrObject.__raw_len__(this);
        }

        // Comparators
        bool TrObject.__eq__(TrObject o)
        {
            var self_eq = this[Class.ic__eq];
            if (self_eq != null)
            {
                return self_eq.Call(o).AsBool();
            }
            return TrObject.__raw_eq__(this, o);
        }

        bool TrObject.__lt__(TrObject o)
        {
            var self_lt = this[Class.ic__lt];
            if (self_lt != null)
            {
                return self_lt.Call(o).AsBool();
            }
            return TrObject.__raw_lt__(this, o);
        }


        // Unary ops
        TrObject TrObject.__neg__()
        {
            var self_neg = this[Class.ic__neg];
            if (self_neg != null)
            {
                return self_neg.Call();
            }
            return TrObject.__raw_neg__(this);
        }

        TrObject TrObject.__invert__()
        {
            var self_inv = this[Class.ic__inv];
            if (self_inv != null)
            {
                return self_inv.Call();
            }
            return TrObject.__raw_invert__(this);
        }

        TrObject TrObject.__pos__()
        {
            var self_pos = this[Class.ic__pos];
            if (self_pos != null)
            {
                return self_pos.Call();
            }
            return TrObject.__raw_pos__(this);
        }

        bool TrObject.__bool__()
        {
            var self_bool = this[Class.ic__bool];
            if (self_bool != null)
            {
                return self_bool.Call().AsBool();
            }
            return TrObject.__raw_bool__(this);
        }
    }
}
