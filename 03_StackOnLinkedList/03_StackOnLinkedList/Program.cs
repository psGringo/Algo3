using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_StackOnLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            StackOnLinkedList<int> s = new StackOnLinkedList<int>();
            s.Push(1);
            s.Push(2);
            s.Push(3);
            // enumeration
            //foreach (var el in s) Console.WriteLine(el);
            

            // Pop
            while (s.Count != 0)
            {
                Console.WriteLine(s.Pop().ToString());
            }
            Console.ReadLine();
        }
    }
}
