using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Traffy.Objects;
using Traffy;
public static partial class JsonExt
{
    public static T JsonParse<T>(string s)
    {
        return SimpleJSON.JSON.Deserialize<T>(s);
    }
}

public class Mark : Attribute
{
    public object Token;
    public Mark(object token = null)
    {
        Token = token;
    }

    public static IEnumerable<(Type t, Mark attr, Action method)> Query(Type entry, Func<object, bool> predicate)
    {
        return Assembly
            .GetAssembly(entry)
            .GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Select(x => (t, x)))
            .Where(((Type t, MethodInfo mi) pair) =>
            {
                var attr = pair.mi.GetCustomAttribute<Mark>();
                return attr != null && pair.mi.GetParameters().Length == 0 && predicate(attr.Token);
            })
            .Select(((Type t, MethodInfo mi) pair) => (
                pair.t,
                pair.mi.GetCustomAttribute<Mark>(),
                (Action)Delegate.CreateDelegate(typeof(Action), null, pair.mi)));
    }
}

// public class InitSetup : Attribute
// {
//     public int Order;
//     public InitSetup(int order = 0)
//     {
//         Order = order;
//     }
//     static IEnumerable<(InitSetup attr, T method)> GetStaticAttributedMethods<T>(Type self, Func<MethodInfo, bool> predicate) where T : Delegate
//     {
//         return self
//             .GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
//             .Where(mi =>
//             {
//                 var attr = mi.GetCustomAttribute<InitSetup>();
//                 return attr != null && predicate(mi);
//             })
//             .Select(mi => (mi.GetCustomAttribute<InitSetup>(), (T)Delegate.CreateDelegate(typeof(T), null, mi)));
//     }
//     public static void ApplyInitialization()
//     {
//         bool predicate(MethodInfo m)
//         {
//             var ps = m.GetParameters();
//             return ps.Length == 0;
//         }
//         Assembly
//             .GetExecutingAssembly()
//             .GetTypes()
//             .SelectMany(x =>
//                 GetStaticAttributedMethods<Action>(x, predicate))
//             .OrderBy(x => x.attr.Order)
//             .ForEach(x => x.method());
//     }
//     public static void ApplyInitialization<A>(A arg)
//     {
//         bool predicate(MethodInfo m)
//         {
//             var ps = m.GetParameters();
//             return ps.Length == 1 && ps[0].ParameterType == typeof(A);
//         }
//         Assembly
//             .GetExecutingAssembly()
//             .GetTypes()
//             .SelectMany(x =>
//                 GetStaticAttributedMethods<Action<A>>(x, predicate))
//             .OrderBy(x => x.attr.Order)
//             .ForEach(x => x.method(arg));
//     }

//     public static void ApplyInitialization<A, B>(A arg1, B arg2)
//     {
//         bool predicate(MethodInfo m)
//         {
//             var ps = m.GetParameters();
//             return ps.Length == 2 && ps[0].ParameterType == typeof(A) && ps[1].ParameterType == typeof(B);
//         }
//         Assembly
//             .GetExecutingAssembly()
//             .GetTypes()
//             .SelectMany(x =>
//                 GetStaticAttributedMethods<Action<A, B>>(x, predicate))
//             .OrderBy(x => x.attr.Order)
//             .ForEach(x => x.method(arg1, arg2));
//     }
// }

public static class Utils
{
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

    public static bool SequenceLessThan(this List<TrObject> seq1, List<TrObject> seq2)
    {
        for (int i = 0; i < seq1.Count; i++)
        {
            if (i == seq2.Count)
            {
                return false;
            }
            else if (seq1[i].__lt__(seq2[i]))
            {
                return true;
            }
        }
        if (seq2.Count > seq1.Count)
            return true;
        return false;
    }

    public static bool SequenceLessThan(this TrObject[] seq1, TrObject[] seq2)
    {
        for (int i = 0; i < seq1.Length; i++)
        {
            if (i == seq2.Length)
            {
                return false;
            }
            else if (seq1[i].__lt__(seq2[i]))
            {
                return true;
            }
        }
        if (seq2.Length > seq1.Length)
            return true;
        return false;
    }
    public static bool SequenceLessThan(this string seq1, string seq2)
    {
        for (int i = 0; i < seq1.Length; i++)
        {
            if (i == seq2.Length)
            {
                return false;
            }
            else if (seq1[i] < seq2[i])
            {
                return true;
            }
        }
        if (seq2.Length > seq1.Length)
            return true;
        return false;
    }

    public static bool SequenceLessEqualThan(this List<TrObject> seq1, List<TrObject> seq2)
    {
        for (int i = 0; i < seq1.Count; i++)
        {
            if (i == seq2.Count)
            {
                return false;
            }
            else if (RTS.object_le(seq1[i], seq2[i]).__bool__())
            {
                return true;
            }
            {
                return true;
            }
        }
        if (seq2.Count >= seq1.Count)
            return true;
        return false;
    }

    public static bool SequenceLessEqualThan(this TrObject[] seq1, TrObject[] seq2)
    {
        for (int i = 0; i < seq1.Length; i++)
        {
            if (i == seq2.Length)
            {
                return false;
            }
            else if (RTS.object_le(seq1[i], seq2[i]).__bool__())
            {
                return true;
            }
        }
        if (seq2.Length >= seq1.Length)
            return true;
        return false;
    }
    public static bool SequenceLessEqualThan(this string seq1, string seq2)
    {
        for (int i = 0; i < seq1.Length; i++)
        {
            if (i == seq2.Length)
            {
                return false;
            }
            else if (seq1[i] <= seq2[i])
            {
                return true;
            }
        }
        if (seq2.Length >= seq1.Length)
            return true;
        return false;
    }
}
