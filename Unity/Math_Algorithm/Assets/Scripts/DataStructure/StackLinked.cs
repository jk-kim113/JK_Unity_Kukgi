using System.Collections;
using System.Collections.Generic;

public class StackLinked<T>
{
    LinkedData<T> node = new LinkedData<T>();
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
        LinkedData<T> temp = new LinkedData<T>();
        temp._element = element;

        if (isEmpty)
        {
            node = temp;
        }
        else
        {
            temp._next = node;
            node = temp;
        }

        _count++;
    }

    public T Pop()
    {
        T temp = node._element;
        node = node._next;

        _count--;

        return temp;
    }

    public T Top()
    {
        return node._element;
    }
}
