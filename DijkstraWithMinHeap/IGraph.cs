using System.Collections.Generic;

namespace DijkstraWithMinHeap
{
    public interface IGraph
    {
        INode ElementAt(int index);
        INode FindShortestPathBetweenNodes(int sourceIndex, int destinationIndex);
    }
}
