using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Test
{
    class TreeNode<T>
    {
        public TreeNode<T> _parent;
        public TreeNode<T> _left;
        public TreeNode<T> _right;
        public T _element;

        public TreeNode(T element)
        {
            _parent = null;
            _left = null;
            _right = null;

            _element = element;
        }
    }
}
