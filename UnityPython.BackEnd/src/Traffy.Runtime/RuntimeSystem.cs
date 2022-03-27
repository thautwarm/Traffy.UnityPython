using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Traffy.Objects;

namespace Traffy
{

    public static class RTS
    {
        internal static Func<TrObject, TrObject, TrObject>[] OOOFuncs = new Func<TrObject, TrObject, TrObject>[]{
            RTS.object_add,
            RTS.object_sub,
            RTS.object_mul,
            RTS.object_truediv,
            RTS.object_floordiv,
            RTS.object_pow,
            RTS.object_lshift,
            RTS.object_rshift,
            RTS.object_bitand,
            RTS.object_bitor,
            RTS.object_bitxor,
            RTS.object_matmul,
            RTS.object_mod,

            RTS.object_eq,
            RTS.object_ne,
            RTS.object_lt,
            RTS.object_le,
            RTS.object_gt,
            RTS.object_ge,
            RTS.object_is,
            RTS.object_isnot,
            RTS.object_in,
            RTS.object_notin,
        };
        internal static Func<TrObject, TrObject, TrObject>[] InplaceOOOFuncs = new Func<TrObject, TrObject, TrObject>[]{
            // TODO: inplace
            RTS.object_add,
            RTS.object_sub,
            RTS.object_mul,
            RTS.object_truediv,
            RTS.object_floordiv,
            RTS.object_pow,
            RTS.object_lshift,
            RTS.object_rshift,
            RTS.object_bitand,
            RTS.object_bitor,
            RTS.object_bitxor,
            RTS.object_matmul,
            RTS.object_mod,
        };

        internal static Func<TrObject, TrObject>[] OOFuncs = new Func<TrObject, TrObject>[]
        {
            RTS.object_inv,
            RTS.object_not,
            RTS.object_neg,
            RTS.object_pos,
        };

        internal static Exception exc_wrap_frame(Exception e, Frame frame)
        {
            TrExceptionBase exc = exc_frombare(e);
            if (exc.traceback == null)
            {
                exc.traceback = new TrTraceback();
            }
            exc.traceback.Record(
                frame.func.fptr.metadata.codename,
                frame.func.fptr.metadata,
                frame.traceback.ToArray());
            frame.err = null;
            return exc.AsException();
        }

        internal static Exception exc_wrap_builtin(Exception e, string caller_name)
        {
            TrExceptionBase exc = exc_frombare(e);
            if (exc.traceback == null)
            {
                exc.traceback = new TrTraceback();
            }
            exc.traceback.Record(caller_name);
            return exc.AsException();
        }

        public static void arg_check_positional_only(BList<TrObject> args, int v)
        {
            if (args.Count != v)
                throw new ValueError($"requires {v} argument(s), got {args.Count}");
        }
        public static void arg_check_positional_atleast(BList<TrObject> args, int v)
        {
            if (args.Count < v)
                throw new ValueError($"requires atleast {v} argument(s), got {args.Count}");
        }

        public static void arg_check_positional_range(BList<TrObject> args, int least, int most)
        {
            var narg = args.Count;
            if (least <= narg && narg <= most) { }
            else
                throw new ValueError($"requires {least}-{most} argument(s), got {narg}");
        }

        public static List<TrObject> object_as_list(TrObject o)
        {
            if (o is TrList lst)
            {
                return lst.container;
            }
            List<TrObject> res = new List<TrObject>();
            var itr = o.__iter__();
            while (itr.MoveNext())
            {
                res.Add(itr.Current);
            }
            return res;
        }
        public static List<TrObject> object_to_list(TrObject o)
        {
            if (o is TrList lst)
            {
                return lst.container.Copy();
            }
            List<TrObject> res = new List<TrObject>();
            var itr = o.__iter__();
            while (itr.MoveNext())
            {
                res.Add(itr.Current);
            }
            return res;
        }

        public static TrObject parse_int(string s)
        {
            if (s.Length > 2)
            {
                return MK.Int(s.Substring(0, 2) switch
                {
                    "0x" => Convert.ToInt64(s.Substring(2), 16),
                    "0o" => Convert.ToInt64(s.Substring(2), 8),
                    "0b" => Convert.ToInt64(s.Substring(2), 2),
                    _ => long.Parse(s)
                });
            }
            return MK.Int(long.Parse(s));
        }
        public static TrObject parse_float(string value) =>
            MK.Float(float.Parse(value));

        public static Exception exc_unpack_toomuch(int length)
        {
            throw new ValueError($"too many elements to unpack, requires exactly {length} one(s).");
        }

