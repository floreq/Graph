using System.Collections.Generic;

namespace GraphModel
{
    interface IGraph
    {
        List<Vertice> V { get; set; }
        List<Edge> E { get; set; }
        void Draw();
    }
}
