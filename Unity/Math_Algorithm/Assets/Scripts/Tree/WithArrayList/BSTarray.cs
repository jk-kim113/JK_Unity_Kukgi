using System.Collections;
using System.Collections.Generic;

public class BSTarray
{
    int[] _arr;
    public int[] _Arr
    {
        get
        {
            return _arr;
        }
    }

    int _count;
    int _maxIndex = 100;

    public BSTarray()
    {
        _arr = new int[_maxIndex];
        for (int n = 0; n < _arr.Length; n++)
            _arr[n] = int.MinValue;
        _count = 0;
    }

    public void Add(int data)
    {
        if(_count == 0)
        {
            _arr[_count] = data;
        }
        else
        {
            int compare = 1;

            while(true)
            {
                if (_arr[compare - 1] > data)
                {
                    compare = 2 * compare;
                    if (_arr[compare - 1] < 0)
                        break;
                }
                else if (_arr[compare - 1] < data)
                {
                    compare = 2 * compare + 1;
                    if (_arr[compare - 1] < 0)
                        break;
                }
                else
                    return;
            }

            _arr[compare - 1] = data;
        }

        _count++;
    }

    public void Remove(int index)
    {
        _arr[index - 1] = int.MinValue;

        if (_arr[2 * index - 1] < 0 && _arr[2 * index] < 0)
        {
            index = 2 * index;
            while(_arr[2 * index - 1] < 0)
            {
                _arr[index - 1] = int.MinValue;
            }
        }

        // 너무 어려움
        ReArrange(index);
    }

    void ReArrange(int index)
    {
        if (_arr[2 * index - 1] < 0)
        {
            _arr[index - 1] = _arr[2 * index];
            ReArrange(2 * index);
        }
    }

    public int FindIndex(int data)
    {
        int compare = 1;

        while (true)
        {
            if (_arr[compare - 1] > data)
            {
                compare = 2 * compare;
                if (_arr[compare - 1] == data)
                    return compare;
            }
            else if (_arr[compare - 1] < data)
            {
                compare = 2 * compare + 1;
                if (_arr[compare - 1] == data)
                    return compare;
            }
        }
    }
}
