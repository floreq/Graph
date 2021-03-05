using System;
using System.Collections.Generic;
using GraphModel;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            var v = new List<Vertice>();
            v.Add(new Vertice("1"));
            v.Add(new Vertice("2"));
            v.Add(new Vertice("3"));
            v.Add(new Vertice("4"));
            v.Add(new Vertice("5"));
            v.Add(new Vertice("6"));
            v.Add(new Vertice("7"));
            v.Add(new Vertice("8"));
            v.Add(new Vertice("9"));
            v.Add(new Vertice("10"));
            var wm = new WeightMatrix(v);
            for (int i = 0; i < 100; i++)
            {
                Console.Write("Drzewo {0} = {{", i);
                var tree = new Tree();
                tree.Generate(v);
                tree.SetEdgesWeight(wm);
                tree.Draw();
            }
        }
    }
}
