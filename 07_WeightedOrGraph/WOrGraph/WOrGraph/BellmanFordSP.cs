using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class BellmanFordSP
    {
        private double[] distTo;               // distTo[v] = distance  of shortest s->v path
        private DirectedEdge[] edgeTo;         // edgeTo[v] = last edge on shortest s->v path
        private bool[] onQueue;             // onQueue[v] = is v currently on the queue?
        private Queue<int> queue;          // queue of vertices to relax
        private int cost;                      // number of calls to relax()
        private IEnumerable<DirectedEdge> cycle;  // negative cycle (or null if no such cycle)
                                                  // by finding a cycle in predecessor graph

        public BellmanFordSP(EdgeWeightedDigraph G, int s)
        {
            distTo = new double[G.GetVertices()];
            edgeTo = new DirectedEdge[G.GetVertices()];
            onQueue = new bool[G.GetVertices()];
            for (int v = 0; v < G.GetVertices(); v++)
                distTo[v] = Double.PositiveInfinity;
            distTo[s] = 0.0;

            // Bellman-Ford algorithm
            queue = new Queue<int>();
            queue.Enqueue(s);
            onQueue[s] = true;
            while (queue.Count!=0  && !IsNegativeCycle())
            {
                int v = queue.Dequeue();
                onQueue[v] = false;
                Relax(G, v);
            }            
        }
        // relax vertex v and put other endpoints on queue if changed
        private void Relax(EdgeWeightedDigraph G, int v)
        {
            //for (DirectedEdge e : G.adj(v))
            foreach (DirectedEdge e in G.GetAdj(v))
            {
                int w = e.To();
                if (distTo[w] > distTo[v] + e.GetWeight())
                {
                    distTo[w] = distTo[v] + e.GetWeight();
                    edgeTo[w] = e;
                    if (!onQueue[w])
                    {
                        queue.Enqueue(w);
                        onQueue[w] = true;
                    }
                }
                if (cost++ % G.GetVertices() == 0)
                {
                    FindNegativeCycle();
                    if (IsNegativeCycle()) return;  // found a negative cycle
                }
            }
        }
        public bool IsNegativeCycle()
        {
            return cycle != null;
        }
        public IEnumerable<DirectedEdge> GetNegativeCycle()
        {
            return cycle;
        }
        private void FindNegativeCycle()
        {
            int V = edgeTo.Length;
            EdgeWeightedDigraph spt = new EdgeWeightedDigraph(V);
            for (int v = 0; v < V; v++)
                if (edgeTo[v] != null)
                    spt.AddEdge(edgeTo[v]);

            EdgeWeightedDirectedCycle finder = new EdgeWeightedDirectedCycle(spt);
            cycle = finder.GetCycle();
        }
        public double DistTo(int v)
        {
            ValidateVertex(v);
            if (IsNegativeCycle())
                throw new ArgumentException("Negative cost cycle exists");
            return distTo[v];
        }
        public bool IsPathTo(int v)
        {
            ValidateVertex(v);
            return distTo[v] < Double.PositiveInfinity;
        }
        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            ValidateVertex(v);
            if (IsNegativeCycle())
                throw new ArgumentException("Negative cost cycle exists");
            if (!IsPathTo(v)) return null;
            Stack<DirectedEdge> path = new Stack<DirectedEdge>();
            for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.From()])
            {
                path.Push(e);
            }
            return path;
        }
        private void ValidateVertex(int v)
        {
            int V = distTo.Length;
            if (v < 0 || v >= V)
                throw new ArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
        }
    }
}
