using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphModel
{
    public class Tree : IGraph
    {
        public List<Vertice> V { get; set; } = new List<Vertice>();
        public List<Edge> E { get; set; } = new List<Edge>();

        public void Generate(in List<Vertice> v)
        {
            Random random = new Random();

            var unallocatedV = v.Select(item => (Vertice)item.Clone()).ToList(); // Usuniecie referencji, przekopiowanie wierzcholkow

            var index = random.Next(0, unallocatedV.Count);
            var newVertice = unallocatedV[index]; // Wylosowanie pierwszego wierzcholka drzewa

            V.Add(newVertice); // Dodanie wierzcholka do drzewa
            unallocatedV.RemoveAt(index); // Usuniecie wytypowanego wierzcholka z listy

            for (int i = 1; i < v.Count; i++)
            {
                var allocatedVertice = V[random.Next(0, V.Count)]; // Wylosowanie wierzcholka z drzewa do utworzenia krawedzi

                index = random.Next(0, unallocatedV.Count);
                newVertice = unallocatedV[index];

                V.Add(newVertice);
                unallocatedV.RemoveAt(index);
                E.Add(new Edge(allocatedVertice, newVertice)); // Utworzenie krawedzi w drzewie
            }
        }
        public void SetEdgesWeight(WeightMatrix w)
        {
            foreach (var edge in E)
            {
                edge.Weight = w.GetEdgeWeight(edge);
            }
        }
        public int GetEdgesWeight()
        {
            var sum = 0;
            foreach (var edge in E)
            {
                sum += edge.Weight;
            }
            return sum;
        }
        public void Draw()
        {
            Console.Write("Waga = {0} ", GetEdgesWeight());
            foreach (var edge in E)
            {
                Console.Write("{0},", edge);
            }
            Console.WriteLine();
        }
    }
}
