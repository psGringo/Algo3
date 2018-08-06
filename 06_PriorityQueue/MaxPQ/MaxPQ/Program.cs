using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueues
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
            Console.WriteLine(" ");
            // ------- min priority queue
            MinPQ<string> minPQ = new MinPQ<string>(11, comparer);
            minPQ.Insert("S");
            minPQ.Insert("T");
            minPQ.Insert("R");
            minPQ.Insert("P");
            minPQ.Insert("N");
            minPQ.Insert("O");
            minPQ.Insert("A");
            minPQ.Insert("E");
            minPQ.Insert("I");
            minPQ.Insert("H");
            minPQ.Insert("G");
            Console.WriteLine("Order in binary pyramid: ");
            foreach (string s in minPQ)
            {
                if (s != null) Console.Write(s + " ");
            }

            Console.WriteLine(" ");
            Console.WriteLine("Order, after getting max each time (like sorted asc...): ");
            while (!minPQ.IsEmpty())
            {
                Console.Write(minPQ.DelMin() + " ");
            }
            // --- INDEXED Priority Queue Max
            IndexMaxPQ<string> iMaxPQ = new IndexMaxPQ<string>(11,comparer);
            iMaxPQ.Insert(0,"S");
            iMaxPQ.Insert(1,"T");
            iMaxPQ.Insert(2,"R");
            iMaxPQ.Insert(3,"P");
            iMaxPQ.Insert(4,"N");
            iMaxPQ.Insert(5,"O");
            iMaxPQ.Insert(6,"A");
            iMaxPQ.Insert(7,"E");
            iMaxPQ.Insert(8,"I");
            iMaxPQ.Insert(9,"H");
            iMaxPQ.Insert(10,"G");
            //
            Console.WriteLine(" ");
            Console.WriteLine("--- INDEXED Priority Queue Max");
            Console.WriteLine("maxIndex "+iMaxPQ.MaxIndex());
            Console.WriteLine("maxKey " + iMaxPQ.MaxKey());
            Console.WriteLine("keyOf(0) " + iMaxPQ.KeyOf(0));

            while (!iMaxPQ.IsEmpty())
            {
                Console.Write(iMaxPQ.DelMax() + " ");
            }
            Console.ReadLine();
        }
    }
}
