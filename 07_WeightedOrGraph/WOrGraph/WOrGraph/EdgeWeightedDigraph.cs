using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class EdgeWeightedDigraph
    {
        private int V; // vertices
        private int E; // edges
        private List<DirectedEdge>[] adj;
        private int[] Indegree;
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V) throw new ArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
        }
        public void AddEdge(DirectedEdge e)
        {
            int v = e.From();
            int w = e.To();
            ValidateVertex(v);
            ValidateVertex(w);
            adj[v].Add(e);
            Indegree[w]++;
            E++;
        }
        public IEnumerable<DirectedEdge> GetAdj(int v)
        {
            ValidateVertex(v);
            return adj[v];
        }
        public int GetOutDegree(int v)
        {
            ValidateVertex(v);
            return adj[v].Count;
        }
        public int GetIndegree(int v)
        {
            ValidateVertex(v);
            return Indegree[v];
        }
        // -- init Graph from file        
        public EdgeWeightedDigraph(string filepath)
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
                            adj = new List<DirectedEdge>[V];
                            Indegree = new int[V];
                            for (int v = 0; v < V; v++)
                            {
                                adj[v] = new List<DirectedEdge>();
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
                            DirectedEdge e = new DirectedEdge(v, w, weight);
                            AddEdge(e);
                        }

                    }
                }
            }
            catch (Exception e) { Console.WriteLine("The process failed: {0}", e.ToString()); };
        }
        public IEnumerable<DirectedEdge> GetEdges()
        {
            List<DirectedEdge> list = new List<DirectedEdge>();
            for (int v = 0; v < V; v++)
            {
                foreach(DirectedEdge e in adj[v])
                {
                    list.Add(e);
                }
            }
            return list;
        }
        public int GetVertices()
        {
            return V;
        }
        public override string ToString()
        {            
            StringBuilder s = new StringBuilder();
            s.Append("Verticles: " + V + " " + "Edges: " + E / 2 + "\n"); // E%2 because each edge twice in verticle
            for (int v = 0; v < V; v++)
            {
                //s.Append(v + ": ");
                foreach (DirectedEdge e in adj[v])
                {
                    s.Append(e.From() +" -> "+e.To()+"  ");
                }
                s.Append("\n");
            }
            return s.ToString();
        }

    }

}
