using System.Collections.Generic;
using Traffy.Objects;

namespace Traffy
{
    public static partial class Box
    {
        public static TrObject Apply(TrObject o)
        {
            return o;
        }
        public static TrObject Apply<T>(T o) where T: TrObject
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

        public static TrObject Apply(byte[] o)
        {
            return MK.Bytes(o);
        }

        public static TrObject Apply(List<byte> o)
        {
            return MK.ByteArray(o);
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

        public static TrObject Apply(HashSet<TrObject> o)
        {
            return MK.Set(o);
        }

        public static TrObject Apply(Awaitable<TrObject> awaitable)
        {
            return TrGenerator.Create(awaitable);
        }

        public static TrObject Apply(IEnumerator<TrObject> awaitable)
        {
            return MK.Iter(awaitable);
        }

        public static TrObject Apply(IEnumerable<TrObject> awaitable)
        {
            return MK.Iter(awaitable.GetEnumerator());
        }

    }
    public static class Unbox
    {

        public static TrObject Apply(THint<TrObject> _, TrObject o)
        {
            return o;
        }

        public static T Apply<T>(THint<T> _, TrObject o) where T : TrObject
        {
            var s_o = o as T;
            if ((object)s_o != null)
            {
                return s_o;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to string");
        }

        public static string Apply(THint<string> _, TrObject o)
        {
            var s_o = o as TrStr;
            if (s_o != null)
            {
                return s_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to string");
        }

        public static TrRef Apply(THint<TrRef> _, TrObject o)
        {
            var s_o = o as TrRef;
            if (s_o != null)
            {
                return s_o;
            }
            throw new TypeError($"requires '{TrRef.CLASS.Name}', got {o.Class.Name}");
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
                return (float) i_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to float");
        }

        public static double Apply(THint<double> _, TrObject o)
        {
            var i_o = o as TrFloat;
            if (i_o != null)
            {
                return i_o.value;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to double float");
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

        public static HashSet<TrObject> Apply(THint<HashSet<TrObject>> _, TrObject o)
        {
            var s_o = o as TrSet;
            if (s_o != null)
            {
                return s_o.container;
            }
            throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to set");   
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

        public static IList<byte> Apply(THint<IList<byte>> _, TrObject o)
        {
            switch(o)
            {
                case TrBytes b:
                    return b.contents.UnList;
                case TrByteArray l:
                    return l.contents.UnList;
                default:
                    throw new TypeError($"Unbox.Apply: cannot unbox {o.Class.Name} to bytes-like");
            }
        }
    }
}