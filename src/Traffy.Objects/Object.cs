using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

    public partial interface TrObject : IEquatable<TrObject>
    {
        bool IEquatable<TrObject>.Equals(TrObject other)
        {
            return __eq__(other);
        }


        public TrClass AsClass => (TrClass)this;

        public bool IsClass => false;

        public List<TrObject> __array__ { get; }
        public object Native => this;
        public TrClass Class { get; }
        Exception unsupported(string op) =>
            throw new TypeError($"{Class.Name} has no {op} method");

        public static bool __instancecheck__(TrObject obj, TrObject classes)
        {
            if (classes is TrClass cls)
            {
                return cls.__subclasscheck__(obj.Class);
            }
            else if (classes is TrTuple tup)
            {
                foreach (var cls_ in tup.elts)
                {
                    if (__instancecheck__(obj, cls_))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                throw new TypeError($"{classes.__repr__()} is not a class or tuple of classes");
            }
        }

        public bool __instancecheck__(TrObject classes) => __instancecheck__(this, classes);

        public static TrObject __raw_init__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        { return RTS.object_none; }

        public static TrObject __raw_new__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_atleast(args, 1);
            if (args[0] is TrClass cls)
            {
                var cls_new = cls[cls.ic__new];
                if (cls_new == null)
                    throw new TypeError($"Fatal: {cls.Name} has no __new__ method");
                var res = cls_new.__call__(args, kwargs);
                return res;
            }
            throw new TypeError($"object.__new__(X): X is not a type object ({args[0].Class.Name})");
        }

        // default '__str__'
        public static string __raw_str__(TrObject self) => self.__repr__();
        public string __str__() => __raw_str__(this);

        // default '__repr__'
        public static string __raw_repr__(TrObject self) => self.Native.ToString();
        public string __repr__() => __raw_repr__(this);

        // default '__next__'
        public static TrObject __raw_next__(TrObject self) =>
            throw self.unsupported(nameof(__next__));
        public TrObject __next__() => TrObject.__raw_next__(this);

        // Arithmetic ops
        // default '__add__'
        public static TrObject __raw_add__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__add__));
        public TrObject __add__(TrObject a) => TrObject.__raw_add__(this, a);
        // default '__sub__'
        public static TrObject __raw_sub__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__sub__));
        public TrObject __sub__(TrObject a) => TrObject.__raw_sub__(this, a);

        // default '__mul__'
        public static TrObject __raw_mul__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__mul__));
        public TrObject __mul__(TrObject a) => TrObject.__raw_mul__(this, a);

        // default '__matmul__'
        public static TrObject __raw_matmul__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__matmul__));
        public TrObject __matmul__(TrObject a) => TrObject.__raw_matmul__(this, a);

        // default '__floordiv__'
        public static TrObject __raw_floordiv__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__floordiv__));
        public TrObject __floordiv__(TrObject a) => TrObject.__raw_floordiv__(this, a);

        // default '__truediv__'
        public static TrObject __raw_truediv__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__truediv__));
        public TrObject __truediv__(TrObject a) => TrObject.__raw_truediv__(this, a);

        // default '__mod__'
        public static TrObject __raw_mod__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__mod__));

        public TrObject __mod__(TrObject a) => TrObject.__raw_mod__(this, a);

        // default '__pow__'
        public static TrObject __raw_pow__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__pow__));
        public TrObject __pow__(TrObject a) => TrObject.__raw_pow__(this, a);

        // Bitwise logic operations
        // default '__bitand__'
        public static TrObject __raw_bitand__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitand__));
        public TrObject __bitand__(TrObject a) => TrObject.__raw_bitand__(this, a);

        // default '__bitor__'
        public static TrObject __raw_bitor__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitor__));
        public TrObject __bitor__(TrObject a) => TrObject.__raw_bitor__(this, a);


        // default '__bitxor__'
        public static TrObject __raw_bitxor__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__bitxor__));
        public TrObject __bitxor__(TrObject a) => TrObject.__raw_bitxor__(this, a);



        // bit shift
        // default '__lshift__'
        public static TrObject __raw_lshift__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__lshift__));
        public TrObject __lshift__(TrObject a) => TrObject.__raw_lshift__(this, a);

        // default '__rshift__'
        public static TrObject __raw_rshift__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__rshift__));
        public TrObject __rshift__(TrObject a) => TrObject.__raw_rshift__(this, a);

        // Object protocol

        // default '__hash__'
        public static int __raw_hash__(TrObject self) => self.Native.GetHashCode();
        public int __hash__() => TrObject.__raw_hash__(this);

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
        // default '__call__'
        public static TrObject __raw_call__(TrObject self, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) =>
            throw self.unsupported(nameof(__call__));
        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) => TrObject.__raw_call__(this, args, kwargs);

        // default '__contains__'
        public static bool __raw_contains__(TrObject self, TrObject a) =>
            throw self.unsupported(nameof(__contains__));
        public bool __contains__(TrObject a) => TrObject.__raw_contains__(this, a);
        public bool __getitem__(TrObject item, out TrObject found)
        {
            var reference = MK.Ref();
            var res = __getitem__(item, reference);
            found = reference.value;
            return res;
        }
        // default '__getitem__'
        public static bool __raw_getitem__(TrObject self, TrObject item, TrRef found) =>
            throw self.unsupported(nameof(__getitem__));
        public bool __getitem__(TrObject item, TrRef found) => TrObject.__raw_getitem__(this, item, found);

        // default '__setitem__'
        public static void __raw_setitem__(TrObject self, TrObject key, TrObject value) =>
            throw self.unsupported(nameof(__setitem__));
        public void __setitem__(TrObject key, TrObject value) => TrObject.__raw_setitem__(this, key, value);

        public static bool __raw_getattr__(TrObject self, TrObject name, TrRef found)
        {
            return self.__getic__(name.AsString(), out found.value);
        }

        public static void __raw_setattr__(TrObject self, TrObject name, TrObject value)
        {
            self.__setic__(name.AsString(), value);
        }

        public bool __getattr__(TrObject name, TrRef found) => __raw_getattr__(this, name, found);
        public void __setattr__(TrObject name, TrObject value) => __raw_setattr__(this, name, value);

        // default '__iter__'
        public static IEnumerator<TrObject> __raw_iter__(TrObject self) =>
            throw self.unsupported(nameof(__iter__));
        public IEnumerator<TrObject> __iter__() => TrObject.__raw_iter__(this);


        // default '__len__'
        public static TrObject __raw_len__(TrObject self) =>
            throw self.unsupported(nameof(__len__));
        public TrObject __len__() => TrObject.__raw_len__(this);


        // Comparators

        public static bool __raw_eq__(TrObject self, TrObject other) =>
            self.Native == other.Native;
        public bool __eq__(TrObject other) => TrObject.__raw_eq__(this, other);

        // default '__lt__'
        public static bool __raw_lt__(TrObject self, TrObject other) =>
            throw self.unsupported(nameof(__lt__));
        public bool __lt__(TrObject other) => TrObject.__raw_lt__(this, other);


        // Unary ops
        // default '__neg__'
        public static TrObject __raw_neg__(TrObject self) =>
            throw self.unsupported(nameof(__neg__));
        public TrObject __neg__() => TrObject.__raw_neg__(this);

        // default '__inv__'
        public static TrObject __raw_inv__(TrObject self) =>
            throw self.unsupported(nameof(__inv__));
        public TrObject __inv__() => TrObject.__raw_inv__(this);

        // default '__pos__'
        public static TrObject __raw_pos__(TrObject self) =>
            throw self.unsupported(nameof(__pos__));
        public TrObject __pos__() => TrObject.__raw_pos__(this);

        // default '__bool__'
        public static bool __raw_bool__(TrObject self) => true;
        public bool __bool__() => TrObject.__raw_bool__(this);
    }

    public class TrRawObject : TrUserObjectBase
    {
        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.RawObjectClassObject();
            CLASS.InitInlineCacheForMagicMethods();
            CLASS.Name = "object";
            CLASS[CLASS.ic__new] = TrSharpFunc.FromFunc("object.__new__", TrRawObject.datanew);
            TrClass.TypeDict[typeof(TrRawObject)] = CLASS;
        }
        [Mark(typeof(TrRawObject))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_atleast(args, 1);
            var clsobj = (TrClass) args[0];
            if (clsobj == CLASS)
            {
                return MK.RawObject();
            }
            return clsobj[clsobj.ic__new].__call__(args, kwargs);
        }
    }

    public class TrUserObject : TrUserObjectBase
    {
        public TrClass Class { get; private set; }

        public List<TrObject> __array__ { get; } = new List<TrObject>();

        public TrUserObject(TrClass cls)
        {
            Class = cls;
        }
    }
}