using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class DijkstraAllPairsSP
    {
        private DijkstraSP[] all;
        public DijkstraAllPairsSP(EdgeWeightedDigraph G)
        {
            all = new DijkstraSP[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                all[v] = new DijkstraSP(G, v);
        }
        public IEnumerable<DirectedEdge> GetPath(int s, int t)
        {
            ValidateVertex(s);
            ValidateVertex(t);
            return all[s].GetPathTo(t);
        }
        private void ValidateVertex(int v)
        {
            int V = all.Length;
            if (v < 0 || v >= V)
                throw new ArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
        }
        public double Dist(int s, int t)
        {
            ValidateVertex(s);
            ValidateVertex(t);
            return all[s].GetDistTo(t);
        }
    }
}
