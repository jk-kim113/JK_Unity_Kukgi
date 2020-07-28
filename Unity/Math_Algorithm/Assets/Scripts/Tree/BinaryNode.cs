using System.Collections;
using System.Collections.Generic;

public class BinaryNode<T>
{
    public BinaryNode<T> _parent;
    public T _data;
    public BinaryNode<T> _left;
    public BinaryNode<T> _right;

    public BinaryNode()
    {
        _parent = null;
        _left = null;
        _right = null;
    }
}
