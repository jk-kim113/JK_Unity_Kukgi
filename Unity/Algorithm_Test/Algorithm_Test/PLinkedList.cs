using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Test
{
    class PLinkedList<T>
    {
        Node<T> _header;

        public int Count { get; set; }

        public void AddFirst(T element)
        {
            Node<T> toAdd = new Node<T>(element);

            AddFirst(toAdd);
        }

        public void AddFirst(Node<T> toAdd)
        {
            if (_header != null)
            {
                _header._prev = toAdd;
                toAdd._next = _header;
            }

            _header = toAdd;
            Count++;
        }

        public void AddLast(T element)
        {
            Node<T> toAdd = new Node<T>(element);

            AddLast(toAdd);
        }

        public void AddLast(Node<T> toAdd)
        {
            if (_header != null)
            {
                Node<T> current = _header;
                while (current._next != null)
                {
                    current = current._next;
                }

                current._next = toAdd;
                toAdd._prev = current;
                toAdd._next = null;
            }
            else
            {
                _header = toAdd;
            }

            Count++;
        }

        public void RemoveFirst()
        {
            if(_header != null)
            {
                _header = _header._next;
            }
        }

        public void RemoveLast()
        {
            if (_header != null)
            {
                Node<T> current = _header;
                while (current._next != null)
                {
                    current = current._next;
                }

                _header = current;
            }
        }
    }
}
