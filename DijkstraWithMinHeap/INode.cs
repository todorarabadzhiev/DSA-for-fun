using System;
using System.Collections.Generic;

namespace DijkstraWithMinHeap
{
    public interface INode : IComparable
    {
        double Value { get; set; }
        int Index { get; set; }
        int PreviousElementIndex { get; set; }
        IEnumerable<KeyValuePair<int, double>> ListOfNeighbors { get; }
    }
}
