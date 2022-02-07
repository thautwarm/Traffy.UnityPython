using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Traffy
{

    using call_func = Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using method_func = Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using str_func = Func<TrObject, string>;
    using unary_func = Func<TrObject, TrObject>;
    using binary_func = Func<TrObject, TrObject, TrObject>;
    using binary_cmp = Func<TrObject, TrObject, bool>;
    using setter = Action<TrObject, TrObject, TrObject>;
    using int_conv = Func<TrObject, int>;
    using bool_conv = Func<TrObject, bool>;
    using iter_conv = Func<TrObject, IEnumerator<TrObject>>;


    public interface TrObject : IEquatable<TrObject>
    {
        bool IEquatable<TrObject>.Equals(TrObject other)
        {
            return __eq__(other);
        }

        public TrClass AsClass => (TrClass)this;
        public string AsString => ((TrStr)this).value;
        public int AsInt => unchecked((int)((TrInt)this).value);
        public Dictionary<TrObject, TrObject> __dict__ { get; }
        public object Native => this;
        public TrClass Class { get; }
        public string __str__() => __repr__();
        public string __repr__() => Native.ToString();
        public TrObject __next__() => throw unsupported_op(this, "__next__");
        static Exception unsupported_op(TrObject a, string op) =>
            new TypeError($"{a.Class.Name} does not support '{op}'");

        // Arithmetic ops
        public TrObject __add__(TrObject a)
        {
            throw unsupported_op(this, "+");
        }
        public TrObject __sub__(TrObject a)
        {
            throw unsupported_op(this, "-");
        }

        public TrObject __mul__(TrObject a)
        {
            throw unsupported_op(this, "*");
        }

        public TrObject __matmul__(TrObject a)
        {
            throw unsupported_op(this, "@");
        }

        public TrObject __floordiv__(TrObject a)
        {
            throw unsupported_op(this, "//");
        }

        public TrObject __truediv__(TrObject a)
        {
            throw unsupported_op(this, "/");
        }

        public TrObject __mod__(TrObject a)
        {
            throw unsupported_op(this, "%");
        }

        public TrObject __pow__(TrObject a)
        {
            throw unsupported_op(this, "**");
        }

        // Bitwise logic operations

        public TrObject __bitand__(TrObject a)
        {
            throw unsupported_op(this, "&");
        }

        public TrObject __bitor__(TrObject a)
        {
            throw unsupported_op(this, "|");
        }

        public TrObject __bitxor__(TrObject a)
        {
            throw unsupported_op(this, "^");
        }

        // bit shift
        public TrObject __lshift__(TrObject a)
        {
            throw unsupported_op(this, "<<");
        }

        public TrObject __rshift__(TrObject a)
        {
            throw unsupported_op(this, ">>");
        }

        // Object protocol
        public int __hash__() => Native.GetHashCode();
        public TrObject Call(params TrObject[] objs)
        {
            var xs = new BList<TrObject>();
            foreach (var e in objs)
            {
                xs.Add(e);
            }
            return __call__(xs, null);
        }

        public TrObject Call()
        {
            var xs = new BList<TrObject>();
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1)
        {
            var xs = new BList<TrObject> { a1 };
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1, TrObject a2)
        {
            var xs = new BList<TrObject> { a1, a2 };
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1, TrObject a2, TrObject a3)
        {
            var xs = new BList<TrObject> { a1, a2, a3 };
            return __call__(xs, null);
        }

        public TrObject Call(TrObject a1, TrObject a2, TrObject a3, TrObject a4)
        {
            var xs = new BList<TrObject> { a1, a2, a3, a4 };
            return __call__(xs, null);
        }


        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw unsupported_op(this, "__call__");
        }

        public bool __contains__(TrObject a)
        {
            throw unsupported_op(this, "__contains__");
        }

        public TrObject __getitem__(TrObject item)
        {
            throw unsupported_op(this, "__getitem__");
        }

        public void __setitem__(TrObject item, TrObject value)
        {
            throw unsupported_op(this, "__getitem__");
        }

        public TrObject __getattr__(TrObject s)
        {
            if (this.__dict__ != null)
            {
                TrObject o = RTS.baredict_get_noerror(__dict__, s);
                if (o != null)
                    return o;
            }
            Dictionary<TrObject, TrObject> MAGIC_METHODS = this.Class.MAGIC_METHOD_GETTERS;
            if (MAGIC_METHODS.TryGetValue(s, out var getter))
            {
                return TrSharpMethod.Bind(getter, this);
            }
            throw new AttributeError($"attribute {s.__repr__()} not found.");
        }

        public void __setattr__(TrObject s, TrObject value)
        {

            var MAGIC_METHODS = this.Class.MAGIC_METHOD_SETTERS;
            if (MAGIC_METHODS.TryGetValue(s, out var setter))
            {
                setter(value);
                return;
            }
            if (this.__dict__ != null)
            {
                RTS.baredict_set(__dict__, s, value);
                return;
            }
            throw new AttributeError($"cannot set attribute {s.__repr__()}.");
        }

        public IEnumerator<TrObject> __iter__()
        {
            throw unsupported_op(this, "__iter__");
        }

        public TrObject __len__()
        {
            throw unsupported_op(this, "__len__");
        }

        // Comparators
        public bool __eq__(TrObject o)
        {
            return Object.ReferenceEquals(o.Native, this.Native);
        }

        public bool __lt__(TrObject o)
        {
            throw unsupported_op(this, "<");
        }

        public bool __le__(TrObject o)
        {
            return __lt__(o) || __eq__(o);
        }

        // Unary ops
        public TrObject __neg__()
        {
            throw unsupported_op(this, "__neg__");
        }

        public TrObject __inv__()
        {
            throw unsupported_op(this, "__inv__");
        }

        public TrObject __pos__()
        {
            throw unsupported_op(this, "__pos__");
        }

        public bool __bool__() => true;
    }

    public class TrUserObject : TrObject
    {
        public Dictionary<TrObject, TrObject> innerDict = RTS.baredict_create();
        public object Native => this;
        public TrClass Class { get; }
        public Dictionary<TrObject, TrObject> __dict__ => innerDict;
        public string __str__() => Class.__str(this);
        public string __repr__() => Class.__repr(this);
        public TrObject __next__() => Class.__next(this);
        // Arithmetic ops
        public TrObject __add__(TrObject a) => Class.__add(this, a);
        public TrObject __sub__(TrObject a) => Class.__sub(this, a);

        public TrObject __mul__(TrObject a) => Class.__mul(this, a);
        public TrObject __floordiv__(TrObject a) => Class.__floordiv(this, a);
        public TrObject __truediv__(TrObject a) => Class.__truediv(this, a);

        public TrObject __mod__(TrObject a) => Class.__mod(this, a);

        public TrObject __pow__(TrObject a) => Class.__pow(this, a);
        // Bitwise logic operations

        public TrObject __bitand__(TrObject a) => Class.__bitand(this, a);

        public TrObject __bitor__(TrObject a) => Class.__bitor(this, a);


        public TrObject __bitxor__(TrObject a) => Class.__bitxor(this, a);


        // bit shift
        public TrObject __lshift__(TrObject a) => Class.__lshift(this, a);

        public TrObject __rshift__(TrObject a) => Class.__rshift(this, a);


        // Object protocol

        public int __hash__() => Class.__hash(this);
        public TrObject Call(params TrObject[] objs)
        {
            var xs = new BList<TrObject>();
            foreach (var e in objs)
            {
                xs.Add(e);
            }
            return __call__(xs, null);
        }

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) =>
            Class.__call(this, args, kwargs);

        public bool __contains__(TrObject a) => Class.__contains(this, a);

        public IEnumerator<TrObject> __iter__() => Class.__iter(this);
        public TrObject __len__() => Class.__len(this);
        // Comparators
        public bool __eq__(TrObject o) => Class.__eq(this, o);

        public bool __lt__(TrObject o) => Class.__lt(this, o);
        // Unary ops
        public TrObject __neg__() => Class.__neg(this);

        public TrObject __inv__() => Class.__inv(this);
        public TrObject __pos__() => Class.__pos(this);
        public bool __bool__() => Class.__bool(this);
    }
    public class TrClass : TrObject
    {
        public static TrClass MetaClass = null;
        public static TrClass IntClass = null;
        public static TrClass FloatClass = null;
        public static TrClass StrClass = null;
        public static TrClass BoolClass = null;
        public static TrClass TupleClass = null;
        public static TrClass NoneClass = null;
        public static TrClass ListClass = null;
        public static TrClass SetClass = null;
        public static TrClass DictClass = null;
        public static TrClass FuncClass = null;
        public static TrClass BuiltinFuncClass = null;
        public static TrClass BuiltinMethodClass = null;
        public static TrClass NotImplementedClass = null;
        public static TrClass SliceClass = null;

        public static TrClass GeneratorClass = null;
        public bool Fixed = false;

        internal Dictionary<TrObject, TrObject> MAGIC_METHOD_GETTERS = RTS.baredict_create();
        internal Dictionary<TrObject, Action<TrObject>> MAGIC_METHOD_SETTERS = new Dictionary<TrObject, Action<TrObject>>(RTS.DICT_COMPARE);


        public TrObject AsObject => this as TrObject;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            MetaClass = _FromPrototype<TrClass>();
            MetaClass.Class = MetaClass;
            MetaClass.__call = typecall;
            MetaClass.Name = "type";
            MetaClass.SetupClass();

            GeneratorClass = _FromPrototype<TraffyCoroutine>();
            GeneratorClass.Name = "generator";
            GeneratorClass.__new = TraffyCoroutine.datanew;
            GeneratorClass.SetupClass();

            SliceClass = _FromPrototype<TrSlice>();
            SliceClass.Name = "slice";
            SliceClass.__new = TrSlice.datanew;
            SliceClass.SetupClass();

            NotImplementedClass = _FromPrototype<TrNotImplemented>();
            NotImplementedClass.Name = "NotImplementedType";
            NotImplementedClass.__new = TrNotImplemented.datanew;
            NotImplementedClass.SetupClass();

            IntClass = _FromPrototype<TrInt>();
            IntClass.Name = "int";
            IntClass.__new = TrInt.datanew;
            IntClass.SetupClass();

            FloatClass = _FromPrototype<TrFloat>();
            FloatClass.Name = "float";
            FloatClass.__new = TrFloat.datanew;
            FloatClass.SetupClass();

            BoolClass = _FromPrototype<TrBool>();
            BoolClass.Name = "bool";
            BoolClass.__new = TrBool.datanew;
            BoolClass.SetupClass();

            StrClass = _FromPrototype<TrStr>();
            StrClass.Name = "str";
            StrClass.__new = TrStr.datanew;
            StrClass.SetupClass();

            NoneClass = _FromPrototype<TrNone>();
            NoneClass.Name = "NoneType";
            NoneClass.__new = TrNone.datanew;
            NoneClass.SetupClass();

            TupleClass = _FromPrototype<TrTuple>();
            TupleClass.Name = "tuple";
            TupleClass.__new = TrTuple.datanew;
            TupleClass.SetupClass();

            ListClass = _FromPrototype<TrList>();
            ListClass.Name = "list";
            ListClass.__new = TrList.datanew;
            ListClass.SetupClass();

            SetClass = _FromPrototype<TrSet>();
            SetClass.Name = "set";
            SetClass.__new = TrSet.datanew;
            SetClass.SetupClass();

            DictClass = _FromPrototype<TrDict>();
            DictClass.Name = "dict";
            DictClass.__new = TrDict.datanew;
            DictClass.SetupClass();

            BuiltinFuncClass = _FromPrototype<TrSharpFunc>();
            BuiltinFuncClass.Name = "builtin_function";
            BuiltinFuncClass.__new = TrSharpFunc.datanew;
            BuiltinFuncClass.SetupClass();

            BuiltinMethodClass = _FromPrototype<TrSharpFunc>();
            BuiltinMethodClass.Name = "method";
            BuiltinMethodClass.__new = TrSharpMethod.datanew;
            BuiltinMethodClass.SetupClass();
        }

        Dictionary<TrObject, TrObject> innerDict = RTS.baredict_create();
        public Dictionary<TrObject, TrObject> __dict__ => innerDict;
        public static TrObject typecall(TrObject clsobj, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrClass cls = (TrClass)clsobj;
            if (cls == MetaClass && args.Count == 1 && kwargs == null)
            {
                return args[0].Class;
            }

            args.AddLeft(cls);
            var o = cls.__new(args, kwargs);
            args.PopLeft();

            if (RTS.isinstanceof(o, cls) && cls.__init != null)
            {
                args.AddLeft(o);
                cls.__init(args, kwargs);
                args.PopLeft();
            }
            return o;
        }

        public static TrObject typenew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            // cls, name, bases, ns
            if (args.Count == 0)
            {
                throw new TypeError($"typenew cannot take zero arguments");
            }
            var cls = (TrClass)args[0];
            if (cls == MetaClass)
            {
                if (args.Count != 4)
                    throw new TypeError($"calling 'type' requires 4 arguments");
                var name = (TrStr)args[1];
                var bases = (TrTuple)args[2];
                var ns = (TrDict)args[3];
                var newCls = _FromPrototype<TrObject>();
                RTS.init_class(cls, newCls, name, bases, ns);
                return newCls;
            }
            throw new NotImplementedException("custom metaclasses not supported yet.");
        }

        public TrClass[] __base;
        public string Name;
        public call_func __init; // can be null
        public call_func __new;
        public str_func __str;
        public str_func __repr;
        public unary_func __next;
        // Arithmetic ops
        public binary_func __add;
        public binary_func __sub;
        public binary_func __mul;

        public binary_func __matmul;
        public binary_func __floordiv;

        public binary_func __truediv;
        public binary_func __mod;
        public binary_func __pow;

        // Bitwise logic operations

        public binary_func __bitand;

        public binary_func __bitor;

        public binary_func __bitxor;
        // bit shift
        public binary_func __lshift;
        public binary_func __rshift;
        // Object protocol
        public int_conv __hash;
        public method_func __call;
        public binary_cmp __contains;
        public binary_func __getitem;
        public setter __setitem;
        public binary_func __getattr;
        public setter __setattr;
        public iter_conv __iter;
        public unary_func __len;
        // Comparators
        public binary_cmp __eq;
        public binary_cmp __lt;    // Unary ops
        public unary_func __neg;
        public unary_func __inv;
        public unary_func __pos;
        public bool_conv __bool;

        public TrClass Class { set; get; }

        static TrClass _FromPrototype<T>() where T : TrObject
        {

            var cls = new TrClass
            {
                Name = typeof(T).Name,
                Class = MetaClass,
                __base = new TrClass[0],
                __str = a => a.__str__(),
                __repr = a => a.__repr__(),
                __next = a => a.__next__(),
                __add = (a, b) => a.__add__(b),
                __mul = (a, b) => a.__mul__(b),
                __matmul = (a, b) => a.__matmul__(b),
                __floordiv = (a, b) => a.__floordiv__(b),
                __truediv = (a, b) => a.__truediv__(b),
                __mod = (a, b) => a.__mod__(b),
                __pow = (a, b) => a.__pow__(b),
                __bitand = (a, b) => a.__bitand__(b),
                __bitor = (a, b) => a.__bitor__(b),
                __bitxor = (a, b) => a.__bitxor__(b),
                __lshift = (a, b) => a.__lshift__(b),
                __rshift = (a, b) => a.__rshift__(b),
                __hash = a => a.__hash__(),
                __call = (a, b, c) => a.__call__(b, c),
                __contains = (a, b) => a.__contains__(b),
                __getattr = (a, b) => a.__getattr__(b),
                __setattr = (a, b, c) => a.__setattr__(b, c),
                __getitem = (a, b) => a.__getitem__(b),
                __setitem = (a, b, c) => a.__setitem__(b, c),
                __iter = (a) => a.__iter__(),
                __len = (a) => a.__len__(),
                __eq = (a, b) => a.__eq__(b),
                __lt = (a, b) => a.__lt__(b),
                __neg = (a) => a.__neg__(),
                __inv = (a) => a.__inv__(),
                __pos = (a) => a.__pos__(),
                __bool = (a) => a.__bool__(),
            };
            return cls;
        }

        public void SetupClass()
        {
            MAGIC_METHOD_GETTERS[MK.Str("__init__")] = TrSharpFunc.FromFunc(__init);
            MAGIC_METHOD_SETTERS[MK.Str("__init__")] = (o) =>
            {
                __init = o.__call__;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__new__")] = TrSharpFunc.FromFunc(__new);
            MAGIC_METHOD_SETTERS[MK.Str("__new__")] = (o) =>
            {
                __new = o.__call__;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__str__")] = TrSharpFunc.FromFunc(__str);
            MAGIC_METHOD_SETTERS[MK.Str("__str__")] = (o) =>
            {
                __str = (o) => o.Call(o).AsString;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__repr__")] = TrSharpFunc.FromFunc(__repr);
            MAGIC_METHOD_SETTERS[MK.Str("__repr__")] = (o) =>
            {
                __repr = (o) => o.Call(o).AsString;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__next__")] = TrSharpFunc.FromFunc(__next);
            MAGIC_METHOD_SETTERS[MK.Str("__next__")] = (o) =>
            {
                __next = (o) => o.Call(o);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__add__")] = TrSharpFunc.FromFunc(__add);
            MAGIC_METHOD_SETTERS[MK.Str("__add__")] = (o) =>
            {
                __add = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__sub__")] = TrSharpFunc.FromFunc(__sub);
            MAGIC_METHOD_SETTERS[MK.Str("__sub__")] = (o) =>
            {
                __sub = (a, b) => o.Call(a, b);
            };

            MAGIC_METHOD_GETTERS[MK.Str("__mul__")] = TrSharpFunc.FromFunc(__mul);
            MAGIC_METHOD_SETTERS[MK.Str("__mul__")] = (o) =>
            {
                __mul = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__matmul__")] = TrSharpFunc.FromFunc(__matmul);
            MAGIC_METHOD_SETTERS[MK.Str("__matmul__")] = (o) =>
            {
                __matmul = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__floordiv__")] = TrSharpFunc.FromFunc(__floordiv);
            MAGIC_METHOD_SETTERS[MK.Str("__floordiv__")] = (o) =>
            {
                __floordiv = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__truediv__")] = TrSharpFunc.FromFunc(__truediv);
            MAGIC_METHOD_SETTERS[MK.Str("__truediv__")] = (o) =>
            {
                __truediv = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__mod__")] = TrSharpFunc.FromFunc(__mod);
            MAGIC_METHOD_SETTERS[MK.Str("__mod__")] = (o) =>
            {
                __mod = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__pow__")] = TrSharpFunc.FromFunc(__pow);
            MAGIC_METHOD_SETTERS[MK.Str("__pow__")] = (o) =>
            {
                __pow = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bitand__")] = TrSharpFunc.FromFunc(__bitand);
            MAGIC_METHOD_SETTERS[MK.Str("__bitand__")] = (o) =>
            {
                __bitand = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bitor__")] = TrSharpFunc.FromFunc(__bitor);
            MAGIC_METHOD_SETTERS[MK.Str("__bitor__")] = (o) =>
            {
                __bitor = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bitxor__")] = TrSharpFunc.FromFunc(__bitxor);
            MAGIC_METHOD_SETTERS[MK.Str("__bitxor__")] = (o) =>
            {
                __bitxor = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__lshift__")] = TrSharpFunc.FromFunc(__lshift);
            MAGIC_METHOD_SETTERS[MK.Str("__lshift__")] = (o) =>
            {
                __lshift = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__rshift__")] = TrSharpFunc.FromFunc(__rshift);
            MAGIC_METHOD_SETTERS[MK.Str("__rshift__")] = (o) =>
            {
                __rshift = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__hash__")] = TrSharpFunc.FromFunc(__hash);
            MAGIC_METHOD_SETTERS[MK.Str("__hash__")] = (o) =>
            {
                __hash = (a) => o.Call(a).AsInt;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__call__")] = TrSharpFunc.FromFunc(__call);
            MAGIC_METHOD_SETTERS[MK.Str("__call__")] = (o) =>
            {
                __call = (a, b, c) =>
                {
                    b.AddLeft(a);
                    var res = o.__call__(b, c);
                    b.PopLeft();
                    return res;
                };
            };
            MAGIC_METHOD_GETTERS[MK.Str("__contains__")] = TrSharpFunc.FromFunc(__contains);
            MAGIC_METHOD_SETTERS[MK.Str("__contains__")] = (o) =>
            {
                __contains = (a, b) => o.Call(a, b).__bool__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__getitem__")] = TrSharpFunc.FromFunc(__getitem);
            MAGIC_METHOD_SETTERS[MK.Str("__getitem__")] = (o) =>
            {
                __getitem = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__setitem__")] = TrSharpFunc.FromFunc(__setitem);
            MAGIC_METHOD_SETTERS[MK.Str("__setitem__")] = (o) =>
            {
                __setitem = (a, b, c) => o.Call(a, b, c);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__getattr__")] = TrSharpFunc.FromFunc(__getattr);
            MAGIC_METHOD_SETTERS[MK.Str("__getattr__")] = (o) =>
            {
                __getattr = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__setattr__")] = TrSharpFunc.FromFunc(__setattr);
            MAGIC_METHOD_SETTERS[MK.Str("__setattr__")] = (o) =>
            {
                __setattr = (a, b, c) => o.Call(a, b, c);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__iter__")] = TrSharpFunc.FromFunc(__iter);
            MAGIC_METHOD_SETTERS[MK.Str("__iter__")] = (o) =>
            {
                __iter = (a) => o.Call(a).__iter__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__len__")] = TrSharpFunc.FromFunc(__len);
            MAGIC_METHOD_SETTERS[MK.Str("__len__")] = (o) =>
            {
                __len = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__eq__")] = TrSharpFunc.FromFunc(__eq);
            MAGIC_METHOD_SETTERS[MK.Str("__eq__")] = (o) =>
            {
                __eq = (a, b) => o.Call(a, b).__bool__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__lt__")] = TrSharpFunc.FromFunc(__lt);
            MAGIC_METHOD_SETTERS[MK.Str("__lt__")] = (o) =>
            {
                __lt = (a, b) => o.Call(a, b).__bool__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__neg__")] = TrSharpFunc.FromFunc(__neg);
            MAGIC_METHOD_SETTERS[MK.Str("__neg__")] = (o) =>
            {
                __neg = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__inv__")] = TrSharpFunc.FromFunc(__inv);
            MAGIC_METHOD_SETTERS[MK.Str("__inv__")] = (o) =>
            {
                __inv = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__pos__")] = TrSharpFunc.FromFunc(__pos);
            MAGIC_METHOD_SETTERS[MK.Str("__pos__")] = (o) =>
            {
                __pos = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bool__")] = TrSharpFunc.FromFunc(__bool);
            MAGIC_METHOD_SETTERS[MK.Str("__bool__")] = (o) =>
            {
                __bool = (a) => o.Call(a).__bool__();
            };
        }

    }



}
