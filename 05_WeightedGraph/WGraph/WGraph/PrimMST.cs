using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGraph
{
    class PrimMST
    {
        private static double FLOATING_POINT_EPSILON = 1E-12;
        private Edge[] edgeTo; // edgeTo[v] = shortest edge from tree vertex to non-tree vertex
        private double[] distTo;  // distTo[v] = weight of shortest such edge
        private bool[] marked;  // marked[v] = true if v on tree, false otherwise
        private IndexMinPQ<Double> pq;

        public PrimMST(EdgeWeightedGraph G)
        {
            edgeTo = new Edge[G.GetVertices()];
            distTo = new double[G.GetVertices()];
            marked = new bool[G.GetVertices()];
            Comparer<double> comparer = Comparer<double>.Default;
            pq = new IndexMinPQ<double>(G.GetVertices(), comparer);
            for (int v = 0; v < G.GetVertices(); v++)
                distTo[v] = Double.PositiveInfinity;
        }
        // run Prim's algorithm in graph G, starting from vertex s
        private void Prim(EdgeWeightedGraph G, int s)
        {
            distTo[s] = 0.0;
            pq.Insert(s, distTo[s]);
            while (!pq.IsEmpty())
            {
                pq.DelMin(out int v, out double dist);
                Scan(G, v);
            }
        }

        private void Scan(EdgeWeightedGraph G, int v)
        {
            marked[v] = true;
            foreach (Edge e in G.GetAdj(v))
            {
                int w = e.Other(v);
                if (marked[w]) continue;
                if (e.GetWeight() < distTo[w])
                {
                    distTo[w] = e.GetWeight();
                    edgeTo[w] = e;
                    if (pq.Contains(w)) pq.DecreaseKey(w, distTo[w]);
                    else pq.Insert(w, distTo[w]);
                }
            }
        }
        /**
         * Returns the edges in a minimum spanning tree (or forest).
         * @return the edges in a minimum spanning tree (or forest) as
         *    an iterable of edges
         */
        public IEnumerable<Edge> GetEdges()
        {
            Queue<Edge> mst = new Queue<Edge>();
            for (int v = 0; v < edgeTo.Length; v++)
            {
                Edge e = edgeTo[v];
                if (e != null)
                {
                    mst.Enqueue(e);
                }
            }
            return mst;
        }
        /**
         * Returns the sum of the edge weights in a minimum spanning tree (or forest).
         * @return the sum of the edge weights in a minimum spanning tree (or forest)
         */
        public double GetWeight()
        {
            double weight = 0.0;
            foreach (Edge e in GetEdges())
            {
                weight += e.GetWeight();
            }                           
            return weight;
        }
    }
}

