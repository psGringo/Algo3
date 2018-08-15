using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            EdgeWeightedDigraph G = new EdgeWeightedDigraph("tinyEWD.txt");
            Console.WriteLine(G.ToString());
            Console.ReadLine();
        }
    }
}
