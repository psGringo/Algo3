using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{

    class Topological
    {
        private Boolean[] marked; // marked[v] = true if v is reachable from source(s)        
        public Queue<int> Pre { get; }
        public Queue<int> Post { get; }
        public Stack<int> ReversePost { get; }

        public Topological(EdgeWeightedDigraph G)
        {
            Pre = new Queue<int>();
            Post = new Queue<int>();
            ReversePost = new Stack<int>();
            marked = new bool[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                if (!marked[v]) Dfs(G, v);
        }
        public bool IsOrder()
        {
            return (ReversePost.Count > 0);
        }
        private void Dfs(EdgeWeightedDigraph G, int v)
        {
            marked[v] = true;
            Pre.Enqueue(v);
            foreach (DirectedEdge e in G.GetAdj(v))            
            {
                int w = e.To();
                if (!marked[w]) Dfs(G, w);
            }
            Post.Enqueue(v);
            ReversePost.Push(v);
        }

        // --- validation
        private void ValidateVertex(int v)
        {
            int V = marked.Length;
            if (v < 0 || v >= V) { throw new ArgumentException("vertex " + v + " is not between 0 and " + "(V - 1)"); }
        }

    }

}
