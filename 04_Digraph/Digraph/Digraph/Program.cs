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
            Digraph G = new Digraph("tinyDG.txt");            
            //how much vertices?
            Console.WriteLine("Vertices: " + G.GetVertices());
            //how much edges?
            Console.WriteLine("Edges: " + G.GetEdges());
            // indegree
            Console.WriteLine("InDegree for 0 vertice: " + G.GetInDegree(0));
            // outdegree
            Console.WriteLine("OutDegree for 0 vertice: " + G.GetOutDegree(0));
            // is v reachible from s ?
            Console.WriteLine("is 1 reachible from 0: " + G.IsReachible(G, 0, 1)); // True
            Console.WriteLine("is 6 reachible from 0: " + G.IsReachible(G, 0, 1)); // False
            // isEdge
            Console.WriteLine("Is Edge ? 0,12 "+G.IsEdge(0,12));
            Console.WriteLine("Is Edge ? 0,5 " + G.IsEdge(0, 5));

            int s = 0; // start vertex
            int f = 2; // finish vertex

            // --- DFS
            Console.WriteLine(" --- DFS...");
            // Has path to ?            
            Console.WriteLine("Has path from " + s + " to " + f + " vertex, DFS? " + G.IsPathToDFS(G, s, f));
            // This is the path, DFS            
            Console.WriteLine("This is the path, DFS?");
            Stack<int> path = new Stack<int>();
            G.GetPathToDFS(G, s, f, ref path);
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
            Console.WriteLine("Has path from " + s + " to " + f + " vertex, DFS? " + G.IsPathToBFS(G, s, f));
            // --- shortest distance
            path.Clear();
            if (G.IsPathToBFS(G, s, f)) Console.WriteLine("Shortest distance: " + G.GetShortestDistToBFS(G, s, f));
            // ---
            Console.WriteLine("This is the path, DFS?");
            G.GetShortestPathToBFS(G, s, f, ref path);
            if (path.Count != 0)
            {
                foreach (int i in path)
                    Console.WriteLine(" > " + i.ToString());
            }
            else
                Console.WriteLine("no path...");
            //--- Cycles
            bool isCycle = G.IsCycle(G);
            Console.WriteLine("Is Cycle? " + isCycle);
            if (isCycle)
            {
                Stack<int> cycle = new Stack<int>();
                cycle = G.GetCycle(G);
                foreach (int v in cycle)
                    Console.WriteLine(v);
            }
            // -- Kosaraju, strongly connected components
            Console.WriteLine("Kosaraju IsStronglyConnected 0 and 2 ");
            Console.WriteLine(G.IsStronglyConnected(G,0,2));
            Console.WriteLine("Kosaraju IsStronglyConnected 0 and 1 ");
            Console.WriteLine(G.IsStronglyConnected(G, 0, 1));

            // --- Topological            
            Queue<int> preOrder = G.GetPreOrder(G);
            Console.WriteLine(" ---preOrder");            
            while (preOrder.Count != 0)
            {
                Console.WriteLine(preOrder.Dequeue());
            }
            Queue<int> postOrder = G.GetPostOrder(G);
            Console.WriteLine(" ---postOrder");
            while (postOrder.Count != 0)
            {
                Console.WriteLine(postOrder.Dequeue());
            }
            /*
            Stack<int> reversePost = G.GetReversePostOrder(G);
            Console.WriteLine(" ---reversePostOrder");
            while (reversePost.Count != 0)
            {
                Console.WriteLine(reversePost.Pop());
            }
            */
            //
            G = new Digraph("tinyDG_noCycles.txt"); // <<< setting new Graph
            Console.WriteLine(" --- Topological reversePostOrder, changing to tinyDG_noCycles.txt");            
            isCycle = G.IsCycle(G);
            if (!isCycle)
            {
                Stack<int> TopologicalOrder = G.GetTopologicalOrder(G);
                while (TopologicalOrder.Count != 0)
                {
                    Console.WriteLine(TopologicalOrder.Pop());
                }
            }
            else
            {                 
                if (isCycle) Console.WriteLine("Topological order impossible, there is cycle there... ");
            }


            // --- just describing the orGraph... toString()
            Console.WriteLine();
            Console.WriteLine("---ToString(): " + G.ToString());

            Console.ReadLine();
        }
    }
}
