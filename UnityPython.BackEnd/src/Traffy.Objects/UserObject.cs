using System;
using System.Collections.Generic;
namespace Traffy.Objects
{
    public abstract class TrUserObjectBase : TrObject
    {
        public override TrObject __init__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (__getic__(Class.ic__init, out var self_init))
            {
                return self_init.__call__(args, kwargs);
            }
            return TrObject.__init__(this, args, kwargs);
        }
        public override string __str__()
        {
            if (__getic__(Class.ic__str, out var self_str))
            {
                return self_str.Call().AsStr();
            }
            return TrObject.__str__(this);
        }
        public override String __repr__()
        {
            if (__getic__(Class.ic__repr, out var self_repr))
            {
                return self_repr.Call().AsStr();
            }
            return TrObject.__repr__(this);
        }
        public override Boolean __trynext__(TrRef refval)
        {
            if (__getic__(Class.ic__trynext, out var self_next))
            {
                return self_next.Call(refval).AsBool();
            }
            return TrObject.__trynext__(this, refval);
        }

        public override TrObject __add__(TrObject a)
        {
            if (__getic__(Class.ic__add, out var self_add))
            {
                return self_add.Call(a);
            }
            return TrObject.__add__(this, a);
        }

        public override TrObject __radd__(TrObject a)
        {
            if (__getic__(Class.ic__radd, out var self_radd))
            {
                return self_radd.Call(a);
            }
            return TrObject.__radd__(this, a);
        }
        public override TrObject __sub__(TrObject a)
        {
            if (__getic__(Class.ic__sub, out var self_sub))
            {
                return self_sub.Call(a);
            }
            return TrObject.__sub__(this, a);
        }
        public override TrObject __rsub__(TrObject a)
        {
            if (__getic__(Class.ic__rsub, out var self_rsub))
            {
                return self_rsub.Call(a);
            }
            return TrObject.__rsub__(this, a);
        }
        public override TrObject __mul__(TrObject a)
        {
            if (__getic__(Class.ic__mul, out var self_mul))
            {
                return self_mul.Call(a);
            }
            return TrObject.__mul__(this, a);
        }
        public override TrObject __rmul__(TrObject a)
        {
            if (__getic__(Class.ic__rmul, out var self_rmul))
            {
                return self_rmul.Call(a);
            }
            return TrObject.__rmul__(this, a);
        }
        public override TrObject __matmul__(TrObject a)
        {
            if (__getic__(Class.ic__matmul, out var self_matmul))
            {
                return self_matmul.Call(a);
            }
            return TrObject.__matmul__(this, a);
        }
        public override TrObject __rmatmul__(TrObject a)
        {
            if (__getic__(Class.ic__rmatmul, out var self_rmatmul))
            {
                return self_rmatmul.Call(a);
            }
            return TrObject.__rmatmul__(this, a);
        }
        public override TrObject __floordiv__(TrObject a)
        {
            if (__getic__(Class.ic__floordiv, out var self_floordiv))
            {
                return self_floordiv.Call(a);
            }
            return TrObject.__floordiv__(this, a);
        }

