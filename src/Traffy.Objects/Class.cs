using System;
using System.Collections.Generic;
using Traffy.Objects;

namespace Traffy.Objects
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

    public class TrClass : TrObject
    {
        public static TrClass MetaClass = null;
        public bool Fixed = false;

        internal Dictionary<TrObject, TrObject> MAGIC_METHOD_GETTERS = RTS.baredict_create();
        internal Dictionary<TrObject, Action<TrObject>> MAGIC_METHOD_SETTERS = new Dictionary<TrObject, Action<TrObject>>(RTS.DICT_COMPARE);


        public TrObject AsObject => this as TrObject;

        public static TrObject default_datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            MetaClass = FromPrototype<TrClass>();
            MetaClass.Class = MetaClass;
            MetaClass.__call = typecall;
            MetaClass.Name = "type";
            MetaClass.SetupClass();
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
                var newCls = FromPrototype<TrObject>();
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

        public string __repr__() => Name;

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) =>
            Class.__call(this, args, kwargs);

        public TrObject __getattr__(TrObject s)
        {
            if (this.__dict__ != null)
            {
                TrObject o = RTS.baredict_get_noerror(__dict__, s);
                if (o != null)
                    return o;
            }
            Dictionary<TrObject, TrObject> MAGIC_METHODS = MAGIC_METHOD_GETTERS;
            if (MAGIC_METHODS.TryGetValue(s, out var getter))
            {
                return getter;
            }
            return null;
            // throw new AttributeError($"attribute {s.__repr__()} not found.");
        }

        public void __setattr__(TrObject s, TrObject value)
        {

            var MAGIC_METHODS = MAGIC_METHOD_SETTERS;
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

        internal static TrClass FromPrototype<T>() where T : TrObject
        {
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
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
            MAGIC_METHOD_GETTERS[MK.Str("__init__")] = TrSharpFunc.FromFunc("__init__", __init);
            MAGIC_METHOD_SETTERS[MK.Str("__init__")] = (o) =>
            {
                __init = o.__call__;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__new__")] = TrSharpFunc.FromFunc("__new__", __new);
            MAGIC_METHOD_SETTERS[MK.Str("__new__")] = (o) =>
            {
                __new = o.__call__;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__str__")] = TrSharpFunc.FromFunc("__str__", __str);
            MAGIC_METHOD_SETTERS[MK.Str("__str__")] = (o) =>
            {
                __str = (o) => o.Call(o).AsString;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__repr__")] = TrSharpFunc.FromFunc("__repr__", __repr);
            MAGIC_METHOD_SETTERS[MK.Str("__repr__")] = (o) =>
            {
                __repr = (o) => o.Call(o).AsString;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__next__")] = TrSharpFunc.FromFunc("__next__", __next);
            MAGIC_METHOD_SETTERS[MK.Str("__next__")] = (o) =>
            {
                __next = (o) => o.Call(o);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__add__")] = TrSharpFunc.FromFunc("__add__", __add);
            MAGIC_METHOD_SETTERS[MK.Str("__add__")] = (o) =>
            {
                __add = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__sub__")] = TrSharpFunc.FromFunc("__sub__", __sub);
            MAGIC_METHOD_SETTERS[MK.Str("__sub__")] = (o) =>
            {
                __sub = (a, b) => o.Call(a, b);
            };

            MAGIC_METHOD_GETTERS[MK.Str("__mul__")] = TrSharpFunc.FromFunc("__mul__", __mul);
            MAGIC_METHOD_SETTERS[MK.Str("__mul__")] = (o) =>
            {
                __mul = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__matmul__")] = TrSharpFunc.FromFunc("__matmul__", __matmul);
            MAGIC_METHOD_SETTERS[MK.Str("__matmul__")] = (o) =>
            {
                __matmul = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__floordiv__")] = TrSharpFunc.FromFunc("__floordiv__", __floordiv);
            MAGIC_METHOD_SETTERS[MK.Str("__floordiv__")] = (o) =>
            {
                __floordiv = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__truediv__")] = TrSharpFunc.FromFunc("__truediv__", __truediv);
            MAGIC_METHOD_SETTERS[MK.Str("__truediv__")] = (o) =>
            {
                __truediv = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__mod__")] = TrSharpFunc.FromFunc("__mod__", __mod);
            MAGIC_METHOD_SETTERS[MK.Str("__mod__")] = (o) =>
            {
                __mod = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__pow__")] = TrSharpFunc.FromFunc("__pow__", __pow);
            MAGIC_METHOD_SETTERS[MK.Str("__pow__")] = (o) =>
            {
                __pow = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bitand__")] = TrSharpFunc.FromFunc("__bitand__", __bitand);
            MAGIC_METHOD_SETTERS[MK.Str("__bitand__")] = (o) =>
            {
                __bitand = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bitor__")] = TrSharpFunc.FromFunc("__bitor__", __bitor);
            MAGIC_METHOD_SETTERS[MK.Str("__bitor__")] = (o) =>
            {
                __bitor = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bitxor__")] = TrSharpFunc.FromFunc("__bitxor__", __bitxor);
            MAGIC_METHOD_SETTERS[MK.Str("__bitxor__")] = (o) =>
            {
                __bitxor = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__lshift__")] = TrSharpFunc.FromFunc("__lshift__", __lshift);
            MAGIC_METHOD_SETTERS[MK.Str("__lshift__")] = (o) =>
            {
                __lshift = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__rshift__")] = TrSharpFunc.FromFunc("__rshift__", __rshift);
            MAGIC_METHOD_SETTERS[MK.Str("__rshift__")] = (o) =>
            {
                __rshift = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__hash__")] = TrSharpFunc.FromFunc("__hash__", __hash);
            MAGIC_METHOD_SETTERS[MK.Str("__hash__")] = (o) =>
            {
                __hash = (a) => o.Call(a).AsInt;
            };
            MAGIC_METHOD_GETTERS[MK.Str("__call__")] = TrSharpFunc.FromFunc("__call__", __call);
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
            MAGIC_METHOD_GETTERS[MK.Str("__contains__")] = TrSharpFunc.FromFunc("__contains__", __contains);
            MAGIC_METHOD_SETTERS[MK.Str("__contains__")] = (o) =>
            {
                __contains = (a, b) => o.Call(a, b).__bool__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__getitem__")] = TrSharpFunc.FromFunc("__getitem__", __getitem);
            MAGIC_METHOD_SETTERS[MK.Str("__getitem__")] = (o) =>
            {
                __getitem = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__setitem__")] = TrSharpFunc.FromFunc("__setitem__", __setitem);
            MAGIC_METHOD_SETTERS[MK.Str("__setitem__")] = (o) =>
            {
                __setitem = (a, b, c) => o.Call(a, b, c);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__getattr__")] = TrSharpFunc.FromFunc("__getattr__", __getattr);
            MAGIC_METHOD_SETTERS[MK.Str("__getattr__")] = (o) =>
            {
                __getattr = (a, b) => o.Call(a, b);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__setattr__")] = TrSharpFunc.FromFunc("__setattr__", __setattr);
            MAGIC_METHOD_SETTERS[MK.Str("__setattr__")] = (o) =>
            {
                __setattr = (a, b, c) => o.Call(a, b, c);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__iter__")] = TrSharpFunc.FromFunc("__iter__", __iter);
            MAGIC_METHOD_SETTERS[MK.Str("__iter__")] = (o) =>
            {
                __iter = (a) => o.Call(a).__iter__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__len__")] = TrSharpFunc.FromFunc("__len__", __len);
            MAGIC_METHOD_SETTERS[MK.Str("__len__")] = (o) =>
            {
                __len = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__eq__")] = TrSharpFunc.FromFunc("__eq__", __eq);
            MAGIC_METHOD_SETTERS[MK.Str("__eq__")] = (o) =>
            {
                __eq = (a, b) => o.Call(a, b).__bool__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__lt__")] = TrSharpFunc.FromFunc("__lt__", __lt);
            MAGIC_METHOD_SETTERS[MK.Str("__lt__")] = (o) =>
            {
                __lt = (a, b) => o.Call(a, b).__bool__();
            };
            MAGIC_METHOD_GETTERS[MK.Str("__neg__")] = TrSharpFunc.FromFunc("__neg__", __neg);
            MAGIC_METHOD_SETTERS[MK.Str("__neg__")] = (o) =>
            {
                __neg = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__inv__")] = TrSharpFunc.FromFunc("__inv__", __inv);
            MAGIC_METHOD_SETTERS[MK.Str("__inv__")] = (o) =>
            {
                __inv = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__pos__")] = TrSharpFunc.FromFunc("__pos__", __pos);
            MAGIC_METHOD_SETTERS[MK.Str("__pos__")] = (o) =>
            {
                __pos = (a) => o.Call(a);
            };
            MAGIC_METHOD_GETTERS[MK.Str("__bool__")] = TrSharpFunc.FromFunc("__bool__", __bool);
            MAGIC_METHOD_SETTERS[MK.Str("__bool__")] = (o) =>
            {
                __bool = (a) => o.Call(a).__bool__();
            };
        }

    }



}
