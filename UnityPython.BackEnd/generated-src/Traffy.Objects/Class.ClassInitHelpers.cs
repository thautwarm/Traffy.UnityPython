using Traffy.InlineCache;
namespace Traffy.Objects
{
    public partial class TrClass
    {
        public PolyIC ic__new = new PolyIC(MagicNames.i___new__);
        public PolyIC ic__init_subclass = new PolyIC(MagicNames.i___init_subclass__);
        public PolyIC ic__init = new PolyIC(MagicNames.i___init__);
        public PolyIC ic__str = new PolyIC(MagicNames.i___str__);
        public PolyIC ic__repr = new PolyIC(MagicNames.i___repr__);
        public PolyIC ic__next = new PolyIC(MagicNames.i___next__);
        public PolyIC ic__add = new PolyIC(MagicNames.i___add__);
        public PolyIC ic__sub = new PolyIC(MagicNames.i___sub__);
        public PolyIC ic__mul = new PolyIC(MagicNames.i___mul__);
        public PolyIC ic__matmul = new PolyIC(MagicNames.i___matmul__);
        public PolyIC ic__floordiv = new PolyIC(MagicNames.i___floordiv__);
        public PolyIC ic__truediv = new PolyIC(MagicNames.i___truediv__);
        public PolyIC ic__mod = new PolyIC(MagicNames.i___mod__);
        public PolyIC ic__pow = new PolyIC(MagicNames.i___pow__);
        public PolyIC ic__bitand = new PolyIC(MagicNames.i___bitand__);
        public PolyIC ic__bitor = new PolyIC(MagicNames.i___bitor__);
        public PolyIC ic__bitxor = new PolyIC(MagicNames.i___bitxor__);
        public PolyIC ic__lshift = new PolyIC(MagicNames.i___lshift__);
        public PolyIC ic__rshift = new PolyIC(MagicNames.i___rshift__);
        public PolyIC ic__hash = new PolyIC(MagicNames.i___hash__);
        public PolyIC ic__call = new PolyIC(MagicNames.i___call__);
        public PolyIC ic__contains = new PolyIC(MagicNames.i___contains__);
        public PolyIC ic__getitem = new PolyIC(MagicNames.i___getitem__);
        public PolyIC ic__setitem = new PolyIC(MagicNames.i___setitem__);
        public PolyIC ic__getattr = new PolyIC(MagicNames.i___getattr__);
        public PolyIC ic__setattr = new PolyIC(MagicNames.i___setattr__);
        public PolyIC ic__iter = new PolyIC(MagicNames.i___iter__);
        public PolyIC ic__len = new PolyIC(MagicNames.i___len__);
        public PolyIC ic__eq = new PolyIC(MagicNames.i___eq__);
        public PolyIC ic__ne = new PolyIC(MagicNames.i___ne__);
        public PolyIC ic__lt = new PolyIC(MagicNames.i___lt__);
        public PolyIC ic__le = new PolyIC(MagicNames.i___le__);
        public PolyIC ic__gt = new PolyIC(MagicNames.i___gt__);
        public PolyIC ic__ge = new PolyIC(MagicNames.i___ge__);
        public PolyIC ic__neg = new PolyIC(MagicNames.i___neg__);
        public PolyIC ic__invert = new PolyIC(MagicNames.i___invert__);
        public PolyIC ic__pos = new PolyIC(MagicNames.i___pos__);
        public PolyIC ic__bool = new PolyIC(MagicNames.i___bool__);
        public PolyIC ic__abs = new PolyIC(MagicNames.i___abs__);
        public PolyIC ic__enter = new PolyIC(MagicNames.i___enter__);
        public PolyIC ic__exit = new PolyIC(MagicNames.i___exit__);
        static void RawClassInit(TrClass cls)
        {
            cls[MagicNames.i___new__] = TrStaticMethod.Bind("object.__new__", TrObject.__new__);
            cls[MagicNames.i___init_subclass__] = TrStaticMethod.Bind("object.__init_subclass__", TrObject.__init_subclass__);
            cls[MagicNames.i___init__] = TrSharpFunc.FromFunc("object.__init__", TrObject.__init__);
            cls[MagicNames.i___str__] = TrSharpFunc.FromFunc("object.__str__", TrObject.__str__);
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc("object.__repr__", TrObject.__repr__);
            cls[MagicNames.i___next__] = TrSharpFunc.FromFunc("object.__next__", TrObject.__next__);
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc("object.__add__", TrObject.__add__);
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc("object.__sub__", TrObject.__sub__);
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc("object.__mul__", TrObject.__mul__);
            cls[MagicNames.i___matmul__] = TrSharpFunc.FromFunc("object.__matmul__", TrObject.__matmul__);
            cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc("object.__floordiv__", TrObject.__floordiv__);
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc("object.__truediv__", TrObject.__truediv__);
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc("object.__mod__", TrObject.__mod__);
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc("object.__pow__", TrObject.__pow__);
            cls[MagicNames.i___bitand__] = TrSharpFunc.FromFunc("object.__bitand__", TrObject.__bitand__);
            cls[MagicNames.i___bitor__] = TrSharpFunc.FromFunc("object.__bitor__", TrObject.__bitor__);
            cls[MagicNames.i___bitxor__] = TrSharpFunc.FromFunc("object.__bitxor__", TrObject.__bitxor__);
            cls[MagicNames.i___lshift__] = TrSharpFunc.FromFunc("object.__lshift__", TrObject.__lshift__);
            cls[MagicNames.i___rshift__] = TrSharpFunc.FromFunc("object.__rshift__", TrObject.__rshift__);
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc("object.__hash__", TrObject.__hash__);
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc("object.__call__", TrObject.__call__);
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc("object.__contains__", TrObject.__contains__);
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc("object.__getitem__", TrObject.__getitem__);
            cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc("object.__setitem__", TrObject.__setitem__);
            cls[MagicNames.i___getattr__] = TrSharpFunc.FromFunc("object.__getattr__", TrObject.__getattr__);
            cls[MagicNames.i___setattr__] = TrSharpFunc.FromFunc("object.__setattr__", TrObject.__setattr__);
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc("object.__iter__", TrObject.__iter__);
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc("object.__len__", TrObject.__len__);
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc("object.__eq__", TrObject.__eq__);
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc("object.__ne__", TrObject.__ne__);
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc("object.__lt__", TrObject.__lt__);
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc("object.__le__", TrObject.__le__);
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc("object.__gt__", TrObject.__gt__);
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc("object.__ge__", TrObject.__ge__);
            cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc("object.__neg__", TrObject.__neg__);
            cls[MagicNames.i___invert__] = TrSharpFunc.FromFunc("object.__invert__", TrObject.__invert__);
            cls[MagicNames.i___pos__] = TrSharpFunc.FromFunc("object.__pos__", TrObject.__pos__);
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc("object.__bool__", TrObject.__bool__);
            cls[MagicNames.i___abs__] = TrSharpFunc.FromFunc("object.__abs__", TrObject.__abs__);
            cls[MagicNames.i___enter__] = TrSharpFunc.FromFunc("object.__enter__", TrObject.__enter__);
            cls[MagicNames.i___exit__] = TrSharpFunc.FromFunc("object.__exit__", TrObject.__exit__);
        }


