using System;
using System.Collections;
using System.Collections.Generic;

public class TreeTest<T>
{
    TreeNode<T> _rootNode;

    int _size;
    public int size
    {
        get
        {
            return _size;
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

    public void Add(TreeNode<T> data)
    {
        if(_rootNode == null)
        {
            _rootNode = data;
        }
        else
        {
            TreeNode<T> current = _rootNode;
            
            int idx = 0;
            while(current._children[idx] != null)
            {
                idx++;
                if(idx > 1)
                {
                    idx = 0;
                    current = current._children[0];
                }
            }

            current._children[idx] = data;
            data._parent = current;
        }

        _size++;
    }

    public int Depth(TreeTest<T> tree, TreeNode<T> node)
    {
        if (tree.isRoot(node))
            return 0;
        else
            return (1 + Depth(tree, node._parent));
    }

    public int Height(TreeTest<T> tree)
    {
        if(tree.isExternal(_rootNode))
            return 0;
        else
        {
            int h = 0;
            foreach (TreeNode<T> width in tree.Children(_rootNode))
                h = Math.Max(h, Depth(tree, width));
            return (h + 1);
        }
    }

    public void IteratorDown(TreeNode<T> parent)
    {
        if (parent != null)
        {
            UnityEngine.Debug.Log(parent._data);
            IteratorDown(parent._children[0]);
            IteratorDown(parent._children[1]);
        }
    }

    public void IteratorUp(TreeNode<T> parent)
    {
        if (parent != null)
        {
            IteratorUp(parent._children[0]);
            UnityEngine.Debug.Log(parent._data);
            IteratorUp(parent._children[1]);
        }
    }

    public TreeNode<T> Root()
    {
        return _rootNode;
    }

    public TreeNode<T> Parent(TreeNode<T> node)
    {
        return node._parent;
    }

    public TreeNode<T>[] Children(TreeNode<T> node)
    {
        return node._children;
    }

    public bool isInternal(TreeNode<T> node)
    {
        return (node._children[0] != null);
    }

    public bool isExternal(TreeNode<T> node)
    {
        return (node._children[0] == null);
    }

    public bool isRoot(TreeNode<T> node)
    {
        return (node._parent == null);
    }
}
