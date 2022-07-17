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
    public static partial class Initialization
    {
#if CODE_GEN
        public static void InitRuntime()
        {
        }
#endif
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            m_Prelude = new Dictionary<string, TrObject>();
        }
        public const string IR_FILE_SUFFIX = ".py.json";
        public static class HashConfig
        {
            public const int BYTE_HASH_SEED = 17;
            public const int BYTE_HASH_PRIME = 23;
            public const int TUPLE_HASH_SEED = 17;
            public const int TUPLE_HASH_PRIME = 23;
        }
        internal const int OBJECT_SHAPE_MAX_FIELD = 255;

        internal static Dictionary<string, TrObject> m_Prelude;
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