// builtin functions in traffy.unitypython
using Traffy.Objects;
using Traffy.Annotations;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace Traffy
{
    public static partial class Builtins
    {

        static IEnumerator<TrObject> _mapN(TrObject func, IEnumerator<TrObject>[] items)
        {
            BList<TrObject> curr = new BList<TrObject>();
            for (int i = 0; i < items.Length; i++)
            {
                curr.Add(null);
            }
            while (true)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (!items[i].MoveNext())
                    {
                        yield break;
                    }
                    curr[i] = items[i].Current;
                }
                yield return func.__call__(curr, null);
            }
        }

        [PyBuiltin]
        static TrObject map(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            int len = args.Count;
            if (len < 2)
            {
                throw new TypeError("map() must have at least two arguments.");
            }

            var items = new IEnumerator<TrObject>[args.Count - 1];
            TrObject func = args[0];
            for (int i = 1; i < args.Count; i++)
            {
                items[i - 1] = args[i].__iter__();
            }
            return MK.Iter(_mapN(func, items));

        }
        static IEnumerator<TrObject> _filter(TrObject func, IEnumerator<TrObject> items)
        {
            var curr = new BList<TrObject> { null };
            while (true)
            {
                if (!items.MoveNext())
                {
                    yield break;
                }
                curr[0] = items.Current;
                var cur = func.__call__(curr, null);
                if (cur.__bool__())
                {
                    yield return items.Current;
                }
            }
        }


        [PyBuiltin]
        static TrObject filter(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            int len = args.Count;
            if (len != 2)
                throw new TypeError($"filter expected 2 arguments, got {len}");
            return MK.Iter(_filter(args[0], args[1].__iter__()));
        }

        static IEnumerator<TrObject> _range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
                yield return MK.Int(i);
        }

        [PyBuiltin]
        static TrObject range(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var narg = args.Count;
            switch(narg)
            {
                case 1:
                    return MK.Iter(_range(0, args[0].AsInt(), 1));
                case 2:
                    return MK.Iter(_range(args[0].AsInt(),  args[1].AsInt(), 1));
                case 3:
                    return MK.Iter(_range(args[0].AsInt(),  args[1].AsInt(), args[2].AsInt()));
                default:
                    throw new TypeError($"range() takes 1 to 3 positional argument(s) but {narg} were given");
            }
        }

        [PyBuiltin]
        static TrObject all(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (args.Count != 1)
                throw new TypeError($"all() takes exactly 1 argument ({args.Count} given)");
            var iter = args[0].__iter__();
            while (true)
            {
                if (!iter.MoveNext())
                {
                    return MK.True;
                }
                if (!iter.Current.__bool__())
                {
                    return MK.False;
                }
            }
        }

        [PyBuiltin]
        static TrObject any(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (args.Count != 1)
                throw new TypeError($"any() takes exactly 1 argument ({args.Count} given)");
            var iter = args[0].__iter__();
            while (true)
            {
                if (!iter.MoveNext())
                {
                    return MK.False;
                }
                if (iter.Current.__bool__())
                {
                    return MK.True;
                }
            }
        }

        static IEnumerator<TrObject> _mkzip(IEnumerator<TrObject>[] items)
        {
            if (items.Length == 0)
            {
                yield break;
            }

            while (true)
            {
                var curr = new TrObject[items.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    if (!items[i].MoveNext())
                    {
                        yield break;
                    }
                    curr[i] = items[i].Current;
                }
                yield return MK.Tuple(curr);
            }
        }

        [PyBuiltin]
        static TrObject zip(BList<TrObject> args, Dictionary<TrObject, TrObject> _)
        {
            var iterators = new IEnumerator<TrObject>[args.Count];
            for(int i = 0; i < args.Count; i++)
            {
                iterators[i] = args[i].__iter__();
            }
            return MK.Iter(_mkzip(iterators));
        }


        static IEnumerator<TrObject> _enumerate(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (args.Count != 1)
                throw new TypeError($"enumerate() expected 1 argument, got {args.Count}");
            var iter = args[0].__iter__();
            int i = 0;
            while (true)
            {
                if (!iter.MoveNext())
                {
                    yield break;
                }
                var curr = new TrObject[2];
                curr[0] = MK.Int(i++);
                curr[1] = iter.Current;
                yield return MK.Tuple(curr);
            }
        }

        [PyBuiltin]
        static TrObject enumerate(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            return MK.Iter(_enumerate(args, kwargs));
        }


        [PyBuiltin]
        static TrObject reversed(TrObject self)
        {
            return self.__reversed__();
        }

        static TrObject _key = MK.Str("key");
        static TrObject _reverse = MK.Str("reverse");

        static Comparison<TrObject> _normal_cmp = (TrObject a, TrObject b) =>
        {
            if (a.__lt__(b))
                return -1;
            if (a.__eq__(b))
                return 0;
            return 1;
        };

        static Comparison<TrObject> _rev_cmp = (TrObject a, TrObject b) =>
        {
            if (a.__eq__(b))
                return 0;
            if (a.__lt__(b))
                return 1;
            return -1;
        };

        static Func<TrObject, Comparison<TrObject>> _normal_cmp_by = (TrObject key) => (TrObject a, TrObject b) =>
        {
            var ka = key.Call(a);
            var kb = key.Call(b);
            if (ka.__eq__(kb))
                return 0;
            if (ka.__lt__(kb))
                return -1;
            return 1;
        };

        static Func<TrObject, Comparison<TrObject>> _rev_cmp_by = (TrObject key) => (TrObject a, TrObject b) =>
        {
            var ka = key.Call(a);
            var kb = key.Call(b);
            if (ka.__eq__(kb))
                return 0;
            if (ka.__lt__(kb))
                return 1;
            return -1;
        };

        [PyBuiltin]
        static TrObject sorted(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (args.Count != 1)
                throw new TypeError($"sorted() expected 1 argument, got {args.Count}");
            var self = args[0];
            TrObject key = null;
            bool reverse = false;

            if (kwargs != null)
            {
                kwargs.TryGetValue(_key, out key);
                if (kwargs.TryGetValue(_reverse, out var o_rev))
                {
                    reverse = o_rev.AsBool();
                }
            }

            List<TrObject> seq;
            if (self is TrList lst)
            {
                seq = lst.container.Copy();
            }
            else
            {
                seq = self.__iter__().ToList();
            }

            if (key == null)
            {
                if (reverse)
                {
                    seq.Sort(_rev_cmp);
                }
                else
                {
                    seq.Sort();
                }
            }
            else
            {
                if (reverse)
                {
                    seq.Sort(_rev_cmp_by(key));
                }
                else
                {
                    seq.Sort(_normal_cmp_by(key));
                }
            }
            return MK.List(seq);
        }
    }

}