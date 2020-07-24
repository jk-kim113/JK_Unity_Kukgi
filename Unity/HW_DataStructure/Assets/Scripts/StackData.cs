using System.Collections;
using System.Collections.Generic;


public class StackData<T>
{
    Node<T> _header = new Node<T>();
    int _count = 0;

    public int size
    {
        get
        {
            return _count;
        }
    }

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

    public void Push(T element)
    {
        Node<T> temp = new Node<T>();
        temp._element = element;

        if (isEmpty)
        {
            _header = temp;
        }
        else
        {
            temp._next = _header;
            _header = temp;
        }

        _count++;
    }

    public T Pop()
    {
        T temp = _header._element;
        _header = _header._next;

        _count--;

        return temp;
    }

    public T Top()
    {
        return _header._element;
    }
}
