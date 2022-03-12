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

    }

}