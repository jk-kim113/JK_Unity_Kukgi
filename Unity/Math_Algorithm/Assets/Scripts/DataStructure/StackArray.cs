using System.Collections;
using System.Collections.Generic;

public class StackArray<T>
{
    const int _maxValue = 100;
    T[] a = new T[_maxValue];
    int _count;

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
            if (_count == 0)
                return true;
            else
                return false;
        }
    }

    public void Push(T element)
    {
        if(!isEmpty)
        {
            for (int n = _count; n > 0; n--)
            {
                a[n] = a[n - 1];
            }
        }

        a[0] = element;
        _count++;
    }

    public T Pop()
    {
        T temp = a[0];
        for(int n = 0; n < _count; n++)
        {
            a[n] = a[n + 1];
        }

        _count--;

        return temp;
    }

    public T Top()
    {
        return a[0];
    }
}
