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
            CycleFinder c1 = new CycleFinder(G1);
            CycleFinder c2 = new CycleFinder(G2);
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
            // BellmanFord
            EdgeWeightedDigraph G3 = new EdgeWeightedDigraph("tinyEWDn.txt");
            BellmanFordSP b = new BellmanFordSP(G3, 0);
            if (!b.IsNegativeCycle())
            {
                Queue<DirectedEdge> q = new Queue<DirectedEdge>(b.PathTo(7));
                foreach (DirectedEdge e in q)
                {
                    Console.WriteLine(e.From() + " - > " + e.To());
                }
            }
            else
            {                 
                Console.WriteLine("There is negative cycle");
                Queue<DirectedEdge> q = new Queue<DirectedEdge>(b.GetNegativeCycle());
                foreach (DirectedEdge e in q)
                {
                    Console.WriteLine(e.From() + " - > " + e.To());
                }
            }                            
            //Console.WriteLine(G.ToString());
            Console.ReadLine();
        }
    }
}
