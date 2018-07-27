using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class DirectedCycle
    {
        private bool[] marked;
        private int[] edgeTo;
        private bool[] onStack;
        private Stack<int> cycle;

        /**
 * Determines whether the digraph {@code G} has a directed cycle and, if so,
 * finds such a cycle.
 * @param G the digraph
 */
        public DirectedCycle(Digraph G)
        {
            marked = new bool[G.GetVertices()];
            onStack = new bool[G.GetVertices()];
            edgeTo = new int[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                if (!marked[v] && cycle == null) Dfs(G, v);
        }

        // check that algorithm computes either the topological order or finds a directed cycle
        private void Dfs(Digraph G, int v) // v is start vertex here
        {
            onStack[v] = true;
            marked[v] = true;
            foreach (int w in G.GetAdj(v))
            {
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
                    Debug.Assert(Check());
                }
            }
            onStack[v] = false;
        }

        // --- does the digraph have a directed cycle?
        public bool IsCycle()
        {
            return cycle != null;
        }
        // --getCycle
        public Stack<int> GetCycle()
        {
            return cycle;
        }
        // --- certify that digraph has a directed cycle if it reports one
        private bool Check()
        {
            if (IsCycle())
            {
                // verify cycle
                int first = -1, last = -1;
                foreach (int v in cycle)
                {
                    if (first == -1) first = v;
                    last = v;
                }
                if (first != last)
                {
                    //  System.err.printf("cycle begins with %d and ends with %d\n", first, last);
                    return false;
                }
            }
            return true;
        }
    }


}
