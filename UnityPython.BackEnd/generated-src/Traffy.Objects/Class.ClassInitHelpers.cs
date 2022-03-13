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
        public PolyIC ic__reversed = new PolyIC(MagicNames.i___reversed__);
        public PolyIC ic__getitem = new PolyIC(MagicNames.i___getitem__);
        public PolyIC ic__delitem = new PolyIC(MagicNames.i___delitem__);
        public PolyIC ic__setitem = new PolyIC(MagicNames.i___setitem__);
        public PolyIC ic__iter = new PolyIC(MagicNames.i___iter__);
        public PolyIC ic__await = new PolyIC(MagicNames.i___await__);
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
            cls[MagicNames.i___str__] = TrSharpFunc.FromFunc("object.__str__", TrObject.__str__);
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc("object.__repr__", TrObject.__repr__);
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc("object.__hash__", TrObject.__hash__);
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc("object.__eq__", TrObject.__eq__);
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc("object.__ne__", TrObject.__ne__);
        }


        static void BuiltinClassInit<T>(TrClass cls) where T : TrObject
        {
            var ownership = typeof(T).GetInterfaceMethodSource();
            if (ownership.Contains("__init__"))
                cls[MagicNames.i___init__] = TrSharpFunc.FromFunc(cls.Name + ".__init__", (self,arg0,arg1) => ((T)self).__init__(arg0,arg1));
            if (ownership.Contains("__str__"))
                cls[MagicNames.i___str__] = TrSharpFunc.FromFunc(cls.Name + ".__str__", (self) => ((T)self).__str__());
            if (ownership.Contains("__repr__"))
                cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((T)self).__repr__());
            if (ownership.Contains("__next__"))
                cls[MagicNames.i___next__] = TrSharpFunc.FromFunc(cls.Name + ".__next__", (self,arg0) => ((T)self).__next__(arg0));
            if (ownership.Contains("__add__"))
                cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((T)self).__add__(arg0));
            if (ownership.Contains("__sub__"))
                cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((T)self).__sub__(arg0));
            if (ownership.Contains("__mul__"))
                cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((T)self).__mul__(arg0));
            if (ownership.Contains("__matmul__"))
                cls[MagicNames.i___matmul__] = TrSharpFunc.FromFunc(cls.Name + ".__matmul__", (self,arg0) => ((T)self).__matmul__(arg0));
            if (ownership.Contains("__floordiv__"))
                cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc(cls.Name + ".__floordiv__", (self,arg0) => ((T)self).__floordiv__(arg0));
            if (ownership.Contains("__truediv__"))
                cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self,arg0) => ((T)self).__truediv__(arg0));
            if (ownership.Contains("__mod__"))
                cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self,arg0) => ((T)self).__mod__(arg0));
            if (ownership.Contains("__pow__"))
                cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self,arg0) => ((T)self).__pow__(arg0));
            if (ownership.Contains("__bitand__"))
                cls[MagicNames.i___bitand__] = TrSharpFunc.FromFunc(cls.Name + ".__bitand__", (self,arg0) => ((T)self).__bitand__(arg0));
            if (ownership.Contains("__bitor__"))
                cls[MagicNames.i___bitor__] = TrSharpFunc.FromFunc(cls.Name + ".__bitor__", (self,arg0) => ((T)self).__bitor__(arg0));
            if (ownership.Contains("__bitxor__"))
                cls[MagicNames.i___bitxor__] = TrSharpFunc.FromFunc(cls.Name + ".__bitxor__", (self,arg0) => ((T)self).__bitxor__(arg0));
            if (ownership.Contains("__lshift__"))
                cls[MagicNames.i___lshift__] = TrSharpFunc.FromFunc(cls.Name + ".__lshift__", (self,arg0) => ((T)self).__lshift__(arg0));
            if (ownership.Contains("__rshift__"))
                cls[MagicNames.i___rshift__] = TrSharpFunc.FromFunc(cls.Name + ".__rshift__", (self,arg0) => ((T)self).__rshift__(arg0));
            if (ownership.Contains("__hash__"))
                cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((T)self).__hash__());
            if (ownership.Contains("__call__"))
                cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((T)self).__call__(arg0,arg1));
            if (ownership.Contains("__contains__"))
                cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((T)self).__contains__(arg0));
            if (ownership.Contains("__reversed__"))
                cls[MagicNames.i___reversed__] = TrSharpFunc.FromFunc(cls.Name + ".__reversed__", (self,arg0) => ((T)self).__reversed__(arg0));
            if (ownership.Contains("__getitem__"))
                cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((T)self).__getitem__(arg0));
            if (ownership.Contains("__delitem__"))
                cls[MagicNames.i___delitem__] = TrSharpFunc.FromFunc(cls.Name + ".__delitem__", (self,arg0) => ((T)self).__delitem__(arg0));
            if (ownership.Contains("__setitem__"))
                cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc(cls.Name + ".__setitem__", (self,arg0,arg1) => ((T)self).__setitem__(arg0,arg1));
            if (ownership.Contains("__iter__"))
                cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((T)self).__iter__());
            if (ownership.Contains("__await__"))
                cls[MagicNames.i___await__] = TrSharpFunc.FromFunc(cls.Name + ".__await__", (self) => ((T)self).__await__());
            if (ownership.Contains("__len__"))
                cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((T)self).__len__());
            if (ownership.Contains("__eq__"))
                cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((T)self).__eq__(arg0));
            if (ownership.Contains("__ne__"))
                cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((T)self).__ne__(arg0));
            if (ownership.Contains("__lt__"))
                cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((T)self).__lt__(arg0));
            if (ownership.Contains("__le__"))
                cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((T)self).__le__(arg0));
            if (ownership.Contains("__gt__"))
                cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((T)self).__gt__(arg0));
            if (ownership.Contains("__ge__"))
                cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((T)self).__ge__(arg0));
            if (ownership.Contains("__neg__"))
                cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc(cls.Name + ".__neg__", (self) => ((T)self).__neg__());
            if (ownership.Contains("__invert__"))
                cls[MagicNames.i___invert__] = TrSharpFunc.FromFunc(cls.Name + ".__invert__", (self) => ((T)self).__invert__());
            if (ownership.Contains("__pos__"))
                cls[MagicNames.i___pos__] = TrSharpFunc.FromFunc(cls.Name + ".__pos__", (self) => ((T)self).__pos__());
            if (ownership.Contains("__bool__"))
                cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((T)self).__bool__());
            if (ownership.Contains("__abs__"))
                cls[MagicNames.i___abs__] = TrSharpFunc.FromFunc(cls.Name + ".__abs__", (self) => ((T)self).__abs__());
            if (ownership.Contains("__enter__"))
                cls[MagicNames.i___enter__] = TrSharpFunc.FromFunc(cls.Name + ".__enter__", (self) => ((T)self).__enter__());
            if (ownership.Contains("__exit__"))
                cls[MagicNames.i___exit__] = TrSharpFunc.FromFunc(cls.Name + ".__exit__", (self,arg0,arg1,arg2) => ((T)self).__exit__(arg0,arg1,arg2));
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
            ic__reversed = new PolyIC(MagicNames.i___reversed__);
            ic__getitem = new PolyIC(MagicNames.i___getitem__);
            ic__delitem = new PolyIC(MagicNames.i___delitem__);
            ic__setitem = new PolyIC(MagicNames.i___setitem__);
            ic__iter = new PolyIC(MagicNames.i___iter__);
            ic__await = new PolyIC(MagicNames.i___await__);
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
