using System.Collections;
using System.Collections.Generic;

public class QueueData<T>
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

    public void Enqueue(T element)
    {
        if (isEmpty)
        {
            _header = new Node<T>();
            _header._element = element;
        }
        else
        {
            Node<T> temp = new Node<T>();
            temp._element = element;

            Node<T> current = _header;
            for (int n = 0; n < _count - 1; n++)
            {
                current = current._next;
            }

            current._next = temp;
        }

        _count++;
    }

    public T Dequeue()
    {
        T temp = _header._element;
        _header = _header._next;
        _count--;

        return temp;
    }

    public T Front()
    {
        return _header._element;
    }
}
