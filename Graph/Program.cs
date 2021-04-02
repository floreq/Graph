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

            //for (int i = 0; i < 100; i++)
            //{
            //    var tree = new Tree();
            //    tree.Generate(v);
            //    tree.SetEdgesWeight(wm);

            //    Console.WriteLine("Orginal: {0}", tree);

            //    tree.Mutation();
            //    Console.WriteLine("Mutation: {0}", tree);
            //}

            // Utworzenie pliku
            //using (StreamWriter file = new("../../../../tree-log.csv"))
            //{
            //    file.WriteLine("Index;Weight;Edges;"); // Dodanie naglowkow
            //    for (int i = 0; i < 100; i++)
            //    {
            //        var tree = new Tree();
            //        tree.Generate(v);
            //        tree.SetEdgesWeight(wm);

            //        var sb = new StringBuilder();
            //        sb.Append($"{i};");
            //        sb.Append(tree);

            //        file.WriteLine(sb); // Dodanie drzewa do pliku
            //    }
            //}

            //Odczytanie z pliku
            List<Tree> trees = new List<Tree>();
            using (StreamReader file = new StreamReader("../../../../tree-log.csv"))
            {
                file.ReadLine();
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine().Split(';');
                    var e = new List<Edge>();

                    for (int i = 2; i < line.Length - 1; i++)
                    {
                        var column = line[i].Split(',');
                        var edge = new Edge(new Vertice(column[0]), new Vertice(column[1]));
                        edge.Weight = wm.GetEdgeWeight(edge);

                        e.Add(edge);
                    }

                    trees.Add(new Tree(e));
                }
            }

            // Utworzenie pliku
            using (StreamWriter file = new("../../../../tree-mutations-log.csv"))
            {
                file.WriteLine("Index;Weight;Edges;"); // Dodanie naglowkow
                for (int i = 0; i < 100; i++)
                {
                    trees[i].Mutation();
                    trees[i].SetEdgesWeight(wm);
                    var sb = new StringBuilder();
                    sb.Append($"{i};");
                    sb.Append(trees[i]);

                    file.WriteLine(sb); // Dodanie drzewa do pliku
                }
            }
        }
    }
}
