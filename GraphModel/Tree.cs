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
        public HashSet<Vertice> GetNeighbors(Vertice v)
        {
            var neighbors = new HashSet<Vertice>();
            foreach (var e in E)
            {
                if (e.I == v) neighbors.Add(e.J);
                else if (e.J == v) neighbors.Add(e.I);
            }
            return neighbors;
        }
        public void Mutation()
        {
            var randomEdge = GetRandomEdge();
            E.Remove(randomEdge);

            var neighborsVerticeI = GetNeighbors(randomEdge.I);
            var neighborsVerticeJ = GetNeighbors(randomEdge.J);

            if (neighborsVerticeI.Count() == 0)
            {
                CreateRandomEdge(randomEdge.I, V);
                return;
            }
            else if (neighborsVerticeJ.Count() == 0)
            {
                CreateRandomEdge(randomEdge.J, V);
                return;
            }

            var allNeighbors = new List<Vertice>(neighborsVerticeJ);

            for (int i = 0; i < allNeighbors.Count; i++)
            {
                var iNeighbors = GetNeighbors(allNeighbors[i]);
                var withoutDuplicates = iNeighbors.Where(x => !allNeighbors.Any(y => x == y));
                allNeighbors.AddRange(withoutDuplicates);
            }

            var verticesDiffrence = new List<Vertice>(V);
            verticesDiffrence.Except(allNeighbors);
            CreateRandomEdge(randomEdge.J, verticesDiffrence);
        }
        public void CreateRandomEdge(Vertice from, List<Vertice> candidates)
        {
            var randomIndex = new Random().Next(candidates.Count);
            var newEdge = new Edge(from, candidates[randomIndex]);

            E.Add(newEdge);
        }
        public Edge GetRandomEdge()
        {
            var randomIndex = new Random().Next(E.Count);
            return E[randomIndex];
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
