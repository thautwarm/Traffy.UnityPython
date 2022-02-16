using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Traffy.Objects;
using static Traffy.MagicNames;

namespace Traffy.Objects
{

    using call_func = Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using method_func = Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using str_func = Func<TrObject, string>;
    using unary_func = Func<TrObject, TrObject>;
    using binary_func = Func<TrObject, TrObject, TrObject>;
    using getter = Func<TrObject, TrObject, TrRef, bool>;
    using binary_cmp = Func<TrObject, TrObject, bool>;
    using setter = Action<TrObject, TrObject, TrObject>;
    using int_conv = Func<TrObject, int>;
    using bool_conv = Func<TrObject, bool>;
    using iter_conv = Func<TrObject, IEnumerator<TrObject>>;

    public class TrClass : TrObject
    {
        public static TrClass MetaClass = null;
        public bool Fixed = false;
        public bool IsSealed = false;
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
            MetaClass = FromPrototype("type");
            MetaClass.Class = MetaClass;
            MetaClass.__call = typecall;
            MetaClass.Fixed = true;
            MetaClass.Name = "type";
        }

        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            MetaClass.SetupClass();
            ModuleInit.Prelude(MetaClass);
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
                var newCls = FromPrototype(name.value);
                RTS.init_class(cls, newCls, name, bases, ns);
                return newCls;
            }
            throw new NotImplementedException("custom metaclasses not supported yet.");
        }

        public TrClass[] __base;
        public TrClass[] __mro;
        public string Name;
        public call_func __init; // can be null
        public call_func __new;
        public method_func __init_subclass;
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
        public getter __getitem;
        public setter __setitem;
        public getter __getattr;
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

        public bool __getattr__(TrObject s, TrRef found)
        {
            TrObject lookup;
            if (this.Class.__base.Length != 0)
            {
                var mro = this.Class.__mro;
                for (int i = 0; i < mro.Length; i++)
                {
                    var get = mro[i].__dict__.TryGetValue(s, out lookup);
                    if (get)
                    {
                        found.value = lookup;
                        return true;
                    }
                }
            }
            return false;
            // throw new AttributeError($"attribute {s.__repr__()} not found.");
        }


        void SetAttr(TrObject s, TrObject value)
        {
            var MAGIC_METHODS = MAGIC_METHOD_SETTERS;
            if (MAGIC_METHODS.TryGetValue(s, out var setter))
            {
                setter(value);
                RTS.baredict_set(__dict__, s, value);
            }
            RTS.baredict_set(__dict__, s, value);
            return;
        }
        public void __setattr__(TrObject s, TrObject value)
        {

            if (Fixed)
                throw new AttributeError(this, s, $"cannot set attribute {s.__repr__()}.");

            SetAttr(s, value);

        }

        class IdComparer : IEqualityComparer<TrClass>
        {
            public bool Equals(TrClass x, TrClass y)
            {
                return object.ReferenceEquals(x, y);
            }

            public int GetHashCode([DisallowNull] TrClass obj)
            {
                return obj.GetHashCode();
            }
        }

        static IdComparer idComparer = new IdComparer();
        static TrClass[] C3_linearize(TrClass root)
        {
            var mro = new List<TrClass>();
            var visited = new HashSet<TrClass>(idComparer);
            var queue = new Queue<TrClass>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var cls = queue.Dequeue();
                if (object.ReferenceEquals(cls, MetaClass) && !object.ReferenceEquals(cls, root))
                {
                    // XXX: may be supported in the future
                    throw new TypeError($"metaclass '{cls.Name}' is not an acceptable base type of '{root.Name}'");
                }
                if (object.ReferenceEquals(cls, TrRawObject.CLASS))
                {
                    continue;
                }
                if (cls.IsSealed && !object.ReferenceEquals(cls, root))
                    throw new TypeError($"inheriting from {cls.Name} is not allowed.");
                if (visited.Contains(cls))
                    continue;
                visited.Add(cls);
                mro.Add(cls);
                if (cls.__base.Length == 0)
                    continue;
                foreach (var base_ in cls.__base)
                {
                    queue.Enqueue(base_);
                }
            }
            mro.Add(TrRawObject.CLASS);
            return mro.ToArray();
        }

        // check if '__xxx' exists in the mro class 'cls', if so, assign '__xxx' to current class.
        internal void ResolveMagicMethods(TrClass cls)
        {
            if (__str == null && cls.__str != null)
                __str = cls.__str;
            if (__repr == null && cls.__repr != null)
                __repr = cls.__repr;
            if (__next == null && cls.__next != null)
                __next = cls.__next;
            if (__add == null && cls.__add != null)
                __add = cls.__add;
            if (__sub == null && cls.__sub != null)
                __sub = cls.__sub;
            if (__mul == null && cls.__mul != null)
                __mul = cls.__mul;
            if (__matmul == null && cls.__matmul != null)
                __matmul = cls.__matmul;
            if (__floordiv == null && cls.__floordiv != null)
                __floordiv = cls.__floordiv;
            if (__truediv == null && cls.__truediv != null)
                __truediv = cls.__truediv;
            if (__mod == null && cls.__mod != null)
                __mod = cls.__mod;
            if (__pow == null && cls.__pow != null)
                __pow = cls.__pow;
            if (__bitand == null && cls.__bitand != null)
                __bitand = cls.__bitand;
            if (__bitor == null && cls.__bitor != null)
                __bitor = cls.__bitor;
            if (__bitxor == null && cls.__bitxor != null)
                __bitxor = cls.__bitxor;
            if (__lshift == null && cls.__lshift != null)
                __lshift = cls.__lshift;
            if (__rshift == null && cls.__rshift != null)
                __rshift = cls.__rshift;
            if (__hash == null && cls.__hash != null)
                __hash = cls.__hash;
            if (__call == null && cls.__call != null)
                __call = cls.__call;
            if (__contains == null && cls.__contains != null)
                __contains = cls.__contains;
            if (__getitem == null && cls.__getitem != null)
                __getitem = cls.__getitem;
            if (__setitem == null && cls.__setitem != null)
                __setitem = cls.__setitem;
            if (__getattr == null && cls.__getattr != null)
                __getattr = cls.__getattr;
            if (__setattr == null && cls.__setattr != null)
                __setattr = cls.__setattr;
            if (__pos == null && cls.__pos != null)
                __pos = cls.__pos;
            if (__bool == null && cls.__bool != null)
                __bool = cls.__bool;
            if (__neg == null && cls.__neg != null)
                __neg = cls.__neg;
            if (__inv == null && cls.__inv != null)
                __inv = cls.__inv;
            if (__init == null && cls.__init != null)
                __init = cls.__init;
            if (__eq == null && cls.__eq != null)
                __eq = cls.__eq;
            if (__lt == null && cls.__lt != null)
                __lt = cls.__lt;

        }

        internal static TrClass FromPrototype(string name, params TrClass[] bases)
        {
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = name,
                Class = MetaClass,
                __mro = new TrClass[0],
                __base = bases,
            };
            return cls;
        }

        // internal static TrClass FromPrototype<T>(params TrClass[] bases) where T : TrObject
        // {
        //     // XXX: builtin types cannot be inherited, or methods report incompatible errors
        //     var cls = new TrClass
        //     {
        //         Name = typeof(T).Name,
        //         Class = MetaClass,
        //         __mro = new TrClass[0],
        //         __base = bases,
        //         __str = a => a.__str__(),
        //         __repr = a => a.__repr__(),
        //         __next = a => a.__next__(),
        //         __add = (a, b) => a.__add__(b),
        //         __mul = (a, b) => a.__mul__(b),
        //         __matmul = (a, b) => a.__matmul__(b),
        //         __floordiv = (a, b) => a.__floordiv__(b),
        //         __truediv = (a, b) => a.__truediv__(b),
        //         __mod = (a, b) => a.__mod__(b),
        //         __pow = (a, b) => a.__pow__(b),
        //         __bitand = (a, b) => a.__bitand__(b),
        //         __bitor = (a, b) => a.__bitor__(b),
        //         __bitxor = (a, b) => a.__bitxor__(b),
        //         __lshift = (a, b) => a.__lshift__(b),
        //         __rshift = (a, b) => a.__rshift__(b),
        //         __hash = a => a.__hash__(),
        //         __call = (a, b, c) => a.__call__(b, c),
        //         __contains = (a, b) => a.__contains__(b),
        //         __getattr = (a, b, c) => a.__getattr__(b, c),
        //         __setattr = (a, b, c) => a.__setattr__(b, c),
        //         __getitem = (a, b, c) => a.__getitem__(b, c),
        //         __setitem = (a, b, c) => a.__setitem__(b, c),
        //         __iter = (a) => a.__iter__(),
        //         __len = (a) => a.__len__(),
        //         __eq = (a, b) => a.__eq__(b),
        //         __lt = (a, b) => a.__lt__(b),
        //         __neg = (a) => a.__neg__(),
        //         __inv = (a) => a.__inv__(),
        //         __pos = (a) => a.__pos__(),
        //         __bool = (a) => a.__bool__(),
        //     };
        //     return cls;
        // }

        public void SetupClass()
        {
            SetupClass(new BList<TrObject> { }, null);
        }
        public void SetupClass(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            Dictionary<TrObject, TrObject> cp_kwargs;
            if (kwargs != null)
                cp_kwargs = kwargs.Copy();
            else
            {
                cp_kwargs = null;
            }

            __mro = C3_linearize(this);
            foreach (var cls in __mro)
            {
                ResolveMagicMethods(cls);
            }



            if (__init != null)
                __dict__[s_init] = TrSharpFunc.FromFunc("__init__", __init);

            MAGIC_METHOD_SETTERS[s_init] = (o) =>
            {
                __init = o.__call__;
            };
            if (__init != null && cp_kwargs != null && cp_kwargs.TryPop(s_init, out var o_init))
                SetAttr(s_init, o_init);
            if (__new != null)
                __dict__[s_new] = TrSharpFunc.FromFunc("__new__", __new);
            MAGIC_METHOD_SETTERS[s_new] = (o) =>
            {
                __new = o.__call__;
            };
            if (__new != null && cp_kwargs != null && cp_kwargs.TryPop(s_new, out var o_new))
                SetAttr(s_new, o_new);

            if (__str != null)
                __dict__[s_str] = TrSharpFunc.FromFunc("__str__", __str);
            MAGIC_METHOD_SETTERS[s_str] = (o) =>
            {
                __str = (o) => o.Call(o).AsString();
            };
            if (__str != null && cp_kwargs != null && cp_kwargs.TryPop(s_str, out var o_str))
                SetAttr(s_str, o_str);
            if (__repr != null)
                __dict__[s_repr] = TrSharpFunc.FromFunc("__repr__", __repr);
            MAGIC_METHOD_SETTERS[s_repr] = (o) =>
            {
                __repr = (o) => o.Call(o).AsString();
            };
            if (__repr != null && cp_kwargs != null && cp_kwargs.TryPop(s_repr, out var o_repr))
                SetAttr(s_repr, o_repr);


            if (__next != null)
                __dict__[s_next] = TrSharpFunc.FromFunc("__next__", __next);
            MAGIC_METHOD_SETTERS[s_next] = (o) =>
            {
                __next = (o) => o.Call(o);
            };
            if (__next != null && cp_kwargs != null && cp_kwargs.TryPop(s_next, out var o_next))
                SetAttr(s_next, o_next);

            if (__add != null)
                __dict__[s_add] = TrSharpFunc.FromFunc("__add__", __add);
            MAGIC_METHOD_SETTERS[s_add] = (o) =>
            {
                __add = (a, b) => o.Call(a, b);
            };
            if (__add != null && cp_kwargs != null && cp_kwargs.TryPop(s_add, out var o_add))
                SetAttr(s_add, o_add);

            if (__sub != null)
                __dict__[s_sub] = TrSharpFunc.FromFunc("__sub__", __sub);
            MAGIC_METHOD_SETTERS[s_sub] = (o) =>
            {
                __sub = (a, b) => o.Call(a, b);
            };
            if (__sub != null && cp_kwargs != null && cp_kwargs.TryPop(s_sub, out var o_sub))
                SetAttr(s_sub, o_sub);

            if (__mul != null)
                __dict__[s_mul] = TrSharpFunc.FromFunc("__mul__", __mul);
            MAGIC_METHOD_SETTERS[s_mul] = (o) =>
            {
                __mul = (a, b) => o.Call(a, b);
            };
            if (__mul != null && cp_kwargs != null && cp_kwargs.TryPop(s_mul, out var o_mul))
                SetAttr(s_mul, o_mul);
            if (__matmul != null)
                __dict__[s_matmul] = TrSharpFunc.FromFunc("__matmul__", __matmul);
            MAGIC_METHOD_SETTERS[s_matmul] = (o) =>
            {
                __matmul = (a, b) => o.Call(a, b);
            };
            if (__matmul != null && cp_kwargs != null && cp_kwargs.TryPop(s_matmul, out var o_matmul))
                SetAttr(s_matmul, o_matmul);

            if (__floordiv != null)
                __dict__[s_floordiv] = TrSharpFunc.FromFunc("__floordiv__", __floordiv);
            MAGIC_METHOD_SETTERS[s_floordiv] = (o) =>
            {
                __floordiv = (a, b) => o.Call(a, b);
            };
            if (__floordiv != null && cp_kwargs != null && cp_kwargs.TryPop(s_floordiv, out var o_floordiv))
                SetAttr(s_floordiv, o_floordiv);



            if (__truediv != null)
                __dict__[s_truediv] = TrSharpFunc.FromFunc("__truediv__", __truediv);
            MAGIC_METHOD_SETTERS[s_truediv] = (o) =>
            {
                __truediv = (a, b) => o.Call(a, b);
            };
            if (__truediv != null && cp_kwargs != null && cp_kwargs.TryPop(s_truediv, out var o_truediv))
                SetAttr(s_truediv, o_truediv);
            if (__mod != null)
                __dict__[s_mod] = TrSharpFunc.FromFunc("__mod__", __mod);
            MAGIC_METHOD_SETTERS[s_mod] = (o) =>
            {
                __mod = (a, b) => o.Call(a, b);
            };
            if (__mod != null && cp_kwargs != null && cp_kwargs.TryPop(s_mod, out var o_mod))
                SetAttr(s_mod, o_mod);


            if (__pow != null)
                __dict__[s_pow] = TrSharpFunc.FromFunc("__pow__", __pow);
            MAGIC_METHOD_SETTERS[s_pow] = (o) =>
            {
                __pow = (a, b) => o.Call(a, b);
            };
            if (__pow != null && cp_kwargs != null && cp_kwargs.TryPop(s_pow, out var o_pow))
                SetAttr(s_pow, o_pow);


            if (__bitand != null)
                __dict__[s_bitand] = TrSharpFunc.FromFunc("__bitand__", __bitand);
            MAGIC_METHOD_SETTERS[s_bitand] = (o) =>
            {
                __bitand = (a, b) => o.Call(a, b);
            };
            if (__bitand != null && cp_kwargs != null && cp_kwargs.TryPop(s_bitand, out var o_bitand))
                SetAttr(s_bitand, o_bitand);
            if (__bitor != null)
                __dict__[s_bitor] = TrSharpFunc.FromFunc("__bitor__", __bitor);
            MAGIC_METHOD_SETTERS[s_bitor] = (o) =>
            {
                __bitor = (a, b) => o.Call(a, b);
            };
            if (__bitor != null && cp_kwargs != null && cp_kwargs.TryPop(s_bitor, out var o_bitor))
                SetAttr(s_bitor, o_bitor);


            if (__bitxor != null)
                __dict__[s_bitxor] = TrSharpFunc.FromFunc("__bitxor__", __bitxor);
            MAGIC_METHOD_SETTERS[s_bitxor] = (o) =>
            {
                __bitxor = (a, b) => o.Call(a, b);
            };
            if (__bitxor != null && cp_kwargs != null && cp_kwargs.TryPop(s_bitxor, out var o_bitxor))
                SetAttr(s_bitxor, o_bitxor);

            if (__lshift != null)
                __dict__[s_lshift] = TrSharpFunc.FromFunc("__lshift__", __lshift);
            MAGIC_METHOD_SETTERS[s_lshift] = (o) =>
            {
                __lshift = (a, b) => o.Call(a, b);
            };
            if (__lshift != null && cp_kwargs != null && cp_kwargs.TryPop(s_lshift, out var o_lshift))
                SetAttr(s_lshift, o_lshift);

            if (__rshift != null)
                __dict__[s_rshift] = TrSharpFunc.FromFunc("__rshift__", __rshift);
            MAGIC_METHOD_SETTERS[s_rshift] = (o) =>
            {
                __rshift = (a, b) => o.Call(a, b);
            };
            if (__rshift != null && cp_kwargs != null && cp_kwargs.TryPop(s_rshift, out var o_rshift))
                SetAttr(s_rshift, o_rshift);



            if (__hash != null)
                __dict__[s_hash] = TrSharpFunc.FromFunc("__hash__", __hash);
            MAGIC_METHOD_SETTERS[s_hash] = (o) =>
            {
                __hash = (a) => o.Call(a).AsIntUnchecked();
            };
            if (__hash != null && cp_kwargs != null && cp_kwargs.TryPop(s_hash, out var o_hash))
                SetAttr(s_hash, o_hash);


            if (__call != null)
                __dict__[s_call] = TrSharpFunc.FromFunc("__call__", __call);
            MAGIC_METHOD_SETTERS[s_call] = (o) =>
            {
                __call = (a, b, c) =>
                {
                    b.AddLeft(a);
                    var res = o.__call__(b, c);
                    b.PopLeft();
                    return res;
                };
            };
            if (__call != null && cp_kwargs != null && cp_kwargs.TryPop(s_call, out var o_call))
                SetAttr(s_call, o_call);

            if (__contains != null)
                __dict__[s_contains] = TrSharpFunc.FromFunc("__contains__", __contains);
            MAGIC_METHOD_SETTERS[s_contains] = (o) =>
            {
                __contains = (a, b) => o.Call(a, b).__bool__();
            };
            if (__contains != null && cp_kwargs != null && cp_kwargs.TryPop(s_contains, out var o_contains))
                SetAttr(s_contains, o_contains);


            if (__getitem != null)
                __dict__[s_getitem] = TrSharpFunc.FromFunc("__getitem__", __getitem);
            MAGIC_METHOD_SETTERS[s_getitem] = (o) =>
            {
                __getitem = (a, b, c) => o.Call(a, b, c).AsBool();
            };
            if (__getitem != null && cp_kwargs != null && cp_kwargs.TryPop(s_getitem, out var o_getitem))
                SetAttr(s_getitem, o_getitem);


            if (__setitem != null)
                __dict__[s_setitem] = TrSharpFunc.FromFunc("__setitem__", __setitem);
            MAGIC_METHOD_SETTERS[s_setitem] = (o) =>
            {
                __setitem = (a, b, c) => o.Call(a, b, c);
            };
            if (__setitem != null && cp_kwargs != null && cp_kwargs.TryPop(s_setitem, out var o_setitem))
                SetAttr(s_setitem, o_setitem);


            if (__getattr != null)
                __dict__[s_getattr] = TrSharpFunc.FromFunc("__getattr__", __getattr);
            MAGIC_METHOD_SETTERS[s_getattr] = (o) =>
            {
                __getattr = (a, b, c) => o.Call(a, b, c).AsBool();
            };
            if (__getattr != null && cp_kwargs != null && cp_kwargs.TryPop(s_getattr, out var o_getattr))
                SetAttr(s_getattr, o_getattr);


            if (__setattr != null)
                __dict__[s_setattr] = TrSharpFunc.FromFunc("__setattr__", __setattr);
            MAGIC_METHOD_SETTERS[s_setattr] = (o) =>
            {
                __setattr = (a, b, c) => o.Call(a, b, c);
            };
            if (__setattr != null && cp_kwargs != null && cp_kwargs.TryPop(s_setattr, out var o_setattr))
                SetAttr(s_setattr, o_setattr);


            if (__iter != null)
                __dict__[s_iter] = TrSharpFunc.FromFunc("__iter__", __iter);
            MAGIC_METHOD_SETTERS[s_iter] = (o) =>
            {
                __iter = (a) => o.Call(a).__iter__();
            };
            if (__iter != null && cp_kwargs != null && cp_kwargs.TryPop(s_iter, out var o_iter))
                SetAttr(s_iter, o_iter);



            if (__len != null)
                __dict__[s_len] = TrSharpFunc.FromFunc("__len__", __len);
            MAGIC_METHOD_SETTERS[s_len] = (o) =>
            {
                __len = (a) => o.Call(a);
            };
            if (__len != null && cp_kwargs != null && cp_kwargs.TryPop(s_len, out var o_len))
                SetAttr(s_len, o_len);


            if (__eq != null)
                __dict__[s_eq] = TrSharpFunc.FromFunc("__eq__", __eq);
            MAGIC_METHOD_SETTERS[s_eq] = (o) =>
            {
                __eq = (a, b) => o.Call(a, b).__bool__();
            };
            if (__eq != null && cp_kwargs != null && cp_kwargs.TryPop(s_eq, out var o_eq))
                SetAttr(s_eq, o_eq);


            if (__lt != null)
                __dict__[s_lt] = TrSharpFunc.FromFunc("__lt__", __lt);
            MAGIC_METHOD_SETTERS[s_lt] = (o) =>
            {
                __lt = (a, b) => o.Call(a, b).__bool__();
            };
            if (__lt != null && cp_kwargs != null && cp_kwargs.TryPop(s_lt, out var o_lt))
                SetAttr(s_lt, o_lt);


            if (__neg != null)
                __dict__[s_neg] = TrSharpFunc.FromFunc("__neg__", __neg);
            MAGIC_METHOD_SETTERS[s_neg] = (o) =>
            {
                __neg = (a) => o.Call(a);
            };
            if (__neg != null && cp_kwargs != null && cp_kwargs.TryPop(s_neg, out var o_neg))
                SetAttr(s_neg, o_neg);


            if (__inv != null)
                __dict__[s_inv] = TrSharpFunc.FromFunc("__inv__", __inv);
            MAGIC_METHOD_SETTERS[s_inv] = (o) =>
            {
                __inv = (a) => o.Call(a);
            };
            if (__inv != null && cp_kwargs != null && cp_kwargs.TryPop(s_inv, out var o_inv))
                SetAttr(s_inv, o_inv);

            if (__pos != null)
                __dict__[s_pos] = TrSharpFunc.FromFunc("__pos__", __pos);
            MAGIC_METHOD_SETTERS[s_pos] = (o) =>
            {
                __pos = (a) => o.Call(a);
            };
            if (__pos != null && cp_kwargs != null && cp_kwargs.TryPop(s_pos, out var o_pos))
                SetAttr(s_pos, o_pos);


            if (__bool != null)
                __dict__[s_bool] = TrSharpFunc.FromFunc("__bool__", __bool);
            MAGIC_METHOD_SETTERS[s_bool] = (o) =>
            {
                __bool = (a) => o.Call(a).__bool__();
            };
            if (__bool != null && cp_kwargs != null && cp_kwargs.TryPop(s_bool, out var o_bool))
                SetAttr(s_bool, o_bool);

            if (cp_kwargs != null)
                foreach (var kv in cp_kwargs)
                {
                    if (kv.Key.IsStr())
                        SetAttr(kv.Key, kv.Value);
                    else
                        throw new Exception($"Invalid keyword argument {kv.Key}");
                }

            foreach (var cls in __mro)
            {
                if (cls.__init_subclass != null)
                {
                    cls.__init_subclass(this, args, kwargs);
                    // XXX: different from CPython, we don't break here
                    // break;
                }
            }
        }
    }

}
