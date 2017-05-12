using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public interface INode
    {
        string NodeId { get; }
        ICollection<IEdge> Connections { get; }
        ICollection<INode> NeighbourNodes { get; }
        void AddConnection(IEdge edge);
        void RemoveConnection(IEdge edge);
    }
}