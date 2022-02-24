using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Traffy.InlineCache;
using Traffy.Objects;
using static Traffy.MagicNames;

namespace Traffy.Objects
{

    using call_func = Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using method_func = Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using str_func = Func<TrObject, TrObject>;
    using unary_func = Func<TrObject, TrObject>;
    using binary_func = Func<TrObject, TrObject, TrObject>;
    using getter = Func<TrObject, TrObject, TrObject, TrObject>;
    using binary_cmp = Func<TrObject, TrObject, TrObject>;
    using setter = Action<TrObject, TrObject, TrObject>;
    using int_conv = Func<TrObject, TrObject>;
    using bool_conv = Func<TrObject, TrObject>;
    using iter_conv = Func<TrObject, IEnumerator<TrObject>>;

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



    public partial class TrClass : TrObject
    {
        public static object AllClassLock = new object();
        public bool InstanceUseInlineCache = true;
        public bool IsClass => true;
        static IdComparer idComparer = new IdComparer();
        public static Dictionary<Type, TrClass> TypeDict = new Dictionary<Type, TrClass>();
        public static TrClass MetaClass = null;

        public bool IsFixed = false;
        public bool IsSealed = false;

        private object _token = new object();
        public object Token => _token;

        public object UpdatePrototype()
        {
            foreach (var subclass in __subclasses)
            {
                subclass._token = new object();
            }
            return _token;
        }

        // when shape update, update all subclasses' 'Token's;
        // include itself
        public HashSet<TrClass> __subclasses = new HashSet<TrClass>(idComparer);

        public List<TrObject> __array__ => null;

        // does not contain those that are inherited from '__mro__'
        // values are never null
        public TrClass[] __base;
        public TrClass[] __mro;

        public bool __getattr__(TrObject name, TrRef found) => this.__getic__(name.AsString(), out found.value);
        public void __setattr__(TrObject name, TrObject value)
        {
            if (IsFixed)
                throw new TypeError($"can't set attribute '{name}' on {this}");
            this.__setic__(name.AsString(), value);
        }
        public string Name;
        public bool __subclasscheck__(TrClass @class)
        {
            // XXX: future extension: allow users to define their own subclass check
            return __subclasses.Contains(@class);
        }
        public TrObject AsObject => this as TrObject;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            MetaClass = CreateClass("type");
            MetaClass.Class = MetaClass;
            MetaClass.InitInlineCacheForMagicMethods();
            MetaClass[MetaClass.ic__call] = TrClassMethod.Bind(TrSharpFunc.FromFunc("type.__call__", typecall));
            MetaClass[MetaClass.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("type.__new__", typenew));
            MetaClass.Name = "type";
            TrClass.TypeDict[typeof(TrClass)] = MetaClass;
        }

        [Mark(typeof(TrClass))]
        static void _SetupClasses()
        {
            MetaClass.SetupClass();
            MetaClass.IsFixed = true;
            Initialization.Prelude(MetaClass);
        }

        public static TrObject typecall(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrClass cls = (TrClass)args[0];

            if (cls == MetaClass && args.Count == 2 && kwargs == null)
            {
                return args[1].Class;
            }
            var cls_new = cls[cls.ic__new];
            if (cls_new == null)
                throw new TypeError($"Fatal: {cls.Name}.__new__() is not defined.");
            var o = cls_new.__call__(args, kwargs);

            var cls_init = cls[cls.ic__init];
            if (RTS.isinstanceof(o, cls) && cls_init != null)
            {
                args[0] = o;
                cls_init.__call__(args, kwargs);
                args[0] = cls;
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
                var name = args[1].AsString();
                var bases = (TrTuple)args[2];
                var ns = (TrDict)args[3];
                var newCls = RTS.new_class(name, bases.elts, ns.container);
                return newCls;
            }
            throw new NotImplementedException("custom metaclasses not supported yet.");
        }

        public TrClass Class { set; get; }

