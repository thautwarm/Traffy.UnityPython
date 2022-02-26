using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Traffy.Annotations;

namespace Traffy.Objects
{

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

    public partial interface TrObject : IEquatable<TrObject>, IComparable<TrObject>
    {
        public static TrObject[] EmptyObjectArray = new TrObject[0];

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
        public TrClass AsClass => (TrClass)this;
        public bool IsClass => false;
        public List<TrObject> __array__ => null;
        public object Native => this;
        public TrClass Class { get; }
        Exception unsupported(string op) =>
            throw new TypeError($"{Class.Name} has no {op} method");

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

        [MagicMethod]
        public static TrObject __init__(TrObject self, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        { return RTS.object_none; }


        [MagicMethod]
        public static string __str__(TrObject self) => self.__repr__();


        public string __str__() => __str__(this);

        [MagicMethod]
        public static string __repr__(TrObject self) => $"<{self.Class.Name} object>";

        public string __repr__() => __repr__(this);

        [MagicMethod]
        public static TrObject __next__(TrObject self) =>
            throw self.unsupported(nameof(__next__));


        public TrObject __next__() => TrObject.__next__(this);

        [MagicMethod]
        public static TrObject __add__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__add__));


        public TrObject __add__(TrObject a) => TrObject.__add__(this, a);