        public override TrObject __rfloordiv__(TrObject a)
        {
            if (__getic__(Class.ic__rfloordiv, out var self_rfloordiv))
            {
                return self_rfloordiv.Call(a);
            }
            return TrObject.__rfloordiv__(this, a);
        }
        public override TrObject __truediv__(TrObject a)
        {
            if (__getic__(Class.ic__truediv, out var self_truediv))
            {
                return self_truediv.Call(a);
            }
            return TrObject.__truediv__(this, a);
        }
        public override TrObject __rtruediv__(TrObject a)
        {
            if (__getic__(Class.ic__rtruediv, out var self_rtruediv))
            {
                return self_rtruediv.Call(a);
            }
            return TrObject.__rtruediv__(this, a);
        }
        public override TrObject __mod__(TrObject a)
        {
            if (__getic__(Class.ic__mod, out var self_mod))
            {
                return self_mod.Call(a);
            }
            return TrObject.__mod__(this, a);
        }
        public override TrObject __rmod__(TrObject a)
        {
            if (__getic__(Class.ic__rmod, out var self_rmod))
            {
                return self_rmod.Call(a);
            }
            return TrObject.__rmod__(this, a);
        }
        public override TrObject __pow__(TrObject a)
        {
            if (__getic__(Class.ic__pow, out var self_pow))
            {
                return self_pow.Call(a);
            }
            return TrObject.__pow__(this, a);
        }
        public override TrObject __rpow__(TrObject a)
        {
            if (__getic__(Class.ic__rpow, out var self_rpow))
            {
                return self_rpow.Call(a);
            }
            return TrObject.__rpow__(this, a);
        }
        public override TrObject __and__(TrObject a)
        {
            if (__getic__(Class.ic__and, out var self_and))
            {
                return self_and.Call(a);
            }
            return TrObject.__and__(this, a);
        }
        public override TrObject __rand__(TrObject a)
        {
            if (__getic__(Class.ic__rand, out var self_rand))
            {
                return self_rand.Call(a);
            }
            return TrObject.__rand__(this, a);
        }
        public override TrObject __or__(TrObject a)
        {
            if (__getic__(Class.ic__or, out var self_or))
            {
                return self_or.Call(a);
            }
            return TrObject.__or__(this, a);
        }
        public override TrObject __ror__(TrObject a)
        {
            if (__getic__(Class.ic__ror, out var self_ror))
            {
                return self_ror.Call(a);
            }
            return TrObject.__ror__(this, a);
        }
        public override TrObject __xor__(TrObject a)
        {
            if (__getic__(Class.ic__xor, out var self_xor))
            {
                return self_xor.Call(a);
            }
            return TrObject.__xor__(this, a);
        }
        public override TrObject __rxor__(TrObject a)
        {
            if (__getic__(Class.ic__rxor, out var self_rxor))
            {
                return self_rxor.Call(a);
            }
            return TrObject.__rxor__(this, a);
        }
        public override TrObject __lshift__(TrObject a)
        {
            if (__getic__(Class.ic__lshift, out var self_lshift))
            {
                return self_lshift.Call(a);
            }
            return TrObject.__lshift__(this, a);
        }
        public override TrObject __rlshift__(TrObject a)
        {
            if (__getic__(Class.ic__rlshift, out var self_rlshift))
            {
                return self_rlshift.Call(a);
            }
            return TrObject.__rlshift__(this, a);
        }
        public override TrObject __rshift__(TrObject a)
        {
            if (__getic__(Class.ic__rshift, out var self_rshift))
            {
                return self_rshift.Call(a);
            }
            return TrObject.__rshift__(this, a);
        }
        public override TrObject __rrshift__(TrObject a)
        {
            if (__getic__(Class.ic__rrshift, out var self_rrshift))
            {
                return self_rrshift.Call(a);
            }
            return TrObject.__rrshift__(this, a);
        }
        public override Int32 __hash__()
        {
            if (__getic__(Class.ic__hash, out var self_hash))
            {
                return self_hash.Call().AsInt();
            }
            return TrObject.__hash__(this);
        }
        public override TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (__getic__(Class.ic__call, out var self_call))
            {
                return self_call.__call__(args, kwargs);
            }
            return TrObject.__call__(this, args, kwargs);
        }
        public override bool __contains__(TrObject a)
        {
            if (__getic__(Class.ic__contains, out var self_contains))
            {
                return self_contains.Call(a).AsBool();
            }
            return TrObject.__contains__(this, a);
        }

        public override TrObject __round__(TrObject ndigits)
        {
            if (__getic__(Class.ic__round, out var self_round))
            {
                return self_round.Call(ndigits);
            }
            return TrObject.__round__(this, ndigits);
        }

        public override TrObject __reversed__()
        {
            if (__getic__(Class.ic__reversed, out var self_reversed))
            {
                return self_reversed.Call();
            }
            return TrObject.__reversed__(this);
        }
        public override TrObject __getitem__(TrObject item)
        {
            if (__getic__(Class.ic__getitem, out var self_getitem))
            {
                return self_getitem.Call(item);
            }
            return TrObject.__getitem__(this, item);
        }

        public override void __setitem__(TrObject key, TrObject value)
        {
            if (__getic__(Class.ic__setitem, out var self_setitem))
            {
                self_setitem.Call(key, value);
                return;
            }
            TrObject.__setitem__(this, key, value);
        }

        public override void __delitem__(TrObject key)
        {
            if (__getic__(Class.ic__delitem, out var self_delitem))
            {
                self_delitem.Call(key);
                return;
            }
            TrObject.__delitem__(this, key);
        }

        public override bool __finditem__(TrObject key, TrRef refval)
        {
            if (__getic__(Class.ic__finditem, out var self_finditem))
            {
                return self_finditem.Call(key, refval).AsBool();
            }
            return TrObject.__finditem__(this, key, refval);
        }

        public override IEnumerator<TrObject> __iter__()
        {
            if (__getic__(Class.ic__iter, out var self_iter))
            {
                return self_iter.Call().__iter__();
            }
            return TrObject.__iter__(this);
        }

        public override Awaitable<TrObject> __await__()
        {
            if (__getic__(Class.ic__await, out var self_await))
            {
                return RTS.object_yield_from(self_await.Call());
            }
            return TrObject.__await__(this);   
        }
        public override TrObject __len__()
        {
            if (__getic__(Class.ic__len, out var self_len))
            {
                return self_len.Call();
            }
            return TrObject.__len__(this);
        }
        public override bool __eq__(TrObject other)
        {
            if (__getic__(Class.ic__eq, out var self_eq))
            {
                return self_eq.Call(other).AsBool();
            }
            return TrObject.__eq__(this, other);
        }
        public override bool __ne__(TrObject other)
        {
            if (__getic__(Class.ic__ne, out var self_ne))
            {
                return self_ne.Call(other).AsBool();
            }
            return TrObject.__ne__(this, other);
        }
        public override bool __lt__(TrObject other)
        {
            if (__getic__(Class.ic__lt, out var self_lt))
            {
                return self_lt.Call(other).AsBool();
            }
            return TrObject.__lt__(this, other);
        }
        public override bool __le__(TrObject other)
        {
            if (__getic__(Class.ic__le, out var self_le))
            {
                return self_le.Call(other).AsBool();
            }
            return TrObject.__le__(this, other);
        }
        public override bool __gt__(TrObject other)
        {
            if (__getic__(Class.ic__gt, out var self_gt))
            {
                return self_gt.Call(other).AsBool();
            }
            return TrObject.__gt__(this, other);
        }
        public override Boolean __ge__(TrObject other)
        {
            if (__getic__(Class.ic__ge, out var self_ge))
            {
                return self_ge.Call(other).AsBool();
            }
            return TrObject.__ge__(this, other);
        }
        public override TrObject __neg__()
        {
            if (__getic__(Class.ic__neg, out var self_neg))
            {
                return self_neg.Call();
            }
            return TrObject.__neg__(this);
        }
        public override TrObject __invert__()
        {
            if (__getic__(Class.ic__invert, out var self_invert))
            {
                return self_invert.Call();
            }
            return TrObject.__invert__(this);
        }
        public override TrObject __pos__()
        {
            if (__getic__(Class.ic__pos, out var self_pos))
            {
                return self_pos.Call();
            }
            return TrObject.__pos__(this);
        }
        public override Boolean __bool__()
        {
            if (__getic__(Class.ic__bool, out var self_bool))
            {
                return self_bool.Call().AsBool();
            }
            return TrObject.__bool__(this);
        }
        public override TrObject __abs__()
        {
            if (__getic__(Class.ic__abs, out var self_abs))
            {
                return self_abs.Call();
            }
            return TrObject.__abs__(this);
        }
        public override TrObject __enter__()
        {
            if (__getic__(Class.ic__enter, out var self_enter))
            {
                return self_enter.Call();
            }
            return TrObject.__enter__(this);
        }
        public override TrObject __exit__(TrObject exc_type, TrObject exc_value, TrObject traceback)
        {
            if (__getic__(Class.ic__exit, out var self_exit))
            {
                return self_exit.Call(exc_type, exc_value, traceback);
            }
            return TrObject.__exit__(this, exc_type, exc_value, traceback);
        }

        public override TrObject __int__()
        {
            if (__getic__(Class.ic__int, out var self_int))
            {
                return self_int.Call();
            }
            return TrObject.__int__(this);
        }

        public override TrObject __float__()
        {
            if (__getic__(Class.ic__float, out var self_float))
            {
                return self_float.Call();
            }
            return TrObject.__float__(this);
        }
    }
}
