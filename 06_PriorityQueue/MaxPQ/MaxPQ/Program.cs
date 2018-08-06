using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Comparer<string> comparer = Comparer<string>.Default;
            MaxPQ<string> maxPQ = new MaxPQ<string>(11, comparer);
            maxPQ.Insert("S");
            maxPQ.Insert("T");
            maxPQ.Insert("R");                        
            maxPQ.Insert("P");
            maxPQ.Insert("N");
            maxPQ.Insert("O");
            maxPQ.Insert("A");
            maxPQ.Insert("E");
            maxPQ.Insert("I");
            maxPQ.Insert("H");
            maxPQ.Insert("G");
            Console.WriteLine("Order in binary pyramid: ");           
            foreach (string s in maxPQ)
            {
                if(s!=null) Console.Write(s + " ");
            }
                

            Console.WriteLine(" ");
            Console.WriteLine("Order, after getting max each time (like sorted desc...): ");            
            while (!maxPQ.IsEmpty())
            {
                Console.Write(maxPQ.DelMax()+" ");
            }
            

            Console.ReadLine();
        }
    }
}
