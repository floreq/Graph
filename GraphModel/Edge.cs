namespace GraphModel
{
    public class Edge
    {
        public Vertice I { get; set; }
        public Vertice J { get; set; }
        public int Weight { get; set; }

        public Edge(Vertice i, Vertice j)
        {
            I = i;
            J = j;
        }

        public override string ToString()
        {
            return $"{I},{J}";
        }
    }
}
