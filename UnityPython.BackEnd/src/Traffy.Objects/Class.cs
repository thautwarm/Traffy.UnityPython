using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using InlineHelper;
using Traffy.InlineCache;
using Traffy.Objects;
using static Traffy.MagicNames;


namespace Traffy.Objects
{

    using binary_cmp = Func<TrObject, TrObject, TrObject>;
    using binary_func = Func<TrObject, TrObject, TrObject>;
    using bool_conv = Func<TrObject, TrObject>;
    using call_func = Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using getter = Func<TrObject, TrObject, TrObject, TrObject>;
    using int_conv = Func<TrObject, TrObject>;
    using iter_conv = Func<TrObject, IEnumerator<TrObject>>;
    using method_func = Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject>;
    using setter = Action<TrObject, TrObject, TrObject>;
    using str_func = Func<TrObject, TrObject>;
    using unary_func = Func<TrObject, TrObject>;


    [Traffy.Annotations.PyBuiltin]
    public sealed partial class TrClass : TrObject, IEquatable<TrClass>
    {

        public override IEnumerable<(TrStr Name, TrObject Ob)> GetDictItems(HashSet<string> visited)
        {
            foreach(var cls in __mro)
            foreach (var kv in cls.__prototype__)
            {
                var shape = kv.Value;
                if (InlineCache.PolyIC.ReadClass(cls, shape, out var value))
                {
                    if (!visited.Contains(kv.Key))
                    {
                        visited.Add(kv.Key);
                        yield return (MK.Str(kv.Key), value);
                    }
                }
            }
        }

        bool IEquatable<TrClass>.Equals(Traffy.Objects.TrClass other)
        {
            return object.ReferenceEquals(this, other);
        }

        static IdComparer idComparer = new IdComparer();
        #region static fields that can be reinitialized
        static bool isRawInit;
        static bool isMetaClassInitialized;
        static int classCnt = 0;
        static TrClass MetaClass;
        static object GlobalLock;
        #endregion
        public static void BeforeReInitRuntime()
        {
            isRawInit = false;
            isMetaClassInitialized = false;
            MetaClass = null;
            classCnt = 0;
            GlobalLock = new object();
        }

        public static TrClass CLASS { get => MetaClass; set => MetaClass = value; }
        public bool IsClassFixed = false;
        public bool IsInstanceFixed = false;
        public bool IsSealed = false;
        public bool IsAbstract = false;
        public int ClassId;
        private object _token = new object();
        public object Token => _token;

        // when shape update, update all subclasses' 'Token's;
        // include itself
        public HashSet<TrClass> __subclasses = new HashSet<TrClass>(idComparer);
        public object UpdatePrototype()
        {
            foreach (var subclass in __subclasses)
            {
                subclass._token = new object();
            }
            return _token;
        }

        // use for enumerations
        public (TrStr Name, TrObject Value)[] EnumHelperField = null;
        // use for components
        #if !NOT_UNITY
        public enum UnityComponentClassKind: System.Byte
        {
            NotUnity,
            UserComponent,
            BuiltinComponent
        }

        public delegate bool __GET_COMPONENT__(TrClass klass, Traffy.Unity2D.TrGameObject uo, out Traffy.Unity2D.TrUnityComponent component);
        public delegate bool __GET_COMPONENTS__(TrClass klass, Traffy.Unity2D.TrGameObject uo, out IEnumerable<Traffy.Unity2D.TrUnityComponent> components);
        public UnityComponentClassKind UnityKind = UnityComponentClassKind.NotUnity;
        public __GET_COMPONENT__ __get_component__ = null;
        public __GET_COMPONENTS__ __get_components__ = null;
        public Func<TrClass, Traffy.Unity2D.TrGameObject, Traffy.Unity2D.TrUnityComponent> __add_component__ = null;
        #endif

        public override List<TrObject> __array__ => null;

