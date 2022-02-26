using System.Collections.Generic;
using System;
namespace Traffy.Objects
{
    public partial interface TrUserObjectBase: TrObject
    {
        TrObject TrObject.__init__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var self_init = this[Class.ic__init];
            return self_init.__call__(args, kwargs);
            // if ((object)self_init == null)
            //     return TrObject.__init__(this, args, kwargs);
            // return self_init.__call__(args, kwargs);
        }

        String TrObject.__str__()
        {
            var self_str = this[Class.ic__str];
            return self_str.Call().AsStr();
            // if ((object)self_str == null)
            //     return TrObject.__str__(this);
        }
        String TrObject.__repr__()
        {
            var self_repr = this[Class.ic__repr];
            return self_repr.Call().AsStr();
            // if ((object)self_repr == null)
            //     return TrObject.__repr__(this);
        }
        TrObject TrObject.__next__()
        {
            var self_next = this[Class.ic__next];
            return self_next.Call();
            // if ((object)self_next == null)
            //     return TrObject.__next__(this);
        }
        TrObject TrObject.__add__(TrObject a)
        {
            var self_add = this[Class.ic__add];
            return self_add.Call(a);
            // if ((object)self_add == null)
            //     return TrObject.__add__(this, a);
        }
        TrObject TrObject.__sub__(TrObject a)
        {
            var self_sub = this[Class.ic__sub];
            return self_sub.Call(a);
            // if ((object)self_sub == null)
            //     return TrObject.__sub__(this, a);
        }
        TrObject TrObject.__mul__(TrObject a)
        {
            var self_mul = this[Class.ic__mul];
            return self_mul.Call(a);
            // if ((object)self_mul == null)
            //     return TrObject.__mul__(this, a);
        }
        TrObject TrObject.__matmul__(TrObject a)
        {
            var self_matmul = this[Class.ic__matmul];
            return self_matmul.Call(a);
            // if ((object)self_matmul == null)
            //     return TrObject.__matmul__(this, a);
        }
        TrObject TrObject.__floordiv__(TrObject a)
        {
            var self_floordiv = this[Class.ic__floordiv];
            return self_floordiv.Call(a);
            // if ((object)self_floordiv == null)
            //     return TrObject.__floordiv__(this, a);
        }
        TrObject TrObject.__truediv__(TrObject a)
        {
            var self_truediv = this[Class.ic__truediv];
            return self_truediv.Call(a);
            // if ((object)self_truediv == null)
            //     return TrObject.__truediv__(this, a);
        }
        TrObject TrObject.__mod__(TrObject a)
        {
            var self_mod = this[Class.ic__mod];
            return self_mod.Call(a);
            // if ((object)self_mod == null)
            //     return TrObject.__mod__(this, a);
        }
        TrObject TrObject.__pow__(TrObject a)
        {
            var self_pow = this[Class.ic__pow];
            return self_pow.Call(a);
            // if ((object)self_pow == null)
            //     return TrObject.__pow__(this, a);
        }
        TrObject TrObject.__bitand__(TrObject a)
        {
            var self_bitand = this[Class.ic__bitand];
            return self_bitand.Call(a);
            // if ((object)self_bitand == null)
            //     return TrObject.__bitand__(this, a);
        }
        TrObject TrObject.__bitor__(TrObject a)
        {
            var self_bitor = this[Class.ic__bitor];
            return self_bitor.Call(a);
            // if ((object)self_bitor == null)
            //     return TrObject.__bitor__(this, a);
        }
        TrObject TrObject.__bitxor__(TrObject a)
        {
            var self_bitxor = this[Class.ic__bitxor];
            return self_bitxor.Call(a);
            // if ((object)self_bitxor == null)
            //     return TrObject.__bitxor__(this, a);
        }
        TrObject TrObject.__lshift__(TrObject a)
        {
            var self_lshift = this[Class.ic__lshift];
            return self_lshift.Call(a);
            // if ((object)self_lshift == null)
            //     return TrObject.__lshift__(this, a);
        }
        TrObject TrObject.__rshift__(TrObject a)
        {
            var self_rshift = this[Class.ic__rshift];
            return self_rshift.Call(a);
            // if ((object)self_rshift == null)
            //     return TrObject.__rshift__(this, a);
        }
        Int32 TrObject.__hash__()
        {
            var self_hash = this[Class.ic__hash];
            return self_hash.Call().AsInt();
            // if ((object)self_hash == null)
            //     return TrObject.__hash__(this);
        }
        TrObject TrObject.__call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var self_call = this[Class.ic__call];
            return self_call.__call__(args, kwargs);
            // if ((object)self_call == null)
            //     return TrObject.__call__(this, args, kwargs);
        }
        Boolean TrObject.__contains__(TrObject a)
        {
            var self_contains = this[Class.ic__contains];
            return self_contains.Call(a).AsBool();
            // if ((object)self_contains == null)
            //     return TrObject.__contains__(this, a);
        }
        Boolean TrObject.__getitem__(TrObject item, TrRef found)
        {
            var self_getitem = this[Class.ic__getitem];
            return self_getitem.Call(item, found).AsBool();
            // if ((object)self_getitem == null)
            //     return TrObject.__getitem__(this, item, found);
        }
        void TrObject.__setitem__(TrObject key, TrObject value)
        {
            var self_setitem = this[Class.ic__setitem];
            self_setitem.Call(key, value);
            // if ((object)self_setitem == null)
            // {
            //     TrObject.__setitem__(this, key, value);
            //     return;
            // }
        }
        Boolean TrObject.__getattr__(TrObject name, TrRef found)
        {
            var self_getattr = this[Class.ic__getattr];
            return self_getattr.Call(name, found).AsBool();
            // if ((object)self_getattr == null)
            //     return TrObject.__getattr__(this, name, found);
        }
        void TrObject.__setattr__(TrObject name, TrObject value)
        {
            var self_setattr = this[Class.ic__setattr];
            self_setattr.Call(name, value);
            // if ((object)self_setattr == null)
            // {
            //     TrObject.__setattr__(this, name, value);
            //     return;
            // }
        }
        IEnumerator<TrObject> TrObject.__iter__()
        {
            var self_iter = this[Class.ic__iter];
            return self_iter.Call().__iter__();
            // if ((object)self_iter == null)
            //     return TrObject.__iter__(this);
        }
        TrObject TrObject.__len__()
        {
            var self_len = this[Class.ic__len];
            return self_len.Call();
            // if ((object)self_len == null)
            //     return TrObject.__len__(this);
        }
        Boolean TrObject.__eq__(TrObject other)
        {
            var self_eq = this[Class.ic__eq];
            return self_eq.Call(other).AsBool();
            // if ((object)self_eq == null)
            //     return TrObject.__eq__(this, other);
        }
        Boolean TrObject.__ne__(TrObject other)
        {
            var self_ne = this[Class.ic__ne];
            return self_ne.Call(other).AsBool();
            // if ((object)self_ne == null)
            //     return TrObject.__ne__(this, other);
        }
        Boolean TrObject.__lt__(TrObject other)
        {
            var self_lt = this[Class.ic__lt];
            return self_lt.Call(other).AsBool();
            // if ((object)self_lt == null)
            //     return TrObject.__lt__(this, other);
        }
        Boolean TrObject.__le__(TrObject other)
        {
            var self_le = this[Class.ic__le];
            return self_le.Call(other).AsBool();
            // if ((object)self_le == null)
            //     return TrObject.__le__(this, other);
        }
        Boolean TrObject.__gt__(TrObject other)
        {
            var self_gt = this[Class.ic__gt];
            return self_gt.Call(other).AsBool();
            // if ((object)self_gt == null)
            //     return TrObject.__gt__(this, other);
        }
        Boolean TrObject.__ge__(TrObject other)
        {
            var self_ge = this[Class.ic__ge];
            return self_ge.Call(other).AsBool();
            // if ((object)self_ge == null)
            //     return TrObject.__ge__(this, other);
        }
        TrObject TrObject.__neg__()
        {
            var self_neg = this[Class.ic__neg];
            return self_neg.Call();
            // if ((object)self_neg == null)
            //     return TrObject.__neg__(this);
        }
        TrObject TrObject.__invert__()
        {
            var self_invert = this[Class.ic__invert];
            return self_invert.Call();
            // if ((object)self_invert == null)
            //     return TrObject.__invert__(this);
        }
        TrObject TrObject.__pos__()
        {
            var self_pos = this[Class.ic__pos];
            return self_pos.Call();
            // if ((object)self_pos == null)
            //     return TrObject.__pos__(this);
        }
        Boolean TrObject.__bool__()
        {
            var self_bool = this[Class.ic__bool];
            return self_bool.Call().AsBool();
            // if ((object)self_bool == null)
            //     return TrObject.__bool__(this);
        }
        TrObject TrObject.__abs__()
        {
            var self_abs = this[Class.ic__abs];
            return self_abs.Call();
            // if ((object)self_abs == null)
            //     return TrObject.__abs__(this);
        }
        TrObject TrObject.__enter__()
        {
            var self_enter = this[Class.ic__enter];
            return self_enter.Call();
            // if ((object)self_enter == null)
            //     return TrObject.__enter__(this);
        }
        TrObject TrObject.__exit__(TrObject exc_type, TrObject exc_value, TrObject traceback)
        {
            var self_exit = this[Class.ic__exit];
            return self_exit.Call(exc_type, exc_value, traceback);
            // if ((object)self_exit == null)
            //     return TrObject.__exit__(this, exc_type, exc_value, traceback);
        }
    }
}
