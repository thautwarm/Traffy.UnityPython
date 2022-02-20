using Traffy.Objects;
namespace Traffy
{

    public static class MagicNames
    {
        public static TrStr s_init = MK.Str("__init__");
        public static TrStr s_new = MK.Str("__new__");
        public static TrStr s_neg = MK.Str("__neg__");
        public static TrStr s_inv = MK.Str("__inv__");
        public static TrStr s_pos = MK.Str("__pos__");
        public static TrStr s_bool = MK.Str("__bool__");
        public static TrStr s_str = MK.Str("__str__");
        public static TrStr s_repr = MK.Str("__repr__");
        public static TrStr s_floordiv = MK.Str("__floordiv__");
        public static TrStr s_add = MK.Str("__add__");
        public static TrStr s_next = MK.Str("__next__");
        public static TrStr s_sub = MK.Str("__sub__");
        public static TrStr s_mul = MK.Str("__mul__");
        public static TrStr s_matmul = MK.Str("__matmul__");

        public static TrStr s_truediv = MK.Str("__truediv__");
        public static TrStr s_pow = MK.Str("__pow__");
        public static TrStr s_mod = MK.Str("__mod__");
        public static TrStr s_bitand = MK.Str("__bitand__");
        public static TrStr s_bitor = MK.Str("__bitor__");
        public static TrStr s_bitxor = MK.Str("__bitxor__");
        public static TrStr s_lshift = MK.Str("__lshift__");
        public static TrStr s_rshift = MK.Str("__rshift__");
        public static TrStr s_hash = MK.Str("__hash__");
        public static TrStr s_contains = MK.Str("__contains__");
        public static TrStr s_call = MK.Str("__call__");
        public static TrStr s_getitem = MK.Str("__getitem__");
        public static TrStr s_setitem = MK.Str("__setitem__");
        public static TrStr s_getattr = MK.Str("__getattr__");
        public static TrStr s_setattr = MK.Str("__setattr__");
        public static TrStr s_iter = MK.Str("__iter__");
        public static TrStr s_len = MK.Str("__len__");
        public static TrStr s_eq = MK.Str("__eq__");
        public static TrStr s_lt = MK.Str("__lt__");

        public static InternedString i___init__ = InternedString.FromString("__init__");
        public static InternedString i___new__ = InternedString.FromString("__new__");
        public static InternedString i___neg__ = InternedString.FromString("__neg__");
        public static InternedString i___inv__ = InternedString.FromString("__inv__");
        public static InternedString i___pos__ = InternedString.FromString("__pos__");
        public static InternedString i___bool__ = InternedString.FromString("__bool__");
        public static InternedString i___str__ = InternedString.FromString("__str__");
        public static InternedString i___repr__ = InternedString.FromString("__repr__");
        public static InternedString i___floordiv__ = InternedString.FromString("__floordiv__");
        public static InternedString i___add__ = InternedString.FromString("__add__");
        public static InternedString i___next__ = InternedString.FromString("__next__");
        public static InternedString i___sub__ = InternedString.FromString("__sub__");
        public static InternedString i___mul__ = InternedString.FromString("__mul__");
        public static InternedString i___matmul__ = InternedString.FromString("__matmul__");
        public static InternedString i___truediv__ = InternedString.FromString("__truediv__");
        public static InternedString i___pow__ = InternedString.FromString("__pow__");
        public static InternedString i___mod__ = InternedString.FromString("__mod__");
        public static InternedString i___bitand__ = InternedString.FromString("__bitand__");
        public static InternedString i___bitor__ = InternedString.FromString("__bitor__");
        public static InternedString i___bitxor__ = InternedString.FromString("__bitxor__");
        public static InternedString i___lshift__ = InternedString.FromString("__lshift__");
        public static InternedString i___rshift__ = InternedString.FromString("__rshift__");
        public static InternedString i___hash__ = InternedString.FromString("__hash__");
        public static InternedString i___contains__ = InternedString.FromString("__contains__");
        public static InternedString i___call__ = InternedString.FromString("__call__");
        public static InternedString i___getitem__ = InternedString.FromString("__getitem__");
        public static InternedString i___setitem__ = InternedString.FromString("__setitem__");
        public static InternedString i___getattr__ = InternedString.FromString("__getattr__");
        public static InternedString i___setattr__ = InternedString.FromString("__setattr__");
        public static InternedString i___iter__ = InternedString.FromString("__iter__");
        public static InternedString i___len__ = InternedString.FromString("__len__");
        public static InternedString i___eq__ = InternedString.FromString("__eq__");
        public static InternedString i___lt__ = InternedString.FromString("__lt__");

        public static InternedString i___init_subclass__ = InternedString.FromString("__init_subclass__");

        // a collection of all 'i__xxx__'
        public static readonly InternedString[] istr_magic_names = new InternedString[] {
              i___init__,
              i___new__,
              i___neg__,
              i___inv__,
              i___pos__,
              i___bool__,
              i___str__,
              i___repr__,
              i___floordiv__,
              i___add__,
              i___next__,
              i___sub__,
              i___mul__,
              i___matmul__,
              i___truediv__,
              i___pow__,
              i___mod__,
              i___bitand__,
              i___bitor__,
              i___bitxor__,
              i___lshift__,
              i___rshift__,
              i___hash__,
              i___contains__,
              i___call__,
              i___getitem__,
              i___setitem__,
              i___getattr__,
              i___setattr__,
              i___iter__,
              i___len__,
              i___eq__,
              i___lt__,
          };

        // index of the first 'i__xxx__'
        public static int
            slot__init = 0, slot__new = 1, slot__neg = 2, slot__inv = 3, slot__pos = 4, slot__bool = 5,
            slot__str = 6, slot__repr = 7, slot__floordiv = 8, slot__add = 9, slot__next = 10,
            slot__sub = 11, slot__mul = 12, slot__matmul = 13, slot__truediv = 14, slot__pow = 15,
            slot__mod = 16, slot__bitand = 17, slot__bitor = 18, slot__bitxor = 19, slot__lshift = 20,
            slot__rshift = 21, slot__hash = 22, slot__contains = 23, slot__call = 24, slot__getitem = 25,
            slot__setitem = 26, slot__getattr = 27, slot__setattr = 28, slot__iter = 29, slot__len = 30,
            slot__eq = 31, slot__lt = 32;



    }
}