        public static TrObject object_from_list(List<TrObject> itr) => MK.List(itr);

        public static Exception exc_unpack_notenough(int nelts, int npatterns)
        {
            throw new ValueError($"not enough elements to unpack, requires exactly {npatterns} one(s), got {nelts}.");
        }

        public static System.Collections.Generic.IEnumerator<TrObject> object_getiter(TrObject o)
        {
            return o.__iter__();
        }
        private static TrObject object_notin(TrObject arg1, TrObject arg2)
        {
            return MK.Bool(!arg2.__contains__(arg1));
        }

        private static TrObject object_in(TrObject arg1, TrObject arg2)
        {
            return MK.Bool(arg2.__contains__(arg1));
        }

        private static TrObject object_isnot(TrObject arg1, TrObject arg2)
        {
            return MK.Bool(!object.ReferenceEquals(arg1, arg2));
        }

        private static TrObject object_is(TrObject arg1, TrObject arg2)
        {
            return MK.Bool(object.ReferenceEquals(arg1, arg2));
        }

        public static bool baredict_get_noerror(Dictionary<TrObject, TrObject> dict__, TrObject s, out TrObject found)
        {
            return dict__.TryGetValue(s, out found);
        }

        public static void baredict_set(Dictionary<TrObject, TrObject> dict__, TrObject s, TrObject value)
        {
            dict__[s] = value;
        }

        public static TrObject object_pos(TrObject arg) => arg.__pos__();

        public static TrObject object_neg(TrObject arg) => arg.__neg__();

        public static TrObject object_not(TrObject arg) => MK.Bool(!arg.__bool__());

        public static TrObject object_inv(TrObject arg) => arg.__invert__();

        public static bool isinstanceof(TrObject o, TrObject cls)
        {
            return TrObject.__instancecheck__(o, cls);
        }

        public static TrObject[] object_as_array(TrObject trObject)
        {
            if (trObject is TrTuple tuple)
            {
                return tuple.elts;
            }

            var res = new List<TrObject>();
            var itr = trObject.__iter__();
            while (itr.MoveNext())
            {
                res.Add(itr.Current);
            }
            return res.ToArray();
        }

        public static TrObject object_lshift(TrObject arg1, TrObject arg2)
        {
            return arg1.__lshift__(arg2);
        }

        public static TrObject object_rshift(TrObject arg1, TrObject arg2)
        {
            return arg1.__rshift__(arg2);
        }

        public static TrObject object_bitand(TrObject arg1, TrObject arg2)
        {
            return arg1.__bitand__(arg2);
        }

        public static TrObject object_bitor(TrObject arg1, TrObject arg2)
        {
            return arg1.__bitor__(arg2);
        }

        public static TrObject object_bitxor(TrObject arg1, TrObject arg2)
        {
            return arg1.__bitxor__(arg2);
        }

        public static TrObject object_matmul(TrObject arg1, TrObject arg2)
        {
            return arg1.__matmul__(arg2);
        }

        public static TrObject object_mod(TrObject arg1, TrObject arg2)
        {
            return arg1.__mod__(arg2);
        }

        public static TrObject object_pow(TrObject arg1, TrObject arg2)
        {
            return arg1.__pow__(arg2);
        }

        public static TrObject object_floordiv(TrObject arg1, TrObject arg2)
        {
            return arg1.__floordiv__(arg2);
        }

        public static TrObject object_truediv(TrObject arg1, TrObject arg2)
        {
            return arg1.__truediv__(arg2);
        }

        public static TrObject object_mul(TrObject arg1, TrObject arg2)
        {
            return arg1.__mul__(arg2);
        }

        public static TrObject object_sub(TrObject arg1, TrObject arg2)
        {
            return arg1.__sub__(arg2);
        }

        public static TrObject object_add(TrObject arg1, TrObject arg2)
        {
            return arg1.__add__(arg2);
        }

        public static TrObject object_getitem(TrObject tos, TrObject item)
        {
            return tos.__getitem__(item);
        }

        public static void object_delitem(TrObject tos, TrObject item)
        {
            tos.__delitem__(item);
        }

        public static void object_setitem(TrObject tos, TrObject item, TrObject value)
        {
            tos.__setitem__(item, value);
        }

        public static void object_setic(TrObject tos, InlineCache.PolyIC ic, TrObject value)
        {
            tos.__setic__(ic, value);
        }

