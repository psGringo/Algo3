using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FixedStack
{
    class FixedStack<T>
    {
        private T[] Items; 
        public int Count { get;set}
        public FixedStack(int aCount)
        {
            Items = new T[aCount];            
        }
        public bool IsEmpty() { return Count == 0; }
        public void Push(T item)
        {
            Items[Count++] = item;
        }
        public T Pop()
        {
            return Items[--Count];
        }
    }
}
