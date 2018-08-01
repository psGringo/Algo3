using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class DigraphTopological
    {
        private Boolean[] marked; // marked[v] = true if v is reachable from source(s)        
        public Queue<int> Pre { get; }
        public Queue<int> Post { get; }
        public Stack<int> ReversePost { get; }        

        public DigraphTopological(Digraph G)
        {            
            Pre = new Queue<int>();
            Post = new Queue<int>();
            ReversePost = new Stack<int>();
            marked = new bool[G.GetVertices()];           
            for (int v=0; v < G.GetVertices(); v++)            
                if (!marked[v]) Dfs(G, v);                        
        }
        private void Dfs(Digraph G, int v)
        {
            marked[v] = true;
            Pre.Enqueue(v);            
            foreach (int w in G.GetAdj(v))
            {
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
