using System.Collections.Generic;

namespace Dijkstra
{
    public interface IPathToNode : INode
    {
        float Value { get; set; }
        ICollection<INode> CurrentPathToNode { get; }
        string PrintPathToNode();
        void UpdateNeighbourValuesInCollection(ICollection<INode> searchNodes);
    }
}