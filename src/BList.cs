using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BList<T>: IList<T>
{
    public List<T> right = new List<T>();
    public List<T> left = new List<T>();
    public T this[int i] {
        set {
            if (i < left.Count)
            {
                this.left[left.Count - i - 1] = value;
            }
            else
            {
                this.right[i - left.Count] = value;
            }
        }

        get {
            if (i < left.Count)
            {
                return this.left[left.Count - i - 1];
            }
            else
            {
                return this.right[i - left.Count];
            }
        }
    }

    public int Count => this.left.Count + this.right.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        right.Add(item);
    }


    public void AddLeft(T item)
    {
        left.Add(item);
    }


    public void Clear()
    {
        left.Clear();
        right.Clear();
    }

    public bool Contains(T item)
    {
        return left.Contains(item) || right.Contains(item);
    }


    // TODO: check correctness
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (left.Count == 0)
        {
            right.CopyTo(array, arrayIndex);
        }
        else
        {
            for(int i = left.Count - 1, j = 0; i >= 0; i--)
            {
                array[arrayIndex + j++] = left[i];
            }
            right.CopyTo(array, arrayIndex + left.Count);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for(int i = left.Count - 1; i >= 0; i--)
        {
            yield return left[i];
        }
        var right_itr = right.GetEnumerator();
        while (right_itr.MoveNext())
        {
            yield return right_itr.Current;
        }
    }

    public void ForEach(System.Action<int, T> act)
    {
        int k = 0;
        for(int i = left.Count - 1; i >= 0; i--)
        {
            act(k, left[i]);
            k++;
        }
        var right_itr = right.GetEnumerator();
        while (right_itr.MoveNext())
        {
            act(k, right_itr.Current);
            k++;
        }
    }
    public void ForEach(System.Action<T> act)
    {
        for(int i = left.Count - 1; i >= 0; i--)
        {
            act(left[i]);
        }
        var right_itr = right.GetEnumerator();
        while (right_itr.MoveNext())
        {
            act(right_itr.Current);
        }
    }
    public int IndexOf(T item)
    {
        var i = left.IndexOf(item);
        if (i != -1)
        {
            return left.Count - i - 1;
        }
        return right.IndexOf(item) + left.Count;
    }

    public void Insert(int index, T item)
    {
        if (index <= left.Count)
        {
            var i = left.Count - index;
            left.Insert(i, item);
        }
        else
        {
            System.Console.WriteLine(index);
            right.Insert(index - left.Count, item);
        }
    }
    public void Pop()
    {
        right.RemoveAt(right.Count - 1);
    }

    public T PopLeft()
    {
        var o = left[right.Count - 1];
        left.RemoveAt(right.Count - 1);
        return o;
    }
    // TODO
    public bool Remove(T item)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var itr = this.GetEnumerator();
        return itr;
    }

    public override string ToString()
    {
        var lcopy = left.GetRange(0, left.Count);
        lcopy.Reverse();
        lcopy.AddRange(right);
        return "[" + string.Join(", ",  lcopy) + "]";
    }
}