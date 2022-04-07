using System;
using System.Collections.Generic;
using System.Linq;

public sealed class MemLessIntMap<V>
{
    int[] s_Keys;
    V[] s_Values;
    public MemLessIntMap()
    {
        s_Keys = Array.Empty<int>();
        s_Values = Array.Empty<V>();
    }

    public V GetOrUpdate(int key, Func<V> create)
    {
        if (TryGetValue(key, out var value))
            return value;

        value = create();
        (int Key, V Value)[] kvs = s_Keys.Zip(s_Values, (k, v) => (k, v)).Append((key, value)).ToArray();
        Array.Sort(kvs, (x, y) => x.Key.CompareTo(y.Key));
        s_Keys = kvs.Select(x => x.Key).ToArray();
        s_Values = kvs.Select(x => x.Value).ToArray();
        return value;
    }

    public V[] GetOrUpdateMany(int[] keys, Func<int, V> create)
    {
        var o = new Dictionary<int, V>();
        var newKeys = new HashSet<int>();
        for(int i = 0; i < s_Keys.Length; i++)
        {
            o[s_Keys[i]] = s_Values[i];
        }
        foreach(int k in keys)
        {
            if (o.ContainsKey(k))
                continue;
            o[k] = create(k);
            newKeys.Add(k);
        }
        var kvs = o.ToArray();
        Array.Sort(kvs, (x, y) => x.Key.CompareTo(y.Key));
        s_Keys = kvs.Select(x => x.Key).ToArray();
        s_Values = kvs.Select(x => x.Value).ToArray();
        return keys.Select(x => o[x]).ToArray();
    }

    public V this[int key]
    {
        get
        {
            var index = Array.BinarySearch(s_Keys, key);
            if (index < 0)
                throw new KeyNotFoundException();
            return s_Values[index];
        }
    }

    public bool TryGetValue(int key, out V value)
    {
        // if it's a small map, use linear scan
        if (s_Keys.Length < 33)
        {
            for (int i = 0; i < s_Keys.Length; i++)
            {
                if (s_Keys[i] == key)
                {
                    value = s_Values[i];
                    return true;
                }
            }
        }
        else
        {
            var index = Array.BinarySearch(s_Keys, key);
            if (index >= 0)
            {
                value = s_Values[index];
                return true;
            }
        }
        value = default(V);
        return false;
    }
}