        public string __repr__() => Name;

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            // XXX: should we support metaclasses other than 'type'?
            args.AddLeft(this);
            var res = typecall(args, kwargs);
            args.PopLeft();
            return res;
        }

        public bool __getic__(TrObject s, TrRef found)
        {
            var istr = s.AsString().ToIntern();
            var o = this[istr];
            return false;
            // throw new AttributeError($"attribute {s.__repr__()} not found.");
        }

        static TrClass[] C3_linearize(TrClass root)
        {
            var mro = new List<TrClass>();
            var visited = new HashSet<TrClass>(idComparer);
            var queue = new Queue<TrClass>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var cls = queue.Dequeue();
                var is_self = object.ReferenceEquals(cls, root);
                if (is_self)
                {
                    if (visited.Contains(cls))
                    {
                        throw new TypeError($"Cycle in MRO of {root.Name}");
                    }
                }
                else
                {
                    if (object.ReferenceEquals(cls, MetaClass))
                    {
                        // XXX: may be supported in the future
                        throw new TypeError($"metaclass '{cls.Name}' is not an acceptable base type of '{root.Name}'");
                    }
                    if (object.ReferenceEquals(cls, TrRawObject.CLASS))
                    {
                        continue;
                    }
                    if (cls.IsSealed)
                        throw new TypeError($"inheriting from {cls.Name} is not allowed.");

                    if (visited.Contains(cls))
                        continue;
                }

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

        internal static TrClass CreateClass(string name, params TrClass[] bases)
        {
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = name,
                __mro = new TrClass[0],
                __base = bases,
            };
            return cls;
        }

        // use for sealed classes which shall not be inherited
        internal static TrClass FromPrototype<T>(params TrClass[] bases) where T : TrObject
        {
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = typeof(T).Name,
                __mro = new TrClass[0],
                __base = bases,
            };

            {
                // repr methods
                cls[MagicNames.i___str__] = TrSharpFunc.FromFunc($"{cls.Name}.__str__", a => ((T)a).__str__());
                cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc($"{cls.Name}.__repr__", a => ((T)a).__repr__());
                cls[MagicNames.i___add__] = TrSharpFunc.FromFunc($"{cls.Name}.__add__", (a, b) => ((T)a).__add__(b));
                cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc($"{cls.Name}.__sub__", (a, b) => ((T)a).__sub__(b));
                cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc($"{cls.Name}.__mul__", (a, b) => ((T)a).__mul__(b));
                cls[MagicNames.i___matmul__] = TrSharpFunc.FromFunc($"{cls.Name}.__matmul__", (a, b) => ((T)a).__matmul__(b));
                cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc($"{cls.Name}.__floordiv__", (a, b) => ((T)a).__floordiv__(b));
                cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc($"{cls.Name}.__truediv__", (a, b) => ((T)a).__truediv__(b));
                cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc($"{cls.Name}.__mod__", (a, b) => ((T)a).__mod__(b));
                cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc($"{cls.Name}.__pow__", (a, b) => ((T)a).__pow__(b));
                cls[MagicNames.i___bitand__] = TrSharpFunc.FromFunc($"{cls.Name}.__bitand__", (a, b) => ((T)a).__bitand__(b));
                cls[MagicNames.i___bitor__] = TrSharpFunc.FromFunc($"{cls.Name}.__bitor__", (a, b) => ((T)a).__bitor__(b));
                cls[MagicNames.i___bitxor__] = TrSharpFunc.FromFunc($"{cls.Name}.__bitxor__", (a, b) => ((T)a).__bitxor__(b));
                cls[MagicNames.i___lshift__] = TrSharpFunc.FromFunc($"{cls.Name}.__lshift__", (a, b) => ((T)a).__lshift__(b));
                cls[MagicNames.i___rshift__] = TrSharpFunc.FromFunc($"{cls.Name}.__rshift__", (a, b) => ((T)a).__rshift__(b));
                cls[MagicNames.i___next__] = TrSharpFunc.FromFunc($"{cls.Name}.__next__", a => ((T)a).__next__());
                cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc($"{cls.Name}.__hash__", a => ((T)a).__hash__());
                cls[MagicNames.i___call__] = TrSharpFunc.FromFunc($"{cls.Name}.__call__", (a, b, c) => ((T)a).__call__(b, c));
                cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc($"{cls.Name}.__contains__", (a, b) => ((T)a).__contains__(b));
                cls[MagicNames.i___getattr__] = TrSharpFunc.FromFunc($"{cls.Name}.__getattr__", (a, b, c) => ((T)a).__getattr__(b, c));
                cls[MagicNames.i___setattr__] = TrSharpFunc.FromFunc($"{cls.Name}.__setattr__", (a, b, c) => ((T)a).__setattr__(b, c));
                cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc($"{cls.Name}.__getitem__", (a, b, c) => ((T)a).__getitem__(b, c));
                cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc($"{cls.Name}.__setitem__", (a, b, c) => ((T)a).__setitem__(b, c));
                cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc($"{cls.Name}.__iter__", a => ((T)a).__iter__());
                cls[MagicNames.i___len__] = TrSharpFunc.FromFunc($"{cls.Name}.__len__", a => ((T)a).__len__());
                cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc($"{cls.Name}.__eq__", (a, b) => ((T)a).__eq__(b));
                cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc($"{cls.Name}.__ne__", (a, b) => ((T)a).__ne__(b));
                cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc($"{cls.Name}.__lt__", (a, b) => ((T)a).__lt__(b));
                cls[MagicNames.i___le__] = TrSharpFunc.FromFunc($"{cls.Name}.__le__", (a, b) => ((T)a).__le__(b));
                cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc($"{cls.Name}.__gt__", (a, b) => ((T)a).__gt__(b));
                cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc($"{cls.Name}.__ge__", (a, b) => ((T)a).__ge__(b));
                cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc($"{cls.Name}.__neg__", a => ((T)a).__neg__());
                cls[MagicNames.i___inv__] = TrSharpFunc.FromFunc($"{cls.Name}.__inv__", a => ((T)a).__invert__());
                cls[MagicNames.i___pos__] = TrSharpFunc.FromFunc($"{cls.Name}.__pos__", a => ((T)a).__pos__());
                cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc($"{cls.Name}.__bool__", a => ((T)a).__bool__());
            }

            cls.IsSealed = true;
            return cls;
        }

        static bool isRawInit = false;

        internal static TrClass RawObjectClassObject()
        {
            if (isRawInit)
                throw new InvalidOperationException("RawObjectClassObject() can only be called once");
            isRawInit = true;
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = "object",
                __mro = new TrClass[0],
                __base = new TrClass[0],
            };
            { // repr methods
                cls[MagicNames.i___str__] = TrSharpFunc.FromFunc($"object.__str__", TrObject.__raw_str__);
                cls[MagicNames.i___repr__] = TrSharpFunc.FromFunc($"object.__repr__", TrObject.__raw_repr__);
                cls[MagicNames.i___add__] = TrSharpFunc.FromFunc($"object.__add__", TrObject.__raw_add__);
                cls[MagicNames.i___sub__] = TrSharpFunc.FromFunc($"object.__sub__", TrObject.__raw_sub__);
                cls[MagicNames.i___mul__] = TrSharpFunc.FromFunc($"object.__mul__", TrObject.__raw_mul__);
                cls[MagicNames.i___matmul__] = TrSharpFunc.FromFunc($"object.__matmul__", TrObject.__raw_matmul__);
                cls[MagicNames.i___floordiv__] = TrSharpFunc.FromFunc($"object.__div__", TrObject.__raw_floordiv__);
                cls[MagicNames.i___truediv__] = TrSharpFunc.FromFunc($"object.__truediv__", TrObject.__raw_truediv__);
                cls[MagicNames.i___mod__] = TrSharpFunc.FromFunc($"object.__mod__", TrObject.__raw_mod__);
                cls[MagicNames.i___pow__] = TrSharpFunc.FromFunc($"object.__pow__", TrObject.__raw_pow__);
                cls[MagicNames.i___bitand__] = TrSharpFunc.FromFunc($"object.__bitand__", TrObject.__raw_bitand__);
                cls[MagicNames.i___bitor__] = TrSharpFunc.FromFunc($"object.__bitor__", TrObject.__raw_bitor__);
                cls[MagicNames.i___bitxor__] = TrSharpFunc.FromFunc($"object.__bitxor__", TrObject.__raw_bitxor__);
                cls[MagicNames.i___lshift__] = TrSharpFunc.FromFunc($"object.__lshift__", TrObject.__raw_lshift__);
                cls[MagicNames.i___rshift__] = TrSharpFunc.FromFunc($"object.__rshift__", TrObject.__raw_rshift__);
                cls[MagicNames.i___next__] = TrSharpFunc.FromFunc($"object.__next__", TrObject.__raw_next__);
                cls[MagicNames.i___hash__] = TrSharpFunc.FromFunc($"object.__hash__", TrObject.__raw_hash__);
                cls[MagicNames.i___call__] = TrSharpFunc.FromFunc($"object.__call__", TrObject.__raw_call__);
                cls[MagicNames.i___contains__] = TrSharpFunc.FromFunc($"object.__contains__", TrObject.__raw_contains__);
                cls[MagicNames.i___getattr__] = TrSharpFunc.FromFunc($"object.__getattr__", TrObject.__raw_getattr__);
                cls[MagicNames.i___setattr__] = TrSharpFunc.FromFunc($"object.__setattr__", TrObject.__raw_setattr__);
                cls[MagicNames.i___getitem__] = TrSharpFunc.FromFunc($"object.__getitem__", TrObject.__raw_getitem__);
                cls[MagicNames.i___setitem__] = TrSharpFunc.FromFunc($"object.__setitem__", TrObject.__raw_setitem__);
                cls[MagicNames.i___iter__] = TrSharpFunc.FromFunc($"object.__iter__", TrObject.__raw_iter__);
                cls[MagicNames.i___len__] = TrSharpFunc.FromFunc($"object.__len__", TrObject.__raw_len__);
                cls[MagicNames.i___eq__] = TrSharpFunc.FromFunc($"object.__eq__", TrObject.__raw_eq__);
                cls[MagicNames.i___ne__] = TrSharpFunc.FromFunc($"object.__ne__", TrObject.__raw_ne__);
                cls[MagicNames.i___lt__] = TrSharpFunc.FromFunc($"object.__lt__", TrObject.__raw_lt__);
                cls[MagicNames.i___le__] = TrSharpFunc.FromFunc($"object.__le__", TrObject.__raw_le__);
                cls[MagicNames.i___gt__] = TrSharpFunc.FromFunc($"object.__gt__", TrObject.__raw_gt__);
                cls[MagicNames.i___ge__] = TrSharpFunc.FromFunc($"object.__ge__", TrObject.__raw_ge__);
                cls[MagicNames.i___neg__] = TrSharpFunc.FromFunc($"object.__neg__", TrObject.__raw_neg__);
                cls[MagicNames.i___inv__] = TrSharpFunc.FromFunc($"object.__inv__", TrObject.__raw_invert__);
                cls[MagicNames.i___pos__] = TrSharpFunc.FromFunc($"object.__pos__", TrObject.__raw_pos__);
                cls[MagicNames.i___bool__] = TrSharpFunc.FromFunc($"object.__bool__", TrObject.__raw_bool__);
            }
            cls.IsSealed = true;
            return cls;
        }

        public void SetupClass()
        {
            SetupClass(null);
        }
        public void SetupClass(Dictionary<TrObject, TrObject> kwargs)
        {
            Class = MetaClass;
            __mro = C3_linearize(this);
            foreach (var cls in __mro)
            {
                cls.__subclasses.Add(this);
            }

            Dictionary<TrObject, TrObject> cp_kwargs;
            if (kwargs != null)
                cp_kwargs = kwargs.Copy();
            else
            {
                cp_kwargs = null;
            }

            if (this[ic__init] == null && cp_kwargs != null && cp_kwargs.TryPop(s_init, out var o_init))
                this[MagicNames.i___init__] = o_init;
            if (this[ic__new] == null && cp_kwargs != null && cp_kwargs.TryPop(s_new, out var o_new))
                this[MagicNames.i___new__] = o_new;
            if (this[ic__str] == null && cp_kwargs != null && cp_kwargs.TryPop(s_str, out var o_str))
                this[MagicNames.i___str__] = o_str;
            if (this[ic__repr] == null && cp_kwargs != null && cp_kwargs.TryPop(s_repr, out var o_repr))
                this[MagicNames.i___repr__] = o_repr;
            if (this[ic__add] == null && cp_kwargs != null && cp_kwargs.TryPop(s_add, out var o_add))
                this[MagicNames.i___add__] = o_add;
            if (this[ic__sub] == null && cp_kwargs != null && cp_kwargs.TryPop(s_sub, out var o_sub))
                this[MagicNames.i___sub__] = o_sub;
            if (this[ic__mul] == null && cp_kwargs != null && cp_kwargs.TryPop(s_mul, out var o_mul))
                this[MagicNames.i___mul__] = o_mul;
            if (this[ic__matmul] == null && cp_kwargs != null && cp_kwargs.TryPop(s_matmul, out var o_matmul))
                this[MagicNames.i___matmul__] = o_matmul;
            if (this[ic__floordiv] == null && cp_kwargs != null && cp_kwargs.TryPop(s_floordiv, out var o_floordiv))
                this[MagicNames.i___floordiv__] = o_floordiv;
            if (this[ic__truediv] == null && cp_kwargs != null && cp_kwargs.TryPop(s_truediv, out var o_truediv))
                this[MagicNames.i___truediv__] = o_truediv;
            if (this[ic__mod] == null && cp_kwargs != null && cp_kwargs.TryPop(s_mod, out var o_mod))
                this[MagicNames.i___mod__] = o_mod;
            if (this[ic__pow] == null && cp_kwargs != null && cp_kwargs.TryPop(s_pow, out var o_pow))
                this[MagicNames.i___pow__] = o_pow;
            if (this[ic__bitand] == null && cp_kwargs != null && cp_kwargs.TryPop(s_bitand, out var o_bitand))
                this[MagicNames.i___bitand__] = o_bitand;
            if (this[ic__bitor] == null && cp_kwargs != null && cp_kwargs.TryPop(s_bitor, out var o_bitor))
                this[MagicNames.i___bitor__] = o_bitor;
            if (this[ic__bitxor] == null && cp_kwargs != null && cp_kwargs.TryPop(s_bitxor, out var o_bitxor))
                this[MagicNames.i___bitxor__] = o_bitxor;
            if (this[ic__lshift] == null && cp_kwargs != null && cp_kwargs.TryPop(s_lshift, out var o_lshift))
                this[MagicNames.i___lshift__] = o_lshift;
            if (this[ic__rshift] == null && cp_kwargs != null && cp_kwargs.TryPop(s_rshift, out var o_rshift))
                this[MagicNames.i___rshift__] = o_rshift;
            if (this[ic__next] == null && cp_kwargs != null && cp_kwargs.TryPop(s_next, out var o_next))
                this[MagicNames.i___next__] = o_next;
            if (this[ic__call] == null && cp_kwargs != null && cp_kwargs.TryPop(s_call, out var o_call))
                this[MagicNames.i___call__] = o_call;
            if (this[ic__contains] == null && cp_kwargs != null && cp_kwargs.TryPop(s_contains, out var o_contains))
                this[MagicNames.i___contains__] = o_contains;


            if (this[ic__getattr] == null && cp_kwargs != null && cp_kwargs.TryPop(s_getattr, out var o_getattr))
            {
                InstanceUseInlineCache = false;
                this[MagicNames.i___getattr__] = o_getattr;
            }
            if (this[ic__setattr] == null && cp_kwargs != null && cp_kwargs.TryPop(s_setattr, out var o_setattr))
            {
                InstanceUseInlineCache = false;
                this[MagicNames.i___setattr__] = o_setattr;
            }
            if (this[ic__getitem] == null && cp_kwargs != null && cp_kwargs.TryPop(s_getitem, out var o_getitem))
                this[MagicNames.i___getitem__] = o_getitem;
            if (this[ic__setitem] == null && cp_kwargs != null && cp_kwargs.TryPop(s_setitem, out var o_setitem))
                this[MagicNames.i___setitem__] = o_setitem;
            if (this[ic__iter] == null && cp_kwargs != null && cp_kwargs.TryPop(s_iter, out var o_iter))
                this[MagicNames.i___iter__] = o_iter;
            if (this[ic__len] == null && cp_kwargs != null && cp_kwargs.TryPop(s_len, out var o_len))
                this[MagicNames.i___len__] = o_len;
            if (this[ic__hash] == null && cp_kwargs != null && cp_kwargs.TryPop(s_hash, out var o_hash))
                this[MagicNames.i___hash__] = o_hash;

            if (this[ic__eq] == null && cp_kwargs != null && cp_kwargs.TryPop(s_eq, out var o_eq))
            {
                if (this[ic__hash] == null)
                { // unhashable if '__eq__' is set but '__hash__' is not set
                    TrObject unhashable(TrObject self)
                    {
                        throw new TypeError($"unhashable type: '{self.Class.Name}'");
                    }
                    this[MagicNames.i___hash__] = TrSharpFunc.FromFunc($"{Class.Name}.__hash__", unhashable);
                }
                this[MagicNames.i___eq__] = o_eq;
            }

            // '__ne__' delegates automatically to '__eq__'
            if (this[ic__ne] == null && cp_kwargs != null && cp_kwargs.TryPop(s_ne, out var o_ne))
            {
                if (this[ic__hash] == null)
                { // unhashable if '__eq__' is set but '__hash__' is not set
                    TrObject unhashable(TrObject self)
                    {
                        throw new TypeError($"unhashable type: '{self.Class.Name}'");
                    }
                    this[MagicNames.i___hash__] = TrSharpFunc.FromFunc($"{Class.Name}.__hash__", unhashable);
                }
                this[MagicNames.i___ne__] = o_ne;
            }

            if (this[ic__lt] == null && cp_kwargs != null && cp_kwargs.TryPop(s_lt, out var o_lt))
                this[MagicNames.i___lt__] = o_lt;
            if (this[ic__le] == null && cp_kwargs != null && cp_kwargs.TryPop(s_le, out var o_le))
                this[MagicNames.i___le__] = o_le;
            if (this[ic__gt] == null && cp_kwargs != null && cp_kwargs.TryPop(s_gt, out var o_gt))
                this[MagicNames.i___gt__] = o_gt;
            if (this[ic__ge] == null && cp_kwargs != null && cp_kwargs.TryPop(s_ge, out var o_ge))
                this[MagicNames.i___ge__] = o_ge;

            if (this[ic__neg] == null && cp_kwargs != null && cp_kwargs.TryPop(s_neg, out var o_neg))
                this[MagicNames.i___neg__] = o_neg;
            if (this[ic__inv] == null && cp_kwargs != null && cp_kwargs.TryPop(s_inv, out var o_inv))
                this[MagicNames.i___inv__] = o_inv;
            if (this[ic__pos] == null && cp_kwargs != null && cp_kwargs.TryPop(s_pos, out var o_pos))
                this[MagicNames.i___pos__] = o_pos;
            if (this[ic__bool] == null && cp_kwargs != null && cp_kwargs.TryPop(s_bool, out var o_bool))
                this[MagicNames.i___bool__] = o_bool;


            if (cp_kwargs != null)
                foreach (var kv in cp_kwargs)
                {
                    if (kv.Key.IsStr())
                        __setattr__(kv.Key, kv.Value);
                    else
                        throw new Exception($"Invalid keyword argument {kv.Key}");
                }

            var args = new BList<TrObject> { this };
            foreach (var cls in __mro)
            {
                if (cls == this)
                    continue;
                var init_class = cls[ic__init_subclass];
                if (init_class != null)
                {
                    init_class.Call(cls, this);
                    // XXX: different from CPython, we don't break here
                    // break;
                }
            }
            args.PopLeft();
        }
    }
}
