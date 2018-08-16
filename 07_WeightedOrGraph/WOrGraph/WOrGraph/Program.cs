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
            DijkstraSP dijkstra = new DijkstraSP(G,0);
            if (dijkstra.HasPathTo(1))
            {
                Queue<DirectedEdge> path = new Queue<DirectedEdge>(dijkstra.GetPathTo(1));
                foreach (DirectedEdge e in path) Console.WriteLine(e.From() + " -> " + e.To());
            }
            else Console.WriteLine("no path");
            // Cycles
            EdgeWeightedDigraph G1 = new EdgeWeightedDigraph("tinyEWD.txt");
            EdgeWeightedDigraph G2 = new EdgeWeightedDigraph("tinyEWDAG.txt"); // Acyclic WOrGraph
            Cycle c1 = new Cycle(G1);
            Cycle c2 = new Cycle(G2);
            if (c1.IsCycle()) Console.WriteLine("Cycle in tinyEWD yes");
            else Console.WriteLine("Cycle in tinyEWD no");
            if (c2.IsCycle()) Console.WriteLine("Cycle in tinyEWDAG yes");
            else Console.WriteLine("Cycle in tinyEWDAG no");
            // AcyclicSP
            Console.WriteLine("Acyclic ASP");
            AcyclicSP asp = new AcyclicSP(G2,5);
            int v = 2;
            if (asp.IsPathTo(v))
            {
                Console.WriteLine("path from 5 to "+v);
                Queue<DirectedEdge> path = new Queue<DirectedEdge>(asp.GetPathTo(v));
                foreach (DirectedEdge e in path) Console.WriteLine(e.From() + " -> " + e.To());
            }
            else Console.WriteLine("no path from 5 to "+v);

            //Console.WriteLine(G.ToString());
            Console.ReadLine();
        }
    }
}
