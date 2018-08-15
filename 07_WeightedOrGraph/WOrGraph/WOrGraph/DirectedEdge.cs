using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrGraph
{
    class DirectedEdge
    {
        private int v;
        private int w;
        private double weight;
        public DirectedEdge(int v, int w, double weight)
        {
            if (v < 0) throw new ArgumentException("Vertex names must be nonnegative integers");
            if (w < 0) throw new ArgumentException("Vertex names must be nonnegative integers");
            if (Double.IsNaN(weight)) throw new ArgumentException("Weight is NaN");
            this.v = v;
            this.w = w;
            this.weight = weight;
        }
        public int From()
        {
            return v;
        }
        public int To()
        {
            return w;
        }
        public double GetWeight()
        {
            return weight;
        }

    }
}
