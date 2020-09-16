using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Test
{
    class Node<T>
    {
        public Node<T> _prev;
        public Node<T> _next;
        public T _element;

        public Node(T value)
        {
            _prev = null;
            _next = null;
            _element = value;
        }
    }
}
