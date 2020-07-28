using System;
using System.Collections;
using System.Collections.Generic;

public class HeapTree<T>
{
    BinaryNode<T> _root;
    BinaryNode<T> _pointer;
    int _count;

    public HeapTree()
    {
        _root = null;
        _pointer = null;
        _count = 0;
    }

    public void Add(T data)
    {
        BinaryNode<T> newNode = new BinaryNode<T>();
        newNode._data = data;

        if (_root == null)
        {
            _root = newNode;
            _count++;
        }
        else
        {
            _pointer = _root;
            string bitcount = Convert.ToString(_count + 1, 2);
            for(int n = 0; n < bitcount.Length; n++)
            {
                if(bitcount[n] == '0')
                {
                    if(_pointer._left == null)
                    {
                        _pointer._left = new BinaryNode<T>();
                    }
                    _pointer = _pointer._left;
                }
                else
                {
                    if(_pointer._right == null)
                    {
                        _pointer._right = new BinaryNode<T>();
                    }
                    _pointer = _pointer._right;
                }
            }
            _pointer._data = data;
            while(true)
            {
                if (_pointer == _root)
                    break;
                Comparer<T> comparer = Comparer<T>.Default;
                if (comparer.Compare(_pointer._data, _pointer._parent._data) == -1)
                {
                    T temp = _pointer._data;
                    _pointer._data = _pointer._parent._data;
                    _pointer._parent._data = temp;
                    _pointer = _pointer._parent;
                }
                else
                    break;
            }
            _count++;
        }
    }

    public void Remove()
    {
        //T output = _root._data;
        _pointer = _root;
        string bitcount = Convert.ToString(_count + 1, 2);
        for (int n = 0; n < bitcount.Length; n++)
        {
            if (bitcount[n] == '0')
                if (_pointer._left == null)
                    _pointer = _pointer._left;
            else
                if (_pointer._right == null)
                    _pointer = _pointer._right;
        }
        _root._data = _pointer._data;
        try
        {
            if (_pointer._parent._left == _pointer)
                _pointer._parent._left = null;
            else
                _pointer._parent._right = null;
            _count--;
            Heapify();
        }
        catch
        {
            _root = null;
        }
    }

    void Heapify()
    {
        BinaryNode<T> compare;
        _pointer = _root;
        Comparer<T> comparer = Comparer<T>.Default;
        while (true)
        {
            if (_pointer._left == null)
                break;
            if (_pointer._right == null)
                compare = _pointer._left;
            else
            {
                if (comparer.Compare(_pointer._left._data, _pointer._right._data) == -1
                    || comparer.Compare(_pointer._left._data, _pointer._right._data) == 0)
                    compare = _pointer._left;
                else
                    compare = _pointer._right;
            }
            if (comparer.Compare(_pointer._data, compare._data) == 1)
            {
                T temp = _pointer._data;
                _pointer._data = compare._data;
                compare._data = temp;
                _pointer = compare;
            }
            else
                break;
        }
    }
}