        // does not contain those that are inherited from '__mro__'
        // values are never null
        public FArray<TrClass> __base;
        public TrClass[] __mro;
        public string Name;
        public bool __subclasscheck__(TrClass @class)
        {
            // XXX: future extension: allow users to define their own subclass check
            return __subclasses.Contains(@class);
        }
        public TrObject AsObject => this as TrObject;



        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            MetaClass = CreateMetaClass("type");
            MetaClass.Name = "type";
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            MetaClass[MetaClass.ic__call] = TrClassMethod.Bind(TrSharpFunc.FromFunc("type.__call__", typecall));
            MetaClass[MetaClass.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("type.__new__", typenew));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            MetaClass.SetupClass();
            MetaClass.IsClassFixed = true;
            MetaClass.IsInstanceFixed = true;
            Initialization.Prelude(MetaClass);
        }

        public static TrObject typecall(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {

            TrClass cls = (TrClass)args[0];

            if (cls == MetaClass && args.Count == 2 && kwargs == null)
            {
                return args[1].Class;
            }
            if (!cls.__getic__(cls.ic__new, out var cls_new))
                throw new TypeError($"Fatal: {cls.Name}.__new__() is not defined.");
            var o = cls_new.__call__(args, kwargs);

            if (RTS.isinstanceof(o, cls) && cls.__getic__(cls.ic__init, out var cls_init))
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
            var narg = args.Count;
            if (narg == 0)
            {
                throw new TypeError($"typenew cannot take zero arguments");
            }
            var cls = (TrClass)args[0];
            if (cls == MetaClass)
            {
                if (narg != 4)
                    throw new TypeError($"calling 'type' requires 4 arguments");
                var name = args[1].AsStr();
                var bases = (TrTuple)args[2];
                var ns = (TrDict)args[3];
                var newCls = RTS.new_class(name, bases.elts, ns.container);
                return newCls;
            }
            throw new NotImplementedException("custom metaclasses not supported yet.");
        }

        public static TrObject new_notallow(TrObject cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {

            throw new TypeError($"manually instantiating {cls.__repr__()} objects is not allowed.");

        }

        public override TrClass Class => MetaClass;

        public override TrObject __getitem__(TrObject item)
        {
            return this;
        }

        TrObject type_union(TrObject a)
        {
            if (a.IsNone())
            {
                return MK.UnionType(this, TrNone.CLASS);
            }
            if (a is TrClass || a is TrUnionType)
            {
                return MK.UnionType(this, a);
            }
            return TrNotImplemented.Unique;
        }
        public override TrObject __or__(Traffy.Objects.TrObject a) => type_union(a);
        public override TrObject __ror__(TrObject a) => type_union(a);
        public override string __repr__() => Name;
        public override TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            // XXX: should we support metaclasses other than 'type'?
            args.AddLeft(this);
            var res = typecall(args, kwargs);
            args.PopLeft();
            return res;
        }

        internal static TrClass[] C3Linearized(TrClass root)
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
                        throw new TypeError($"inheriting from sealed class {cls.Name} is not allowed.");

                    if (visited.Contains(cls))
                        continue;
                }

