using System.Collections.Generic;

public class OrderedList<T>
{
    private readonly List<T> _list;
    private readonly IComparer<T> _comparer;

    public int Count
    {
        get { return _list.Count; }
    }

    public T this[int index]
    {
        get { return _list[index]; }
        set { _list[index] = value; }
    }

    public OrderedList(int capacity, IComparer<T> comparer)
    {
        _list = new List<T>(capacity);
        _comparer = comparer;
    }

    public int Add(T item)
    {
        int index = _list.BinarySearch(item, _comparer);
        if (index < 0)
        {
            index = ~index;
        }
        _list.Insert(index, item);
        return index;
    }

    public bool Contains(T item)
    {
        return _list.Contains(item);
    }

    public void RemoveAt(int index) { _list.RemoveAt(index); }
}