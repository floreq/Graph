using System.Collections.Generic;

namespace GraphModel
{
    public interface IGraph
    {
        List<Vertice> V { get; set; }
        List<Edge> E { get; set; }
        void Draw();
    }
}
