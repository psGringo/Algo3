using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class DigraphTopological2
    /* topological order of graph without cycles, if cycles - no topological order */
  
    {
        public IEnumerable<int> Order { get; set; } // topological order
        private int[] rank; // rank of vertex v in order
        private int V;

        /**
  * Determines whether the digraph {@code G} has a topological order and, if so,
  * finds such a topological order.
  * @param G the digraph
  */
        public DigraphTopological2(Digraph G)
        {
            V = G.GetVertices();
            DirectedCycle finder = new DirectedCycle(G);
            if (!finder.IsCycle())
            {
                DigraphTopological d = new DigraphTopological(G);
                Order = d.ReversePost;
                rank = new int[G.GetVertices()];
                int i = 0;
                foreach (int v in Order)
                {
                    rank[v] = i++;
                }
            }
        }
        // ---
        public bool IsOrder()
        {
            return Order != null;
        }
        // ---
        public int GetRank(int v)
        {
            ValidateVertex(v);
            if (Order != null) return rank[v];
            else return -1;
        }        
        // --- Validation...
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V) { throw new ArgumentException("vertex " + v + " is not between 0 and " + "(V - 1)"); }
        }
    }
}