                visited.Add(cls);
                mro.Add(cls);
                if (cls.__base.Count == 0)
                    continue;
                foreach (var base_ in cls.__base)
                {
                    queue.Enqueue(base_);
                }
            }
            mro.Add(TrRawObject.CLASS);
            return mro.ToArray();
        }

        internal static TrClass CreateClass(string name, params TrClass[] parents)
        {
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = name,
                __mro = new TrClass[0],
                __base = parents
            };
            lock (GlobalLock)
            {   
                cls.ClassId = classCnt++;
            }
            if (parents.Length == 0)
                cls.__mro = new TrClass[] { cls };
            cls.InitInlineCacheForMagicMethods();
            return cls;
        }


        internal static TrClass CreateMetaClass(string name)
        {
            if (isMetaClassInitialized)
                throw new TypeError("MetaClass is already initialized.");
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = name,
                __mro = new TrClass[1],
                __base = new TrClass[0],
            };
            lock (GlobalLock)
            {   
                cls.ClassId = classCnt++;
            }
            cls.__mro[0] = cls;
            cls.InitInlineCacheForMagicMethods();
            return cls;
        }


        // use for sealed classes which shall not be inherited
        internal static TrClass FromPrototype<T>(string name) where T : TrObject
        {
            // XXX: builtin types cannot be inherited, or methods report incompatible errors
            var cls = new TrClass
            {
                Name = name,
                __mro = new TrClass[1],
                __base = new TrClass[0],
            };
            lock (GlobalLock)
            {   
                cls.ClassId = classCnt++;
            }
            cls.__mro[0] = cls;
            cls.InitInlineCacheForMagicMethods();
            BuiltinClassInit<T>(cls);
            cls.IsSealed = true;
            return cls;
        }

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
            lock (GlobalLock)
            {   
                cls.ClassId = classCnt++;
            }
            cls.InitInlineCacheForMagicMethods();
            RawClassInit(cls);
            cls.IsSealed = true;
            return cls;
        }


        bool IsSet(InternedString name)
        {
            return IsSet(name.Value);
        }
        bool IsSet(string name)
        {
            return IsSet(name, out _);
        }

        bool IsSet(InternedString name, out TrObject o)
        {
            return IsSet(name.Value, out o);
        }

        bool IsSet(string name, out TrObject o)
        {
            var o_name = MK.Str(name);
            if (__getic_refl__(o_name, out var found))
            {
                if (TrRawObject.CLASS.__getic_refl__(o_name, out var foundFromObject))
                {
                    if (!Object.ReferenceEquals(found, foundFromObject))
                    {
                        o = found;
                        return true;
                    }
                }
                else
                {
                    o = found;
                    return true;
                }
            }
            o = null;
            return false;
        }


        public void SetupUserClass(Dictionary<TrObject, TrObject> kwargs = null)
        {
            if (kwargs != null
                && !this.__base.Contains(Traffy.Interfaces.Callable.CLASS)
                && kwargs.TryGetValue(MagicNames.s_call, out var callmethod)
                && !callmethod.IsNone())
            {
                this.__base = this.__base.Append(Traffy.Interfaces.Callable.CLASS).ToArray();
            }
            __mro = C3Linearized(this);
            SetupClass(kwargs);
        }
        internal void SetupClass()
        {
            SetupClass(null);
        }

        internal void SetupClass(Dictionary<TrObject, TrObject> kwargs)
        {
            UpdatePrototype();
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

            if (this.IsSet(i___eq__) && !this.IsSet(i___hash__))
            {
                this[MagicNames.i___hash__] = MK.None();
            }
            if (this.IsSet(i___ne__) && !this.IsSet(i___hash__))
            {
                this[MagicNames.i___hash__] = MK.None();
            }

            if (this.IsSet(i___eq__))
            {
                if (!this.IsSet(i___ne__))
                {
                    static TrObject default_ne(TrObject self, TrObject other)
                    {
                        return MK.Bool(!self.__eq__(other));
                    }
                    this[MagicNames.i___ne__] = TrSharpFunc.FromFunc($"{Class.Name}.__ne__", default_ne);
                }
            }
            else if (this.IsSet(i___ne__))
            {
                if (!this.IsSet(i___eq__))
                {
                    static TrObject default_eq(TrObject self, TrObject other)
                    {
                        return MK.Bool(!self.__eq__(other));
                    }
                    this[MagicNames.i___eq__] = TrSharpFunc.FromFunc($"{Class.Name}.__eq__", default_eq);
                }
            }

            if (cp_kwargs != null)
                foreach (var kv in cp_kwargs)
                {
                    if (kv.Key.IsStr())
                        RTS.object_setattr(this, kv.Key, kv.Value);
                    else
                        throw new TypeError($"Invalid keyword argument {kv.Key}");
                }

            HashSet<TrClass> abs_classes = new HashSet<TrClass>(RTS.Py_COMPARER);
            foreach (var cls in __mro)
            {
                if (object.ReferenceEquals(cls, this))
                    continue;
                if (cls.IsAbstract)
                {
                    abs_classes.Add(cls);
                    continue;
                }
                var init_class = cls[cls.ic__init_subclass];
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
