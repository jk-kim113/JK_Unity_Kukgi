using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Test
{
    class HeapTree<T>
    {
        TreeNode<T> _root;

        public int Count { get; set; }

        public HeapTree()
        {
            _root = null;
            Count = 0;
        }

        public void Add(T element)
        {
            TreeNode<T> toAdd = new TreeNode<T>(element);

            Add(toAdd);
        }

        public void Add(TreeNode<T> toAdd)
        {
            if (_root == null)
            {
                _root = toAdd;
            }
            else
            {
                TreeNode<T> current = _root;
                string bitcount = Convert.ToString(Count + 1, 2);
                for (int n = 1; n < bitcount.Length; n++)
                {
                    if (bitcount[n] == '0')
                    {
                        if (current._left == null)
                        {
                            current._left = toAdd;
                            current._left._parent = current;
                        }

                        current = current._left;
                    }
                    else
                    {
                        if (current._right == null)
                        {
                            current._right = toAdd;
                            current._right._parent = current;
                        }

                        current = current._right;
                    }
                }

                Comparer<T> comparer = Comparer<T>.Default;
                while (true)
                {
                    if (current == _root)
                        break;
                    if (comparer.Compare(current._element, current._parent._element) == -1)
                    {
                        T temp = current._element;
                        current._element = current._parent._element;
                        current._parent._element = temp;
                        current = current._parent;
                    }
                    else
                        break;
                }
            }

            Count++;
        }

        public void Remove()
        {
            TreeNode<T> current = _root;
            string bitcount = Convert.ToString(Count, 2);
            for (int n = 1; n < bitcount.Length; n++)
            {
                if (bitcount[n] == '0')
                    current = current._left;
                else
                    current = current._right;
            }
            _root._element = current._element;

            try
            {
                if (current._parent._left == current)
                    current._parent._left = null;
                else
                    current._parent._right = null;
                Count--;
                Heapify();
            }
            catch
            {
                _root = null;
            }
        }

        void Heapify()
        {
            TreeNode<T> current = _root;
            TreeNode<T> compare;

            Comparer<T> comparer = Comparer<T>.Default;
            while (true)
            {
                if (current._left == null)
                    break;
                if (current._right == null)
                    compare = current._left;
                else
                {
                    if (comparer.Compare(current._left._element, current._right._element) == -1
                        || comparer.Compare(current._left._element, current._right._element) == 0)
                        compare = current._left;
                    else
                        compare = current._right;
                }
                if (comparer.Compare(current._element, compare._element) == 1)
                {
                    T temp = current._element;
                    current._element = compare._element;
                    compare._element = temp;
                    current = compare;
                }
                else
                    break;
            }
        }
    }
}