        public static TrObject object_getic(TrObject tos, InlineCache.PolyIC ic)
        {
            if (tos.__getic__(ic, out var o))
            {
                return o;
            }
            if (tos is TrClass cls)
                throw new AttributeError(tos, MK.IStr(ic.Name), $"class {cls.Name} has no attribute {ic.Name}");
            throw new AttributeError(tos, MK.IStr(ic.Name), $"{tos.Class.Name} object has no attribute {ic.Name}");
        }

        public static void object_setattr(TrObject tos, TrObject name, TrObject value)
        {
            object_setattr(tos, (TrStr) name, value);
        }
        public static void object_setattr(TrObject tos, TrStr name, TrObject value)
        {
            tos.__setic_refl__(name, value);
        }

        public static TrObject object_getattr(TrObject tos, TrObject name)
        {
            return object_getattr(tos, (TrStr) name);
        }
        public static TrObject object_getattr(TrObject tos, TrStr name)
        {
            if (tos.__getic_refl__(name, out var o))
            {
                return o;
            }
            if (tos is TrClass cls)
                throw new AttributeError(tos, name, $"class {cls.Name} has no attribute {name}");
            throw new AttributeError(tos, name, $"{tos.Class.Name} object has no attribute {name}");
        }

        public static TrObject object_call(TrObject f, BList<TrObject> args) => f.__call__(args, null);

        public static TrObject object_eq(TrObject l, TrObject r) => MK.Bool(l.__eq__(r));

        public static TrObject object_ne(TrObject l, TrObject r) => MK.Bool(!l.__ne__(r));

        public static TrObject object_lt(TrObject l, TrObject r) => MK.Bool(l.__lt__(r));

        public static TrObject object_le(TrObject l, TrObject r) => MK.Bool(l.__le__(r));

        public static TrObject object_gt(TrObject l, TrObject r) => MK.Bool(l.__gt__(r));

        public static TrObject object_ge(TrObject l, TrObject r) => MK.Bool(l.__ge__(r));

        public static TrObject object_from_bool(bool v) => MK.Bool(v);

        public static bool object_contains(TrObject r, TrObject l) => r.__contains__(l);

        public static TrObject object_from_int(int v) => MK.Int(v);
        public static TrObject object_from_int(Int64 v) => MK.Int(v);

        public static TrObject object_from_float(float v) => MK.Float(v);
        public static TrObject object_none => TrNone.Unique;

        public static TrObject object_from_string(string result) => MK.Str(result);

        public static TrObject tuple_construct(TrObject[] array) => MK.Tuple(array);

        internal static bool object_bool(TrObject trObject)
        {
            return trObject.__bool__();
        }

        public static IEqualityComparer<TrObject> Py_COMPARER = new TraffyComparer();
        internal static Dictionary<TrObject, TrObject> baredict_create() => new Dictionary<TrObject, TrObject>(Py_COMPARER);

        internal static void baredict_extend(Dictionary<TrObject, TrObject> dict, TrObject other)
        {
            if (other is TrDict map)
            {
                foreach (var kv in map.container)
                {
                    dict.Add(kv.Key, kv.Value);
                }
            }
            else
            {
                var itr = other.__iter__();
                while (itr.MoveNext())
                {
                    var each = itr.Current;
                    if (each is TrTuple tuple)
                    {
                        if (tuple.elts.Length != 2)
                        {
                            throw new ValueError($"updating dictionaries requires a 2-element tuple sequence, got {tuple.elts.Length}-element ones.");
                        }
                        dict.Add(tuple.elts[0], tuple.elts[1]);
                    }
                    else
                    {
                        throw new ValueError($"updating dictionaries requires a 2-element tuple sequence, got '{each.Class.Name}' ones.");
                    }
                }
            }
        }

        public static void baredict_add(Dictionary<TrObject, TrObject> dict, TrObject rt_key, TrObject rt_value)
        {
            dict.Add(rt_key, rt_value);
        }

        public static TrObject object_from_baredict(Dictionary<TrObject, TrObject> dict)
        {
            return MK.Dict(dict);
        }

        public static List<TrObject> barelist_create()
        {
            return new List<TrObject>();
        }

        public static void barelist_extend(List<TrObject> lst, TrObject rt_seq)
        {
            var itr = rt_seq.__iter__();
            while (itr.MoveNext())
            {
                lst.Add(itr.Current);
            }
        }

        public static void barelist_add(List<TrObject> lst, TrObject rt_each)
        {
            lst.Add(rt_each);
        }

        public static TrObject object_from_barelist(List<TrObject> lst)
        {
            return MK.List(lst);
        }

