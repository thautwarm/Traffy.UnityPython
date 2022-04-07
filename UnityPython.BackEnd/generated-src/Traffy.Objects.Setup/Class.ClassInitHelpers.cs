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
        public PolyIC ic__int = new PolyIC(MagicNames.i___int__);
        public PolyIC ic__float = new PolyIC(MagicNames.i___float__);
        public PolyIC ic__trynext = new PolyIC(MagicNames.i___trynext__);
        public PolyIC ic__add = new PolyIC(MagicNames.i___add__);
        public PolyIC ic__radd = new PolyIC(MagicNames.i___radd__);
        public PolyIC ic__sub = new PolyIC(MagicNames.i___sub__);
        public PolyIC ic__rsub = new PolyIC(MagicNames.i___rsub__);
        public PolyIC ic__mul = new PolyIC(MagicNames.i___mul__);
        public PolyIC ic__rmul = new PolyIC(MagicNames.i___rmul__);
        public PolyIC ic__matmul = new PolyIC(MagicNames.i___matmul__);
        public PolyIC ic__rmatmul = new PolyIC(MagicNames.i___rmatmul__);
        public PolyIC ic__floordiv = new PolyIC(MagicNames.i___floordiv__);
        public PolyIC ic__rfloordiv = new PolyIC(MagicNames.i___rfloordiv__);
        public PolyIC ic__truediv = new PolyIC(MagicNames.i___truediv__);
        public PolyIC ic__rtruediv = new PolyIC(MagicNames.i___rtruediv__);
        public PolyIC ic__mod = new PolyIC(MagicNames.i___mod__);
        public PolyIC ic__rmod = new PolyIC(MagicNames.i___rmod__);
        public PolyIC ic__pow = new PolyIC(MagicNames.i___pow__);
        public PolyIC ic__rpow = new PolyIC(MagicNames.i___rpow__);
        public PolyIC ic__and = new PolyIC(MagicNames.i___and__);
        public PolyIC ic__rand = new PolyIC(MagicNames.i___rand__);
        public PolyIC ic__or = new PolyIC(MagicNames.i___or__);
        public PolyIC ic__ror = new PolyIC(MagicNames.i___ror__);
        public PolyIC ic__xor = new PolyIC(MagicNames.i___xor__);
        public PolyIC ic__rxor = new PolyIC(MagicNames.i___rxor__);
        public PolyIC ic__lshift = new PolyIC(MagicNames.i___lshift__);
        public PolyIC ic__rlshift = new PolyIC(MagicNames.i___rlshift__);
        public PolyIC ic__rshift = new PolyIC(MagicNames.i___rshift__);
        public PolyIC ic__rrshift = new PolyIC(MagicNames.i___rrshift__);
        public PolyIC ic__hash = new PolyIC(MagicNames.i___hash__);
        public PolyIC ic__call = new PolyIC(MagicNames.i___call__);
        public PolyIC ic__contains = new PolyIC(MagicNames.i___contains__);
        public PolyIC ic__round = new PolyIC(MagicNames.i___round__);
        public PolyIC ic__reversed = new PolyIC(MagicNames.i___reversed__);
        public PolyIC ic__getitem = new PolyIC(MagicNames.i___getitem__);
        public PolyIC ic__delitem = new PolyIC(MagicNames.i___delitem__);
        public PolyIC ic__setitem = new PolyIC(MagicNames.i___setitem__);
        public PolyIC ic__finditem = new PolyIC(MagicNames.i___finditem__);
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


        #if !NOT_UNITY
        static void BuiltinClassInit_TrTraffyBehaviour(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrPolygonCollider2D(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrEventData(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrEventTriggerType(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrRawImage(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrSprite(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrFont(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrUI(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrUnityObject(TrClass cls)
        {
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrVector2(TrClass cls)
        {
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__add__(arg0));
            cls[MagicNames.i___radd__] = TrSharpFunc.FromFunc(cls.Name + ".__radd__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__radd__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__sub__(arg0));
            cls[MagicNames.i___rsub__] = TrSharpFunc.FromFunc(cls.Name + ".__rsub__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__rsub__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__mul__(arg0));
            cls[MagicNames.i___rmul__] = TrSharpFunc.FromFunc(cls.Name + ".__rmul__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__rmul__(arg0));
            cls[MagicNames.i___matmul__] = TrSharpFunc.FromFunc(cls.Name + ".__matmul__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__matmul__(arg0));
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__truediv__(arg0));
            cls[MagicNames.i___rtruediv__] = TrSharpFunc.FromFunc(cls.Name + ".__rtruediv__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__rtruediv__(arg0));
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__mod__(arg0));
            cls[MagicNames.i___rmod__] = TrSharpFunc.FromFunc(cls.Name + ".__rmod__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__rmod__(arg0));
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__pow__(arg0));
            cls[MagicNames.i___rpow__] = TrSharpFunc.FromFunc(cls.Name + ".__rpow__", (self,arg0) => ((Traffy.Unity2D.TrVector2)self).__rpow__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Unity2D.TrVector2)self).__iter__());
            cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc(cls.Name + ".__neg__", (self) => ((Traffy.Unity2D.TrVector2)self).__neg__());
            cls[MagicNames.i___abs__] = TrSharpFunc.FromFunc(cls.Name + ".__abs__", (self) => ((Traffy.Unity2D.TrVector2)self).__abs__());
        }
        #endif
        #if !NOT_UNITY
        static void BuiltinClassInit_TrVector3(TrClass cls)
        {
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__add__(arg0));
            cls[MagicNames.i___radd__] = TrSharpFunc.FromFunc(cls.Name + ".__radd__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__radd__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__sub__(arg0));
            cls[MagicNames.i___rsub__] = TrSharpFunc.FromFunc(cls.Name + ".__rsub__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__rsub__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__mul__(arg0));
            cls[MagicNames.i___rmul__] = TrSharpFunc.FromFunc(cls.Name + ".__rmul__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__rmul__(arg0));
            cls[MagicNames.i___matmul__] = TrSharpFunc.FromFunc(cls.Name + ".__matmul__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__matmul__(arg0));
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__truediv__(arg0));
            cls[MagicNames.i___rtruediv__] = TrSharpFunc.FromFunc(cls.Name + ".__rtruediv__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__rtruediv__(arg0));
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__mod__(arg0));
            cls[MagicNames.i___rmod__] = TrSharpFunc.FromFunc(cls.Name + ".__rmod__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__rmod__(arg0));
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__pow__(arg0));
            cls[MagicNames.i___rpow__] = TrSharpFunc.FromFunc(cls.Name + ".__rpow__", (self,arg0) => ((Traffy.Unity2D.TrVector3)self).__rpow__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Unity2D.TrVector3)self).__iter__());
            cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc(cls.Name + ".__neg__", (self) => ((Traffy.Unity2D.TrVector3)self).__neg__());
            cls[MagicNames.i___abs__] = TrSharpFunc.FromFunc(cls.Name + ".__abs__", (self) => ((Traffy.Unity2D.TrVector3)self).__abs__());
        }
        #endif
        static void BuiltinClassInit_TrModule_abc(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrModule_enum(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrModule_json(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrModule_types(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrModule_typing(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrModule_future(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrABC(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrBool(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrBool)self).__repr__());
            cls[MagicNames.i___int__] = TrSharpFunc.FromFunc(cls.Name + ".__int__", (self) => ((Traffy.Objects.TrBool)self).__int__());
            cls[MagicNames.i___float__] = TrSharpFunc.FromFunc(cls.Name + ".__float__", (self) => ((Traffy.Objects.TrBool)self).__float__());
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrBool)self).__hash__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrBool)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrBool)self).__ne__(arg0));
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
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__mul__(arg0));
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__contains__(arg0));
            cls[MagicNames.i___reversed__] = TrSharpFunc.FromFunc(cls.Name + ".__reversed__", (self) => ((Traffy.Objects.TrByteArray)self).__reversed__());
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__getitem__(arg0));
            cls[MagicNames.i___delitem__] = TrSharpFunc.FromFunc(cls.Name + ".__delitem__", (self,arg0) => ((Traffy.Objects.TrByteArray)self).__delitem__(arg0));
            cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc(cls.Name + ".__setitem__", (self,arg0,arg1) => ((Traffy.Objects.TrByteArray)self).__setitem__(arg0,arg1));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrByteArray)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrByteArray)self).__len__());
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
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__mul__(arg0));
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrBytes)self).__hash__());
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__contains__(arg0));
            cls[MagicNames.i___reversed__] = TrSharpFunc.FromFunc(cls.Name + ".__reversed__", (self) => ((Traffy.Objects.TrBytes)self).__reversed__());
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrBytes)self).__getitem__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrBytes)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrBytes)self).__len__());
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
            cls[MagicNames.i___or__] = TrSharpFunc.FromFunc(cls.Name + ".__or__", (self,arg0) => ((Traffy.Objects.TrClass)self).__or__(arg0));
            cls[MagicNames.i___ror__] = TrSharpFunc.FromFunc(cls.Name + ".__ror__", (self,arg0) => ((Traffy.Objects.TrClass)self).__ror__(arg0));
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrClass)self).__call__(arg0,arg1));
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrClass)self).__getitem__(arg0));
        }
        static void BuiltinClassInit_TrClassMethod(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrDict(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrDict)self).__repr__());
            cls[MagicNames.i___or__] = TrSharpFunc.FromFunc(cls.Name + ".__or__", (self,arg0) => ((Traffy.Objects.TrDict)self).__or__(arg0));
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrDict)self).__contains__(arg0));
            cls[MagicNames.i___reversed__] = TrSharpFunc.FromFunc(cls.Name + ".__reversed__", (self) => ((Traffy.Objects.TrDict)self).__reversed__());
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrDict)self).__getitem__(arg0));
            cls[MagicNames.i___delitem__] = TrSharpFunc.FromFunc(cls.Name + ".__delitem__", (self,arg0) => ((Traffy.Objects.TrDict)self).__delitem__(arg0));
            cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc(cls.Name + ".__setitem__", (self,arg0,arg1) => ((Traffy.Objects.TrDict)self).__setitem__(arg0,arg1));
            cls[MagicNames.i___finditem__] = TrSharpFunc.FromFunc(cls.Name + ".__finditem__", (self,arg0,arg1) => ((Traffy.Objects.TrDict)self).__finditem__(arg0,arg1));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrDict)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrDict)self).__len__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrDict)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrDict)self).__ne__(arg0));
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
        static void BuiltinClassInit_NotImplementedError(TrClass cls)
        {
        }
        static void BuiltinClassInit_NativeError(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrFloat(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrFloat)self).__repr__());
            cls[MagicNames.i___int__] = TrSharpFunc.FromFunc(cls.Name + ".__int__", (self) => ((Traffy.Objects.TrFloat)self).__int__());
            cls[MagicNames.i___float__] = TrSharpFunc.FromFunc(cls.Name + ".__float__", (self) => ((Traffy.Objects.TrFloat)self).__float__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__add__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__sub__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__mul__(arg0));
            cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc(cls.Name + ".__floordiv__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__floordiv__(arg0));
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__truediv__(arg0));
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__mod__(arg0));
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self,arg0) => ((Traffy.Objects.TrFloat)self).__pow__(arg0));
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrFloat)self).__hash__());
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
            cls[MagicNames.i___trynext__] = TrSharpFunc.FromFunc(cls.Name + ".__trynext__", (self,arg0) => ((Traffy.Objects.TrGenerator)self).__trynext__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrGenerator)self).__iter__());
            cls[MagicNames.i___await__] = TrSharpFunc.FromFunc(cls.Name + ".__await__", (self) => ((Traffy.Objects.TrGenerator)self).__await__());
        }
        static void BuiltinClassInit_TrInt(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrInt)self).__repr__());
            cls[MagicNames.i___int__] = TrSharpFunc.FromFunc(cls.Name + ".__int__", (self) => ((Traffy.Objects.TrInt)self).__int__());
            cls[MagicNames.i___float__] = TrSharpFunc.FromFunc(cls.Name + ".__float__", (self) => ((Traffy.Objects.TrInt)self).__float__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrInt)self).__add__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((Traffy.Objects.TrInt)self).__sub__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrInt)self).__mul__(arg0));
            cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc(cls.Name + ".__floordiv__", (self,arg0) => ((Traffy.Objects.TrInt)self).__floordiv__(arg0));
            cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc(cls.Name + ".__truediv__", (self,arg0) => ((Traffy.Objects.TrInt)self).__truediv__(arg0));
            cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc(cls.Name + ".__mod__", (self,arg0) => ((Traffy.Objects.TrInt)self).__mod__(arg0));
            cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc(cls.Name + ".__pow__", (self,arg0) => ((Traffy.Objects.TrInt)self).__pow__(arg0));
            cls[MagicNames.i___and__] = TrSharpFunc.FromFunc(cls.Name + ".__and__", (self,arg0) => ((Traffy.Objects.TrInt)self).__and__(arg0));
            cls[MagicNames.i___or__] = TrSharpFunc.FromFunc(cls.Name + ".__or__", (self,arg0) => ((Traffy.Objects.TrInt)self).__or__(arg0));
            cls[MagicNames.i___xor__] = TrSharpFunc.FromFunc(cls.Name + ".__xor__", (self,arg0) => ((Traffy.Objects.TrInt)self).__xor__(arg0));
            cls[MagicNames.i___lshift__] = TrSharpFunc.FromFunc(cls.Name + ".__lshift__", (self,arg0) => ((Traffy.Objects.TrInt)self).__lshift__(arg0));
            cls[MagicNames.i___rshift__] = TrSharpFunc.FromFunc(cls.Name + ".__rshift__", (self,arg0) => ((Traffy.Objects.TrInt)self).__rshift__(arg0));
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrInt)self).__hash__());
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
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrInt)self).__bool__());
        }
        static void BuiltinClassInit_TrIter(TrClass cls)
        {
            cls[MagicNames.i___trynext__] = TrSharpFunc.FromFunc(cls.Name + ".__trynext__", (self,arg0) => ((Traffy.Objects.TrIter)self).__trynext__(arg0));
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
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrList)self).__bool__());
        }
        static void BuiltinClassInit_TrSharpMethod(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrSharpMethod)self).__repr__());
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrSharpMethod)self).__call__(arg0,arg1));
        }
        static void BuiltinClassInit_TrAnnotatedType(TrClass cls)
        {
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrAnnotatedType)self).__getitem__(arg0));
        }
        static void BuiltinClassInit_TrEnum(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrEnum)self).__repr__());
        }
        static void BuiltinClassInit_TrTypedDict(TrClass cls)
        {
        }
        static void BuiltinClassInit_TrModule(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrModule)self).__repr__());
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrModule)self).__bool__());
        }
        static void BuiltinClassInit_TrNone(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrNone)self).__repr__());
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrNone)self).__hash__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrNone)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrNone)self).__ne__(arg0));
        }
        static void BuiltinClassInit_TrNotImplemented(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrNotImplemented)self).__repr__());
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
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrSet)self).__add__(arg0));
            cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc(cls.Name + ".__sub__", (self,arg0) => ((Traffy.Objects.TrSet)self).__sub__(arg0));
            cls[MagicNames.i___and__] = TrSharpFunc.FromFunc(cls.Name + ".__and__", (self,arg0) => ((Traffy.Objects.TrSet)self).__and__(arg0));
            cls[MagicNames.i___or__] = TrSharpFunc.FromFunc(cls.Name + ".__or__", (self,arg0) => ((Traffy.Objects.TrSet)self).__or__(arg0));
            cls[MagicNames.i___xor__] = TrSharpFunc.FromFunc(cls.Name + ".__xor__", (self,arg0) => ((Traffy.Objects.TrSet)self).__xor__(arg0));
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrSet)self).__contains__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrSet)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrSet)self).__len__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrSet)self).__eq__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrSet)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrSet)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrSet)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrSet)self).__ge__(arg0));
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrSet)self).__bool__());
        }
        static void BuiltinClassInit_TrSlice(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrSlice)self).__repr__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrSlice)self).__eq__(arg0));
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
            cls[MagicNames.i___int__] = TrSharpFunc.FromFunc(cls.Name + ".__int__", (self) => ((Traffy.Objects.TrStr)self).__int__());
            cls[MagicNames.i___float__] = TrSharpFunc.FromFunc(cls.Name + ".__float__", (self) => ((Traffy.Objects.TrStr)self).__float__());
            cls[MagicNames.i___add__] = TrSharpFunc.FromFunc(cls.Name + ".__add__", (self,arg0) => ((Traffy.Objects.TrStr)self).__add__(arg0));
            cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc(cls.Name + ".__mul__", (self,arg0) => ((Traffy.Objects.TrStr)self).__mul__(arg0));
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrStr)self).__hash__());
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrStr)self).__contains__(arg0));
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrStr)self).__getitem__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrStr)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrStr)self).__len__());
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
            cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc(cls.Name + ".__hash__", (self) => ((Traffy.Objects.TrTuple)self).__hash__());
            cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc(cls.Name + ".__contains__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__contains__(arg0));
            cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc(cls.Name + ".__getitem__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__getitem__(arg0));
            cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc(cls.Name + ".__iter__", (self) => ((Traffy.Objects.TrTuple)self).__iter__());
            cls[MagicNames.i___len__] = TrSharpFunc.FromFunc(cls.Name + ".__len__", (self) => ((Traffy.Objects.TrTuple)self).__len__());
            cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc(cls.Name + ".__eq__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__eq__(arg0));
            cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc(cls.Name + ".__ne__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__ne__(arg0));
            cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc(cls.Name + ".__lt__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__lt__(arg0));
            cls[MagicNames.i___le__] = TrSharpFunc.FromFunc(cls.Name + ".__le__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__le__(arg0));
            cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc(cls.Name + ".__gt__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__gt__(arg0));
            cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc(cls.Name + ".__ge__", (self,arg0) => ((Traffy.Objects.TrTuple)self).__ge__(arg0));
            cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc(cls.Name + ".__bool__", (self) => ((Traffy.Objects.TrTuple)self).__bool__());
        }
        static void BuiltinClassInit_TrUnionType(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrUnionType)self).__repr__());
            cls[MagicNames.i___or__] = TrSharpFunc.FromFunc(cls.Name + ".__or__", (self,arg0) => ((Traffy.Objects.TrUnionType)self).__or__(arg0));
        }
        static void BuiltinClassInit_TrFunc(TrClass cls)
        {
            cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc(cls.Name + ".__repr__", (self) => ((Traffy.Objects.TrFunc)self).__repr__());
            cls[MagicNames.i___call__] = TrSharpFunc.FromFunc(cls.Name + ".__call__", (self,arg0,arg1) => ((Traffy.Objects.TrFunc)self).__call__(arg0,arg1));
        }
        static void BuiltinClassInit<T>(TrClass cls) where T : TrObject
        {
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrTraffyBehaviour))
            {
                BuiltinClassInit_TrTraffyBehaviour(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrPolygonCollider2D))
            {
                BuiltinClassInit_TrPolygonCollider2D(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrEventData))
            {
                BuiltinClassInit_TrEventData(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrEventTriggerType))
            {
                BuiltinClassInit_TrEventTriggerType(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrRawImage))
            {
                BuiltinClassInit_TrRawImage(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrSprite))
            {
                BuiltinClassInit_TrSprite(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrFont))
            {
                BuiltinClassInit_TrFont(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrUI))
            {
                BuiltinClassInit_TrUI(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrUnityObject))
            {
                BuiltinClassInit_TrUnityObject(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrVector2))
            {
                BuiltinClassInit_TrVector2(cls);
                return;
            }
            #endif
            #if !NOT_UNITY
            if (typeof(T) == typeof(Traffy.Unity2D.TrVector3))
            {
                BuiltinClassInit_TrVector3(cls);
                return;
            }
            #endif
            if (typeof(T) == typeof(Traffy.Modules.TrModule_abc))
            {
                BuiltinClassInit_TrModule_abc(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Modules.TrModule_enum))
            {
                BuiltinClassInit_TrModule_enum(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Modules.TrModule_json))
            {
                BuiltinClassInit_TrModule_json(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Modules.TrModule_types))
            {
                BuiltinClassInit_TrModule_types(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Modules.TrModule_typing))
            {
                BuiltinClassInit_TrModule_typing(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Modules.TrModule_future))
            {
                BuiltinClassInit_TrModule_future(cls);
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
            if (typeof(T) == typeof(Traffy.Objects.NotImplementedError))
            {
                BuiltinClassInit_NotImplementedError(cls);
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
            if (typeof(T) == typeof(Traffy.Objects.TrAnnotatedType))
            {
                BuiltinClassInit_TrAnnotatedType(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrEnum))
            {
                BuiltinClassInit_TrEnum(cls);
                return;
            }
            if (typeof(T) == typeof(Traffy.Objects.TrTypedDict))
            {
                BuiltinClassInit_TrTypedDict(cls);
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
            if (typeof(T) == typeof(Traffy.Objects.TrNotImplemented))
            {
                BuiltinClassInit_TrNotImplemented(cls);
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
            ic__int = new PolyIC(MagicNames.i___int__);
            ic__float = new PolyIC(MagicNames.i___float__);
            ic__trynext = new PolyIC(MagicNames.i___trynext__);
            ic__add = new PolyIC(MagicNames.i___add__);
            ic__radd = new PolyIC(MagicNames.i___radd__);
            ic__sub = new PolyIC(MagicNames.i___sub__);
            ic__rsub = new PolyIC(MagicNames.i___rsub__);
            ic__mul = new PolyIC(MagicNames.i___mul__);
            ic__rmul = new PolyIC(MagicNames.i___rmul__);
            ic__matmul = new PolyIC(MagicNames.i___matmul__);
            ic__rmatmul = new PolyIC(MagicNames.i___rmatmul__);
            ic__floordiv = new PolyIC(MagicNames.i___floordiv__);
            ic__rfloordiv = new PolyIC(MagicNames.i___rfloordiv__);
            ic__truediv = new PolyIC(MagicNames.i___truediv__);
            ic__rtruediv = new PolyIC(MagicNames.i___rtruediv__);
            ic__mod = new PolyIC(MagicNames.i___mod__);
            ic__rmod = new PolyIC(MagicNames.i___rmod__);
            ic__pow = new PolyIC(MagicNames.i___pow__);
            ic__rpow = new PolyIC(MagicNames.i___rpow__);
            ic__and = new PolyIC(MagicNames.i___and__);
            ic__rand = new PolyIC(MagicNames.i___rand__);
            ic__or = new PolyIC(MagicNames.i___or__);
            ic__ror = new PolyIC(MagicNames.i___ror__);
            ic__xor = new PolyIC(MagicNames.i___xor__);
            ic__rxor = new PolyIC(MagicNames.i___rxor__);
            ic__lshift = new PolyIC(MagicNames.i___lshift__);
            ic__rlshift = new PolyIC(MagicNames.i___rlshift__);
            ic__rshift = new PolyIC(MagicNames.i___rshift__);
            ic__rrshift = new PolyIC(MagicNames.i___rrshift__);
            ic__hash = new PolyIC(MagicNames.i___hash__);
            ic__call = new PolyIC(MagicNames.i___call__);
            ic__contains = new PolyIC(MagicNames.i___contains__);
            ic__round = new PolyIC(MagicNames.i___round__);
            ic__reversed = new PolyIC(MagicNames.i___reversed__);
            ic__getitem = new PolyIC(MagicNames.i___getitem__);
            ic__delitem = new PolyIC(MagicNames.i___delitem__);
            ic__setitem = new PolyIC(MagicNames.i___setitem__);
            ic__finditem = new PolyIC(MagicNames.i___finditem__);
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

