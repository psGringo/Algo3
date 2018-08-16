using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class AcyclicSP
    {
        private double[] distTo;
        private DirectedEdge[] edgeTo;
        // relax edge e
        private void Relax(DirectedEdge e)
        {
            int v = e.From(), w = e.To();
            if (distTo[w] > distTo[v] + e.GetWeight())
            {
                distTo[w] = distTo[v] + e.GetWeight();
                edgeTo[w] = e;
            }
        }
    }
}
