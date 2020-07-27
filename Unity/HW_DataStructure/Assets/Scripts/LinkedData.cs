using System.Collections;
using System.Collections.Generic;

public class LinkedData<T>
{
    Node<T> _header;

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
        Node<T> temp = new Node<T>();
        temp._element = element;

        if (_header == null)
        {
            _header = new Node<T>();
            _header = temp;
        }
        else
        {
            Node<T> current = _header;
            while (current._next != null)
            {
                current = current._next;
            }

            current._next = temp;
        }

        _size++;
    }

    public void Remove(int idx)
    {
        if(idx == 0)
        {
            _header = _header._next;
        }
        else if(idx == 1)
        {
            _header._next = _header._next._next;
        }
        else if(idx == 2)
        {
            _header._next._next = null;
        }

        //if(size == 1)
        //{
        //    _header = null;
        //}
        //else
        //{
        //    Node<T> previous = _header;

        //    for (int n = 0; n < idx - 1; n++)
        //    {
        //        previous = previous._next;
        //    }

        //    previous._next = previous._next._next;
        //}

        _size--;
    }

    public T Get(int idx)
    {
        Node<T> current = _header;
        for (int n = 0; n < idx; n++)
        {
            current = current._next;
        }

        return current._element;
    }

    public void Set(int idx, T element)
    {
        Node<T> current = _header;
        for (int n = 0; n < idx; n++)
        {
            current = current._next;
        }

        current._element = element;
    }
}
