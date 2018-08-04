using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGraph
{
    class EdgeWeightedGraph
    {
        private int V;
        private int E;
        private Bag<Edge>[] adj;

        // -- init empty Graph
        public EdgeWeightedGraph(int V)
        {
            if (V < 0) throw new ArgumentException("Number of vertices must be nonnegative");
            this.V = V;
            this.E = 0;
            adj = new Bag<Edge>[V];
            for (int v = 0; v < V; v++)
            {
                adj[v] = new Bag<Edge>();
            }
        }
        // -- init Random Graph
        public EdgeWeightedGraph(int V, int E)
        {
            if ((V < 0) || (E < 0)) throw new ArgumentException("must be non-negative");
            Random r = new Random();
            for (int i = 0; i < E; i++)
            {
                int v = r.Next(0, V);
                int w = r.Next(0, V);
                double weight = r.NextDouble();
                Edge e = new Edge(v, w, weight);
                AddEdge(e);
            }
        }
        // -- init Graph from file        
        public EdgeWeightedGraph(string filepath)
        {
            try
            {
                if (!File.Exists(filepath)) { throw new Exception("no file with such filepath"); }
                using (StreamReader sr = new StreamReader(filepath))
                {
                    int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        // reading number of verticles
                        if (i == 0)
                        {
                            V = Convert.ToInt32(sr.ReadLine());
                            adj = new Bag<Edge>[V];
                            for (int v = 0; v < V; v++)
                            {
                                adj[v] = new Bag<Edge>();
                            }
                            i++;
                        }
                        // reading number of edges
                        else
                            if (i == 1)
                        {
                            E = Convert.ToInt32(sr.ReadLine());
                            if (E < 0) throw new ArgumentException("number of edges in a Graph must be nonnegative");
                            i++;
                        }
                        else
                        {
                            string s = sr.ReadLine();
                            Char delimiter = ' ';
                            String[] substrings = s.Split(delimiter);
                            int v = Convert.ToInt32(substrings[0]);
                            int w = Convert.ToInt32(substrings[1]);
                            double weight = Convert.ToDouble(substrings[2]);
                            ValidateVertex(v);
                            ValidateVertex(w);
                            Edge e = new Edge(v, w, weight);
                            AddEdge(e);
                        }

                    }
                }
            }
            catch (Exception e) { Console.WriteLine("The process failed: {0}", e.ToString()); };
        }

        // -- add edge
        public void AddEdge(Edge e)
        {
            int v = e.Either();
            int w = e.Other(v);
            ValidateVertex(v);
            ValidateVertex(w);
            adj[v].Push(e);
            adj[w].Push(e);
            E++;
        }
        //-- validate Vertex
        private void ValidateVertex(int v)
        {
            if ((v < 0) || (v >= V)) throw new ArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
        }
        //
        public int GetVertices()
        {
            return V;
        }
        public int GetEdges()
        {
            return E;
        }
        /**
           * Returns the edges incident on vertex {@code v}.
           *
           * @param  v the vertex
           * @return the edges incident on vertex {@code v} as an Iterable
           * @throws IllegalArgumentException unless {@code 0 <= v < V}
           */
        public IEnumerable<Edge> GetAdj(int v)
        {
            ValidateVertex(v);
            return adj[v];
        }
        public int GetDegree(int v)
        {
            ValidateVertex(v);
            return adj[v].Size();
        }
        /**
         * Returns all edges in this edge-weighted graph.
         * To iterate over the edges in this edge-weighted graph, use foreach notation:
         * {@code for (Edge e : G.edges())}.
         *
         * @return all edges in this edge-weighted graph, as an iterable
         */
        public IEnumerable<Edge> GetAllEdges()
        {
            Bag<Edge> list = new Bag<Edge>();
            for (int v = 0; v < V; v++)
            {
                int selfLoops = 0;
                foreach (Edge e in GetAdj(v))
                {
                    if (e.Other(v) > v)
                    {
                        list.Push(e);
                    }
                    // add only one copy of each self loop (self loops will be consecutive)
                    else if (e.Other(v) == v)
                    {
                        if (selfLoops % 2 == 0) list.Push(e);
                        selfLoops++;
                    }
                }
            }
            return list;
        }
        /**
           * Returns a string representation of the edge-weighted graph.
           * This method takes time proportional to <em>E</em> + <em>V</em>.
           *
           * @return the number of vertices <em>V</em>, followed by the number of edges <em>E</em>,
           *         followed by the <em>V</em> adjacency lists of edges
           */
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Verticles: "+ V + " " + "Edges: "+ E/2 + "\n"); // E%2 because each edge twice in verticle
            for (int v = 0; v < V; v++)
            {
                s.Append(v + ": ");
                foreach (Edge e in adj[v])
                {
                    s.Append(e + "  ");
                }
                s.Append("\n");
            }
            return s.ToString();
        }
    }
}
