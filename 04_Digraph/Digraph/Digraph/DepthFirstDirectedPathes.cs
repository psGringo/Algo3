using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class DepthFirstDirectedPathes
    {
        private Boolean[] marked;
        private int[] edgeTo;
        private int s;
        public DepthFirstDirectedPathes(Digraph G, int s)
        {
            marked = new Boolean[G.GetVertices()];
            edgeTo = new int[G.GetVertices()];
            ValidateVertex(s);
            this.s = s;
            Dfs(G,s);
        }
        // --- dfs and pathes
        private void Dfs(Digraph G, int v)
        {
            marked[v] = true;
            foreach (int w in G.GetAdj(v))
            {
                if (!marked[w])
                {
                    edgeTo[w] = v;
                    Dfs(G, w);
                }
            }
        }
        // ---GetPathTo
        public void GetPathTo(int v, ref Stack<int> path)
        {
            ValidateVertex(v);
            if (!marked[v]) return;
          //  Stack<int> path = new Stack<int>();
            for (int x = v; x != s; x = edgeTo[x])
                path.Push(x);
            path.Push(s);
          //  return path;
        }
        
        // --- HasPathTo
        public Boolean IsPathTo(int v)
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
    }
}
