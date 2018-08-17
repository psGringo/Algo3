using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class CycleFinder
    {
        private bool[] marked;
        private int[] edgeTo;
        private bool[] onStack;
        private Stack<int> cycle;
        public CycleFinder(EdgeWeightedDigraph G)
        {
            marked = new bool[G.GetVertices()];
            onStack = new bool[G.GetVertices()];
            edgeTo = new int[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                if (!marked[v] && cycle == null) Dfs(G, v);
        }
        private void Dfs(EdgeWeightedDigraph G, int v) // v is start vertex here
        {
            onStack[v] = true;
            marked[v] = true;
            foreach (DirectedEdge e in G.GetAdj(v))         
            {
                int w = e.To();
                // short circuit if directed cycle found
                if (cycle != null) return;
                // found new vertex, so recur
                else if (!marked[w])
                {
                    edgeTo[w] = v;
                    Dfs(G, w);
                }
                // trace back directed cycle
                else if (onStack[w])
                {
                    cycle = new Stack<int>();
                    for (int x = v; x != w; x = edgeTo[x])
                    {
                        cycle.Push(x);
                    }
                    cycle.Push(w);
                    cycle.Push(v);
                   // Debug.Assert(Check());
                }
            }
            onStack[v] = false;
        }
        public bool IsCycle()
        {
            return cycle != null;
        }
        // --getCycle
        public IEnumerable<int> GetCycle()
        {
            return cycle;
        }
    }
}
