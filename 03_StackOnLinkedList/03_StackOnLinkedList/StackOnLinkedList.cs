using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_StackOnLinkedList
{
    class StackOnLinkedList<T> : IEnumerable<T>
    {
        class Node<T>
        {
            public T item;
            public Node<T> next;
        }
        Node<T> first;
        public int Count { get; set; }
        public bool IsEmpty() { return Count == 0; }
        public void Push(T item)
        {
            Node<T> tempNode = first;
            first = new Node<T>();
            first.item = item;
            first.next = tempNode;
            Count++;
        }

        public T Pop()
        {
            T item = first.item;
            first = first.next;
            Count--;
            return item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = first;
            while (node != null)
            {
                yield return node.item;
                node = node.next;
            }
        }
    }
}
