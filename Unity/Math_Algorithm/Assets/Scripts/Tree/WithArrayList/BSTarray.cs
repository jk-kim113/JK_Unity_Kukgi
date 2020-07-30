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
                    {
                        _arr[compare - 1] = data;
                        break;
                    }   
                }
                else if (_arr[compare - 1] < data)
                {
                    compare = 2 * compare + 1;
                    if (_arr[compare - 1] < 0)
                    {
                        _arr[compare - 1] = data;
                        break;
                    }   
                }
                else
                {
                    return;
                }
            }
        }

        _count++;
    }

    public void Remove(int data)
    {
        int compare = 1;

        while (true)
        {
            if (_arr[compare - 1] > data)
            {
                compare = 2 * compare;
                if (_arr[compare - 1] == data)
                    _arr[compare - 1] = int.MinValue;
            }
            else if (_arr[compare - 1] < data)
            {
                compare = 2 * compare + 1;
                if (_arr[compare - 1] == data)
                    _arr[compare - 1] = int.MinValue;
            }
        }
    }
}
