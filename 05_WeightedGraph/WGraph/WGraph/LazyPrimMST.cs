using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGraph
{
    class LazyPrimMST // getting minimum spanning tree or mininmum spanning forest in edge weighted graph
    {
        private double FLOATING_POINT_EPSILON = 1E-12;
        private double weight; // total weight of MST
        private Queue<Edge> mst; // edges in mst
        private bool[] marked;
        private MinPQ<Edge> pq;
        public LazyPrimMST(EdgeWeightedGraph G)
        {
            mst = new Queue<Edge>();
            marked = new bool[G.GetVertices()];
            Comparer<Edge> comparer = Comparer<Edge>.Default;
            pq = new MinPQ<Edge>(1, comparer);

            for (int v = 0; v < G.GetVertices(); v++)
            {
                // to do
                if (!marked[v]) Prim(G, v);
            }
        }

        private void Prim(EdgeWeightedGraph G, int s)
        {
            Scan(G, s);
            while (!pq.IsEmpty())
            {
                Edge e = pq.DelMin();
                int v = e.Either();
                int w = e.Other(v);
                Debug.Assert(marked[v] || marked[w]);
                if (marked[v] && marked[w]) continue;
                mst.Enqueue(e);
                weight += e.GetWeight();
                if (!marked[v]) Scan(G, v);
                if (!marked[v]) Scan(G, v);
            }
        }

        private void Scan(EdgeWeightedGraph G, int v)
        {
            Debug.Assert(!marked[v]);
            marked[v] = true;
            foreach (Edge e in G.GetAdj(v))
            {
                if (!marked[e.Other(v)]) pq.Insert(e);
            }
        }

        public IEnumerable<Edge> GetEdges()
        {
            return mst;
        }

        public double GetWeight()
        {
            return weight;
        }
    }
}
