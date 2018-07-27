using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class Program
    {
        static void Main(string[] args)
        {
            Digraph d = new Digraph("tinyDG.txt");

            //how much vertices?
            Console.WriteLine("Vertices: " + d.GetVertices());
            //how much edges?
            Console.WriteLine("Edges: " + d.GetEdges());
            // indegree
            Console.WriteLine("InDegree for 0 vertice: " + d.GetInDegree(0));
            // outdegree
            Console.WriteLine("OutDegree for 0 vertice: " + d.GetInDegree(0));
            // is v reachible from s ?
            Console.WriteLine("is 1 reachible from 0: " + d.IsReachible(d, 0, 1)); // True
            Console.WriteLine("is 6 reachible from 0: " + d.IsReachible(d, 0, 1)); // False

            int s=0; // start vertex
            int f=2; // finish vertex

            // --- DFS
            Console.WriteLine(" --- DFS...");
            // Has path to ?            
            Console.WriteLine("Has path from "+s+" to "+f+" vertex, DFS? " + d.IsPathToDFS(d, s, f));            
            // This is the path, DFS            
            Console.WriteLine("This is the path, DFS?");
            Stack<int> path = new Stack<int>();
            d.GetPathToDFS(d, s, f, ref path);
            if (path.Count != 0)
            {
                foreach (int i in path)
                    Console.WriteLine(" > " + i.ToString());
            }
            else
                Console.WriteLine("no path...");            
            
            // --- BFS
            Console.WriteLine("--- BFS...");
            // Haspath to
            Console.WriteLine("Has path from "+s+" to "+f+" vertex, DFS? " + d.IsPathToBFS(d, s, f));
            // --- shortest distance
            path.Clear();
            if (d.IsPathToBFS(d, s, f)) Console.WriteLine("Shortest distance: "+ d.GetShortestDistToBFS(d,s,f));
            // ---
            Console.WriteLine("This is the path, DFS?");            
            d.GetShortestPathToBFS(d, s, f, ref path);
            if (path.Count != 0)
            {
                foreach (int i in path)
                    Console.WriteLine(" > " + i.ToString());
            }
            else
                Console.WriteLine("no path...");

            // --- just describing the orGraph... toString()
            Console.WriteLine("ToString(): " + d.ToString());

            Console.ReadLine();
        }
    }
}
