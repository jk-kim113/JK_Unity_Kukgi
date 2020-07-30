using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
//using UnityEngine;

public class ArrayTest<T>
{
    T[] a;

    public T this[int index]
    {
        get { return a[index]; }
        set 
        {
            try
            {
                a[index] = value;
            }
            catch (IndexOutOfRangeException ex)
            {
                UnityEngine.Debug.Log(ex.Message);
            }
        }
    }


    public int size
    {
        get 
        {
            if (a == null)
                return 0;
            else
                return a.Length; 
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

    public void Add(T element, int idx)
    {
        if(idx >= size)
        {
            // 배열을 한개 확장한 후 맨뒤에 element를 넣는다
            idx = size;
            Array.Resize<T>(ref a, idx + 1);
            a[idx] = element;
        }
        else
        {
            // 배열을 한개 확장한 후 idx 위치에 element를 넣는다
            Array.Resize<T>(ref a, a.Length + 1);
            for(int n = a.Length - 1; n > idx; n--)
            {
                a[n] = a[n - 1];
            }

            a[idx] = element;
        }
    }

    public void Remove(int idx)
    {
        if(idx >= size)
        {
            Array.Resize<T>(ref a, a.Length - 1);
        }
        else
        {
            for (int n = idx; n < a.Length - 1; n++)
            {
                a[n] = a[n + 1];
            }

            Array.Resize<T>(ref a, a.Length - 1);
        }
    }

    public T Get(int idx)
    {
        try
        {
            return a[idx];
        }
        catch (IndexOutOfRangeException ex)
        {
            UnityEngine.Debug.Log(ex.Message);
            return default(T);
        }
    }

    public void Set(int idx, T obj)
    {
        try
        {
            a[idx] = obj;
        }
        catch (IndexOutOfRangeException ex)
        {
            UnityEngine.Debug.Log(ex.Message);
        }
    }
}
