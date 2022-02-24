using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

namespace Traffy.Objects
{
    public partial class TrList : TrObject
    {
        public List<TrObject> container;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrList>();
            CLASS.Name = "list";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("list.__new__", TrList.datanew);
            CLASS["append".ToIntern()] = TrSharpFunc.FromFunc("list.append", TrList.append);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrList)] = CLASS;
        }

        public static TrObject append(TrObject self, TrObject value)
        {
            ((TrList)self).container.Add(value);
            return RTS.object_none;
        }

        [Mark(typeof(TrList))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.List();
            if (narg == 2 && kwargs == null)
            {
                return MK.List(RTS.object_to_list(args[1]));
            }
            throw new TypeError($"{clsobj.AsClass.Name}.__new__() takes 1 or 2 positional argument(s) but {narg} were given");
        }

        public IEnumerator<TrObject> __iter__()
        {
            return container.GetEnumerator();
        }

        public TrObject __len__() => MK.Int(container.Count);

        public string __repr__() => "[" + String.Join(",", container.Select((i) => i.__str__())) + "]";

        public string __str__() => __repr__();

        bool TrObject.__getitem__(TrObject item, TrRef found)
        {
            var oitem = item as TrInt;
            if ((object) oitem != null)
            {
                var i = oitem.value;
                if (i < 0)
                    i += container.Count;
                if (i < 0 || i >= container.Count)
                    return false;
                found.value = container[unchecked((int)i)];
                return true;
            }
            return false;
        }

        void TrObject.__setitem__(TrObject item, TrObject value)
        {
            var oitem = item as TrInt;
            if ((object) oitem != null)
            {
                var i = oitem.value;
                if (i < 0)
                    i += container.Count;
                if (i < 0 || i >= container.Count)
                    throw new IndexError($"list assignment index out of range");
                container[unchecked((int)i)] = value;
                return;
            }
            throw new TypeError($"list indices must be integers, not {item.Class.Name}");
        }
    }

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

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrMapObject>();
            CLASS.Name = "map";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("map.__new__", TrMapObject.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrMapObject)] = CLASS;
        }

        [Mark(typeof(TrMapObject))]
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

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrFilter>();
            CLASS.Name = "filter";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("filter.__new__", TrFilter.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrFilter)] = CLASS;
        }

        [Mark(typeof(TrFilter))]
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