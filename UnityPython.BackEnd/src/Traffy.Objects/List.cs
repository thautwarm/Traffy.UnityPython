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

        public TrObject __add__(TrObject other)
        {
            append(this, other);
            return this;
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

        public TrMapObject(TrObject func, List<IEnumerator<TrObject>> items)
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

        public static TrObject datanew(TrClass cls, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            int len = args.Count;
            if (len < 2)
            {
                throw new TypeError("map() must have at least two arguments.");
            }

            var list = new List<IEnumerator<TrObject>>();
            TrObject func = args[0];
            int index = 0;
            foreach (var item in args)
            {
                if (index != 0)
                {
                    IEnumerator<TrObject> it = item.__iter__();
                    list.Add(it);
                }
                index += 1;
            }
            return MK.Map(func, list);
        }

        static IEnumerator<TrObject> generator(TrObject func, List<IEnumerator<TrObject>> items)
        {
            while (true)
            {
                BList<TrObject> curr = new BList<TrObject> { };
                foreach (var it in items)
                {
                    if (!it.MoveNext())
                    {
                        yield break;
                    }

                    curr.Add(it.Current);
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