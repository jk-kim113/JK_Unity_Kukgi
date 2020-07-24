using System.Collections;
using System.Collections.Generic;

public class QueueLinked<T>
{
    // LinkedList를 이용하면 할당 중복(?)이 발생하여 안좋음, Node를 이용해야 함
    LinkedData<T> node = new LinkedData<T>();
    int _count  = 0;

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
        if(isEmpty)
        {
            node._element = element;
        }
        else
        {
            LinkedData<T> temp = new LinkedData<T>();
            temp._element = element;

            LinkedData<T> current = node;
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
        T temp = node._element;
        node = node._next;
        _count--;

        return temp;
    }

    public T Front()
    {
        return node._element;
    }
}
