using System.Collections.Generic;

namespace GraphModel
{
    public class WeightMatrix
    {
        public int[,] W { get; private set; }
        public List<Vertice> V { get; private set; }

        public WeightMatrix(List<Vertice> v)
        {
            V = v;
            Generate(v.Count);
        }
        private void Generate(int verticeCount)
        {
            W = new int[verticeCount, verticeCount];

            int count = 1;
            for (int i = 0; i < verticeCount; i++)
            {
                for (int j = i; j < verticeCount; j++)
                {
                    if (j == i)
                    {
                        W[i, j] = 0;
                        continue;
                    }
                    W[i, j] = count;
                    W[j, i] = count;
                    count++;
                }
            }
        }
        public int GetEdgeWeight(Edge e)
        {
            var i = V.FindIndex(x => x.Name == e.I.Name);
            var j = V.FindIndex(x => x.Name == e.J.Name);
            return W[i, j];
        }
    }
}
