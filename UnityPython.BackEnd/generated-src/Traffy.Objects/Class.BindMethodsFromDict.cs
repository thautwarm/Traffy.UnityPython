using System.Collections.Generic;
using Traffy;
namespace Traffy.Objects
{
    public partial class TrClass
    {
        void BindMethodsFromDict(Dictionary<TrObject, TrObject> cp_kwargs)
        {
            if (this[ic__new] == null && cp_kwargs.TryPop(MagicNames.s_new, out var o_new))
                this[MagicNames.i___new__] = o_new;
            if (this[ic__init_subclass] == null && cp_kwargs.TryPop(MagicNames.s_init_subclass, out var o_init_subclass))
                this[MagicNames.i___init_subclass__] = o_init_subclass;
            if (this[ic__init] == null && cp_kwargs.TryPop(MagicNames.s_init, out var o_init))
                this[MagicNames.i___init__] = o_init;
            if (this[ic__str] == null && cp_kwargs.TryPop(MagicNames.s_str, out var o_str))
                this[MagicNames.i___str__] = o_str;
            if (this[ic__repr] == null && cp_kwargs.TryPop(MagicNames.s_repr, out var o_repr))
                this[MagicNames.i___repr__] = o_repr;
            if (this[ic__next] == null && cp_kwargs.TryPop(MagicNames.s_next, out var o_next))
                this[MagicNames.i___next__] = o_next;
            if (this[ic__add] == null && cp_kwargs.TryPop(MagicNames.s_add, out var o_add))
                this[MagicNames.i___add__] = o_add;
            if (this[ic__sub] == null && cp_kwargs.TryPop(MagicNames.s_sub, out var o_sub))
                this[MagicNames.i___sub__] = o_sub;
            if (this[ic__mul] == null && cp_kwargs.TryPop(MagicNames.s_mul, out var o_mul))
                this[MagicNames.i___mul__] = o_mul;
            if (this[ic__matmul] == null && cp_kwargs.TryPop(MagicNames.s_matmul, out var o_matmul))
                this[MagicNames.i___matmul__] = o_matmul;
            if (this[ic__floordiv] == null && cp_kwargs.TryPop(MagicNames.s_floordiv, out var o_floordiv))
                this[MagicNames.i___floordiv__] = o_floordiv;
            if (this[ic__truediv] == null && cp_kwargs.TryPop(MagicNames.s_truediv, out var o_truediv))
                this[MagicNames.i___truediv__] = o_truediv;
            if (this[ic__mod] == null && cp_kwargs.TryPop(MagicNames.s_mod, out var o_mod))
                this[MagicNames.i___mod__] = o_mod;
            if (this[ic__pow] == null && cp_kwargs.TryPop(MagicNames.s_pow, out var o_pow))
                this[MagicNames.i___pow__] = o_pow;
            if (this[ic__bitand] == null && cp_kwargs.TryPop(MagicNames.s_bitand, out var o_bitand))
                this[MagicNames.i___bitand__] = o_bitand;
            if (this[ic__bitor] == null && cp_kwargs.TryPop(MagicNames.s_bitor, out var o_bitor))
                this[MagicNames.i___bitor__] = o_bitor;
            if (this[ic__bitxor] == null && cp_kwargs.TryPop(MagicNames.s_bitxor, out var o_bitxor))
                this[MagicNames.i___bitxor__] = o_bitxor;
            if (this[ic__lshift] == null && cp_kwargs.TryPop(MagicNames.s_lshift, out var o_lshift))
                this[MagicNames.i___lshift__] = o_lshift;
            if (this[ic__rshift] == null && cp_kwargs.TryPop(MagicNames.s_rshift, out var o_rshift))
                this[MagicNames.i___rshift__] = o_rshift;
            if (this[ic__hash] == null && cp_kwargs.TryPop(MagicNames.s_hash, out var o_hash))
                this[MagicNames.i___hash__] = o_hash;
            if (this[ic__call] == null && cp_kwargs.TryPop(MagicNames.s_call, out var o_call))
                this[MagicNames.i___call__] = o_call;
            if (this[ic__contains] == null && cp_kwargs.TryPop(MagicNames.s_contains, out var o_contains))
                this[MagicNames.i___contains__] = o_contains;
            if (this[ic__getitem] == null && cp_kwargs.TryPop(MagicNames.s_getitem, out var o_getitem))
                this[MagicNames.i___getitem__] = o_getitem;
            if (this[ic__setitem] == null && cp_kwargs.TryPop(MagicNames.s_setitem, out var o_setitem))
                this[MagicNames.i___setitem__] = o_setitem;
            if (this[ic__findattr] == null && cp_kwargs.TryPop(MagicNames.s_findattr, out var o_findattr))
                this[MagicNames.i___findattr__] = o_findattr;
            if (this[ic__getattr] == null && cp_kwargs.TryPop(MagicNames.s_getattr, out var o_getattr))
                this[MagicNames.i___getattr__] = o_getattr;
            if (this[ic__setattr] == null && cp_kwargs.TryPop(MagicNames.s_setattr, out var o_setattr))
                this[MagicNames.i___setattr__] = o_setattr;
            if (this[ic__iter] == null && cp_kwargs.TryPop(MagicNames.s_iter, out var o_iter))
                this[MagicNames.i___iter__] = o_iter;
            if (this[ic__len] == null && cp_kwargs.TryPop(MagicNames.s_len, out var o_len))
                this[MagicNames.i___len__] = o_len;
            if (this[ic__eq] == null && cp_kwargs.TryPop(MagicNames.s_eq, out var o_eq))
                this[MagicNames.i___eq__] = o_eq;
            if (this[ic__ne] == null && cp_kwargs.TryPop(MagicNames.s_ne, out var o_ne))
                this[MagicNames.i___ne__] = o_ne;
            if (this[ic__lt] == null && cp_kwargs.TryPop(MagicNames.s_lt, out var o_lt))
                this[MagicNames.i___lt__] = o_lt;
            if (this[ic__le] == null && cp_kwargs.TryPop(MagicNames.s_le, out var o_le))
                this[MagicNames.i___le__] = o_le;
            if (this[ic__gt] == null && cp_kwargs.TryPop(MagicNames.s_gt, out var o_gt))
                this[MagicNames.i___gt__] = o_gt;
            if (this[ic__ge] == null && cp_kwargs.TryPop(MagicNames.s_ge, out var o_ge))
                this[MagicNames.i___ge__] = o_ge;
            if (this[ic__neg] == null && cp_kwargs.TryPop(MagicNames.s_neg, out var o_neg))
                this[MagicNames.i___neg__] = o_neg;
            if (this[ic__invert] == null && cp_kwargs.TryPop(MagicNames.s_invert, out var o_invert))
                this[MagicNames.i___invert__] = o_invert;
            if (this[ic__pos] == null && cp_kwargs.TryPop(MagicNames.s_pos, out var o_pos))
                this[MagicNames.i___pos__] = o_pos;
            if (this[ic__bool] == null && cp_kwargs.TryPop(MagicNames.s_bool, out var o_bool))
                this[MagicNames.i___bool__] = o_bool;
            if (this[ic__abs] == null && cp_kwargs.TryPop(MagicNames.s_abs, out var o_abs))
                this[MagicNames.i___abs__] = o_abs;
            if (this[ic__enter] == null && cp_kwargs.TryPop(MagicNames.s_enter, out var o_enter))
                this[MagicNames.i___enter__] = o_enter;
            if (this[ic__exit] == null && cp_kwargs.TryPop(MagicNames.s_exit, out var o_exit))
                this[MagicNames.i___exit__] = o_exit;
        }
    }
}