        public static TrObject object_from_barearray(TrObject[] trObjects)
        {
            return MK.Tuple(trObjects);
        }

        public static HashSet<TrObject> bareset_create()
        {
            return new HashSet<TrObject>(Py_COMPARER);
        }

        public static void bareset_extend(HashSet<TrObject> set, TrObject rt_each)
        {
            var itr = rt_each.__iter__();
            while (itr.MoveNext())
            {
                set.Add(itr.Current);
            }
        }

        public static void bareset_add(HashSet<TrObject> set, TrObject rt_each)
        {
            set.Add(rt_each);
        }

        public static TrObject object_from_bareset(HashSet<TrObject> set)
        {
            return MK.Set(set);
        }

        public static bool exc_check_instance(Exception e, TrObject rt_exc)
        {
            var e_obj = exc_frombare(e);
            return e_obj.__instancecheck__(rt_exc);
        }

        public static TrExceptionBase exc_frombare(Exception e)
        {
            if (e is TrExceptionWrapper wrapper)
            {
                return wrapper.TrO;
            }
            var exc = new NativeError(e);
            return exc;
        }

        public static Exception exc_tobare(TrObject rt_exc)
        {
            if (rt_exc is TrExceptionBase o)
                return new TrExceptionWrapper(o);
            throw new TrExceptionWrapper(new TypeError($"exceptions must be old-style classes or derived from BaseException, not {rt_exc.Class.Name}"));
        }

        public static Awaitable<TrObject> generator_of_iter(IEnumerator<TrObject> o)
        {
            if (o is TrGenerator coro)
            {
                return coro.m_Generator;
            }
            return o.YieldFrom();
        }

        public static Awaitable<TrObject> object_yield_from(TrObject rt_value)
        {
            var o = rt_value.__iter__();
            return generator_of_iter(o);
        }

        public static Awaitable<TrObject> object_await(TrObject rt_value)
        {
            return rt_value.__await__();
        }

        public static TrClass new_class(string name, TrObject[] rt_bases, Dictionary<TrObject, TrObject> ns)
        {
            var cls = TrClass.CreateClass(name, rt_bases.Select(x => (TrClass)x).ToArray());
            cls.Name = name;
            TrObject new_inst(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                return MK.UserObject(cls);
            }
            cls[cls.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc($"{name}.__new__", new_inst));
            cls.SetupUserClass(ns);
            return cls;
        }

        public static TrObject object_of_coroutine(MonoAsync<TrObject> rt_value)
        {

            return TrGenerator.Create(rt_value);
        }


        public static TrObject object_call_ex(TrObject rt_func, BList<TrObject> rt_args, Dictionary<TrObject, TrObject> rt_kwargs)
        {
            return rt_func.__call__(rt_args, rt_kwargs);
        }

        internal static TrObject object_enter(TrObject rt_context) => rt_context.__enter__();

        internal static TrObject object_str(TrObject rt_value)
        {
            return MK.Str(rt_value.__str__());
        }

        internal static TrObject object_repr(TrObject rt_value)
        {
            return MK.Str(rt_value.__repr__());
        }

        public static Stack<(TrStr, TrObject)> import_from_module([AllowNull] string module, int level, Dictionary<TrObject, TrObject> globals, [AllowNull] string[] __all__)
        {

            var imported = new Stack<(TrStr, TrObject)>();
            if (module == null)
            {
                Func<string, string> resolveName;
                if (level == 0)
                {
                    resolveName = name => name;
                }
                else
                {
                    var sectors = globals[MK.Str("__name__")].AsStr().Split('.');
                    sectors = sectors.Take(sectors.Length - level).ToArray();
                    resolveName = (name) =>
                    {
                        var newsectors = new string[sectors.Length + 1];
                        Array.Copy(sectors, newsectors, sectors.Length);
                        newsectors[newsectors.Length - 1] = name;
                        return string.Join(".", newsectors);
                    };
                }
                if (__all__ == null)
                {
                    throw new ValueError("import * from None is not allowed");
                }
                foreach (var each in __all__)
                {
                    var name = MK.Str(each);
                    var value = ModuleSystem.ImportModule(resolveName(each));
                    imported.Push((name, value));
                }
            }
            else
            {
                string resolvedName;
                if (level == 0)
                {
                    resolvedName = module;
                }
                else
                {
                    var sectors = globals[MK.Str("__name__")].AsStr().Split('.');
                    resolvedName = string.Join(".", sectors.Take(sectors.Length - level).Append(module));
                }
                var mod = ModuleSystem.ImportModule(resolvedName);
                if (__all__ == null)
                {
                    if (mod.__getic_refl__(MK.Str("__all__"), out var export))
                    {
                        export.__iter__().ToList().ForEach(x =>
                        {
                            if (x is TrStr key)
                            {
                                var value = RTS.object_getattr(mod, key);
                                imported.Push((key, value));
                            }
                            else
                            {
                                throw new TypeError($"__all__ must be a list of strings, not {x.Class.Name}");
                            }
                        });
                    }
                    else
                    {
                        foreach (var (key, value) in mod.GetDictItems())
                        {
                            if (key.value.StartsWith("_"))
                            {
                                continue;
                            }
                            imported.Push((key, value));
                        }
                    }

                }
                else
                {
                    foreach (var each in __all__)
                    {
                        var name = MK.Str(each);
                        var value = RTS.object_getattr(mod, name);
                        imported.Push((name, value));
                    }
                }
            }

            return imported;
        }
    }

