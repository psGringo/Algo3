using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class AcyclicLP
    {
        private double[] distTo;
        private DirectedEdge[] edgeTo;
        public AcyclicLP(EdgeWeightedDigraph G, int s)
        {
            distTo = new double[G.GetVertices()];
            edgeTo = new DirectedEdge[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                distTo[v] = Double.NegativeInfinity;
            distTo[s] = 0.0;
            ValidateVertex(s);
            Topological topological = new Topological(G);
            if (!topological.IsOrder())
                throw new ArgumentException("Digraph is not acyclic.");
            foreach (int v in topological.ReversePost)
            {
                foreach (DirectedEdge e in G.GetAdj(v))
                    Relax(e);
            }
        }
        // relax edge e
        private void Relax(DirectedEdge e)
        {
            int v = e.From(), w = e.To();
            if (distTo[w] < distTo[v] + e.GetWeight())
            {
                distTo[w] = distTo[v] + e.GetWeight();
                edgeTo[w] = e;
            }
        }
        private void ValidateVertex(int v)
        {
            int V = distTo.Length;
            if (v < 0 || v >= V)
                throw new ArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
        }
        public double DistTo(int v)
        {
            ValidateVertex(v);
            return distTo[v];
        }
        public bool IsPathTo(int v)
        {
            ValidateVertex(v);
            return distTo[v] > Double.NegativeInfinity;
        }
        public IEnumerable<DirectedEdge> GetPathTo(int v)
        {
            ValidateVertex(v);
            if (!IsPathTo(v)) return null;
            Stack<DirectedEdge> path = new Stack<DirectedEdge>();
            for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.From()])
            {
                path.Push(e);
            }
            return path;
        }
    }
}
