using System.Collections.Generic;
using Traffy.Objects;
namespace Traffy
{
    public static class ModuleInit
    {
        static Dictionary<string, TrObject> m_Prelude = new Dictionary<string, TrObject>();

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