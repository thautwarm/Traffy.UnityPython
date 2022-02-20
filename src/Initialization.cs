using System;
using System.Linq;
using System.Collections.Generic;
using Traffy.Objects;

namespace Traffy
{
    public class SetupSortPair
    {
        public Action f;
        public TrClass cls;
    }
    public class MroComparer : IComparer<SetupSortPair>
    {
        public int Compare(SetupSortPair x, SetupSortPair y)
        {
            if (x.cls == y.cls)
                return 0;
            if (x.cls.__mro.Contains(y.cls))
                return 1;
            if (y.cls.__mro.Contains(x.cls))
                return -1;
            return 0;
        }
    }
    public static class Initialization
    {
        internal const int OBJECT_SHAPE_MAX_FIELD = 255;
        internal const string TokenClassInit = "Traffy.ClassInit";
        static Dictionary<string, TrObject> m_Prelude = new Dictionary<string, TrObject>();
        public static void InitRuntime()
        {
            Mark.Query(typeof(TrObject), x => x is string s && s == TokenClassInit).ToList().ForEach(
                f => f.method()
            );
            var triples = Mark.Query(typeof(TrObject), x => x is Type t && typeof(TrObject).IsAssignableFrom(t)).ToArray();
#if DEBUG
            Console.WriteLine($"Found {triples.Length} classes");
#endif
            foreach (var (t, attr, f) in triples)
            {
                if (!(attr.Token is Type tokenT) || tokenT != t)
                {
                    throw new Exception($"invalid mark for {t.Name}");
                }
                if (!TrClass.TypeDict.ContainsKey(tokenT))
                {
                    throw new Exception($"typedict not registered for {t.Name}");
                }
            }
            triples
                .Select(((Type t, Mark attr, Action f) x) =>
                    new SetupSortPair
                    {
                        f = x.f,
                        cls = TrClass.TypeDict[(Type)x.attr.Token]
                    })
                .OrderBy(x => x, new MroComparer())
                .ToList()
                .ForEach(x => x.f());
        }
        public static void Prelude(string name, TrObject o)
        {
            m_Prelude[name] = o;
        }

        public static void Prelude(TrClass cls)
        {
            m_Prelude[cls.Name] = cls;
        }


        // add all the prelude objects to the module
        public static void Populate(Dictionary<TrObject, TrObject> globals)
        {
            foreach (var kv in m_Prelude)
            {
                globals[MK.Str(kv.Key)] = kv.Value;
            }
        }
    }
}