using System;
using System.Collections;
using System.Collections.Generic;

public class AVLNode<T> : BTNode<T> where T : IComparable
{
    public AVLNode(T value)
        : base(value)
    {
    }

    public new AVLNode<T> LeftChild
    {
        get
        {
            return (AVLNode<T>)base.LeftChild;
        }
        set
        {
            base.LeftChild = value;
        }
    }

    public new AVLNode<T> RightChild
    {
        get
        {
            return (AVLNode<T>)base.RightChild;
        }
        set
        {
            base.RightChild = value;
        }
    }

    public new AVLNode<T> Parent
    {
        get
        {
            return (AVLNode<T>)base.Parent;
        }
        set
        {
            base.Parent = value;
        }
    }
}
