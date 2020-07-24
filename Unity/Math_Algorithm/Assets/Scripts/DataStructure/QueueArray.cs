using System.Collections;
using System.Collections.Generic;
using System;

public class QueueArray<T> 
{
    const int _maxValue = 100;
    T[] a = new T[_maxValue];
    int _tale = 0;

    public int size
    {
        get
        {
            return _tale;
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
        if(_tale == _maxValue - 1)
        {
            Array.Resize<T>(ref a, _maxValue + 1);
        }

        a[_tale++] = element;
    }

    public T Dequeue()
    {
        if (isEmpty)
            return default(T);

        T temp = a[0];

        for(int n = 1; n <= _tale; n++)
        {
            a[n - 1] = a[n];
        }

        //a[_tale - 1] = default(T); // 안해줘도 될듯
        _tale--;

        return temp;
    }

    public T Front()
    {
        return a[0];
    }
}
