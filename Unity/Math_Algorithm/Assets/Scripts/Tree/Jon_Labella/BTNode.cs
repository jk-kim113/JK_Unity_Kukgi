using System;
using System.Collections;
using System.Collections.Generic;

public class BTNode<T> where T : IComparable
{
    private T value;
    private BTNode<T> leftChild;
    private BTNode<T> rightChild;
    private BTNode<T> parent;
    private BST<T> tree;
    private int index; // TODO Delete Later

    // TODO Delete Later
    public virtual int Index
    {
        get { return index; }
        set { this.index = value; }
    }

    /// <summary>
    /// The value stored at the node
    /// </summary>
    public virtual T Value
    {
        get { return value; }
        set { this.value = value; }
    }

    /// <summary>
    /// Gets or sets the left child node
    /// </summary>
    public virtual BTNode<T> LeftChild
    {
        get { return leftChild; }
        set { leftChild = value; }
    }

    /// <summary>
    /// Gets or sets the right child node
    /// </summary>
    public virtual BTNode<T> RightChild
    {
        get { return rightChild; }
        set { rightChild = value; }
    }

    /// <summary>
    /// Gets or sets the parent node
    /// </summary>
    public virtual BTNode<T> Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    /// <summary>
    /// Gets or sets the Binary Tree the node belongs to
    /// </summary>
    public virtual BST<T> Tree
    {
        get { return tree; }
        set { tree = value; }
    }

    /// <summary>
    /// Gets whether the node is a leaf (has no children)
    /// </summary>
    public virtual bool IsLeaf
    {
        get { return this.ChildCount == 0; }
    }

    /// <summary>
    /// Gets whether the node is internal (has children nodes)
    /// </summary>
    public virtual bool IsInternal
    {
        get { return this.ChildCount > 0; }
    }

    /// <summary>
    /// Gets whether the node is the left child of its parent
    /// </summary>
    public virtual bool IsLeftChild
    {
        get { return this.Parent != null && this.Parent.LeftChild == this; }
    }

    /// <summary>
    /// Gets whether the node is the right child of its parent
    /// </summary>
    public virtual bool IsRightChild
    {
        get { return this.Parent != null && this.Parent.RightChild == this; }
    }

    /// <summary>
    /// Gets the number of children this node has
    /// </summary>
    public virtual int ChildCount
    {
        get
        {
            int count = 0;

            if (this.LeftChild != null)
                count++;

            if (this.RightChild != null)
                count++;

            return count;
        }
    }

    /// <summary>
    /// Gets whether the node has a left child node
    /// </summary>
    public virtual bool HasLeftChild
    {
        get { return (this.LeftChild != null); }
    }

    /// <summary>
    /// Gets whether the node has a right child node
    /// </summary>
    public virtual bool HasRightChild
    {
        get { return (this.RightChild != null); }
    }

    /// <summary>
    /// Create a new instance of a Binary Tree node
    /// </summary>
    public BTNode(T value)
    {
        this.value = value;
    }
}
