using System.Collections.Generic;
using System;
    namespace Traffy.Objects
    {
    public partial interface TrUserObjectBase
    {
        TrObject TrObject.__init__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var self_init = this[Class.ic__init];
            if ((object)self_init == null)
                return TrObject.__init__(this, args, kwargs);
            return self_init.__init__(args, kwargs);
        }
        String TrObject.__str__()
        {
            var self_str = this[Class.ic__str];
            if ((object)self_str == null)
                return TrObject.__str__(this);
            return self_str.__str__();
        }
        String TrObject.__repr__()
        {
            var self_repr = this[Class.ic__repr];
            if ((object)self_repr == null)
                return TrObject.__repr__(this);
            return self_repr.__repr__();
        }
        TrObject TrObject.__next__()
        {
            var self_next = this[Class.ic__next];
            if ((object)self_next == null)
                return TrObject.__next__(this);
            return self_next.__next__();
        }
        TrObject TrObject.__add__(TrObject a)
        {
            var self_add = this[Class.ic__add];
            if ((object)self_add == null)
                return TrObject.__add__(this, a);
            return self_add.__add__(a);
        }
        TrObject TrObject.__sub__(TrObject a)
        {
            var self_sub = this[Class.ic__sub];
            if ((object)self_sub == null)
                return TrObject.__sub__(this, a);
            return self_sub.__sub__(a);
        }
        TrObject TrObject.__mul__(TrObject a)
        {
            var self_mul = this[Class.ic__mul];
            if ((object)self_mul == null)
                return TrObject.__mul__(this, a);
            return self_mul.__mul__(a);
        }
        TrObject TrObject.__matmul__(TrObject a)
        {
            var self_matmul = this[Class.ic__matmul];
            if ((object)self_matmul == null)
                return TrObject.__matmul__(this, a);
            return self_matmul.__matmul__(a);
        }
        TrObject TrObject.__floordiv__(TrObject a)
        {
            var self_floordiv = this[Class.ic__floordiv];
            if ((object)self_floordiv == null)
                return TrObject.__floordiv__(this, a);
            return self_floordiv.__floordiv__(a);
        }
        TrObject TrObject.__truediv__(TrObject a)
        {
            var self_truediv = this[Class.ic__truediv];
            if ((object)self_truediv == null)
                return TrObject.__truediv__(this, a);
            return self_truediv.__truediv__(a);
        }
        TrObject TrObject.__mod__(TrObject a)
        {
            var self_mod = this[Class.ic__mod];
            if ((object)self_mod == null)
                return TrObject.__mod__(this, a);
            return self_mod.__mod__(a);
        }
        TrObject TrObject.__pow__(TrObject a)
        {
            var self_pow = this[Class.ic__pow];
            if ((object)self_pow == null)
                return TrObject.__pow__(this, a);
            return self_pow.__pow__(a);
        }
        TrObject TrObject.__bitand__(TrObject a)
        {
            var self_bitand = this[Class.ic__bitand];
            if ((object)self_bitand == null)
                return TrObject.__bitand__(this, a);
            return self_bitand.__bitand__(a);
        }
        TrObject TrObject.__bitor__(TrObject a)
        {
            var self_bitor = this[Class.ic__bitor];
            if ((object)self_bitor == null)
                return TrObject.__bitor__(this, a);
            return self_bitor.__bitor__(a);
        }
        TrObject TrObject.__bitxor__(TrObject a)
        {
            var self_bitxor = this[Class.ic__bitxor];
            if ((object)self_bitxor == null)
                return TrObject.__bitxor__(this, a);
            return self_bitxor.__bitxor__(a);
        }
        TrObject TrObject.__lshift__(TrObject a)
        {
            var self_lshift = this[Class.ic__lshift];
            if ((object)self_lshift == null)
                return TrObject.__lshift__(this, a);
            return self_lshift.__lshift__(a);
        }
        TrObject TrObject.__rshift__(TrObject a)
        {
            var self_rshift = this[Class.ic__rshift];
            if ((object)self_rshift == null)
                return TrObject.__rshift__(this, a);
            return self_rshift.__rshift__(a);
        }
        Int32 TrObject.__hash__()
        {
            var self_hash = this[Class.ic__hash];
            if ((object)self_hash == null)
                return TrObject.__hash__(this);
            return self_hash.__hash__();
        }
        TrObject TrObject.__call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var self_call = this[Class.ic__call];
            if ((object)self_call == null)
                return TrObject.__call__(this, args, kwargs);
            return self_call.__call__(args, kwargs);
        }
        Boolean TrObject.__contains__(TrObject a)
        {
            var self_contains = this[Class.ic__contains];
            if ((object)self_contains == null)
                return TrObject.__contains__(this, a);
            return self_contains.__contains__(a);
        }
        Boolean TrObject.__getitem__(TrObject item, TrRef found)
        {
            var self_getitem = this[Class.ic__getitem];
            if ((object)self_getitem == null)
                return TrObject.__getitem__(this, item, found);
            return self_getitem.__getitem__(item, found);
        }
        void TrObject.__setitem__(TrObject key, TrObject value)
        {
            var self_setitem = this[Class.ic__setitem];
            if ((object)self_setitem == null)
            {
                TrObject.__setitem__(this, key, value);
                return;
            }
            self_setitem.__setitem__(key, value);
        }
        Boolean TrObject.__getattr__(TrObject name, TrRef found)
        {
            var self_getattr = this[Class.ic__getattr];
            if ((object)self_getattr == null)
                return TrObject.__getattr__(this, name, found);
            return self_getattr.__getattr__(name, found);
        }
        void TrObject.__setattr__(TrObject name, TrObject value)
        {
            var self_setattr = this[Class.ic__setattr];
            if ((object)self_setattr == null)
            {
                TrObject.__setattr__(this, name, value);
                return;
            }
            self_setattr.__setattr__(name, value);
        }
        IEnumerator<TrObject> TrObject.__iter__()
        {
            var self_iter = this[Class.ic__iter];
            if ((object)self_iter == null)
                return TrObject.__iter__(this);
            return self_iter.__iter__();
        }
        TrObject TrObject.__len__()
        {
            var self_len = this[Class.ic__len];
            if ((object)self_len == null)
                return TrObject.__len__(this);
            return self_len.__len__();
        }
        Boolean TrObject.__eq__(TrObject other)
        {
            var self_eq = this[Class.ic__eq];
            if ((object)self_eq == null)
                return TrObject.__eq__(this, other);
            return self_eq.__eq__(other);
        }
        Boolean TrObject.__ne__(TrObject other)
        {
            var self_ne = this[Class.ic__ne];
            if ((object)self_ne == null)
                return TrObject.__ne__(this, other);
            return self_ne.__ne__(other);
        }
        Boolean TrObject.__lt__(TrObject other)
        {
            var self_lt = this[Class.ic__lt];
            if ((object)self_lt == null)
                return TrObject.__lt__(this, other);
            return self_lt.__lt__(other);
        }
        Boolean TrObject.__le__(TrObject other)
        {
            var self_le = this[Class.ic__le];
            if ((object)self_le == null)
                return TrObject.__le__(this, other);
            return self_le.__le__(other);
        }
        Boolean TrObject.__gt__(TrObject other)
        {
            var self_gt = this[Class.ic__gt];
            if ((object)self_gt == null)
                return TrObject.__gt__(this, other);
            return self_gt.__gt__(other);
        }
        Boolean TrObject.__ge__(TrObject other)
        {
            var self_ge = this[Class.ic__ge];
            if ((object)self_ge == null)
                return TrObject.__ge__(this, other);
            return self_ge.__ge__(other);
        }
        TrObject TrObject.__neg__()
        {
            var self_neg = this[Class.ic__neg];
            if ((object)self_neg == null)
                return TrObject.__neg__(this);
            return self_neg.__neg__();
        }
        TrObject TrObject.__invert__()
        {
            var self_invert = this[Class.ic__invert];
            if ((object)self_invert == null)
                return TrObject.__invert__(this);
            return self_invert.__invert__();
        }
        TrObject TrObject.__pos__()
        {
            var self_pos = this[Class.ic__pos];
            if ((object)self_pos == null)
                return TrObject.__pos__(this);
            return self_pos.__pos__();
        }
        Boolean TrObject.__bool__()
        {
            var self_bool = this[Class.ic__bool];
            if ((object)self_bool == null)
                return TrObject.__bool__(this);
            return self_bool.__bool__();
        }
        TrObject TrObject.__abs__()
        {
            var self_abs = this[Class.ic__abs];
            if ((object)self_abs == null)
                return TrObject.__abs__(this);
            return self_abs.__abs__();
        }
        TrObject TrObject.__enter__()
        {
            var self_enter = this[Class.ic__enter];
            if ((object)self_enter == null)
                return TrObject.__enter__(this);
            return self_enter.__enter__();
        }
        TrObject TrObject.__exit__(TrObject exc_type, TrObject exc_value, TrObject traceback)
        {
            var self_exit = this[Class.ic__exit];
            if ((object)self_exit == null)
                return TrObject.__exit__(this, exc_type, exc_value, traceback);
            return self_exit.__exit__(exc_type, exc_value, traceback);
        }
    }
}