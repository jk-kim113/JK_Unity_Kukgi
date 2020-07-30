using System.Collections;
using System.Collections.Generic;
using System;

public class AVLTree<T>
{
    BinaryNode<T> _root;

    public void Add(T data)
    {
        BinaryNode<T> newNode = new BinaryNode<T>();
        newNode._data = data;

        if (_root == null)
            _root = newNode;
        else
            _root = RecursiveInsert(_root, newNode);
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

    public void Delete(T data)
    {
        _root = Delete(_root, data);
    }

    private BinaryNode<T> Delete(BinaryNode<T> current, T data)
    {
        Comparer<T> comparer = Comparer<T>.Default;
        BinaryNode<T> parent;

        if (current == null)
            return null;
        else
        {
            //left subtree
            if (comparer.Compare(data, current._data) == -1)
            {
                current._left = Delete(current._left, data);
                if (balance_factor(current) == -2)//here
                    if (balance_factor(current._right) <= 0)
                        current = RotateRR(current);
                    else
                        current = RotateRL(current);
            }
            //right subtree
            else if (comparer.Compare(data, current._data) == 1)
            {
                current._right = Delete(current._right, data);
                if (balance_factor(current) == 2)
                    if (balance_factor(current._left) >= 0)
                        current = RotateLL(current);
                    else
                        current = RotateLR(current);
            }
            //if target is found
            else
            {
                if (current._right != null)
                {
                    //delete its inorder successor
                    parent = current._right;
                    while (parent._left != null)
                        parent = parent._left;

                    current._data = parent._data;
                    current._right = Delete(current._right, parent._data);
                    if (balance_factor(current) == 2)//rebalancing
                        if (balance_factor(current._left) >= 0)
                            current = RotateLL(current);
                        else
                            current = RotateLR(current);
                }
                else
                {   //if current.left != null
                    return current._left;
                }
            }
        }
        return current;
    }

    public BinaryNode<T> Root()
    {
        return _root;
    }
}
