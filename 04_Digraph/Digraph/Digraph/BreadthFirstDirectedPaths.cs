using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class BreadthFirstDirectedPaths
    {
        private bool[] marked;
        private int[] edgeTo;
        private int[] distTo;
        private static int INFINITY = int.MaxValue;

        // BFS from single source
        private void Bfs(Digraph G, int s)
        {
            Queue<int> q = new Queue<int>();
            marked[s] = true;
            distTo[s] = 0;
            q.Enqueue(s);
            while (q.Count != 0)
            {
                int v = q.Dequeue();
                foreach (int w in G.GetAdj(v))
                {
                    if (!marked[w])
                    {
                        edgeTo[w] = v;
                        distTo[w] = distTo[v] + 1;
                        marked[w] = true;
                        q.Enqueue(w);
                    }
                }
            }
        }

        // BFS from multiple sources
        private void Bfs(Digraph G, IEnumerable<int> sources)
        {
            Queue<int> q = new Queue<int>();
            foreach (int s in sources)
            {
                marked[s] = true;
                distTo[s] = 0;
                q.Enqueue(s);
            }
            while (q.Count != 0)
            {
                int v = q.Dequeue();
                foreach (int w in G.GetAdj(v))
                {
                    if (!marked[w])
                    {
                        edgeTo[w] = v;
                        distTo[w] = distTo[v] + 1;
                        marked[w] = true;
                        q.Enqueue(w);
                    }
                }
            }
        }
        // Is there a directed path from the source, IsPathTo
        public bool IsPathTo(int v)
        {
            ValidateVertex(v);
            return marked[v];
        }
        // Returns the number of edges in a shortest path from the source
        public int GetShortestDistTo(int v)
        {
            ValidateVertex(v);
            return distTo[v];
        }
        // Returns the shortest path
        public void GetShortestPathTo(int v, ref Stack<int> path)
        {
            if (!IsPathTo(v)) return;
            int x;
            for (x = v; distTo[x] != 0; x = edgeTo[x])
                path.Push(x);
            path.Push(x);
        }
        // --- Computes the shortest path from {s}
        public BreadthFirstDirectedPaths(Digraph G, int s)
        {
            marked = new bool[G.GetVertices()];
            distTo = new int[G.GetVertices()];
            edgeTo = new int[G.GetVertices()];
            ValidateVertex(s);
            Bfs(G, s);
        }
        // --- Computes the shortest path from any one of the source vertices in {@code sources}
        public BreadthFirstDirectedPaths(Digraph G, IEnumerable<int> sources)
        {
            marked = new bool[G.GetVertices()];
            distTo = new int[G.GetVertices()];
            edgeTo = new int[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                distTo[v] = INFINITY;
            ValidateVertices(sources);
            Bfs(G, sources);
        }
        // --- validation
        private void ValidateVertex(int v)
        {
            int V = marked.Length;
            if (v < 0 || v >= V) { throw new ArgumentException("vertex " + v + " is not between 0 and " + "(V - 1)"); }
        }
        // --- validation, many vertices
        private void ValidateVertices(IEnumerable<int> vertices)
        {
            if (vertices == null) { throw new ArgumentException("argument is null"); }
            int V = marked.Length;
            foreach (int v in vertices)
            {
                if (v < 0 || v >= V) throw new ArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
            }
        }

    }
}
