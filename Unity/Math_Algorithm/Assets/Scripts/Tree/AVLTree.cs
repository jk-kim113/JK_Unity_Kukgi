using System.Collections;
using System.Collections.Generic;
using System;

public class AVLTree<T>
{
    BinaryNode<T> _root;
    int _count = 0;
    List<T> _dataGroup = new List<T>();

    public void Add(T data)
    {
        BinaryNode<T> newNode = new BinaryNode<T>();
        newNode._data = data;

        if (_root == null)
            _root = newNode;
        else
            _root = RecursiveInsert(_root, newNode);

        _count++;
        AllNumber();
    }

    private BinaryNode<T> RecursiveInsert(BinaryNode<T> current, BinaryNode<T> node)
    {
        Comparer<T> comparer = Comparer<T>.Default;
        if (current == null)
        {
            current = node;
            return current;
        }
        else if (comparer.Compare(node._data, current._data) == -1)
        {
            current._left = RecursiveInsert(current._left, node);
            current = balance_tree(current);
        }
        else if (comparer.Compare(node._data, current._data) == 1)
        {
            current._right = RecursiveInsert(current._right, node);
            current = balance_tree(current);
        }
        return current;
    }

    private BinaryNode<T> balance_tree(BinaryNode<T> current)
    {
        int b_factor = balance_factor(current);
        if (b_factor > 1)
            if (balance_factor(current._left) > 0)
                current = RotateLL(current);
            else
                current = RotateLR(current);
        else if (b_factor < -1)
            if (balance_factor(current._right) > 0)
                current = RotateRL(current);
            else
                current = RotateRR(current);

        return current;
    }

    private int getHeight(BinaryNode<T> current)
    {
        int height = 0;
        if (current != null)
        {
            int l = getHeight(current._left);
            int r = getHeight(current._right);
            int m = Math.Max(l, r);
            height = m + 1;
        }
        return height;
    }

    private int balance_factor(BinaryNode<T> current)
    {
        int l = getHeight(current._left);
        int r = getHeight(current._right);
        int b_factor = l - r;
        return b_factor;
    }

    private BinaryNode<T> RotateRR(BinaryNode<T> parent)
    {
        BinaryNode<T> pivot = parent._right;
        parent._right = pivot._left;
        pivot._left = parent;
        return pivot;
    }

    private BinaryNode<T> RotateLL(BinaryNode<T> parent)
    {
        BinaryNode<T> pivot = parent._left;
        parent._left = pivot._right;
        pivot._right = parent;
        return pivot;
    }

    private BinaryNode<T> RotateLR(BinaryNode<T> parent)
    {
        BinaryNode<T> pivot = parent._left;
        parent._left = RotateRR(pivot);
        return RotateLL(parent);
    }

    private BinaryNode<T> RotateRL(BinaryNode<T> parent)
    {
        BinaryNode<T> pivot = parent._right;
        parent._right = RotateLL(pivot);
        return RotateRR(parent);
    }

    public void IteratorDown(BinaryNode<T> parent)
    {
        if (parent != null)
        {
            UnityEngine.Debug.Log(parent._data);
            IteratorDown(parent._left);
            IteratorDown(parent._right);
        }
    }

    public BinaryNode<T> Root()
    {
        return _root;
    }

    public void AllNumber()
    {
        _dataGroup.Clear();
        
        for (int m = 1; m <= _count; m++)
        {
            BinaryNode<T> _pointer = _root;
            string bitcount = Convert.ToString(m + 1, 2);
            for (int n = 1; n < bitcount.Length; n++)
            {
                if (bitcount[n] == '0')
                    _pointer = _pointer._left;
                else
                    _pointer = _pointer._right;
            }
            _dataGroup.Add(_pointer._data);
        }

        for(int n = 0; n < _dataGroup.Count; n++)
        {
            UnityEngine.Debug.Log(_dataGroup[n]);
        }
    }
}
