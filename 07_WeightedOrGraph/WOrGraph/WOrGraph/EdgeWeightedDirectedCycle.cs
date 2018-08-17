using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class EdgeWeightedDirectedCycle
    {
        private bool[] marked;             // marked[v] = has vertex v been marked?
        private DirectedEdge[] edgeTo;        // edgeTo[v] = previous edge on path to v
        private bool[] onStack;            // onStack[v] = is vertex on the stack?
        private Stack<DirectedEdge> cycle;    // directed cycle (or null if no such cycle)
        public EdgeWeightedDirectedCycle(EdgeWeightedDigraph G)
        {
            marked = new bool[G.GetVertices()];
            onStack = new bool[G.GetVertices()];
            edgeTo = new DirectedEdge[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                if (!marked[v]) Dfs(G, v);                       
        }

        // check that algorithm computes either the topological order or finds a directed cycle
        private void Dfs(EdgeWeightedDigraph G, int v)
        {
            onStack[v] = true;
            marked[v] = true;            
            foreach(DirectedEdge e in G.GetAdj(v))
            {
                int w = e.To();
                // short circuit if directed cycle found
                if (cycle != null) return;
                // found new vertex, so recur
                else if (!marked[w])
                {
                    edgeTo[w] = e;
                    Dfs(G, w);
                }

                // trace back directed cycle
                else if (onStack[w])
                {
                    cycle = new Stack<DirectedEdge>();

                    DirectedEdge f = e;
                    while (f.From() != w)
                    {
                        cycle.Push(f);
                        f = edgeTo[f.From()];
                    }
                    cycle.Push(f);
                    return;
                }
            }
            onStack[v] = false;
        }
        public bool HasCycle()
        {
            return cycle != null;
        }
        public IEnumerable<DirectedEdge> GetCycle()
        {
            return cycle;
        }

    }
}
