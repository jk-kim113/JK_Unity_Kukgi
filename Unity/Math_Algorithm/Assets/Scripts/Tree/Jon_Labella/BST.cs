using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class BST<T> : ICollection<T> where T : IComparable
{ // JON LABELLA
    /// <summary>
    /// Specifies the mode of scanning through the tree
    /// </summary>
    public enum TraversalMode
    {
        InOrder = 0,
        PostOrder,
        PreOrder
    }

    private BTNode<T> head;
    private Comparison<IComparable> comparer = CompareElements;
    private int size;
    private TraversalMode traversalMode = TraversalMode.InOrder;

    /// <summary>
    /// Gets or sets the root of the tree (the top-most node)
    /// </summary>
    public virtual BTNode<T> Root
    {
        get { return head; }
        set { head = value; }
    }

    /// <summary>
    /// Gets the number of elements stored in the tree
    /// </summary>
    public virtual int Count
    {
        get { return size; }
    }

    /// <summary>
    /// Gets whether the tree is read-only
    /// </summary>
    public virtual bool IsReadOnly
    {
        get { return false; }
    }

    /// <summary>
    /// Gets or sets the traversal mode of the tree
    /// </summary>
    public virtual TraversalMode TraversalOrder
    {
        get { return traversalMode; }
        set { traversalMode = value; }
    }

    /// <summary>
    /// Creates a new instance of a Binary Tree
    /// </summary>
    public BST()
    {
        head = null;
        size = 0;
    }

    /// <summary>
    /// Adds a new element to the tree
    /// </summary>
    public virtual void Add(T value)
    {
        BTNode<T> node = new BTNode<T>(value);
        this.Add(node);
    }

    /// <summary>
    /// Adds a node to the tree
    /// </summary>
    public virtual void Add(BTNode<T> node)
    {
        if (this.head == null) //first element being added
        {
            this.head = node; //set node as root of the tree
            node.Tree = this;
            node.Index = 1; // TODO Delete Later
            size++;
        }
        else
        {
            if (node.Parent == null)
                node.Parent = head; //start at head

            //Node is inserted on the left side if it is smaller or equal to the parent
            bool insertLeftSide = comparer((IComparable)node.Value, (IComparable)node.Parent.Value) <= 0;

            if (insertLeftSide) //insert on the left
            {
                if (node.Parent.LeftChild == null)
                {
                    node.Parent.LeftChild = node; //insert in left
                    node.Index = node.Parent.Index * 2; // TODO Delete Later
                    //UnityEngine.Debug.Log(node.Index);
                    size++;
                    node.Tree = this; //assign node to this binary tree
                }
                else
                {
                    node.Parent = node.Parent.LeftChild; //scan down to left child
                    this.Add(node); //recursive call
                }
            }
            else //insert on the right
            {
                if (node.Parent.RightChild == null)
                {
                    node.Parent.RightChild = node; //insert in right
                    node.Index = node.Parent.Index * 2 + 1; // TODO Delete Later
                    //UnityEngine.Debug.Log(node.Index);
                    size++;
                    node.Tree = this; //assign node to this binary tree
                }
                else
                {
                    node.Parent = node.Parent.RightChild;
                    this.Add(node);
                }
            }
        }
    }

    /// <summary>
    /// Returns the first node in the tree with the parameter value.
    /// </summary>
    public virtual BTNode<T> Find(T value)
    {
        BTNode<T> node = this.head; //start at head
        while (node != null)
        {
            if (node.Value.Equals(value)) //parameter value found
                return node;
            else
            {
                //Search left if the value is smaller than the current node
                bool searchLeft = comparer((IComparable)value, (IComparable)node.Value) < 0;

                if (searchLeft)
                    node = node.LeftChild; //search left
                else
                    node = node.RightChild; //search right
            }
        }

        return null; //not found
    }

    /// <summary>
    /// Removes all the elements stored in the tree
    /// </summary>
    public virtual void Clear()
    {
        //Remove children first, then parent
        //(Post-order traversal)
        IEnumerator<T> enumerator = GetPostOrderEnumerator();
        while (enumerator.MoveNext())
        {
            this.Remove(enumerator.Current);
        }
        enumerator.Dispose();
    }

    /// <summary>
    /// Returns whether a value is stored in the tree
    /// </summary>
    public virtual bool Contains(T value)
    {
        return (this.Find(value) != null);
    }

    /// <summary>
    /// Copies the elements in the tree to an array using the traversal mode specified.
    /// </summary>
    public virtual void CopyTo(T[] array)
    {
        this.CopyTo(array, 0);
    }

    /// <summary>
    /// Copies the elements in the tree to an array using the traversal mode specified.
    /// </summary>
    public virtual void CopyTo(T[] array, int startIndex)
    {
        IEnumerator<T> enumerator = this.GetEnumerator();
        
        for (int i = startIndex; i < array.Length; i++)
        {
            if (enumerator.MoveNext())
                array[i] = enumerator.Current;
            else
                break;
        }
    }

    /// <summary>
    /// Returns an enumerator to scan through the elements stored in tree.
    /// The enumerator uses the traversal set in the TraversalMode property.
    /// </summary>
    public virtual IEnumerator<T> GetEnumerator()
    {
        switch (this.TraversalOrder)
        {
            case TraversalMode.InOrder:
                return GetInOrderEnumerator();
            case TraversalMode.PostOrder:
                return GetPostOrderEnumerator();
            case TraversalMode.PreOrder:
                return GetPreOrderEnumerator();
            default:
                return GetInOrderEnumerator();
        }
    }

    /// <summary>
    /// Removes a value from the tree and returns whether the removal was successful.
    /// </summary>
    public virtual bool Remove(T value)
    {
        BTNode<T> removeNode = Find(value);

        return this.Remove(removeNode);
    }

    /// <summary>
    /// Removes a node from the tree and returns whether the removal was successful.
    /// </summary>>
    public virtual bool Remove(BTNode<T> removeNode)
    {
        if (removeNode == null || removeNode.Tree != this)
            return false; //value doesn't exist or not of this tree

        //Note whether the node to be removed is the root of the tree
        bool wasHead = (removeNode == head);

        if (this.Count == 1)
        {
            this.head = null; //only element was the root
            removeNode.Tree = null;

            size--; //decrease total element count
        }
        else if (removeNode.IsLeaf) //Case 1: No Children
        {
            //Remove node from its parent
            if (removeNode.IsLeftChild)
                removeNode.Parent.LeftChild = null;
            else
                removeNode.Parent.RightChild = null;

            removeNode.Tree = null;
            removeNode.Parent = null;

            size--; //decrease total element count
        }
        else if (removeNode.ChildCount == 1) //Case 2: One Child
        {
            if (removeNode.HasLeftChild)
            {
                //Put left child node in place of the node to be removed
                removeNode.LeftChild.Parent = removeNode.Parent; //update parent

                if (wasHead)
                    this.Root = removeNode.LeftChild; //update root reference if needed

                if (removeNode.IsLeftChild) //update the parent's child reference
                    removeNode.Parent.LeftChild = removeNode.LeftChild;
                else
                    removeNode.Parent.RightChild = removeNode.LeftChild;
            }
            else //Has right child
            {
                //Put left node in place of the node to be removed
                removeNode.RightChild.Parent = removeNode.Parent; //update parent

                if (wasHead)
                    this.Root = removeNode.RightChild; //update root reference if needed

                if (removeNode.IsLeftChild) //update the parent's child reference
                    removeNode.Parent.LeftChild = removeNode.RightChild;
                else
                    removeNode.Parent.RightChild = removeNode.RightChild;
            }

            removeNode.Tree = null;
            removeNode.Parent = null;
            removeNode.LeftChild = null;
            removeNode.RightChild = null;

            size--; //decrease total element count
        }
        else //Case 3: Two Children
        {
            //Find inorder predecessor (right-most node in left subtree)
            BTNode<T> successorNode = removeNode.LeftChild;
            while (successorNode.RightChild != null)
            {
                successorNode = successorNode.RightChild;
            }

            removeNode.Value = successorNode.Value; //replace value

            this.Remove(successorNode); //recursively remove the inorder predecessor
        }

        return true;
    }

    /// <summary>
    /// Returns the height of the entire tree
    /// </summary>
    public virtual int GetHeight()
    {
        return this.GetHeight(this.Root);
    }

    /// <summary>
    /// Returns the height of the subtree rooted at the parameter value
    /// </summary>
    public virtual int GetHeight(T value)
    {
        //Find the value's node in tree
        BTNode<T> valueNode = this.Find(value);
        if (value != null)
            return this.GetHeight(valueNode);
        else
            return 0;
    }

    /// <summary>
    /// Returns the height of the subtree rooted at the parameter node
    /// </summary>
    public virtual int GetHeight(BTNode<T> startNode)
    {
        if (startNode == null)
            return 0;
        else
            return 1 + Math.Max(GetHeight(startNode.LeftChild), GetHeight(startNode.RightChild));
    }

    /// <summary>
    /// Returns the depth of a subtree rooted at the parameter value
    /// </summary>
    public virtual int GetDepth(T value)
    {
        BTNode<T> node = this.Find(value);
        return this.GetDepth(node);
    }

    /// <summary>
    /// Returns the depth of a subtree rooted at the parameter node
    /// </summary>
    public virtual int GetDepth(BTNode<T> startNode)
    {
        int depth = 0;

        if (startNode == null)
            return depth;

        BTNode<T> parentNode = startNode.Parent; //start a node above
        while (parentNode != null)
        {
            depth++;
            parentNode = parentNode.Parent; //scan up towards the root
        }

        return depth;
    }

    /// <summary>
    /// Returns an enumerator to scan through the elements stored in tree.
    /// The enumerator uses the traversal set in the TraversalMode property.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that visits node in the order: left child, parent, right child
    /// </summary>
    public virtual IEnumerator<T> GetInOrderEnumerator()
    {
        return new BinaryTreeInOrderEnumerator(this);
    }

    /// <summary>
    /// Returns an enumerator that visits node in the order: left child, right child, parent
    /// </summary>
    public virtual IEnumerator<T> GetPostOrderEnumerator()
    {
        return new BinaryTreePostOrderEnumerator(this);
    }

    /// <summary>
    /// Returns an enumerator that visits node in the order: parent, left child, right child
    /// </summary>
    public virtual IEnumerator<T> GetPreOrderEnumerator()
    {
        return new BinaryTreePreOrderEnumerator(this);
    }

    /// <summary>
    /// Compares two elements to determine their positions within the tree.
    /// </summary>
    public static int CompareElements(IComparable x, IComparable y)
    {
        return x.CompareTo(y);
    }

    internal class BinaryTreeInOrderEnumerator : IEnumerator<T>
    {
        private BTNode<T> current;
        private BST<T> tree;
        internal Queue<BTNode<T>> traverseQueue;

        public BinaryTreeInOrderEnumerator(BST<T> tree)
        {
            this.tree = tree;

            //Build queue
            traverseQueue = new Queue<BTNode<T>>();
            visitNode(this.tree.Root);
        }

        private void visitNode(BTNode<T> node)
        {
            if (node == null)
                return;
            else
            {
                visitNode(node.LeftChild);
                traverseQueue.Enqueue(node);
                visitNode(node.RightChild);
            }
        }

        public T Current
        {
            get { return current.Value; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            current = null;
            tree = null;
        }

        public void Reset()
        {
            current = null;
        }

        public bool MoveNext()
        {
            if (traverseQueue.Count > 0)
                current = traverseQueue.Dequeue();
            else
                current = null;

            return (current != null);
        }
    }

    /// <summary>
    /// Returns a postorder-traversal enumerator for the tree values
    /// </summary>
    internal class BinaryTreePostOrderEnumerator : IEnumerator<T>
    {
        private BTNode<T> current;
        private BST<T> tree;
        internal Queue<BTNode<T>> traverseQueue;

        public BinaryTreePostOrderEnumerator(BST<T> tree)
        {
            this.tree = tree;

            //Build queue
            traverseQueue = new Queue<BTNode<T>>();
            visitNode(this.tree.Root);
        }

        private void visitNode(BTNode<T> node)
        {
            if (node == null)
                return;
            else
            {
                visitNode(node.LeftChild);
                visitNode(node.RightChild);
                traverseQueue.Enqueue(node);
            }
        }

        public T Current
        {
            get { return current.Value; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            current = null;
            tree = null;
        }

        public void Reset()
        {
            current = null;
        }

        public bool MoveNext()
        {
            if (traverseQueue.Count > 0)
                current = traverseQueue.Dequeue();
            else
                current = null;

            return (current != null);
        }
    }

    /// <summary>
    /// Returns an preorder-traversal enumerator for the tree values
    /// </summary>
    internal class BinaryTreePreOrderEnumerator : IEnumerator<T>
    {
        private BTNode<T> current;
        private BST<T> tree;
        internal Queue<BTNode<T>> traverseQueue;

        public BinaryTreePreOrderEnumerator(BST<T> tree)
        {
            this.tree = tree;

            //Build queue
            traverseQueue = new Queue<BTNode<T>>();
            visitNode(this.tree.Root);
            Rearrange(); // TODO Delete Later
        }

        private void visitNode(BTNode<T> node)
        {
            if (node == null)
                return;
            else
            {
                traverseQueue.Enqueue(node);
                visitNode(node.LeftChild);
                visitNode(node.RightChild);
            }
        }

        // TODO Delete Later
        void Rearrange()
        {
            BTNode<T>[] tempArr = new BTNode<T>[63];

            while(traverseQueue.Count > 0)
            {
                BTNode<T> temp = traverseQueue.Dequeue();

                if (temp.Index - 1 > 62)
                {
                    UnityEngine.Debug.Log(temp.Index);
                    continue;
                }
                tempArr[temp.Index - 1] = temp;
            }

            traverseQueue.Clear();

            for (int n = 0; n < tempArr.Length; n++)
            {
                if(tempArr[n] == null)
                {
                    BTNode<T> temp = new BTNode<T>(default(T));
                    traverseQueue.Enqueue(temp);
                }
                else
                {
                    traverseQueue.Enqueue(tempArr[n]);
                }
            }
        }

        public T Current
        {
            get { return current.Value; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            current = null;
            tree = null;
        }

        public void Reset()
        {
            current = null;
        }

        public bool MoveNext()
        {
            if (traverseQueue.Count > 0)
                current = traverseQueue.Dequeue();
            else
                current = null;

            return (current != null);
        }
    }
}
