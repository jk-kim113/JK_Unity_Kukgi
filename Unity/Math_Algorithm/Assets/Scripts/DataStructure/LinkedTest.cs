using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class LinkedTest<T>
{
    LinkedData<T> _header;
    public LinkedData<T> _Header { get { return _header; } }

    int _size = 0;
    public int size { get { return _size; } }

    public bool isEmpty 
    {
        get
        {
            if (size == 0)
                return true;
            else
                return false;
        }
    }

    public void AddLast(T element)
    {
        if (_header == null)
        {
            _header = new LinkedData<T>();

            _header._element = element;
            _header._prev = null;
            _header._next = null;
        }
        else
        {
            LinkedData<T> toAdd = new LinkedData<T>();
            toAdd._element = element;

            LinkedData<T> current = _header;
            while (current._next != null)
            {
                current = current._next;
            }

            current._next = toAdd;
            toAdd._prev = current;
            toAdd._next = null;
        }

        _size++;
    }

    public void AddFirst(T element)
    {
        LinkedData<T> toAdd = new LinkedData<T>();

        if(_header != null)
            _header._prev = toAdd;

        toAdd._element = element;
        toAdd._next = _header;

        _header = toAdd;

        _size++;
    }

    public void Add(T element, int idx)
    {
        LinkedData<T> toAdd = new LinkedData<T>();
        toAdd._element = element;

        LinkedData<T> current = _header;
        for(int n = 0; n < idx; n++)
        {
            current = current._next;
        }

        LinkedData<T> previous = current._prev;
        previous._next = toAdd;

        toAdd._prev = current._prev;
        current._prev = toAdd;
        toAdd._next = current;

        _size++;
    }

    public void RemoveAt(int idx)
    {
        LinkedData<T> current = _header;
        for (int n = 0; n < idx; n++)
        {
            current = current._next;
        }

        LinkedData<T> previous = current._prev;
        LinkedData<T> next = current._next;
        previous._next = next;
        current._prev = previous;

        _size--;
    }

    public void Remove(T element)
    {
        LinkedData<T> current = _header;
        while(current._next != null)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            if(comparer.Compare(current._element, element) == 0)
            {
                LinkedData<T> previous = current._prev;
                LinkedData<T> next = current._next;
                previous._next = next;
                current._prev = previous;

                _size--;
            }

            current = current._next;
        }
    }

    public T Get(int idx)
    {
        LinkedData<T> current = _header;
        for (int n = 0; n < idx; n++)
        {
            current = current._next;
        }

        return current._element;
    }

    public void Set(int idx, T element)
    {
        LinkedData<T> current = _header;
        for (int n = 0; n < idx; n++)
        {
            current = current._next;
        }

        current._element = element;
    }
}