// A random-access list that supports bidirectionally pushing elements.
using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;

// a double-ended list that has infinite capacity and random access.
public enum ListSizeKind : int
{
    Size3 = 0b1,
    Size7 = 0b11,
    Size15 = 0b111,
    Size31 = 0b1111,
    Size255 = 0b1111111,
}

public sealed class BList<T> : IList<T>
{

    private T[] _array;
    private int _head;
    private int _tail;

    public int Count => _tail - _head;


    public bool IsReadOnly => false;
    public T this[int index]
    {
        get => _array[_head + index];
        set => _array[_head + index] = value;
    }


    public BList(ListSizeKind sizeKind)
    {
        int capacity = (int)sizeKind;
        _array = new T[(capacity << 1) + 1];
        _head = 2;
        _tail = 2;
    }

    public BList()
    {
        _array = new T[7];
        _head = 2;
        _tail = 2;
    }

    // the left side is full, double the capacity
    [MethodImpl(MethodImplOptionsCompat.Best)]
    void GrowLeft()
    {
        int oldCapacity = _array.Length;
        T[] newArray = new T[(oldCapacity << 1) + 1];
        Array.Copy(_array, 0, newArray, oldCapacity + 1, _tail);
        _array = newArray;
        // '_head == 0'
        _head = oldCapacity + 1;
        _tail = oldCapacity + 1 + _tail;
    }

    // the right side is full, double the capacity
    [MethodImpl(MethodImplOptionsCompat.Best)]
    void GrowRight()
    {
        int oldCapacity = _array.Length;
        T[] newArray = new T[(oldCapacity << 1) + 1];
        Array.Copy(_array, _head, newArray, _head, oldCapacity - _head);
        _array = newArray;
    }

    // push an element to the left side;
    // if the left side is full, the underlying array will grow,
    // '_head' and '_tail' will be updated accordingly.
    [MethodImpl(MethodImplOptionsCompat.Best)]
    public void AddLeft(T item)
    {
        if (_head == 0)
        {
            GrowLeft();
        }
        _array[--_head] = item;
    }

    [MethodImpl(MethodImplOptionsCompat.Best)]
    public void AddRight(T item)
    {
        if (_tail == _array.Length)
        {
            GrowRight();
        }
        _array[_tail++] = item;
    }

    [MethodImpl(MethodImplOptionsCompat.Best)]
    public T PopLeft()
    {
        if (_head == _tail)
        {
            throw new IndexOutOfRangeException();
        }
        return _array[_head++];
    }

    [MethodImpl(MethodImplOptionsCompat.Best)]
    public T PopRight()
    {
        if (_head == _tail)
        {
            throw new IndexOutOfRangeException();
        }
        return _array[--_tail];
    }

    [MethodImpl(MethodImplOptionsCompat.Best)]
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = _head; i < _tail; i++)
        {
            yield return _array[i];
        }
    }

    public int IndexOf(T item)
    {
        for (int i = _head; i < _tail; i++)
        {
            if (_array[i].Equals(item))
            {
                return i - _head;
            }
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0)
        {
            AddLeft(item);
        }
        else if (index >= Count)
        {
            AddRight(item);
        }
        else
        {
            if (_tail == _array.Length)
            {
                // the right side is full, double the capacity
                GrowRight();
            }
            Array.Copy(_array, index + _head, _array, index + _head + 1, Count - index);
            _array[index + _head] = item;
            _tail++;
        }
    }

    public void RemoveAt(int index)
    {
        if (index < 0)
        {
            throw new IndexOutOfRangeException();
        }
        else if (index >= _tail)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            Array.Copy(_array, index + _head + 1, _array, index + _head, Count - index - 1);
            _tail--;
        }
    }

    [MethodImpl(MethodImplOptionsCompat.Best)]
    public void Add(T item)
    {
        AddRight(item);
    }


    public void Clear()
    {
        int oldCapacity = _array.Length / 2;
        _head = oldCapacity;
        _tail = oldCapacity;
    }

    public bool Contains(T item)
    {
        for (int i = _head; i < _tail; i++)
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
        Array.Copy(_array, _head, array, arrayIndex, Count);
    }

    public T[] ToArray()
    {
        T[] newArray = new T[Count];
        Array.Copy(_array, _head, newArray, 0, Count);
        return newArray;
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index < 0)
        {
            return false;
        }
        RemoveAt(index);
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}