    public static class MK
    {

        public static TrBool Bool(bool v)
        {
            if (v)
                return TrBool.TrBool_True;
            return TrBool.TrBool_False;
        }

        public static TrBool True => TrBool.TrBool_True;
        public static TrBool False => TrBool.TrBool_False;

        public static TrDict Dict(Dictionary<TrObject, TrObject> v)
        {
            return new TrDict { container = v };
        }

        public static TrDict Dict()
        {
            return new TrDict { container = RTS.baredict_create() };
        }

        public static TrFloat Float(float v)
        {
            return new TrFloat { value = v };
        }

        public static TrFloat Float(double v)
        {
            return new TrFloat { value = (float)v };
        }

        public static TrInt Int(long p)
        {
            return new TrInt { value = p };
        }

        public static TrInt Int(ulong p)
        {
            return new TrInt { value = unchecked((long)p) };
        }
        public static TrInt Int(int p)
        {
            return new TrInt { value = unchecked((long)p) };
        }

        public static TrInt Int(bool p)
        {
            return new TrInt { value = p ? 1L : 0L };
        }

        public static TrList List(List<TrObject> trObjects)
        {
            return new TrList { container = trObjects };
        }

        public static TrList List()
        {
            return new TrList { container = RTS.barelist_create() };
        }

        public static TrNone None()
        {
            return TrNone.Unique;
        }

        public static TrRef Ref()
        {
            return new TrRef { value = null };
        }

        public static TrRef Ref(TrObject v)
        {
            return new TrRef { value = v };
        }

        public static TrSet Set()
        {
            return new TrSet { container = RTS.bareset_create() };
        }

        public static TrSet Set(HashSet<TrObject> hashset)
        {
            return new TrSet { container = hashset };
        }

        public static TrStr Str(string v) => new TrStr { value = v };

        public static TrStr IStr(InternedString v) => new TrStr { value = v.Value, isInterned = true };

        public static TrStr IStr(string v) => new TrStr { value = String.Intern(v), isInterned = true };


        static byte[] _zerobytes = new byte[0];

        public static TrByteArray ByteArray(List<byte> v) => new TrByteArray { contents = v };
        public static TrByteArray ByteArray() => new TrByteArray { contents = new List<byte>() };

        public static TrBytes Bytes(byte[] v) => new TrBytes { contents = v };
        public static TrBytes Bytes() => new TrBytes { contents = _zerobytes };

        public static TrTuple NTuple(params TrObject[] trObjects)
        {
            return Tuple(trObjects);
        }
        public static TrTuple Tuple(TrObject[] trObjects)
        {
            return new TrTuple { elts = trObjects };
        }

        static TrObject[] _zeroelts = new TrObject[0];
        public static TrTuple Tuple() => new TrTuple { elts = _zeroelts };

        public static TrObject Iter(IEnumerator<TrObject> v)
        {
            if (v is TrGenerator coro)
            {
                return coro;
            }
            if (v is TrIter iterable)
            {
                return iterable;
            }
            return new TrIter(v);
        }

        public static TrObject UnionType(TrClass left, TrClass right)
        {
            return new TrUnionType(left, right);
        }

        public static TrObject object_imod(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_imatmul(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_ibitxor(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_ibitor(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_ibitand(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_irshift(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_ilshift(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_ipow(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_ifloordiv(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_itruediv(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_imul(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_isub(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrObject object_iadd(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        public static TrRawObject RawObject()
        {
            return new TrRawObject { };
        }

        public static TrUserObject UserObject(TrClass cls)
        {
            return new TrUserObject(cls);
        }
    }
}