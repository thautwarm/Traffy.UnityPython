
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

namespace Traffy.Objects
{
    public partial class TrMapObject : TrObject, IEnumerator<TrObject>
    {
        public static TrClass CLASS;
        public TrClass Class => CLASS;

        TrObject func;
        IEnumerator<TrObject> gen;

        public TrMapObject(TrObject func, IEnumerator<TrObject>[] items)
        {
            this.func = func;
            this.gen = generator(func, items);
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrMapObject>("map");
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("map.__new__", TrMapObject.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrMapObject)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrMapObject))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
        public static TrObject datanew(TrObject cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            int len = args.Count;
            if (len < 2)
            {
                throw new TypeError("map() must have at least two arguments.");
            }

            var items = new IEnumerator<TrObject>[args.Count - 1];
            TrObject func = args[0];
            for(int i = 1; i < args.Count; i++)
            {
                items[i - 1] = args[i].__iter__();
            }
            return MK.Map(func, items);
        }

        static IEnumerator<TrObject> generator(TrObject func, IEnumerator<TrObject>[] items)
        {
            BList<TrObject> curr = new BList<TrObject>();
            for(int i = 0; i < items.Length; i++)
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

        public string __str__() => $"{__repr__()}";

        public string __repr__() => $"<map as {this}>";

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