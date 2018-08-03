using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGraph
{
    class Edge :IComparable<Edge>
    {
        private int v;
        private int w;
        private double weight;
        /**
         * Initializes an edge between vertices {@code v} and {@code w} of
         * the given {@code weight}.
         *
         * @param  v one vertex
         * @param  w the other vertex
         * @param  weight the weight of this edge
         * @throws IllegalArgumentException if either {@code v} or {@code w} 
         *         is a negative integer
         * @throws IllegalArgumentException if {@code weight} is {@code NaN}
         */
        public Edge(int v, int w, double weight)
        {
            if ((v < 0) || (w < 0)) throw new ArgumentException("vertex index must be a nonnegative integer");
            if (Double.IsNaN(weight)) throw new ArgumentException("Weight is NaN");
            this.v = v;
            this.w = w;
            this.weight = weight;
        }
        /**
           * Returns the weight of this edge.
           *
           * @return the weight of this edge
           */
        public double GetWeight()
        {
            return weight;
        }
        /**
          * Returns either endpoint of this edge.
          *
          * @return either endpoint of this edge
          */
        public int Either()
        {
            return v;
        }
        /**
         * Returns the endpoint of this edge that is different from the given vertex.
         *
         * @param  vertex one endpoint of this edge
         * @return the other endpoint of this edge
         * @throws IllegalArgumentException if the vertex is not one of the
         *         endpoints of this edge
         */
        public int Other(int vertex)
        {
            if (vertex == v) return w;
            if (vertex == w) return v;
            else throw new ArgumentException("Illegal endpoint");
        }
        /**
* Compares two edges by weight.
* Note that {@code compareTo()} is not consistent with {@code equals()},
* which uses the reference equality implementation inherited from {@code Object}.
*
* @param  that the other edge
* @return a negative integer, zero, or positive integer depending on whether
*         the weight of this is less than, equal to, or greater than the
*         argument edge
*/
        public int CompareTo(Edge that)
        {            
            if (this.GetWeight() < that.GetWeight()) return -1;
            if (this.GetWeight() > that.GetWeight()) return 1;
            else return 0;
        }
        // to string
        public override string ToString()
        {
            return String.Format("{0}-{1},weight {2}",v,w,weight);
        }
    }
}
