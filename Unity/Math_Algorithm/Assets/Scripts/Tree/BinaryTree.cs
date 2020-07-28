using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using System;

public class BinaryTree<T>
{
    BinaryNode<T> _root;
    public BinaryNode<T> _Root { set { _root = value; } }

    public BinaryTree()
    {
        _root = null;
    }

    public bool Add(T data)
    {
        BinaryNode<T> newNode = new BinaryNode<T>();
        newNode._data = data;

        if (_root == null)
        {
            _root = newNode;
            return true;
        }   
        else
        {
            BinaryNode<T> current = _root;
            BinaryNode<T> parent;
            while (true)
            {
                parent = current;
                Comparer<T> comparer = Comparer<T>.Default;
                if (comparer.Compare(data, current._data) == -1)
                {
                    current = current._left;
                    if (current == null)
                    {
                        parent._left = newNode;
                        return true;
                    }
                }
                else if(comparer.Compare(data, current._data) == 0)
                {
                    UnityEngine.Debug.Log("Key Already Exists.");
                    return false;
                }
                else
                {
                    current = current._right;
                    if (current == null)
                    {
                        parent._right = newNode;
                        return true;
                    }
                }
            }
        }    
    }

    public BinaryNode<T> Remove(T data, BinaryNode<T> node)
    {
        Comparer<T> comparer = Comparer<T>.Default;

        if (node == null)
            return node;
        if (comparer.Compare(data, node._data) == -1)
            node._left = Remove(data, node._left);
        else if(comparer.Compare(data, node._data) == 1)
            node._right = Remove(data, node._right);
        else
        {
            if (node._left == null)
                return node._right;
            else if (node._right == null)
                return node._left;

            return node._left;
        }

        return node;
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

    public BinaryNode<T> Search(T data, BinaryNode<T> node)
    {
        Comparer<T> comparer = Comparer<T>.Default;

        if (isExternal(node))
            return node;
        if (comparer.Compare(data, node._data) == 0)
            return node;
        if (comparer.Compare(data, node._data) == -1)
            return Search(data, node._left);
        else
            return Search(data, node._right);
    }

    public BinaryNode<T> Root()
    {
        return _root;
    }

    public BinaryNode<T> Left(BinaryNode<T> node)
    {
        return node._left;
    }

    public BinaryNode<T> Right(BinaryNode<T> node)
    {
        return node._right;
    }

    public bool hasLeft(BinaryNode<T> node)
    {
        if (node._left != null)
            return true;
        else
            return false;
    }

    public bool hasRight(BinaryNode<T> node)
    {
        if (node._right != null)
            return true;
        else
            return false;
    }

    public bool isExternal(BinaryNode<T> node)
    {
        return ((node._left == null) && (node._right == null));
    }
}
