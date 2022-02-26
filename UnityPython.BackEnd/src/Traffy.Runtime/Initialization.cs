using System;
using System.Linq;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy;

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
        public static class HashConfig
        {
            public const int BYTE_HASH_SEED = 17;
            public const int BYTE_HASH_PRIME = 23;
            public const int TUPLE_HASH_SEED = 17;
            public const int TUPLE_HASH_PRIME = 23;
        }
        internal const int OBJECT_SHAPE_MAX_FIELD = 255;
        internal const string TokenClassInit = "Traffy.ClassInit";

        internal const string TokenBuiltinInit = "Traffy.BuiltinInit";
        static Dictionary<string, TrObject> m_Prelude = new Dictionary<string, TrObject>();
        public static void InitRuntime()
        {
            Traffy.Annotations.Mark.Query(typeof(TrObject), x => x is string s && s == TokenClassInit).ToList().ForEach(
                f => f.method()
            );
            var triples = Traffy.Annotations.Mark.Query(typeof(TrObject), x => x is Type t && typeof(TrObject).IsAssignableFrom(t)).ToArray();
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
                .Select(((Type t, Traffy.Annotations.Mark attr, Action f) x) =>
                    new SetupSortPair
                    {
                        f = x.f,
                        cls = TrClass.TypeDict[(Type)x.attr.Token]
                    })
                .OrderBy(x => x, new MroComparer())
                .ToList()
                .ForEach(x => x.f());


            Traffy.Annotations.Mark.Query(typeof(TrObject), x => x is string s && s == TokenBuiltinInit).ToList().ForEach(
                f => f.method()
            );
        }
        public static void Prelude(string name, TrObject o)
        {
            m_Prelude[name] = o;
        }

        public static void Prelude(TrSharpFunc o)
        {
            m_Prelude[o.name] = o;
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