using System.Collections.Generic;
using Traffy;
namespace Traffy.Objects
{
    public partial class TrClass
    {
        void BindMethodsFromDict(Dictionary<TrObject, TrObject> cp_kwargs)
        {
            if (cp_kwargs.TryPop(MagicNames.s_new, out var o_new))
                this[MagicNames.i___new__] = o_new;
            if (cp_kwargs.TryPop(MagicNames.s_init_subclass, out var o_init_subclass))
                this[MagicNames.i___init_subclass__] = o_init_subclass;
            if (cp_kwargs.TryPop(MagicNames.s_init, out var o_init))
                this[MagicNames.i___init__] = o_init;
            if (cp_kwargs.TryPop(MagicNames.s_str, out var o_str))
                this[MagicNames.i___str__] = o_str;
            if (cp_kwargs.TryPop(MagicNames.s_repr, out var o_repr))
                this[MagicNames.i___repr__] = o_repr;
            if (cp_kwargs.TryPop(MagicNames.s_trynext, out var o_trynext))
                this[MagicNames.i___trynext__] = o_trynext;
            if (cp_kwargs.TryPop(MagicNames.s_add, out var o_add))
                this[MagicNames.i___add__] = o_add;
            if (cp_kwargs.TryPop(MagicNames.s_sub, out var o_sub))
                this[MagicNames.i___sub__] = o_sub;
            if (cp_kwargs.TryPop(MagicNames.s_mul, out var o_mul))
                this[MagicNames.i___mul__] = o_mul;
            if (cp_kwargs.TryPop(MagicNames.s_matmul, out var o_matmul))
                this[MagicNames.i___matmul__] = o_matmul;
            if (cp_kwargs.TryPop(MagicNames.s_floordiv, out var o_floordiv))
                this[MagicNames.i___floordiv__] = o_floordiv;
            if (cp_kwargs.TryPop(MagicNames.s_truediv, out var o_truediv))
                this[MagicNames.i___truediv__] = o_truediv;
            if (cp_kwargs.TryPop(MagicNames.s_mod, out var o_mod))
                this[MagicNames.i___mod__] = o_mod;
            if (cp_kwargs.TryPop(MagicNames.s_pow, out var o_pow))
                this[MagicNames.i___pow__] = o_pow;
            if (cp_kwargs.TryPop(MagicNames.s_and, out var o_and))
                this[MagicNames.i___and__] = o_and;
            if (cp_kwargs.TryPop(MagicNames.s_or, out var o_or))
                this[MagicNames.i___or__] = o_or;
            if (cp_kwargs.TryPop(MagicNames.s_xor, out var o_xor))
                this[MagicNames.i___xor__] = o_xor;
            if (cp_kwargs.TryPop(MagicNames.s_lshift, out var o_lshift))
                this[MagicNames.i___lshift__] = o_lshift;
            if (cp_kwargs.TryPop(MagicNames.s_rshift, out var o_rshift))
                this[MagicNames.i___rshift__] = o_rshift;
            if (cp_kwargs.TryPop(MagicNames.s_hash, out var o_hash))
                this[MagicNames.i___hash__] = o_hash;
            if (cp_kwargs.TryPop(MagicNames.s_call, out var o_call))
                this[MagicNames.i___call__] = o_call;
            if (cp_kwargs.TryPop(MagicNames.s_contains, out var o_contains))
                this[MagicNames.i___contains__] = o_contains;
            if (cp_kwargs.TryPop(MagicNames.s_round, out var o_round))
                this[MagicNames.i___round__] = o_round;
            if (cp_kwargs.TryPop(MagicNames.s_reversed, out var o_reversed))
                this[MagicNames.i___reversed__] = o_reversed;
            if (cp_kwargs.TryPop(MagicNames.s_getitem, out var o_getitem))
                this[MagicNames.i___getitem__] = o_getitem;
            if (cp_kwargs.TryPop(MagicNames.s_delitem, out var o_delitem))
                this[MagicNames.i___delitem__] = o_delitem;
            if (cp_kwargs.TryPop(MagicNames.s_setitem, out var o_setitem))
                this[MagicNames.i___setitem__] = o_setitem;
            if (cp_kwargs.TryPop(MagicNames.s_finditem, out var o_finditem))
                this[MagicNames.i___finditem__] = o_finditem;
            if (cp_kwargs.TryPop(MagicNames.s_iter, out var o_iter))
                this[MagicNames.i___iter__] = o_iter;
            if (cp_kwargs.TryPop(MagicNames.s_await, out var o_await))
                this[MagicNames.i___await__] = o_await;
            if (cp_kwargs.TryPop(MagicNames.s_len, out var o_len))
                this[MagicNames.i___len__] = o_len;
            if (cp_kwargs.TryPop(MagicNames.s_eq, out var o_eq))
                this[MagicNames.i___eq__] = o_eq;
            if (cp_kwargs.TryPop(MagicNames.s_ne, out var o_ne))
                this[MagicNames.i___ne__] = o_ne;
            if (cp_kwargs.TryPop(MagicNames.s_lt, out var o_lt))
                this[MagicNames.i___lt__] = o_lt;
            if (cp_kwargs.TryPop(MagicNames.s_le, out var o_le))
                this[MagicNames.i___le__] = o_le;
            if (cp_kwargs.TryPop(MagicNames.s_gt, out var o_gt))
                this[MagicNames.i___gt__] = o_gt;
            if (cp_kwargs.TryPop(MagicNames.s_ge, out var o_ge))
                this[MagicNames.i___ge__] = o_ge;
            if (cp_kwargs.TryPop(MagicNames.s_neg, out var o_neg))
                this[MagicNames.i___neg__] = o_neg;
            if (cp_kwargs.TryPop(MagicNames.s_invert, out var o_invert))
                this[MagicNames.i___invert__] = o_invert;
            if (cp_kwargs.TryPop(MagicNames.s_pos, out var o_pos))
                this[MagicNames.i___pos__] = o_pos;
            if (cp_kwargs.TryPop(MagicNames.s_bool, out var o_bool))
                this[MagicNames.i___bool__] = o_bool;
            if (cp_kwargs.TryPop(MagicNames.s_abs, out var o_abs))
                this[MagicNames.i___abs__] = o_abs;
            if (cp_kwargs.TryPop(MagicNames.s_enter, out var o_enter))
                this[MagicNames.i___enter__] = o_enter;
            if (cp_kwargs.TryPop(MagicNames.s_exit, out var o_exit))
                this[MagicNames.i___exit__] = o_exit;
        }
    }
}

