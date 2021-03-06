﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class Digraph
    {

        private int V; // vertices
        private int E; // edges

        private Bag<int>[] adj; // adjency list of vertex v
        private int[] indegree; // indegree of vertex v

        // Initializes an empty digraph with V vertices.
        public Digraph(int V)
        {
            if (V < 0) throw new ArgumentException("Number of vertices in a Digraph must be nonnegative");
            this.V = V;
            this.E = 0;
            indegree = new int[V];
            adj = new Bag<int>[V];
            for (int v = 0; v < V; v++)
            {
                adj[v] = new Bag<int>();
            }
        }
        /**
 * Initializes a new digraph that is a deep copy of the specified digraph.
 *
 * @param  G the digraph to copy
 */
        public Digraph(Digraph G)
        {
            this.V= (G.GetVertices());
            this.E = G.GetEdges();
            for (int v = 0; v < V; v++)
                this.indegree[v] = G.GetInDegree(v);

            for (int v = 0; v < G.GetVertices(); v++)
            {
                // reverse so that adjacency list is in same order as original
                Stack<int> reverse = new Stack<int>();
                foreach (int w in G.adj[v])
                {
                    reverse.Push(w);
                }
                foreach (int w in reverse)
                {
                    adj[v].Push(w);
                }
            }
        }


        // --- read Graph from file
        public Digraph(string filepath)
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
                            adj = new Bag<int>[V];
                            for (int v = 0; v < V; v++)
                            {
                                adj[v] = new Bag<int>();
                            }
                            indegree = new int[V];
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
                            ValidateVertex(v);
                            ValidateVertex(w);
                            AddEdge(v, w);
                        }

                    }
                }
            }
            catch (Exception e) { Console.WriteLine("The process failed: {0}", e.ToString()); };
        }
        //
        // --- read Graph from file
        public Digraph(string filepath, bool IsAllowParallelEdgesAndLoops)
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
                            adj = new Bag<int>[V];
                            for (int v = 0; v < V; v++)
                            {
                                adj[v] = new Bag<int>();
                            }
                            indegree = new int[V];
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
                            ValidateVertex(v);
                            ValidateVertex(w);
                            if (!IsAllowParallelEdgesAndLoops)
                            {
                                if ((IsEdge(v, w)) || (v == w)) {/*do smth*/}
                                AddEdge(v, w);
                            }
                            else
                            AddEdge(v, w);
                        }

                    }
                }
            }
            catch (Exception e) { Console.WriteLine("The process failed: {0}", e.ToString()); };
        }

        // --- vertices
        public int GetVertices()
        {
            return V;
        }
        // --- edges
        public int GetEdges()
        {
            return E;
        }
        public bool IsEdge(int v,int w)
        {
            foreach (int x in adj[v])
                if (x == w) return true;
            return false;
        }
        // sources, with indegree=0
        public IEnumerable<int> Sources()
        {
            Stack<int> sources = new Stack<int>();
            for(int v=0;v<V;v++)
            {
                if (GetInDegree(v) == 0) sources.Push(v);
            }
            return sources;
        }
        // sinks, with outdegree=0
        public IEnumerable<int> Sinks()
        {
            Stack<int> sinks = new Stack<int>();
            for (int v = 0; v < V; v++)
            {
                if (GetOutDegree(v) == 0) sinks.Push(v);
            }
            return sinks;
        }
        // isMap
        public bool IsMap()
        {
            for (int v = 0; v < V; v++)
            {
                if (GetOutDegree(v) != 0)
                {
                    return false;                                       
                }
            }
            return true;
        }
        // --- Validation...
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V) { throw new ArgumentException("vertex " + v + " is not between 0 and " + "(V - 1)"); }
        }
        // ---  Adds the directed edge v→w to this digraph.
        public void AddEdge(int v, int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            adj[v].Push(w);
            indegree[w]++;
            E++;
        }
        // --- Adjacent
        public Bag<int> GetAdj(int v)
        {
            ValidateVertex(v);
            return adj[v];
        }
        // --- outDegree
        public int GetOutDegree(int v)
        {
            ValidateVertex(v);
            return adj[v].Size();
        }
        // --- inDegree
        public int GetInDegree(int v)
        {
            ValidateVertex(v);
            return indegree[v];
        }
        // --- reverseGraph
        public Digraph Reverse()
        {
            Digraph reverseGraph = new Digraph(V);
            
            for (int v = 0; v < V; v++)
            {
                foreach (int w in adj[v])
                {
                    reverseGraph.AddEdge(w, v);
                }
            }
            return reverseGraph;
        }

        // --- to string (not tested...)
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(V + " vertices," + E + " edges " + "\n");
            for (int v = 0; v < V; v++)
            {
                s.Append(v.ToString() + " ");
                foreach (int w in adj[v]) { s.Append(w + " "); }
                s.Append("\n");
            }
            return s.ToString();
        }
        // ---
        public Boolean IsReachible(Digraph G, int s, int v)
        {
            DirectedDFS dfs = new DirectedDFS(G, s);
            return dfs.IsMarked(v);
        }
        // --- HasPathToDFS
        public Boolean IsPathToDFS(Digraph G, int s, int v)
        {
            DepthFirstDirectedPathes d = new DepthFirstDirectedPathes(G, s);
            return d.IsPathTo(v);
        }
        // --- GetPathToDFS
        public void GetPathToDFS(Digraph G, int s, int v, ref Stack<int> path)
        {
            if (!IsPathToDFS(G, s, v)) return;
            DepthFirstDirectedPathes d = new DepthFirstDirectedPathes(G, s);
            d.GetPathTo(v, ref path);
        }
        // --- HasPathToBFS
        public Boolean IsPathToBFS(Digraph G, int s, int v)
        {
            BreadthFirstDirectedPaths d = new BreadthFirstDirectedPaths(G, s);
            return d.IsPathTo(v);
        }
        // --- GetShortestDistTo
        public int GetShortestDistToBFS(Digraph G, int s, int v)
        {
            if (!IsPathToBFS(G, s, v)) return -1;
            BreadthFirstDirectedPaths d = new BreadthFirstDirectedPaths(G, s);
            return d.GetShortestDistTo(v);
        }
        // --- GetShortestPathTo
        public void GetShortestPathToBFS(Digraph G, int s, int v, ref Stack<int> path)
        {
            if (!IsPathToBFS(G, s, v)) return;
            BreadthFirstDirectedPaths d = new BreadthFirstDirectedPaths(G, s);
            d.GetShortestPathTo(v, ref path);
        }
        // --- Cycles
        public bool IsCycle(Digraph G)
        {
            DirectedCycle o = new DirectedCycle(G);
            return o.IsCycle();
        }
        // --- GetCycle
        public Stack<int> GetCycle(Digraph G)
        {
            DirectedCycle o = new DirectedCycle(G);
            return o.GetCycle();
        }
        // --- Different orders...
        public Queue<int> GetPreOrder(Digraph G)
        {
            DigraphTopological o = new DigraphTopological(G);
            return o.Pre;
        }        
        public Queue<int> GetPostOrder(Digraph G)
        {
            DigraphTopological o = new DigraphTopological(G);
            return o.Post;
        }
        public Stack<int> GetReversePostOrder(Digraph G)
        {
            DigraphTopological o = new DigraphTopological(G);
            return o.ReversePost;
        }
        // --- Topological Order...
        public Stack<int> GetTopologicalOrder(Digraph G)        
        {
            DigraphTopological2 o = new DigraphTopological2(G);
            return new Stack<int>(o.Order);
        }
        public bool IsOrderIsTopologicalOrder(Digraph G, Stack<int> SomeOrder)
        {
            DigraphTopological2 o = new DigraphTopological2(G);
            Stack<int> topologicalOrder = new Stack<int>(o.Order);
            for (int i = 0; i < G.GetVertices(); i++)
            {
                int v = SomeOrder.Pop();
                int w = topologicalOrder.Pop();
                if (v != w) return false;
            }
            return true;              
        }
        // --- Kosaraju
        public bool IsStronglyConnected(Digraph G, int v, int w)
        {
            KosarajuSCC k = new KosarajuSCC(G);
            return k.IsStronglyConnected(v,w);
        }
    }
}
