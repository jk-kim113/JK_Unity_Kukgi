using System.Collections;
using System.Collections.Generic;

public class TreeNode<T>
{
    public TreeNode<T> _parent;
    public T _data;
    public TreeNode<T>[] _children;

    public TreeNode(T data)
    {
        _parent = null;
        _data = data;
        _children = new TreeNode<T>[2] { null, null };
    }
}
