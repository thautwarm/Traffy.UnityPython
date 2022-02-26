using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;

public static partial class JsonExt
{
    public static T JsonParse<T>(string s)
    {
        return SimpleJSON.JSON.Deserialize<T>(s);
    }
}

public static class Utils
{

    public static bool TryFindValue<T>(this List<T> self, Func<T, bool> predicate, out T value)
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
    public static G[] Map<T, G>(this T[] self, Func<T, G> tranform)
    {
        var xs = new G[self.Length];
        for (int i = 0; i < self.Length; i++)
            xs[i] = tranform(self[i]);
        return xs;
    }

    public static int ByteSequenceHash<TList>(this TList xs, int seed, int primSeed) where TList : IList<byte>
    {
        unchecked
        {
            int hash = seed;
            for (int i = 0; i < xs.Count; i++)
            {
                hash = hash * primSeed + xs[i];
            }
            return hash;
        }
    }

    // length equal
    public static bool SeqEq<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IEquatable<T>
    {
        if (seq1.Count != seq2.Count)
            return false;
        for (int i = 0; i < seq1.Count; i++)
        {
            if (!seq1[i].Equals(seq2[i]))
                return false;
        }
        return true;
    }

    public static bool SeqNe<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IEquatable<T>
    {
        if (seq1.Count != seq2.Count)
            return true;
        for (int i = 0; i < seq1.Count; i++)
        {
            if (seq1[i].Equals(seq2[i]))
                return false;
        }
        return true;
    }


    public static bool SeqLtE<Col1, Col2, T>(this Col1 seq1, Col2 seq2, out bool seqIsEqual) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
    {
        var commonLen = Math.Min(seq1.Count, seq2.Count);
        int cmp;
        for (int i = 0; i < commonLen; i++)
        {
            cmp = seq1[i].CompareTo(seq2[i]);
            if (cmp < 0)
            {
                seqIsEqual = false;
                return true;
            }
            else if (cmp == 0)
            {
                continue;
            }
            else
            {
                seqIsEqual = false;
                return false;
            }
        }
        if (seq1.Count < seq2.Count)
        {
            seqIsEqual = false;
            return true;
        }
        else if (seq1.Count == seq2.Count)
        {
            seqIsEqual = true;
            return true;
        }
        seqIsEqual = false;
        return false;
    }

    [MethodImpl(MethodImplOptionsCompat.Best)]
    public static bool SeqLt<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
    {
        return seq1.SeqLtE<Col1, Col2, T>(seq2, out var isEqual) && !isEqual;
    }

    public static bool SeqGtE<Col1, Col2, T>(this Col1 seq1, Col2 seq2, out bool seqIsEqual) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
    {
        var commonLen = Math.Min(seq1.Count, seq2.Count);
        for (int i = 0; i < commonLen; i++)
        {
            var cmp = seq1[i].CompareTo(seq2[i]);
            if (cmp > 0)
            {
                seqIsEqual = false;
                return true;
            }
            else if (cmp == 0)
            {
                continue;
            }
            else
            {
                seqIsEqual = false;
                return false;
            }
        }
        if (seq1.Count > seq2.Count)
        {
            seqIsEqual = false;
            return true;
        }
        else if (seq1.Count == seq2.Count)
        {
            seqIsEqual = true;
            return true;
        }
        seqIsEqual = false;
        return false;
    }

    [MethodImpl(MethodImplOptionsCompat.Best)]
    public static bool SeqGt<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
    {
        return seq1.SeqGtE<Col1, Col2, T>(seq2, out var isEqual) && !isEqual;
    }

    public static T[] ConcatArray<T>(this T[] self, T[] other)
    {
        var xs = new T[self.Length + other.Length];
        for (int i = 0; i < self.Length; i++)
            xs[i] = self[i];
        for (int i = 0, j = self.Length; i < other.Length; i++, j++)
            xs[j] = other[i];
        return xs;
    }

    public static int IndexOfNth(this string str, string value, int offset, int nth = 0)
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

    public static int IndexOf<T>(this T[] array, T value) where T : IEquatable<T>
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
        buf.Append("\"");
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
        buf.Append("\"");
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
    public static void By<G>(this G me, Action<G> apply)
    {
        apply(me);
    }
    public static Dictionary<K, V> Copy<K, V>(this Dictionary<K, V> me)
    {
        var res = new Dictionary<K, V>(me.Comparer);
        foreach (var kv in me)
        {
            res.Add(kv.Key, kv.Value);
        }
        return res;
    }

    public static IEnumerable<int> Range(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            yield return i;
        }
    }
    public static bool TryPop<K, V>(this Dictionary<K, V> me, K key, out V v)
    {
        if (me.TryGetValue(key, out v))
        {
            me.Remove(key);
            return true;
        }
        return false;
    }
    public static T[] GetRange<T>(this T[] arr, int index, int count)
    {
        T[] result = new T[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = arr[index + i];
        }
        return result;
    }
    public static List<T> Copy<T>(this List<T> lst)
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

    public static bool Exist<T>(this IEnumerable<T> lst, Func<T, bool> pred)
    {
        foreach (var e in lst)
        {
            if (pred(e))
                return true;
        }
        return false;
    }
    public static bool Exist<T>(this T[] lst, Func<T, bool> pred)
    {
        for (var i = 0; i < lst.Length; i++)
        {
            if (pred(lst[i]))
                return true;
        }
        return false;
    }
}