        [MagicMethod]
        public static TrObject __sub__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__sub__));

        public TrObject __sub__(TrObject a) => TrObject.__sub__(this, a);

        [MagicMethod]
        public static TrObject __mul__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__mul__));

        public TrObject __mul__(TrObject a) => TrObject.__mul__(this, a);

        [MagicMethod]
        public static TrObject __matmul__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__matmul__));
        public TrObject __matmul__(TrObject a) => TrObject.__matmul__(this, a);

        [MagicMethod]
        public static TrObject __floordiv__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__floordiv__));

        public TrObject __floordiv__(TrObject a) => TrObject.__floordiv__(this, a);

        [MagicMethod]
        public static TrObject __truediv__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__truediv__));


        public TrObject __truediv__(TrObject a) => TrObject.__truediv__(this, a);

        [MagicMethod]
        public static TrObject __mod__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__mod__));


        public TrObject __mod__(TrObject a) => TrObject.__mod__(this, a);

        [MagicMethod]
        public static TrObject __pow__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__pow__));
        public TrObject __pow__(TrObject a) => TrObject.__pow__(this, a);

        // Bitwise logic operations
        [MagicMethod]
        public static TrObject __bitand__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitand__));
        public TrObject __bitand__(TrObject a) => TrObject.__bitand__(this, a);

        [MagicMethod]
        public static TrObject __bitor__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitor__));
        public TrObject __bitor__(TrObject a) => TrObject.__bitor__(this, a);


        [MagicMethod]
        public static TrObject __bitxor__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitxor__));
        public TrObject __bitxor__(TrObject a) => TrObject.__bitxor__(this, a);



        // bit shift
        [MagicMethod]
        public static TrObject __lshift__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__lshift__));
        public TrObject __lshift__(TrObject a) => TrObject.__lshift__(this, a);

        [MagicMethod]
        public static TrObject __rshift__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__rshift__));
        public TrObject __rshift__(TrObject a) => TrObject.__rshift__(this, a);

        // Object protocol

        [MagicMethod]
        public static int __hash__(TrObject self) => self.Native.GetHashCode();
        public int __hash__() => TrObject.__hash__(this);

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
        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) => TrObject.__call__(this, args, kwargs);

        [MagicMethod]
        public static bool __contains__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__contains__));
        public bool __contains__(TrObject a) => TrObject.__contains__(this, a);
        public bool __getitem__(TrObject item, out TrObject found)
        {
            var reference = MK.Ref();
            var res = __getitem__(item, reference);
            found = reference.value;
            return res;
        }

        [MagicMethod]
        public static bool __getitem__(TrObject self, TrObject item, TrRef found) =>
            throw self.unsupported(nameof(__getitem__));
        public bool __getitem__(TrObject item, TrRef found) => TrObject.__getitem__(this, item, found);


        [MagicMethod]
        public static void __setitem__(TrObject self, TrObject key, TrObject value) =>
            throw self.unsupported(nameof(__setitem__));
        public void __setitem__(TrObject key, TrObject value) => TrObject.__setitem__(this, key, value);

        [MagicMethod]
        public static bool __getattr__(TrObject self, TrObject name, TrRef found)
        {
            return self.__getic__(name.AsStr(), out found.value);
        }


        [MagicMethod]
        public static void __setattr__(TrObject self, TrObject name, TrObject value)
        {
            self.__setic__(name.AsStr(), value);
        }

        public bool __getattr__(TrObject name, TrRef found) => __getattr__(this, name, found);
        public void __setattr__(TrObject name, TrObject value) => __setattr__(this, name, value);

        // default '__iter__'
        [MagicMethod]
        public static IEnumerator<TrObject> __iter__(TrObject self) =>
            throw self.unsupported(nameof(__iter__));
        public IEnumerator<TrObject> __iter__() => TrObject.__iter__(this);


        [MagicMethod]
        public static TrObject __len__(TrObject self) =>
            throw self.unsupported(nameof(__len__));
        public TrObject __len__() => TrObject.__len__(this);

        // Comparators
        [MagicMethod]
        public static bool __eq__(TrObject self, TrObject other) =>
            self.Native == other.Native;
        public bool __eq__(TrObject other) => TrObject.__eq__(this, other);


        [MagicMethod]
        public static bool __ne__(TrObject self, TrObject other) =>
            self.Native != other.Native;
        public bool __ne__(TrObject other) => TrObject.__ne__(this, other);

        [MagicMethod]
        public static bool __lt__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__lt__));
        public bool __lt__(TrObject other) => TrObject.__lt__(this, other);

        [MagicMethod]
        public static bool __le__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__le__));
        public bool __le__(TrObject other) => TrObject.__le__(this, other);

        [MagicMethod]
        public static bool __gt__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__gt__));

        public bool __gt__(TrObject other) => TrObject.__gt__(this, other);


        [MagicMethod]
        public static bool __ge__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__ge__));
        public bool __ge__(TrObject other) => TrObject.__ge__(this, other);

        // Unary ops
        [MagicMethod]
        public static TrObject __neg__(TrObject self) =>
            throw self.unsupported(nameof(__neg__));
        public TrObject __neg__() => TrObject.__neg__(this);

        [MagicMethod]
        public static TrObject __invert__(TrObject self) =>
            throw self.unsupported(nameof(__invert__));
        public TrObject __invert__() => TrObject.__invert__(this);

        [MagicMethod]
        public static TrObject __pos__(TrObject self) =>
            throw self.unsupported(nameof(__pos__));
        public TrObject __pos__() => TrObject.__pos__(this);

        [MagicMethod]
        public static bool __bool__(TrObject self) => true;
        public bool __bool__() => TrObject.__bool__(this);

        [MagicMethod]
        public static TrObject __abs__(TrObject self) =>
            throw self.unsupported(nameof(__abs__));
        public TrObject __abs__() => TrObject.__abs__(this);

        [MagicMethod]
        public static TrObject __enter__(TrObject self) =>
            throw self.unsupported(nameof(__enter__));
        public TrObject __enter__() => TrObject.__enter__(this);

        [MagicMethod]
        public static TrObject __exit__(TrObject self, TrObject exc_type, TrObject exc_value, TrObject traceback) =>
            throw self.unsupported(nameof(__exit__));

        public TrObject __exit__(TrObject exc_type, TrObject exc_value, TrObject traceback) => TrObject.__exit__(this, exc_type, exc_value, traceback);

    }

    public class TrRawObject : TrUserObjectBase
    {
        internal static TrClass CLASS;
        TrClass TrObject.Class => CLASS;

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.RawObjectClassObject();
            CLASS.InitInlineCacheForMagicMethods();
            CLASS.Name = "object";
            CLASS[CLASS.ic__new] = TrSharpFunc.FromFunc("object.__new__", TrRawObject.datanew);
            TrClass.TypeDict[typeof(TrRawObject)] = CLASS;
        }
        [Traffy.Annotations.Mark(typeof(TrRawObject))]
        static void _SetupClasses()
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
        TrClass TrObject.Class => CLASS;

        List<TrObject> TrObject.__array__ { get; } = new List<TrObject>();
        public TrUserObject(TrClass cls)
        {
            CLASS = cls;
        }
    }
}