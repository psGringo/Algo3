using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class DijkstraSP
    {
        private double[] distTo;
        private DirectedEdge[] edgeTo;
        private IndexMinPQ<Double> pq;
        public DijkstraSP(EdgeWeightedDigraph G, int s)
        {
            foreach (DirectedEdge e in G.GetEdges())            
                if (e.GetWeight() < 0) throw new ArgumentException("Negative weight");
                distTo = new double[G.GetVertices()];
                edgeTo = new DirectedEdge[G.GetVertices()];
                ValidateVertex(s);
                for (int v = 0; v < G.GetVertices(); v++)
                    distTo[v] = double.PositiveInfinity;
                distTo[s] = 0.0;
                // relax vertices in order of distance from s
                Comparer<double> comparer = Comparer<double>.Default;
                pq = new IndexMinPQ<Double>(G.GetVertices(), comparer);
                pq.Insert(s, distTo[s]);
                while (!pq.IsEmpty())
                {
                    pq.DelMin(out int index, out double key);
                    int v = index;
                    foreach (DirectedEdge e in G.GetAdj(v))
                        Relax(e);
                }            
        }
        private void ValidateVertex(int v)
        {
            int V = distTo.Length;
            if (v < 0 || v >= V)
                throw new ArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
        }
        // relax edge e and update pq if changed
        private void Relax(DirectedEdge e)
        {
            int v = e.From(), w = e.To();
            if (distTo[w] > distTo[v] + e.GetWeight())
            {
                distTo[w] = distTo[v] + e.GetWeight();
                edgeTo[w] = e;
                if (pq.Contains(w)) pq.DecreaseKey(w, distTo[w]);
                else pq.Insert(w, distTo[w]);
            }
        }
        // distTo
        public double GetDistTo(int v)
        {
            ValidateVertex(v);
            return distTo[v];
        }
        public bool HasPathTo(int v)
        {
            ValidateVertex(v);
            return distTo[v] < Double.PositiveInfinity;
        }
        public IEnumerable<DirectedEdge> GetPathTo(int v)
        {
            ValidateVertex(v);
            if (!HasPathTo(v)) return null;
            Stack<DirectedEdge> path = new Stack<DirectedEdge>();
            for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.From()])
            {
                path.Push(e);
            }
            return path;
        }
    }
}
