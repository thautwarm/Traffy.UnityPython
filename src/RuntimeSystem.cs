using System;
using System.Collections.Generic;
using System.Linq;
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
            // TODO
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

        internal static void arg_check_positional_only(BList<TrObject> args, int v)
        {
            if (args.Count != v)
                throw new ValueError($"requires {v} argument(s), got {args.Count}");
        }
        internal static void arg_check_positional_atleast(BList<TrObject> args, int v)
        {
            if (args.Count < v)
                throw new ValueError($"requires atleast {v} argument(s), got {args.Count}");
        }

        internal static void arg_check_positional_range(BList<TrObject> args, int least, int most)
        {
            var narg = args.Count;
            if (least <= narg && narg <= most) {}
            else
                throw new ValueError($"requires {least}-{most} argument(s), got {narg}");
        }

        internal static List<TrObject> object_as_list(TrObject o)
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
        internal static List<TrObject> object_to_list(TrObject o)
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

        internal static TrObject parse_int(string s)
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
        internal static TrObject parse_float(string value) =>
            MK.Float(float.Parse(value));

        internal static Exception exc_unpack_toomuch(int length)
        {
            throw new ValueError($"too many elements to unpack, requires exactly {length} one(s).");
        }

        internal static TrObject object_from_list(List<TrObject> itr) => MK.List(itr);

        internal static Exception exc_unpack_notenough(int nelts, int npatterns)
        {
            throw new ValueError($"not enough elements to unpack, requires exactly {npatterns} one(s), got {nelts}.");
        }

        internal static System.Collections.Generic.IEnumerator<TrObject> object_getiter(TrObject o)
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

        internal static TrObject baredict_get_noerror(Dictionary<TrObject, TrObject> dict__, TrObject s)
        {
            if (dict__.TryGetValue(s, out var value))
            {
                return value;
            }
            return null;
        }

        internal static void baredict_set(Dictionary<TrObject, TrObject> dict__, TrObject s, TrObject value)
        {
            dict__[s] = value;
        }

        internal static TrObject object_pos(TrObject arg) => arg.__pos__();

        internal static TrObject object_neg(TrObject arg) => arg.__neg__();

        internal static TrObject object_not(TrObject arg) =>  MK.Bool(!arg.__bool__());

        internal static TrObject object_inv(TrObject arg) => arg.__inv__();


        internal static bool isinstanceof_impl(TrObject o, TrClass cls)
        {
            if (o.Class == cls)
            {
                return true;
            }
            if (cls.__base.Length != 0)
                for(int i = 0; i < cls.__base.Length; i++)
                {
                    if(isinstanceof(o, cls.__base[i])) return true;
                }
            return false;
        }

        internal static bool isinstanceof(TrObject o, TrObject cls)
        {
            if (cls is TrTuple tuple)
            {
                return tuple.elts.Exist(x => isinstanceof(o, x));
            }
            else if (cls is TrClass t)
            {
                return isinstanceof_impl(o, t);
            }
            throw new TypeError($"{cls.__repr__()} is not a class or a tuple or classes");
        }

        internal static TrObject[] object_as_array(TrObject trObject)
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

        internal static void init_class(TrClass cls, TrClass newCls, TrStr name, TrTuple bases, TrDict ns)
        {
            // TODO
            throw new NotImplementedException();
        }

        internal static TrObject object_lshift(TrObject arg1, TrObject arg2)
        {
            return arg1.__lshift__(arg2);
        }

        internal static TrObject object_rshift(TrObject arg1, TrObject arg2)
        {
            return arg1.__rshift__(arg2);
        }

        internal static TrObject object_bitand(TrObject arg1, TrObject arg2)
        {
            return arg1.__bitand__(arg2);
        }

        internal static TrObject object_bitor(TrObject arg1, TrObject arg2)
        {
            return arg1.__bitor__(arg2);
        }

        internal static TrObject object_bitxor(TrObject arg1, TrObject arg2)
        {
            return arg1.__bitxor__(arg2);
        }

        internal static TrObject object_matmul(TrObject arg1, TrObject arg2)
        {
            return arg1.__matmul__(arg2);
        }

        internal static TrObject object_mod(TrObject arg1, TrObject arg2)
        {
            return arg1.__mod__(arg2);
        }

        internal static TrObject object_pow(TrObject arg1, TrObject arg2)
        {
            return arg1.__pow__(arg2);
        }

        internal static TrObject object_floordiv(TrObject arg1, TrObject arg2)
        {
            return arg1.__floordiv__(arg2);
        }

        internal static TrObject object_truediv(TrObject arg1, TrObject arg2)
        {
            return arg1.__truediv__(arg2);
        }

        internal static TrObject object_mul(TrObject arg1, TrObject arg2)
        {
            return arg1.__mul__(arg2);
        }

        internal static TrObject object_sub(TrObject arg1, TrObject arg2)
        {
            return arg1.__sub__(arg2);
        }

        internal static TrObject object_add(TrObject arg1, TrObject arg2)
        {
            return arg1.__add__(arg2);
        }

        internal static TrObject object_getitem(TrObject tos, TrObject item)
        {
            return tos.__getitem__(item);
        }

        internal static void object_setitem(TrObject tos, TrObject item, TrObject value)
        {
            tos.__setitem__(item, value);
        }

        internal static void object_delitem(TrObject tos, TrObject item)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_getattr(TrObject tos, string attr)
        {
            return tos.__getattr__(MK.Str(attr));
        }

        internal static void object_setattr(TrObject tos, string attr, TrObject value)
        {
            tos.__setattr__(MK.Str(attr), value);
        }

        internal static void object_delattr(TrObject tos, string attr)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_call(TrObject f, BList<TrObject> args) => f.__call__(args, null);

        internal static TrObject object_eq(TrObject l, TrObject r) => MK.Bool(l.__eq__(r));

        internal static TrObject object_ne(TrObject l, TrObject r)
        {
            return MK.Bool(!(l.__eq__(r)));
        }

        internal static TrObject object_lt(TrObject l, TrObject r) => MK.Bool(l.__lt__(r));

        internal static TrObject object_le(TrObject l, TrObject r)
        {
            if (l.__lt__(r) || l.__eq__(r))
                return MK.Bool(true);
            return MK.Bool(false);
        }

        internal static TrObject object_gt(TrObject l, TrObject r) => object_le(r, l);

        internal static TrObject object_ge(TrObject l, TrObject r) => object_lt(r, l);

        internal static TrObject object_from_bool(bool v) => MK.Bool(v);

        internal static bool object_contains(TrObject r, TrObject l) => r.__contains__(l);

        internal static TrObject object_from_int(int v) => MK.Int(v);
        internal static TrObject object_from_int(Int64 v) => MK.Int(v);

        internal static TrObject object_from_float(float v) => MK.Float(v);

        internal static TrObject object_none => TrNone.Unique;

        internal static TrObject object_from_string(string result) => MK.Str(result);

        internal static TrObject tuple_construct(TrObject[] array) => MK.Tuple(array);

        internal static bool object_bool(TrObject trObject)
        {
            return trObject.__bool__();
        }

        public static IEqualityComparer<TrObject> DICT_COMPARE = new TraffyComparer();
        internal static Dictionary<TrObject, TrObject> baredict_create() => new Dictionary<TrObject, TrObject>(DICT_COMPARE);

        internal static void baredict_extend(Dictionary<TrObject, TrObject> dict, TrObject other)
        {
            if (other is TrDict map)
            {
                foreach(var kv in map.container)
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

        internal static void baredict_add(Dictionary<TrObject, TrObject> dict, TrObject rt_key, TrObject rt_value)
        {
            dict.Add(rt_key, rt_value);
        }

        internal static TrObject object_from_baredict(Dictionary<TrObject, TrObject> dict)
        {
            return MK.Dict(dict);
        }

        internal static List<TrObject> barelist_create()
        {
            return new List<TrObject>();
        }

        internal static void barelist_extend(List<TrObject> lst, TrObject rt_seq)
        {
            var itr = rt_seq.__iter__();
            while (itr.MoveNext())
            {
                lst.Add(itr.Current);
            }
        }

        internal static void barelist_add(List<TrObject> lst, TrObject rt_each)
        {
            lst.Add(rt_each);
        }

        internal static TrObject object_from_barelist(List<TrObject> lst)
        {
            return MK.List(lst);
        }

        internal static TrObject object_from_barearray(TrObject[] trObjects)
        {
            return MK.Tuple(trObjects);
        }

        internal static HashSet<TrObject> bareset_create()
        {
            return new HashSet<TrObject>(DICT_COMPARE);
        }

        internal static void bareset_extend(HashSet<TrObject> set, TrObject rt_each)
        {
            var itr = rt_each.__iter__();
            while (itr.MoveNext())
            {
                set.Add(itr.Current);
            }
        }

        internal static void bareset_add(HashSet<TrObject> set, TrObject rt_each)
        {
            set.Add(rt_each);
        }

        internal static TrObject object_from_bareset(HashSet<TrObject> set)
        {
            return MK.Set(set);
        }

        internal static bool exc_check_instance(Exception e, TrObject rt_exc)
        {
            // TODO
            throw new NotImplementedException();
        }

        internal static TrObject exc_frombare(Exception e)
        {
            // TODO
            throw new NotImplementedException();
        }

        internal static Exception exc_tobare(TrObject rt_exc)
        {
            // TODO
            throw new NotImplementedException();
        }

        static IEnumerator<TrObject> coroutine_of_object_mkCont0(IEnumerator<TrObject> itr, TraffyCoroutine coro)
        {
            while (itr.MoveNext())
            {
                yield return itr.Current;
            }
        }

        internal static TraffyCoroutine coroutine_of_iter(IEnumerator<TrObject> o)
        {
            if (o is TraffyCoroutine coro)
                return coro;
            coro = new TraffyCoroutine();
            coro.generator = coroutine_of_object_mkCont0(o, coro);
            return coro;
        }
        internal static TraffyCoroutine coroutine_of_object(TrObject rt_value)
        {
            var o = rt_value.__iter__();
            return coroutine_of_iter(o);
        }

        internal static TrObject object_call_ex(TrObject rt_func, BList<TrObject> rt_args, Dictionary<TrObject, TrObject> rt_kwargs)
        {
            return rt_func.__call__(rt_args, rt_kwargs);
        }
    }

    public static class MK
    {

        internal static TrBool Bool(bool v)
        {
            if (v)
                return TrBool.TrBool_True;
            return TrBool.TrBool_False;
        }

        internal static TrDict Dict(Dictionary<TrObject, TrObject> v)
        {
            return new TrDict { container = v };
        }

        internal static TrDict Dict()
        {
            return new TrDict { container = RTS.baredict_create() };
        }

        internal static TrFloat Float(float v)
        {
            return new TrFloat { value = v };
        }

        internal static TrInt Int(long p)
        {
            return new TrInt { value = p };
        }

        internal static TrInt Int(ulong p)
        {
            return new TrInt { value = unchecked((long) p) };
        }
        internal static TrInt Int(int p)
        {
            return new TrInt { value = unchecked((long) p) };
        }

        internal static TrInt Int(bool p)
        {
            return new TrInt { value = p ? 1L : 0L };
        }

        internal static TrList List(List<TrObject> trObjects)
        {
            return new TrList { container = trObjects };
        }

        internal static TrList List()
        {
            return new TrList { container = RTS.barelist_create() };
        }

        internal static TrNone None()
        {
            return TrNone.Unique;
        }

        internal static TrSet Set()
        {
            return new TrSet { container = RTS.bareset_create() };
        }

        internal static TrSet Set(HashSet<TrObject> hashset)
        {
            return new TrSet { container = hashset };
        }

        internal static TrStr Str(string v)
        {
            return new TrStr { value = v };
        }

        internal static TrTuple Tuple(TrObject[] trObjects)
        {
            return new TrTuple { elts = trObjects };
        }

        static TrObject[] _zeroelts = new TrObject[0];
        internal static TrTuple Tuple()
        {
            return new TrTuple { elts = _zeroelts };
        }

        internal static TrObject object_imod(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_imatmul(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_ibitxor(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_ibitor(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_ibitand(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_irshift(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_ilshift(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_ipow(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_ifloordiv(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_itruediv(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_imul(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_isub(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }

        internal static TrObject object_iadd(TrObject arg1, TrObject arg2)
        {
            throw new NotImplementedException();
        }
    }
}