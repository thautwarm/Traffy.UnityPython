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

        public bool __getattr__(TrObject name, TrRef found) => this.__getic__(name.AsStr(), out found.value);
        public void __setattr__(TrObject name, TrObject value)
        {
            if (IsFixed)
                throw new TypeError($"can't set attribute '{name}' on {this}");
            this.__setic__(name.AsStr(), value);
        }
        public string Name;
        public bool __subclasscheck__(TrClass @class)
        {
            // XXX: future extension: allow users to define their own subclass check
            return __subclasses.Contains(@class);
        }
        public TrObject AsObject => this as TrObject;

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            MetaClass = CreateMetaClass("type");
            MetaClass[MetaClass.ic__call] = TrClassMethod.Bind(TrSharpFunc.FromFunc("type.__call__", typecall));
            MetaClass[MetaClass.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("type.__new__", typenew));
            MetaClass.Name = "type";
            TrClass.TypeDict[typeof(TrClass)] = MetaClass;
        }

        [Traffy.Annotations.Mark(typeof(TrClass))]
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
                var name = args[1].AsStr();
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
            var istr = s.AsStr().ToIntern();
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
            cls.InitInlineCacheForMagicMethods();
            return cls;
        }

        static bool isMetaClassInitialized = false;
        internal static TrClass CreateMetaClass(string name, params TrClass[] bases)
        {
            if (isMetaClassInitialized)
                throw new TypeError("MetaClass is already initialized.");
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = name,
                __mro = new TrClass[0],
                __base = bases,
            };
            cls.InitInlineCacheForMagicMethods();
            cls.Class = cls;
            return cls;
        }


        // use for sealed classes which shall not be inherited
        internal static TrClass FromPrototype<T>(string name, params TrClass[] bases) where T : TrObject
        {
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = name,
                __mro = new TrClass[0],
                __base = bases,
            };
            cls.InitInlineCacheForMagicMethods();
            BuiltinClassInit<T>(cls);
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
            cls.InitInlineCacheForMagicMethods();
            RawClassInit(cls);
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

            if (cp_kwargs != null)
                BindMethodsFromDict(cp_kwargs);
            if (this[ic__setattr] != null)
                InstanceUseInlineCache = false;
            if (this[ic__getattr] != null)
                InstanceUseInlineCache = false;

            if (this[ic__eq] != null && this[ic__hash] == null)
            {
                TrObject unhashable(TrObject self)
                {
                    throw new TypeError($"unhashable type: '{self.Class.Name}'");
                }
                this[MagicNames.i___hash__] = TrSharpFunc.FromFunc($"{Class.Name}.__hash__", unhashable);
            }
            if (this[ic__ne] != null && this[ic__hash] == null)
            {
                TrObject unhashable(TrObject self)
                {
                    throw new TypeError($"unhashable type: '{self.Class.Name}'");
                }
                this[MagicNames.i___hash__] = TrSharpFunc.FromFunc($"{Class.Name}.__hash__", unhashable);
            }
            if (this[ic__eq] == null)
            {
                if (this[ic__ne] != null)
                {
                    // define '__eq__' using '__ne__'
                    TrObject eq(TrObject self, TrObject other)
                    {
                        return MK.Bool(!self.__ne__(other));
                    }
                    this[MagicNames.i___eq__] = TrSharpFunc.FromFunc($"{Class.Name}.__eq__", eq);
                }
            }
            else if (this[ic__ne] == null)
            {
                // define '__ne__' using '__eq__'
                TrObject ne(TrObject self, TrObject other)
                {
                    return MK.Bool(!self.__eq__(other));
                }
                this[MagicNames.i___ne__] = TrSharpFunc.FromFunc($"{Class.Name}.__ne__", ne);
            }

            if (cp_kwargs != null)
                foreach (var kv in cp_kwargs)
                {
                    if (kv.Key.IsStr())
                        __setattr__(kv.Key, kv.Value);
                    else
                        throw new Exception($"Invalid keyword argument {kv.Key}");
                }
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
        }
    }
}