        static void BuiltinClassInit<T>(TrClass cls) where T : TrObject
        {
            cls[MagicNames.i___init__] = TrSharpFunc.FromFunc(cls.Name + ".__init__", (self, arg0, arg1) => ((T)self).__init__(arg0, arg1));
            cls[MagicNames.i___str__] = TrSharpFunc.FromFunc(cls.Name + ".__str__", (self) => ((T)self).__str__());
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((T)self).__repr__());
            cls[MagicNames.i___next__] = TrSharpFunc.FromFunc(cls.Name + ".__next__", (self) => ((T)self).__next__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self, arg0) => ((T)self).__add__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self, arg0) => ((T)self).__sub__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self, arg0) => ((T)self).__mul__(arg0));
            cls[MagicNames.i___matmul__] = TrSharpFunc.FromFunc(cls.Name + ".__matmul__", (self, arg0) => ((T)self).__matmul__(arg0));
            cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc(cls.Name + ".__floordiv__", (self, arg0) => ((T)self).__floordiv__(arg0));
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self, arg0) => ((T)self).__truediv__(arg0));
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self, arg0) => ((T)self).__mod__(arg0));
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self, arg0) => ((T)self).__pow__(arg0));
            cls[MagicNames.i___bitand__] = TrSharpFunc.FromFunc(cls.Name + ".__bitand__", (self, arg0) => ((T)self).__bitand__(arg0));
            cls[MagicNames.i___bitor__] = TrSharpFunc.FromFunc(cls.Name + ".__bitor__", (self, arg0) => ((T)self).__bitor__(arg0));
            cls[MagicNames.i___bitxor__] = TrSharpFunc.FromFunc(cls.Name + ".__bitxor__", (self, arg0) => ((T)self).__bitxor__(arg0));
            cls[MagicNames.i___lshift__] = TrSharpFunc.FromFunc(cls.Name + ".__lshift__", (self, arg0) => ((T)self).__lshift__(arg0));
            cls[MagicNames.i___rshift__] = TrSharpFunc.FromFunc(cls.Name + ".__rshift__", (self, arg0) => ((T)self).__rshift__(arg0));
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((T)self).__hash__());
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self, arg0, arg1) => ((T)self).__call__(arg0, arg1));
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self, arg0) => ((T)self).__contains__(arg0));
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self, arg0, arg1) => ((T)self).__getitem__(arg0, arg1));
            cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc(cls.Name + ".__setitem__", (self, arg0, arg1) => ((T)self).__setitem__(arg0, arg1));
            cls[MagicNames.i___getattr__] = TrSharpFunc.FromFunc(cls.Name + ".__getattr__", (self, arg0, arg1) => ((T)self).__getattr__(arg0, arg1));
            cls[MagicNames.i___setattr__] = TrSharpFunc.FromFunc(cls.Name + ".__setattr__", (self, arg0, arg1) => ((T)self).__setattr__(arg0, arg1));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((T)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((T)self).__len__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self, arg0) => ((T)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self, arg0) => ((T)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self, arg0) => ((T)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self, arg0) => ((T)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self, arg0) => ((T)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self, arg0) => ((T)self).__ge__(arg0));
            cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc(cls.Name + ".__neg__", (self) => ((T)self).__neg__());
            cls[MagicNames.i___invert__] = TrSharpFunc.FromFunc(cls.Name + ".__invert__", (self) => ((T)self).__invert__());
            cls[MagicNames.i___pos__] = TrSharpFunc.FromFunc(cls.Name + ".__pos__", (self) => ((T)self).__pos__());
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((T)self).__bool__());
            cls[MagicNames.i___abs__] = TrSharpFunc.FromFunc(cls.Name + ".__abs__", (self) => ((T)self).__abs__());
            cls[MagicNames.i___enter__] = TrSharpFunc.FromFunc(cls.Name + ".__enter__", (self) => ((T)self).__enter__());
            cls[MagicNames.i___exit__] = TrSharpFunc.FromFunc(cls.Name + ".__exit__", (self, arg0, arg1, arg2) => ((T)self).__exit__(arg0, arg1, arg2));
        }
        public void InitInlineCacheForMagicMethods()
        {
            ic__new = new PolyIC(MagicNames.i___new__);
            ic__init_subclass = new PolyIC(MagicNames.i___init_subclass__);
            ic__init = new PolyIC(MagicNames.i___init__);
            ic__str = new PolyIC(MagicNames.i___str__);
            ic__repr = new PolyIC(MagicNames.i___repr__);
            ic__next = new PolyIC(MagicNames.i___next__);
            ic__add = new PolyIC(MagicNames.i___add__);
            ic__sub = new PolyIC(MagicNames.i___sub__);
            ic__mul = new PolyIC(MagicNames.i___mul__);
            ic__matmul = new PolyIC(MagicNames.i___matmul__);
            ic__floordiv = new PolyIC(MagicNames.i___floordiv__);
            ic__truediv = new PolyIC(MagicNames.i___truediv__);
            ic__mod = new PolyIC(MagicNames.i___mod__);
            ic__pow = new PolyIC(MagicNames.i___pow__);
            ic__bitand = new PolyIC(MagicNames.i___bitand__);
            ic__bitor = new PolyIC(MagicNames.i___bitor__);
            ic__bitxor = new PolyIC(MagicNames.i___bitxor__);
            ic__lshift = new PolyIC(MagicNames.i___lshift__);
            ic__rshift = new PolyIC(MagicNames.i___rshift__);
            ic__hash = new PolyIC(MagicNames.i___hash__);
            ic__call = new PolyIC(MagicNames.i___call__);
            ic__contains = new PolyIC(MagicNames.i___contains__);
            ic__getitem = new PolyIC(MagicNames.i___getitem__);
            ic__setitem = new PolyIC(MagicNames.i___setitem__);
            ic__getattr = new PolyIC(MagicNames.i___getattr__);
            ic__setattr = new PolyIC(MagicNames.i___setattr__);
            ic__iter = new PolyIC(MagicNames.i___iter__);
            ic__len = new PolyIC(MagicNames.i___len__);
            ic__eq = new PolyIC(MagicNames.i___eq__);
            ic__ne = new PolyIC(MagicNames.i___ne__);
            ic__lt = new PolyIC(MagicNames.i___lt__);
            ic__le = new PolyIC(MagicNames.i___le__);
            ic__gt = new PolyIC(MagicNames.i___gt__);
            ic__ge = new PolyIC(MagicNames.i___ge__);
            ic__neg = new PolyIC(MagicNames.i___neg__);
            ic__invert = new PolyIC(MagicNames.i___invert__);
            ic__pos = new PolyIC(MagicNames.i___pos__);
            ic__bool = new PolyIC(MagicNames.i___bool__);
            ic__abs = new PolyIC(MagicNames.i___abs__);
            ic__enter = new PolyIC(MagicNames.i___enter__);
            ic__exit = new PolyIC(MagicNames.i___exit__);
        }
    }
}
