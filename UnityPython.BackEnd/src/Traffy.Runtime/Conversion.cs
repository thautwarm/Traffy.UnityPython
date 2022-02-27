using System.Collections.Generic;
using Traffy.Objects;

namespace Traffy
{
    public struct THint<T>
    {
        public static THint<T> Unique = new THint<T>();
    }

    public static class Box
    {
        public static TrObject Apply(TrObject o)
        {
            return o;
        }
        public static TrObject Apply(bool o)
        {
            return MK.Bool(o);
        }
        public static TrObject Apply(int o)
        {
            return MK.Int(o);
        }
        public static TrObject Apply(string o)
        {
            return MK.Str(o);
        }
        public static TrObject Apply(float o)
        {
            return MK.Float(o);
        }
        public static TrObject Apply(long o)
        {
            return MK.Int(o);
        }
        public static TrObject Apply(byte o)
        {
            return MK.Int(o);
        }
        public static TrObject Apply(double o)
        {
            return MK.Float(o);
        }

        public static TrObject Apply(List<TrObject> o)
        {
            return MK.List(o);
        }

        public static TrObject Apply(Dictionary<TrObject, TrObject> o)
        {
            return MK.Dict(o);
        }

    }
    public static class Unbox
    {
        public static string Apply(THint<string> _, TrObject o)
        {
            var s_o = o as TrStr;
            if (s_o != null)
            {
                return s_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to string");
        }

        public static byte[] Apply(THint<byte[]> _, TrObject o)
        {
            var b_o = o as TrBytes;
            if (b_o != null)
            {
                return b_o.contents;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to byte[]");
        }

        public static int Apply(THint<int> _, TrObject o)
        {
            var i_o = o as TrInt;
            if (i_o != null)
            {
                return (int)i_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to int");
        }

        public static long Apply(THint<long> _, TrObject o)
        {
            var i_o = o as TrInt;
            if (i_o != null)
            {
                return i_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to long");
        }

        public static float Apply(THint<float> _, TrObject o)
        {
            var i_o = o as TrFloat;
            if (i_o != null)
            {
                return i_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to float");
        }

        public static bool Apply(THint<bool> _, TrObject o)
        {
            var i_o = o as TrBool;
            if (i_o != null)
            {
                return i_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to bool");
        }
        public static IEnumerator<TrObject> Apply(THint<IEnumerator<TrObject>> _, TrObject o)
        {
            return o.__iter__();
        }

        public static List<TrObject> Apply(THint<List<TrObject>> _, TrObject o)
        {
            var l_o = o as TrList;
            if (l_o != null)
            {
                return l_o.container;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to list");
        }

        public static Dictionary<TrObject, TrObject> Apply(THint<Dictionary<TrObject, TrObject>> _, TrObject o)
        {
            var d_o = o as TrDict;
            if (d_o != null)
            {
                return d_o.container;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to dict");
        }

        public static byte Apply(THint<byte> _, TrObject o)
        {
            var i_o = o as TrInt;
            if (i_o != null)
            {
                return (byte)i_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to byte");
        }

        public static TrObject Apply(THint<TrObject> _, TrObject o)
        {
            return o;
        }
    }
}