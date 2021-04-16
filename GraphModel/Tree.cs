using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModel
{
    public class Tree : IGraph
    {
        public List<Vertice> V { get; set; }
        public List<Edge> E { get; set; }

        public Tree()
        {
            V = new List<Vertice>();
            E = new List<Edge>();
        }
        public Tree(List<Edge> e)
        {
            V = new List<Vertice>();
            E = new List<Edge>(e);

            for (int i = 0; i < e.Count; i++)
            {
                if (!V.Any(x => x.Name == e[i].I.Name)) V.Add(e[i].I);
                if (!V.Any(x => x.Name == e[i].J.Name)) V.Add(e[i].J);
            }
        }
        public void Generate(in List<Vertice> v)
        {
            Random random = new Random();

            // Usuniecie referencji, przekopiowanie wierzcholkow
            var unallocatedV = v.Select(item => (Vertice)item.Clone()).ToList();

            var index = random.Next(0, unallocatedV.Count);
            // Wylosowanie pierwszego wierzcholka drzewa
            var newVertice = unallocatedV[index];

            // Dodanie wierzcholka do drzewa
            V.Add(newVertice);
            // Usuniecie wytypowanego wierzcholka z listy
            unallocatedV.RemoveAt(index);

            for (int i = 1; i < v.Count; i++)
            {
                // Wylosowanie wierzcholka z drzewa do utworzenia krawedzi
                var allocatedVertice = V[random.Next(0, V.Count)];

                index = random.Next(0, unallocatedV.Count);
                newVertice = unallocatedV[index];

                V.Add(newVertice);
                unallocatedV.RemoveAt(index);
                // Utworzenie krawedzi w drzewie
                E.Add(new Edge(allocatedVertice, newVertice));
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
                if (e.I.Name == v.Name) neighbors.Add(e.J);
                else if (e.J.Name == v.Name) neighbors.Add(e.I);
            }
            return neighbors;
        }
        public void Mutation()
        {
            // Wylosowanie krawedzi do usuniecia
            var randomEdge = GetRandomEdge();
            E.Remove(randomEdge);

            // Znalezienie sasiadow  wierzcholkow usnietej krawedzi
            var neighborsVerticeI = GetNeighbors(randomEdge.I);
            var neighborsVerticeJ = GetNeighbors(randomEdge.J);

            // Sprawdzenie czy wierzcholek krawedzi konczy sie lisciem
            if (neighborsVerticeI.Count() == 0)
            {
                // Usuniecie liscia z puli wierzcholkow
                var v = V.Where(x => x.Name != randomEdge.I.Name).ToList();
                // Utworzenie krawedzi
                CreateRandomEdge(randomEdge.I, v);
                return;
            }
            else if (neighborsVerticeJ.Count() == 0)
            {
                var v = V.Where(x => x.Name != randomEdge.J.Name).ToList();
                CreateRandomEdge(randomEdge.J, v);
                return;
            }

            var allNeighbors = new List<Vertice>(neighborsVerticeJ);
            allNeighbors.Add(randomEdge.J);

            // Znalezienie wszystkich wierzcholkow nalezacych do zbioru zawierajacego wierzcholek randomEdge.J
            for (int i = 0; i < allNeighbors.Count; i++)
            {
                // Znalezienie sasiadow wierzcholka
                var iNeighbors = GetNeighbors(allNeighbors[i]);
                // Usuniecie wierzcholkow, ktore juz zostaly zapisane do zbioru allNeighbors
                var withoutDuplicates = iNeighbors.Where(x => !allNeighbors.Any(y => x.Name == y.Name));
                allNeighbors.AddRange(withoutDuplicates);
            }

            // Od zbioru wszystkich wieszcholkow wykonanie roznicy ze zbioru allNeighbors
            List<Vertice> verticesDiffrence = V.Where(x => !allNeighbors.Contains(x)).ToList();
            // Utworzenie nowej krawedzi
            CreateRandomEdge(randomEdge.J, verticesDiffrence);
        }
        // Parametr maxNumberOfTries odpowiedzialny za ograniczenie maksymalnej ilosc prob mutacji
        public int MutationWithLocalSearch(int maxNumberOfTries = 10, WeightMatrix w)
        {
            // Zapisanie orginalnych krawedzi drzewa
            var orginalEdges = new List<Edge>(E);
            // Zapisanie orginalnej wagi calego drzewa
            var orginalEdgesWeight = GetEdgesWeight();
            // Ustawienie licznika porob
            int numberOfTries = 0;

            // Wykonanie serii prob zmniejszenia wagi drzewa
            for (int i = 0; i < maxNumberOfTries; i++)
            {
                // Jezeli udalo sie zmniejszyc wage drzewa to przerwac dzialanie
                if (orginalEdgesWeight > GetEdgesWeight()) break;
                // Przypisanie drzewu orginalnych krawedzi
                E = new List<Edge>(orginalEdges);
                // Wykonanie mutacji
                Mutation();
                // Ustawienie wag dla zmutowanego drzewa
                SetEdgesWeight(w);
                numberOfTries++;
            }
            return numberOfTries;
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
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"{GetEdgesWeight()};");
            foreach (var edge in E)
            {
                result.Append($"{edge};");
            }
            return result.ToString();
        }
    }
}
