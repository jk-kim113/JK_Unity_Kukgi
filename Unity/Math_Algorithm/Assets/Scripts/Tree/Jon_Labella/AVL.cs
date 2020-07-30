using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AVL Tree data structure
/// </summary>
public class AVL<T> : BST<T> where T : IComparable
{
    /// <summary>
    /// Returns the AVL Node of the tree
    /// </summary>
    public new AVLNode<T> Root
    {
        get
        {
            return (AVLNode<T>)base.Root;
        }
        set
        {
            base.Root = value;
        }
    }

    /// <summary>
    /// Returns the AVL Node corresponding to the given value
    /// </summary>
    public new AVLNode<T> Find(T value)
    {
        return (AVLNode<T>)base.Find(value);
    }

    /// <summary>
    /// Insert a value in the tree and rebalance the tree if necessary.
    /// </summary>
    public override void Add(T value)
    {
        AVLNode<T> node = new AVLNode<T>(value);

        base.Add(node); //add normally

        //Balance every node going up, starting with the parent
        AVLNode<T> parentNode = node.Parent;

        while (parentNode != null)
        {
            int balance = this.getBalance(parentNode);
            if (Math.Abs(balance) == 2) //-2 or 2 is unbalanced
            {
                //Rebalance tree
                this.balanceAt(parentNode, balance);
            }

            parentNode = parentNode.Parent; //keep going up
        }
    }

    /// <summary>
    /// Removes a given value from the tree and rebalances the tree if necessary.
    /// </summary>
    public override bool Remove(T value)
    {
        AVLNode<T> valueNode = this.Find(value);
        return this.Remove(valueNode);
    }

    /// <summary>
    /// Wrapper method for removing a node within the tree
    /// </summary>
    protected new bool Remove(BTNode<T> removeNode)
    {
        return this.Remove((AVLNode<T>)removeNode);
    }

    /// <summary>
    /// Removes a given node from the tree and rebalances the tree if necessary.
    /// </summary>
    public bool Remove(AVLNode<T> valueNode)
    {
        //Save reference to the parent node to be removed
        AVLNode<T> parentNode = valueNode.Parent;

        //Remove the node as usual
        bool removed = base.Remove(valueNode);

        if (!removed)
        {
            return false;    //removing failed, no need to rebalance
        }
        else
        {
            //Balance going up the tree
            while (parentNode != null)
            {
                int balance = this.getBalance(parentNode);

                if (Math.Abs(balance) == 1) //1, -1
                {
                    break;    //height hasn't changed, can stop
                }
                else if (Math.Abs(balance) == 2) //2, -2
                {
                    //Rebalance tree
                    this.balanceAt(parentNode, balance);
                }

                parentNode = parentNode.Parent;
            }

            return true;
        }
    }

    /// <summary>
    /// Balances an AVL Tree node
    /// </summary>
    protected virtual void balanceAt(AVLNode<T> node, int balance)
    {
        if (balance == 2) //right outweighs
        {
            int rightBalance = getBalance(node.RightChild);

            if (rightBalance == 1 || rightBalance == 0)
            {
                //Left rotation needed
                rotateLeft(node);
            }
            else if (rightBalance == -1)
            {
                //Right rotation needed
                rotateRight(node.RightChild);

                //Left rotation needed
                rotateLeft(node);
            }
        }
        else if (balance == -2) //left outweighs
        {
            int leftBalance = getBalance(node.LeftChild);
            if (leftBalance == 1)
            {
                //Left rotation needed
                rotateLeft(node.LeftChild);

                //Right rotation needed
                rotateRight(node);
            }
            else if (leftBalance == -1 || leftBalance == 0)
            {
                //Right rotation needed
                rotateRight(node);
            }
        }
    }

    /// <summary>
    /// Determines the balance of a given node
    /// </summary>
    protected virtual int getBalance(AVLNode<T> root)
    {
        //Balance = right child's height - left child's height
        return this.GetHeight(root.RightChild) - this.GetHeight(root.LeftChild);
    }

    /// <summary>
    /// Rotates a node to the left within an AVL Tree
    /// </summary>
    protected virtual void rotateLeft(AVLNode<T> root)
    {
        if (root == null)
        {
            return;
        }

        AVLNode<T> pivot = root.RightChild;

        if (pivot == null)
        {
            return;
        }
        else
        {
            AVLNode<T> rootParent = root.Parent; //original parent of root node
            bool isLeftChild = (rootParent != null) && rootParent.LeftChild == root; //whether the root was the parent's left node
            bool makeTreeRoot = root.Tree.Root == root; //whether the root was the root of the entire tree

            //Rotate
            root.RightChild = pivot.LeftChild;
            pivot.LeftChild = root;

            //Update parents
            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.RightChild != null)
            {
                root.RightChild.Parent = root;
            }

            //Update the entire tree's Root if necessary
            if (makeTreeRoot)
            {
                pivot.Tree.Root = pivot;
            }

            //Update the original parent's child node
            if (isLeftChild)
            {
                rootParent.LeftChild = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.RightChild = pivot;
            }
        }
    }

    /// <summary>
    /// Rotates a node to the right within an AVL Tree
    /// </summary>
    protected virtual void rotateRight(AVLNode<T> root)
    {
        if (root == null)
        {
            return;
        }

        AVLNode<T> pivot = root.LeftChild;

        if (pivot == null)
        {
            return;
        }
        else
        {
            AVLNode<T> rootParent = root.Parent; //original parent of root node
            bool isLeftChild = (rootParent != null) && rootParent.LeftChild == root; //whether the root was the parent's left node
            bool makeTreeRoot = root.Tree.Root == root; //whether the root was the root of the entire tree

            //Rotate
            root.LeftChild = pivot.RightChild;
            pivot.RightChild = root;

            //Update parents
            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.LeftChild != null)
            {
                root.LeftChild.Parent = root;
            }

            //Update the entire tree's Root if necessary
            if (makeTreeRoot)
            {
                pivot.Tree.Root = pivot;
            }

            //Update the original parent's child node
            if (isLeftChild)
            {
                rootParent.LeftChild = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.RightChild = pivot;
            }
        }
    }
}

