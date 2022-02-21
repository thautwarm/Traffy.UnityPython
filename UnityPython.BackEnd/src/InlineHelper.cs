using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace InlineHelper
{
    public struct FList<T> : IList<T> where T: IEquatable<T>
    {
        private List<T> _list;
        public List<T> UnList => _list;

        public T this[int index] { get => _list[index]; set => _list[index] = value; }

        public int Count => _list.Count;

        public bool IsReadOnly => true;

        public void Add(T item) => throw new NotSupportedException("InlineArrayAsList is read-only");

        public void Clear() => throw new NotSupportedException("InlineArrayAsList is read-only");
        public bool Contains(T item)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(int i = 0; i < _list.Count; i++)
            {
                array[arrayIndex + i] = _list[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _list).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for(int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item) => throw new NotSupportedException("InlineArrayAsList is read-only");

        public bool Remove(T item) => throw new NotSupportedException("InlineArrayAsList is read-only");

        public void RemoveAt(int index) => throw new NotSupportedException("InlineArrayAsList is read-only");

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public static implicit operator FList<T>(List<T> list)
        {
            return new FList<T>() { _list = list };
        }

        public static implicit operator List<T>(FList<T> list)
        {
            return list._list;
        }
    }
    public struct FArray<T> : IList<T> where T: IEquatable<T>
    {
        private T[] _array;

        public T[] UnList => _array;

        public T this[int index] { get => _array[index]; set => _array[index] = value; }

        public int Count => _array.Length;

        public bool IsReadOnly => true;

        public void Add(T item) => throw new NotSupportedException("InlineArrayAsList is read-only");

        public void Clear() => throw new NotSupportedException("InlineArrayAsList is read-only");
        public bool Contains(T item)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(int i = 0; i < _array.Length; i++)
            {
                array[arrayIndex + i] = _array[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _array).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for(int i = 0; i < _array.Length; i++)
            {
                if (_array[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item) => throw new NotSupportedException("InlineArrayAsList is read-only");

        public bool Remove(T item) => throw new NotSupportedException("InlineArrayAsList is read-only");

        public void RemoveAt(int index) => throw new NotSupportedException("InlineArrayAsList is read-only");

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _array.GetEnumerator();
        }

        public static implicit operator FArray<T>(T[] array)
        {
            return new FArray<T>() { _array = array };
        }

        public static implicit operator T[](FArray<T> list)
        {
            return list._array;
        }
    }

    public struct FString : IList<char>
    {
        string s;

        public string UnList => s;

        public char this[int index] { get => s[index]; set => throw new NotSupportedException("Cannot mutate a list"); }

        public int Count => s.Length;

        public bool IsReadOnly => true;

        public void Add(char item)
        {
            throw new NotSupportedException("Cannot mutate a list");
        }

        public void Clear()
        {
            throw new NotSupportedException("Cannot mutate a list");
        }

        public bool Contains(char item)
        {
            return s.Contains(item);
        }

        public void CopyTo(char[] array, int arrayIndex)
        {
            for (int i = 0; i < s.Length; i++)
            {
                array[arrayIndex + i] = s[i];
            }
        }

        public IEnumerator<char> GetEnumerator()
        {
            return s.GetEnumerator();
        }

        public int IndexOf(char item)
        {
            return s.IndexOf(item);
        }

        public void Insert(int index, char item)
        {
            throw new NotSupportedException("Cannot mutate a list");
        }

        public bool Remove(char item)
        {
            throw new NotSupportedException("Cannot mutate a list");
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("Cannot mutate a list");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return s.GetEnumerator();
        }

        public static implicit operator FString(string s)
        {
            return new FString { s = s };
        }

        public static implicit operator string(FString w)
        {
            return w.s;
        }
    }

    public static class ExtInlineHelper
    {
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static FList<T> Inline<T>(this List<T> list) where T: IEquatable<T>
        {
            return list;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static FArray<T> Inline<T>(this T[] list) where T: IEquatable<T>
        {
            return list;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static FString Inline(this string list) => list;

    }
}