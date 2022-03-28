using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public static class Utils
{
    internal static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
    {
        return new HashSet<T>(source);
    }

    internal static List<T> ToList<T>(this IEnumerator<T> self)
    {
        var lst = new List<T>();
        while (self.MoveNext())
        {
            lst.Add(self.Current);
        }
        return lst;
    }
    internal static bool TryFindValue<T>(this List<T> self, Func<T, bool> predicate, out T value)
    {
        foreach (var item in self)
        {
            if (predicate(item))
            {
                value = item;
                return true;
            }
        }
        value = default(T);
        return false;
    }
    internal static G[] Map<T, G>(this T[] self, Func<T, G> tranform)
    {
        var xs = new G[self.Length];
        for (int i = 0; i < self.Length; i++)
            xs[i] = tranform(self[i]);
        return xs;
    }


    internal static int IndexOfNth(this string str, string value, int offset, int nth = 0)
    {
        if (nth < 0)
            throw new ArgumentException("Can not find a negative index of substring in string. Must start with 0");

        offset = str.IndexOf(value, offset);
        for (int i = 0; i < nth; i++)
        {
            if (offset == -1) return -1;
            offset = str.IndexOf(value, offset + 1);
        }

        return offset;
    }

    internal static int IndexOf<T>(this T[] array, T value) where T : IEquatable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(value))
                return i;
        }
        return -1;
    }

    public static string Escape(this string s)
    {
        var buf = new System.Text.StringBuilder();
        buf.Append('"');
        foreach (var c in s)
        {
            switch (c)
            {
                case '\n': buf.Append("\\n"); break;
                case '\r': buf.Append("\\r"); break;
                case '\t': buf.Append("\\t"); break;
                case '\f': buf.Append("\\f"); break;
                case '\b': buf.Append("\\b"); break;
                case '\\': buf.Append("\\\\"); break;
                case '\'': buf.Append("\\\'"); break;
                case '\"': buf.Append("\\\""); break;
                default:
                    buf.Append(c);
                    break;
            }
        }
        buf.Append('"');
        return buf.ToString();
    }

    public static string Unescape(string s)
    {
        var buf = new System.Text.StringBuilder();
        for (int i = 1; i < s.Length - 1; i++)
        {
            if (s[i] == '\\')
            {
                switch (s[i + 1])
                {
                    case 'n': buf.Append('\n'); break;
                    case 'r': buf.Append('\r'); break;
                    case 't': buf.Append('\t'); break;
                    case 'f': buf.Append('\f'); break;
                    case 'b': buf.Append('\b'); break;
                    case '\\': buf.Append('\\'); break;
                    case '\'': buf.Append('\''); break;
                    case '\"': buf.Append('\"'); break;
                    default:
                        buf.Append(s[i]);
                        break;
                }
                i++;
            }
            else
            {
                buf.Append(s[i]);
            }
        }
        return buf.ToString();
    }

    public static T By<G, T>(this G me, Func<G, T> apply)
    {
        return apply(me);
    }

    public static string Repeat(this string me, int i)
    {
        var buf = new System.Text.StringBuilder();
        for (int j = 0; j < i; j++)
        {
            buf.Append(me);
        }
        return buf.ToString();
    }

    public static byte[] Repeat(this byte[] me, int i)
    {
        var buf = new List<byte>();
        for (int j = 0; j < i; j++)
        {
            buf.AddRange(me);
        }
        return buf.ToArray();
    }

    public static List<byte> Repeat(this List<byte> me, int i)
    {
        var buf = new List<byte>();
        for (int j = 0; j < i; j++)
        {
            buf.AddRange(me);
        }
        return buf;
    }
    public static void By<G>(this G me, Action<G> apply)
    {
        apply(me);
    }
    internal static Dictionary<K, V> Copy<K, V>(this Dictionary<K, V> me)
    {
        var res = new Dictionary<K, V>(me.Comparer);
        foreach (var kv in me)
        {
            res.Add(kv.Key, kv.Value);
        }
        return res;
    }

    internal static IEnumerable<int> Range(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            yield return i;
        }
    }
    internal static bool TryPop<K, V>(this Dictionary<K, V> me, K key, out V v)
    {
        if (me.TryGetValue(key, out v))
        {
            me.Remove(key);
            return true;
        }
        return false;
    }
    internal static T[] GetRange<T>(this T[] arr, int index, int count)
    {
        T[] result = new T[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = arr[index + i];
        }
        return result;
    }
    internal static List<T> Copy<T>(this List<T> lst)
    {
        List<T> res = new List<T>(lst.Count);
        res.AddRange(lst);
        return res;
    }
    public static void ForEach<T>(this IEnumerable<T> lst, Action<T> action)
    {
        foreach (var e in lst)
        {
            action(e);
        }
    }

    internal static bool Exist<T>(this IEnumerable<T> lst, Func<T, bool> pred)
    {
        foreach (var e in lst)
        {
            if (pred(e))
                return true;
        }
        return false;
    }
    internal static bool Exist<T>(this T[] lst, Func<T, bool> pred)
    {
        for (var i = 0; i < lst.Length; i++)
        {
            if (pred(lst[i]))
                return true;
        }
        return false;
    }
}
