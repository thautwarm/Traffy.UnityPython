using System.Collections.Generic;
using Traffy;
namespace Traffy.Objects
{
    public partial class TrClass
    {
        void BindMethodsFromDict(Dictionary<TrObject, TrObject> cp_kwargs)
        {
            if (!this.IsSet(MagicNames.i___new__) && cp_kwargs.TryPop(MagicNames.s_new, out var o_new))
                this[MagicNames.i___new__] = o_new;
            if (!this.IsSet(MagicNames.i___init_subclass__) && cp_kwargs.TryPop(MagicNames.s_init_subclass, out var o_init_subclass))
                this[MagicNames.i___init_subclass__] = o_init_subclass;
            if (!this.IsSet(MagicNames.i___init__) && cp_kwargs.TryPop(MagicNames.s_init, out var o_init))
                this[MagicNames.i___init__] = o_init;
            if (!this.IsSet(MagicNames.i___str__) && cp_kwargs.TryPop(MagicNames.s_str, out var o_str))
                this[MagicNames.i___str__] = o_str;
            if (!this.IsSet(MagicNames.i___repr__) && cp_kwargs.TryPop(MagicNames.s_repr, out var o_repr))
                this[MagicNames.i___repr__] = o_repr;
            if (!this.IsSet(MagicNames.i___next__) && cp_kwargs.TryPop(MagicNames.s_next, out var o_next))
                this[MagicNames.i___next__] = o_next;
            if (!this.IsSet(MagicNames.i___add__) && cp_kwargs.TryPop(MagicNames.s_add, out var o_add))
                this[MagicNames.i___add__] = o_add;
            if (!this.IsSet(MagicNames.i___sub__) && cp_kwargs.TryPop(MagicNames.s_sub, out var o_sub))
                this[MagicNames.i___sub__] = o_sub;
            if (!this.IsSet(MagicNames.i___mul__) && cp_kwargs.TryPop(MagicNames.s_mul, out var o_mul))
                this[MagicNames.i___mul__] = o_mul;
            if (!this.IsSet(MagicNames.i___matmul__) && cp_kwargs.TryPop(MagicNames.s_matmul, out var o_matmul))
                this[MagicNames.i___matmul__] = o_matmul;
            if (!this.IsSet(MagicNames.i___floordiv__) && cp_kwargs.TryPop(MagicNames.s_floordiv, out var o_floordiv))
                this[MagicNames.i___floordiv__] = o_floordiv;
            if (!this.IsSet(MagicNames.i___truediv__) && cp_kwargs.TryPop(MagicNames.s_truediv, out var o_truediv))
                this[MagicNames.i___truediv__] = o_truediv;
            if (!this.IsSet(MagicNames.i___mod__) && cp_kwargs.TryPop(MagicNames.s_mod, out var o_mod))
                this[MagicNames.i___mod__] = o_mod;
            if (!this.IsSet(MagicNames.i___pow__) && cp_kwargs.TryPop(MagicNames.s_pow, out var o_pow))
                this[MagicNames.i___pow__] = o_pow;
            if (!this.IsSet(MagicNames.i___bitand__) && cp_kwargs.TryPop(MagicNames.s_bitand, out var o_bitand))
                this[MagicNames.i___bitand__] = o_bitand;
            if (!this.IsSet(MagicNames.i___bitor__) && cp_kwargs.TryPop(MagicNames.s_bitor, out var o_bitor))
                this[MagicNames.i___bitor__] = o_bitor;
            if (!this.IsSet(MagicNames.i___bitxor__) && cp_kwargs.TryPop(MagicNames.s_bitxor, out var o_bitxor))
                this[MagicNames.i___bitxor__] = o_bitxor;
            if (!this.IsSet(MagicNames.i___lshift__) && cp_kwargs.TryPop(MagicNames.s_lshift, out var o_lshift))
                this[MagicNames.i___lshift__] = o_lshift;
            if (!this.IsSet(MagicNames.i___rshift__) && cp_kwargs.TryPop(MagicNames.s_rshift, out var o_rshift))
                this[MagicNames.i___rshift__] = o_rshift;
            if (!this.IsSet(MagicNames.i___hash__) && cp_kwargs.TryPop(MagicNames.s_hash, out var o_hash))
                this[MagicNames.i___hash__] = o_hash;
            if (!this.IsSet(MagicNames.i___call__) && cp_kwargs.TryPop(MagicNames.s_call, out var o_call))
                this[MagicNames.i___call__] = o_call;
            if (!this.IsSet(MagicNames.i___contains__) && cp_kwargs.TryPop(MagicNames.s_contains, out var o_contains))
                this[MagicNames.i___contains__] = o_contains;
            if (!this.IsSet(MagicNames.i___reversed__) && cp_kwargs.TryPop(MagicNames.s_reversed, out var o_reversed))
                this[MagicNames.i___reversed__] = o_reversed;
            if (!this.IsSet(MagicNames.i___getitem__) && cp_kwargs.TryPop(MagicNames.s_getitem, out var o_getitem))
                this[MagicNames.i___getitem__] = o_getitem;
            if (!this.IsSet(MagicNames.i___delitem__) && cp_kwargs.TryPop(MagicNames.s_delitem, out var o_delitem))
                this[MagicNames.i___delitem__] = o_delitem;
            if (!this.IsSet(MagicNames.i___setitem__) && cp_kwargs.TryPop(MagicNames.s_setitem, out var o_setitem))
                this[MagicNames.i___setitem__] = o_setitem;
            if (!this.IsSet(MagicNames.i___iter__) && cp_kwargs.TryPop(MagicNames.s_iter, out var o_iter))
                this[MagicNames.i___iter__] = o_iter;
            if (!this.IsSet(MagicNames.i___await__) && cp_kwargs.TryPop(MagicNames.s_await, out var o_await))
                this[MagicNames.i___await__] = o_await;
            if (!this.IsSet(MagicNames.i___len__) && cp_kwargs.TryPop(MagicNames.s_len, out var o_len))
                this[MagicNames.i___len__] = o_len;
            if (!this.IsSet(MagicNames.i___eq__) && cp_kwargs.TryPop(MagicNames.s_eq, out var o_eq))
                this[MagicNames.i___eq__] = o_eq;
            if (!this.IsSet(MagicNames.i___ne__) && cp_kwargs.TryPop(MagicNames.s_ne, out var o_ne))
                this[MagicNames.i___ne__] = o_ne;
            if (!this.IsSet(MagicNames.i___lt__) && cp_kwargs.TryPop(MagicNames.s_lt, out var o_lt))
                this[MagicNames.i___lt__] = o_lt;
            if (!this.IsSet(MagicNames.i___le__) && cp_kwargs.TryPop(MagicNames.s_le, out var o_le))
                this[MagicNames.i___le__] = o_le;
            if (!this.IsSet(MagicNames.i___gt__) && cp_kwargs.TryPop(MagicNames.s_gt, out var o_gt))
                this[MagicNames.i___gt__] = o_gt;
            if (!this.IsSet(MagicNames.i___ge__) && cp_kwargs.TryPop(MagicNames.s_ge, out var o_ge))
                this[MagicNames.i___ge__] = o_ge;
            if (!this.IsSet(MagicNames.i___neg__) && cp_kwargs.TryPop(MagicNames.s_neg, out var o_neg))
                this[MagicNames.i___neg__] = o_neg;
            if (!this.IsSet(MagicNames.i___invert__) && cp_kwargs.TryPop(MagicNames.s_invert, out var o_invert))
                this[MagicNames.i___invert__] = o_invert;
            if (!this.IsSet(MagicNames.i___pos__) && cp_kwargs.TryPop(MagicNames.s_pos, out var o_pos))
                this[MagicNames.i___pos__] = o_pos;
            if (!this.IsSet(MagicNames.i___bool__) && cp_kwargs.TryPop(MagicNames.s_bool, out var o_bool))
                this[MagicNames.i___bool__] = o_bool;
            if (!this.IsSet(MagicNames.i___abs__) && cp_kwargs.TryPop(MagicNames.s_abs, out var o_abs))
                this[MagicNames.i___abs__] = o_abs;
            if (!this.IsSet(MagicNames.i___enter__) && cp_kwargs.TryPop(MagicNames.s_enter, out var o_enter))
                this[MagicNames.i___enter__] = o_enter;
            if (!this.IsSet(MagicNames.i___exit__) && cp_kwargs.TryPop(MagicNames.s_exit, out var o_exit))
                this[MagicNames.i___exit__] = o_exit;
        }
    }
}
