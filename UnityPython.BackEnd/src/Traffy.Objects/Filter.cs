using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;


namespace Traffy.Objects
{
    public partial class TrFilter : TrObject, IEnumerator<TrObject>
    {
        public static TrClass CLASS;
        public TrClass Class => CLASS;

        TrObject func;
        IEnumerator<TrObject> gen;

        public TrFilter(TrObject func, IEnumerator<TrObject> items)
        {
            this.func = func;
            this.gen = generator(func, items);
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrFilter>("filter");
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("filter.__new__", TrFilter.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrFilter)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrFilter))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(TrObject cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            int len = args.Count;
            if (len != 2)
                throw new TypeError($"filter expected 2 arguments, got {len}");
            return MK.Filter(args[0], args[1].__iter__());
        }

        static IEnumerator<TrObject> generator(TrObject func, IEnumerator<TrObject> items)
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

        public string __str__() => $"{__repr__()}";

        public string __repr__() => $"<filter as {this}>";

        public TrObject __next__() => gen.MoveNext() ? gen.Current : throw new StopIteration();

        public IEnumerator<TrObject> __iter__() => gen;

        public TrObject Current => gen.Current;

        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            return gen.MoveNext();
        }

        public void Reset()
        {
            gen.Reset();
        }

        public void Dispose()
        {
            gen.Dispose();
        }
    }
}