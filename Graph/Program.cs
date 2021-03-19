using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

            using StreamWriter file = new("../../../../tree-log.csv"); // Utworzenie pliku
            file.WriteLine("Index;Weight;Edges;"); // Dodanie naglowkow
            for (int i = 0; i < 100; i++)
            {
                var tree = new Tree();
                tree.Generate(v);
                tree.SetEdgesWeight(wm);

                var sb = new StringBuilder();
                sb.Append($"{i};");
                sb.Append(tree);
                Console.WriteLine(sb);

                file.WriteLine(sb); // Dodanie drzewa do pliku
            }
        }
    }
}
