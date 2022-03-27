using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Traffy.Annotations;

namespace Traffy.Objects
{

    public static class ExtTrObject
    {
        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptionsCompat.Best)]
        public static TrObject AsObject<T>(this T self) where T: TrObject
        {
            return self;
        }

        public static bool IsNone(this TrObject self) => object.ReferenceEquals(self, TrNone.Unique);
    }

    public class TraffyComparer : IEqualityComparer<TrObject>
    {
        public bool Equals(TrObject x, TrObject y)
        {
            return x.__eq__(y);
        }

        public int GetHashCode([DisallowNull] TrObject obj)
        {
            return obj.__hash__();
        }
    }

    public abstract partial class TrObject : IEquatable<TrObject>, IComparable<TrObject>
    {
        public static TrObject[] EmptyObjectArray = new TrObject[0];

        public virtual IEnumerable<(TrStr, TrObject)> GetDictItems()
        {
            var array = __array__;
            if ((object)array != null)
            {
                foreach(var fshape in this.Class.__instance_fields__)
                {
                    var i = fshape.Get.FieldIndex;
                    if (i < array.Count)
                    {
                        yield return (MK.IStr(fshape.Get.Name), array[i]);
                    }
                }
            }
            var cls = Class;
            foreach(var kv in cls.__prototype__)
            {
                var shape = kv.Value;
                if (InlineCache.PolyIC.ReadClass(cls, shape, out var value))
                {
                    yield return (MK.Str(kv.Key), value);
                }
            }
        }

        bool IEquatable<TrObject>.Equals(TrObject other)
        {
            return __eq__(other);
        }
        int IComparable<TrObject>.CompareTo(TrObject other)
        {
            if (__eq__(other)) return 0;
            if (__lt__(other)) return -1;
            return 1;
        }

        // if the object is a class, cast it.
        public TrClass AsClass => (TrClass)this;
        public abstract List<TrObject> __array__ { get; }
        public virtual object Native => this;
        public abstract TrClass Class { get; }
        Exception unsupported(string op) =>
            throw new TypeError($"{Class.Name} has no attribute '{op}'");

        // public static TrObject __new__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        // {
        //     RTS.arg_check_positional_atleast(args, 1);
        //     if (args[0] is TrClass cls)
        //     {
        //         var cls_new = cls[cls.ic__new];
        //         if (cls_new == null)
        //             throw new TypeError($"Fatal: {cls.Name} has no __new__ method");
        //         var res = cls_new.__call__(args, kwargs);
        //         return res;
        //     }
        //     throw new TypeError($"object.__new__(X): X is not a type object ({args[0].Class.Name})");
        // }

        [MagicMethod(NonInstance = true, Default = true)]
        public static TrObject __new__(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        { return RTS.object_none; }

        [MagicMethod(NonInstance = true, Default = true)]
        public static TrObject __init_subclass__(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        { return RTS.object_none; }

        [MagicMethod]
        public static TrObject __init__(TrObject self, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        { return RTS.object_none; }


        [MagicMethod(Default = true)]
        public static string __str__(TrObject self) => self.__repr__();

        [MagicMethod(Default = true)]
        public static string __repr__(TrObject self) => $"<{self.Class.Name} object>";


        [MagicMethod]
        public static bool __next__(TrObject self, TrRef refval) =>
            throw self.unsupported(nameof(__next__));

        [MagicMethod]
        public static TrObject __add__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__add__));

        [MagicMethod]
        public static TrObject __sub__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__sub__));

        [MagicMethod]
        public static TrObject __mul__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__mul__));


        [MagicMethod]
        public static TrObject __matmul__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__matmul__));

        [MagicMethod]
        public static TrObject __floordiv__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__floordiv__));


        [MagicMethod]
        public static TrObject __truediv__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__truediv__));


        [MagicMethod]
        public static TrObject __mod__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__mod__));


        [MagicMethod]
        public static TrObject __pow__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__pow__));

        // Bitwise logic operations
        [MagicMethod]
        public static TrObject __bitand__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitand__));

        [MagicMethod]
        public static TrObject __bitor__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitor__));


        [MagicMethod]
        public static TrObject __bitxor__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitxor__));




        // bit shift
        [MagicMethod]
        public static TrObject __lshift__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__lshift__));


        [MagicMethod]
        public static TrObject __rshift__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__rshift__));


        // Object protocol

        [MagicMethod(Default = true)]
        public static int __hash__(TrObject self) => self.Native.GetHashCode();


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

        [MagicMethod]
        public static TrObject __call__(TrObject self, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) =>
            throw self.unsupported(nameof(__call__));


        [MagicMethod]
        public static bool __contains__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__contains__));

        [MagicMethod]
        public static TrObject __round__(TrObject self, TrObject ndigits) =>
            throw self.unsupported(nameof(__round__));

        [MagicMethod]
        public static TrObject __reversed__(TrObject self)
        {
            var cls = self.Class;
            var meth_len = cls[cls.ic__len];
            var meth_getitem = cls[cls.ic__getitem];
            if (meth_len == null || meth_getitem == null)
                throw new TypeError($"{cls.Name} object is not reversible");
            var count = meth_len.Call(self).AsInt();
            static IEnumerator<TrObject> reversed_from_protocol(TrObject self, TrObject getitem, int count)
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    yield return getitem.Call(self, MK.Int(i));
                }
            }
            return MK.Iter(reversed_from_protocol(self, meth_getitem, count));
        }

        [MagicMethod]
        public static TrObject __getitem__(TrObject self, TrObject item) =>
            throw self.unsupported(nameof(__getitem__));


        [MagicMethod]
        public static void __delitem__(TrObject self, TrObject item) =>
            throw self.unsupported(nameof(__delitem__));

        [MagicMethod]
        public static void __setitem__(TrObject self, TrObject key, TrObject value) =>
            throw self.unsupported(nameof(__setitem__));

        // default '__iter__'
        [MagicMethod]
        public static IEnumerator<TrObject> __iter__(TrObject self) =>
            throw self.unsupported(nameof(__iter__));

        [MagicMethod]
        public static Awaitable<TrObject> __await__(TrObject self) =>
            throw self.unsupported(nameof(__await__));

        [MagicMethod]
        public static TrObject __len__(TrObject self) =>
            throw self.unsupported(nameof(__len__));

        // Comparators
        [MagicMethod(Default = true)]
        public static bool __eq__(TrObject self, TrObject other) =>
            self.Native == other.Native;



        [MagicMethod(Default = true)]
        public static bool __ne__(TrObject self, TrObject other) =>
            self.Native != other.Native;


        [MagicMethod]
        public static bool __lt__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__lt__));


        [MagicMethod]
        public static bool __le__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__le__));


        [MagicMethod]
        public static bool __gt__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__gt__));




        [MagicMethod]
        public static bool __ge__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__ge__));


        // Unary ops
        [MagicMethod]
        public static TrObject __neg__(TrObject self) =>
            throw self.unsupported(nameof(__neg__));


        [MagicMethod]
        public static TrObject __invert__(TrObject self) =>
            throw self.unsupported(nameof(__invert__));


        [MagicMethod]
        public static TrObject __pos__(TrObject self) =>
            throw self.unsupported(nameof(__pos__));


        [MagicMethod]
        public static bool __bool__(TrObject self) => true;


        [MagicMethod]
        public static TrObject __abs__(TrObject self) =>
            throw self.unsupported(nameof(__abs__));


        [MagicMethod]
        public static TrObject __enter__(TrObject self) =>
            throw self.unsupported(nameof(__enter__));


        [MagicMethod]
        public static TrObject __exit__(TrObject self, TrObject exc_type, TrObject exc_value, TrObject traceback) =>
            throw self.unsupported(nameof(__exit__));

    }

    [PyBuiltin]
    public class TrRawObject : TrUserObjectBase
    {
        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.RawObjectClassObject();
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrSharpFunc.FromFunc("object.__new__", TrRawObject.datanew);
            TrClass.TypeDict[typeof(TrRawObject)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_atleast(args, 1);
            var clsobj = (TrClass)args[0];
            if (clsobj == CLASS)
            {
                return MK.RawObject();
            }
            return clsobj[clsobj.ic__new].__call__(args, kwargs);
        }
    }

    public class TrUserObject : TrUserObjectBase
    {
        public TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ { get; } = new List<TrObject>();
        public TrUserObject(TrClass cls)
        {
            CLASS = cls;
        }
    }
}