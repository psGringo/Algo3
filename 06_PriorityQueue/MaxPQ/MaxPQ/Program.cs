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
            Console.WriteLine(" ");
            Console.WriteLine("--- INDEXED Priority Queue Max");
            Console.WriteLine("maxIndex "+iMaxPQ.MaxIndex());
            Console.WriteLine("maxKey " + iMaxPQ.MaxKey());
            Console.WriteLine("keyOf(0) " + iMaxPQ.KeyOf(0));
                                                
            while (!iMaxPQ.IsEmpty())
            {            
                iMaxPQ.DelMax(out int i,out string s);
                Console.Write(i + ":" + s + " ");
            }
            //--- INDEXED Priority Queue Min            
            IndexMinPQ<string> iMinPQ = new IndexMinPQ<string>(11, comparer);
            iMinPQ.Insert(0, "S");
            iMinPQ.Insert(1, "T");
            iMinPQ.Insert(2, "R");
            iMinPQ.Insert(3, "P");
            iMinPQ.Insert(4, "N");
            iMinPQ.Insert(5, "O");
            iMinPQ.Insert(6, "A");
            iMinPQ.Insert(7, "E");
            iMinPQ.Insert(8, "I");
            iMinPQ.Insert(9, "H");
            iMinPQ.Insert(10, "G");
            Console.WriteLine(" ");
            Console.WriteLine("--- INDEXED Priority Queue Min");
            Console.WriteLine("minIndex " + iMinPQ.MinIndex());
            Console.WriteLine("minKey " + iMinPQ.MinKey());
            Console.WriteLine("keyOf(0) " + iMinPQ.KeyOf(0));

            while (!iMinPQ.IsEmpty())
            {
                iMinPQ.DelMin(out int i, out string s);
                Console.Write(i+":"+ s + " ");
            }

            Console.ReadLine();
        }
    }
}
