using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FixedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            FixedStack<int> f = new FixedStack<int>(4);
            f.Push(1);
            f.Push(2);
            f.Push(3);
            //
            Console.WriteLine("Count is..." + f.Count);
            while (f.Count != 0)
            {
                Console.WriteLine(f.Pop().ToString());
            }
            Console.WriteLine("Count is..." + f.Count);
            Console.ReadLine();
        }
    }
}
