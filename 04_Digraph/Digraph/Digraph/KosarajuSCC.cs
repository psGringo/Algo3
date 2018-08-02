using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph
{
    class KosarajuSCC // Kosaraju Strong Connected Components
    {
        private bool[] marked;
        private int[] id;
        private int count;
        //
        public KosarajuSCC(Digraph G)
        {
            marked = new bool[G.GetVertices()];
            id = new int[G.GetVertices()];
            DigraphTopological d = new DigraphTopological(G.Reverse());

            foreach (int s in d.ReversePost) // reversePost Order here
            {
                if (!marked[s])
                {
                    Dfs(G, s);
                    count++;
                }

            }
        }
        // DFS on graph G
        private void Dfs(Digraph G, int v)
        {
            marked[v] = true;
            id[v] = count;
            foreach (int w in G.GetAdj(v))
            {
                if (!marked[w]) Dfs(G, w);
            }
        }

        public bool IsStronglyConnected(int v, int w)
        {
            return id[v] == id[w];
        }

        public int GetID(int v)
        {
            return id[v];
        }

        public int GetCount()
        {
            return count;
        }

    }
}
