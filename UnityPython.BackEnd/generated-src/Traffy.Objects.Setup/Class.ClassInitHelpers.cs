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
        public PolyIC ic__round = new PolyIC(MagicNames.i___round__);
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


        static void BuiltinClassInit_TrModule_abc(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrABC(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrBool(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrBool)self).__repr__());
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrBool)self).__bool__());
        }
        static void BuiltinClassInit_TrSharpFunc(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrSharpFunc)self).__repr__());
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrSharpFunc)self).__call__(arg0,arg1));
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrSharpFunc)self).__bool__());
        }
        static void BuiltinClassInit_TrByteArray(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrByteArray)self).__repr__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__add__(arg0));
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__ge__(arg0));
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrByteArray)self).__bool__());
        }
        static void BuiltinClassInit_TrBytes(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrBytes)self).__repr__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__add__(arg0));
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrBytes)self).__hash__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__ge__(arg0));
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrBytes)self).__bool__());
        }
        static void BuiltinClassInit_TrClass(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrClass)self).__repr__());
            cls[MagicNames.i___bitor__] = TrSharpFunc.FromFunc(cls.Name + ".__bitor__", (self,arg0) => ((Traffy.Objects.TrClass)self).__bitor__(arg0));
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrClass)self).__call__(arg0,arg1));
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrClass)self).__getitem__(arg0));
        }
        static void BuiltinClassInit_TrClassMethod(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrDict(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrDict)self).__repr__());
            cls[MagicNames.i___reversed__] = TrSharpFunc.FromFunc(cls.Name + ".__reversed__", (self) => ((Traffy.Objects.TrDict)self).__reversed__());
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrDict)self).__getitem__(arg0));
            cls[MagicNames.i___delitem__] = TrSharpFunc.FromFunc(cls.Name + ".__delitem__", (self,arg0) => ((Traffy.Objects.TrDict)self).__delitem__(arg0));
            cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc(cls.Name + ".__setitem__", (self,arg0,arg1) => ((Traffy.Objects.TrDict)self).__setitem__(arg0,arg1));
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrDict)self).__len__());
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrDict)self).__bool__());
        }
        static void BuiltinClassInit_TrBaseException(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrException(TrClass cls)
        {
        }
        static void BuiltinClassInit_AttributeError(TrClass cls)
        {
        }
        static void BuiltinClassInit_NameError(TrClass cls)
        {
        }
        static void BuiltinClassInit_TypeError(TrClass cls)
        {
        }
        static void BuiltinClassInit_ValueError(TrClass cls)
        {
        }
        static void BuiltinClassInit_StopIteration(TrClass cls)
        {
        }
        static void BuiltinClassInit_LookupError(TrClass cls)
        {
        }
        static void BuiltinClassInit_KeyError(TrClass cls)
        {
        }
        static void BuiltinClassInit_IndexError(TrClass cls)
        {
        }
        static void BuiltinClassInit_AssertionError(TrClass cls)
        {
        }
        static void BuiltinClassInit_ImportError(TrClass cls)
        {
        }
        static void BuiltinClassInit_RuntimeError(TrClass cls)
        {
        }
        static void BuiltinClassInit_NotImplementError(TrClass cls)
        {
        }
        static void BuiltinClassInit_NativeError(TrClass cls)
        {
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.NativeError)self).__eq__(arg0));
        }
        static void BuiltinClassInit_TrFloat(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrFloat)self).__repr__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__add__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__sub__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__mul__(arg0));
            cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc(cls.Name + ".__floordiv__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__floordiv__(arg0));
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__truediv__(arg0));
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__mod__(arg0));
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__pow__(arg0));
            cls[MagicNames.i___round__] = TrSharpFunc.FromFunc(cls.Name + ".__round__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__round__(arg0));
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__ge__(arg0));
            cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc(cls.Name + ".__neg__", (self) => ((Traffy.Objects.TrFloat)self).__neg__());
            cls[MagicNames.i___pos__] = TrSharpFunc.FromFunc(cls.Name + ".__pos__", (self) => ((Traffy.Objects.TrFloat)self).__pos__());
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrFloat)self).__bool__());
        }
        static void BuiltinClassInit_TrGenerator(TrClass cls)
        {
            cls[MagicNames.i___next__] = TrSharpFunc.FromFunc(cls.Name + ".__next__", (self,arg0) => ((Traffy.Objects.TrGenerator)self).__next__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrGenerator)self).__iter__());
            cls[MagicNames.i___await__] = TrSharpFunc.FromFunc(cls.Name + ".__await__", (self) => ((Traffy.Objects.TrGenerator)self).__await__());
        }
        static void BuiltinClassInit_TrInt(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrInt)self).__repr__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrInt)self).__add__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((Traffy.Objects.TrInt)self).__sub__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrInt)self).__mul__(arg0));
            cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc(cls.Name + ".__floordiv__", (self,arg0) => ((Traffy.Objects.TrInt)self).__floordiv__(arg0));
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self,arg0) => ((Traffy.Objects.TrInt)self).__truediv__(arg0));
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self,arg0) => ((Traffy.Objects.TrInt)self).__mod__(arg0));
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self,arg0) => ((Traffy.Objects.TrInt)self).__pow__(arg0));
            cls[MagicNames.i___bitand__] = TrSharpFunc.FromFunc(cls.Name + ".__bitand__", (self,arg0) => ((Traffy.Objects.TrInt)self).__bitand__(arg0));
            cls[MagicNames.i___bitor__] = TrSharpFunc.FromFunc(cls.Name + ".__bitor__", (self,arg0) => ((Traffy.Objects.TrInt)self).__bitor__(arg0));
            cls[MagicNames.i___bitxor__] = TrSharpFunc.FromFunc(cls.Name + ".__bitxor__", (self,arg0) => ((Traffy.Objects.TrInt)self).__bitxor__(arg0));
            cls[MagicNames.i___lshift__] = TrSharpFunc.FromFunc(cls.Name + ".__lshift__", (self,arg0) => ((Traffy.Objects.TrInt)self).__lshift__(arg0));
            cls[MagicNames.i___rshift__] = TrSharpFunc.FromFunc(cls.Name + ".__rshift__", (self,arg0) => ((Traffy.Objects.TrInt)self).__rshift__(arg0));
            cls[MagicNames.i___round__] = TrSharpFunc.FromFunc(cls.Name + ".__round__", (self,arg0) => ((Traffy.Objects.TrInt)self).__round__(arg0));
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrInt)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrInt)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrInt)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrInt)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrInt)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrInt)self).__ge__(arg0));
            cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc(cls.Name + ".__neg__", (self) => ((Traffy.Objects.TrInt)self).__neg__());
            cls[MagicNames.i___invert__] = TrSharpFunc.FromFunc(cls.Name + ".__invert__", (self) => ((Traffy.Objects.TrInt)self).__invert__());
            cls[MagicNames.i___pos__] = TrSharpFunc.FromFunc(cls.Name + ".__pos__", (self) => ((Traffy.Objects.TrInt)self).__pos__());
        }
        static void BuiltinClassInit_TrIter(TrClass cls)
        {
            cls[MagicNames.i___next__] = TrSharpFunc.FromFunc(cls.Name + ".__next__", (self,arg0) => ((Traffy.Objects.TrIter)self).__next__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrIter)self).__iter__());
        }
        static void BuiltinClassInit_TrList(TrClass cls)
        {
            cls[MagicNames.i___str__] = TrSharpFunc.FromFunc(cls.Name + ".__str__", (self) => ((Traffy.Objects.TrList)self).__str__());
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrList)self).__repr__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrList)self).__add__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrList)self).__mul__(arg0));
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrList)self).__contains__(arg0));
            cls[MagicNames.i___reversed__] = TrSharpFunc.FromFunc(cls.Name + ".__reversed__", (self) => ((Traffy.Objects.TrList)self).__reversed__());
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrList)self).__getitem__(arg0));
            cls[MagicNames.i___delitem__] = TrSharpFunc.FromFunc(cls.Name + ".__delitem__", (self,arg0) => ((Traffy.Objects.TrList)self).__delitem__(arg0));
            cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc(cls.Name + ".__setitem__", (self,arg0,arg1) => ((Traffy.Objects.TrList)self).__setitem__(arg0,arg1));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrList)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrList)self).__len__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrList)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrList)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrList)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrList)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrList)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrList)self).__ge__(arg0));
        }
        static void BuiltinClassInit_TrSharpMethod(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrSharpMethod)self).__repr__());
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrSharpMethod)self).__call__(arg0,arg1));
        }
        static void BuiltinClassInit_TrModule(TrClass cls)
        {
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrModule)self).__bool__());
        }
        static void BuiltinClassInit_TrNone(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrRawObject(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrProperty(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrRef(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrSet(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrSet)self).__repr__());
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrSet)self).__iter__());
        }
        static void BuiltinClassInit_TrSlice(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrSlice)self).__repr__());
        }
        static void BuiltinClassInit_TrStaticMethod(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrStaticMethod)self).__repr__());
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrStaticMethod)self).__call__(arg0,arg1));
        }
        static void BuiltinClassInit_TrStr(TrClass cls)
        {
            cls[MagicNames.i___str__] = TrSharpFunc.FromFunc(cls.Name + ".__str__", (self) => ((Traffy.Objects.TrStr)self).__str__());
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrStr)self).__repr__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrStr)self).__add__(arg0));
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrStr)self).__hash__());
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrStr)self).__contains__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrStr)self).__iter__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrStr)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrStr)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrStr)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrStr)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrStr)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrStr)self).__ge__(arg0));
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrStr)self).__bool__());
        }
        static void BuiltinClassInit_TrTraceback(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrTuple(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrTuple)self).__repr__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__add__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__mul__(arg0));
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__getitem__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrTuple)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrTuple)self).__len__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__ge__(arg0));
        }
        static void BuiltinClassInit_TrUnionType(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrUnionType)self).__repr__());
        }
        static void BuiltinClassInit_TrFunc(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrFunc)self).__repr__());
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrFunc)self).__call__(arg0,arg1));
        }
        static void BuiltinClassInit<T>(TrClass cls) where T : TrObject
        {
            if (typeof(T) == typeof(Traffy.Modules.TrModule_abc))
            {
                BuiltinClassInit_TrModule_abc(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrABC))
            {
                BuiltinClassInit_TrABC(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrBool))
            {
                BuiltinClassInit_TrBool(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrSharpFunc))
            {
                BuiltinClassInit_TrSharpFunc(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrByteArray))
            {
                BuiltinClassInit_TrByteArray(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrBytes))
            {
                BuiltinClassInit_TrBytes(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrClass))
            {
                BuiltinClassInit_TrClass(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrClassMethod))
            {
                BuiltinClassInit_TrClassMethod(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrDict))
            {
                BuiltinClassInit_TrDict(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrBaseException))
            {
                BuiltinClassInit_TrBaseException(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrException))
            {
                BuiltinClassInit_TrException(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.AttributeError))
            {
                BuiltinClassInit_AttributeError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.NameError))
            {
                BuiltinClassInit_NameError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TypeError))
            {
                BuiltinClassInit_TypeError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.ValueError))
            {
                BuiltinClassInit_ValueError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.StopIteration))
            {
                BuiltinClassInit_StopIteration(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.LookupError))
            {
                BuiltinClassInit_LookupError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.KeyError))
            {
                BuiltinClassInit_KeyError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.IndexError))
            {
                BuiltinClassInit_IndexError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.AssertionError))
            {
                BuiltinClassInit_AssertionError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.ImportError))
            {
                BuiltinClassInit_ImportError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.RuntimeError))
            {
                BuiltinClassInit_RuntimeError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.NotImplementError))
            {
                BuiltinClassInit_NotImplementError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.NativeError))
            {
                BuiltinClassInit_NativeError(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrFloat))
            {
                BuiltinClassInit_TrFloat(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrGenerator))
            {
                BuiltinClassInit_TrGenerator(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrInt))
            {
                BuiltinClassInit_TrInt(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrIter))
            {
                BuiltinClassInit_TrIter(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrList))
            {
                BuiltinClassInit_TrList(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrSharpMethod))
            {
                BuiltinClassInit_TrSharpMethod(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrModule))
            {
                BuiltinClassInit_TrModule(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrNone))
            {
                BuiltinClassInit_TrNone(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrRawObject))
            {
                BuiltinClassInit_TrRawObject(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrProperty))
            {
                BuiltinClassInit_TrProperty(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrRef))
            {
                BuiltinClassInit_TrRef(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrSet))
            {
                BuiltinClassInit_TrSet(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrSlice))
            {
                BuiltinClassInit_TrSlice(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrStaticMethod))
            {
                BuiltinClassInit_TrStaticMethod(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrStr))
            {
                BuiltinClassInit_TrStr(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrTraceback))
            {
                BuiltinClassInit_TrTraceback(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrTuple))
            {
                BuiltinClassInit_TrTuple(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrUnionType))
            {
                BuiltinClassInit_TrUnionType(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrFunc))
            {
                BuiltinClassInit_TrFunc(cls);
                return;
            }
            throw new System.Exception("Unsupported type: " + typeof(T).FullName);
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
            ic__round = new PolyIC(MagicNames.i___round__);
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

