﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            EdgeWeightedGraph G = new EdgeWeightedGraph("tinyEWG.txt");
            Console.WriteLine("Test Api");
            Console.WriteLine("Number of Vertices: "+G.GetVertices());
            Console.WriteLine("Number of Edges: " + G.GetVertices());
            Console.WriteLine("Edges " + G.GetVertices());
            Console.WriteLine("Adjacaent edges to 0");
            Stack<Edge> adjEdges = new Stack<Edge>(G.GetAdj(0));
            foreach(Edge e in adjEdges)
            {
                int v = e.Either();
                int w = e.Other(v);
                Console.WriteLine(v+"-"+w);
            }
            Console.WriteLine("All edges");
            Stack<Edge> allEdges = new Stack<Edge>(G.GetAllEdges());
            foreach (Edge e in allEdges)
            {
                int v = e.Either();
                int w = e.Other(v);
                Console.WriteLine(v + "-" + w);
            }
            Console.WriteLine("Comparing Edges by weight");
            Stack<Edge> edges = new Stack<Edge>(G.GetAdj(0));
            Edge e1 = edges.Pop();
            Edge e2 = edges.Pop();
            if (e1.CompareTo(e2) == -1) Console.WriteLine("e1<e2");
            else Console.WriteLine("e1>e2");
            Console.WriteLine("Weight of e1 == "+e1.GetWeight());
            Console.WriteLine("Weight of e2 == " + e2.GetWeight());
            // --- LAZY PRIM
            Console.WriteLine(" ");
            Console.WriteLine("LazyPrimMST ");
            LazyPrimMST lazyPrimMST = new LazyPrimMST(G);
            Queue<Edge> minSpanningTreeQueue = new Queue<Edge>();
            minSpanningTreeQueue= (Queue<Edge>)(lazyPrimMST.GetEdges());
            Console.WriteLine("weight of minimal spanning tree tree is " + lazyPrimMST.GetWeight());

            foreach (Edge e in minSpanningTreeQueue)
            {
                int v = e.Either();
                int w = e.Other(v);
                Console.WriteLine(v +" - " + w);
            }
            // --- PRIM
            Console.WriteLine(" ");
            Console.WriteLine("PrimMST ");
            LazyPrimMST primMST = new LazyPrimMST(G);
            Queue<Edge> minSpanningTreeQueue2 = new Queue<Edge>();
            minSpanningTreeQueue2 = (Queue<Edge>)(primMST.GetEdges());
            Console.WriteLine("weight of minimal spanning tree tree is " + primMST.GetWeight());

            foreach (Edge e in minSpanningTreeQueue2)
            {
                int v = e.Either();
                int w = e.Other(v);
                Console.WriteLine(v + " - " + w);
            }

            Console.WriteLine(" ");
            Console.WriteLine("String Representation of Graph");
            Console.WriteLine(G.ToString()); 
            Console.ReadLine();
        }
    }
}
