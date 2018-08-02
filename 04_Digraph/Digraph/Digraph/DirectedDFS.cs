using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class DirectedDFS
    {
        private Boolean[] marked; // marked[v] = true iff v is reachable from source(s)
        private int Count; // number of vertices reachable from source(s)        
        // --- constructor, from 1 source
        public DirectedDFS(Digraph G, int s)
        {
            marked = new Boolean[G.GetVertices()];
            ValidateVertex(s);
            Dfs(G, s);
        }
        //--- constructor 2, from many sources...
        public DirectedDFS(Digraph G, IEnumerable<int> s)
        {
            marked = new Boolean[G.GetVertices()];
            ValidateVertices(s);
            foreach(int v in s)
                if(!marked[v])Dfs(G, v);
        }

        // --- Depth first search
        private void Dfs(Digraph G, int s)
        {
            Count++;
            marked[s] = true;
            foreach (int w in G.GetAdj(s))
            {
                if (!marked[w]) Dfs(G,w);
            }
        }
        // --- isMarked?
        public Boolean IsMarked(int v)
        {
            ValidateVertex(v);
            return marked[v];
        }
        // --- validation
        private void ValidateVertex(int v)
        {
            int V = marked.Length;
            if (v < 0 || v >= V) { throw new ArgumentException("vertex " + v + " is not between 0 and " + "(V - 1)"); }
        }
        // --- validation
        private void ValidateVertices(IEnumerable<int> vertices)
        {         
            int V = marked.Length;
            foreach(int v in vertices)
            if (v < 0 || v >= V) { throw new ArgumentException("vertex " + v + " is not between 0 and " + "(V - 1)"); }
        }
    }
}
