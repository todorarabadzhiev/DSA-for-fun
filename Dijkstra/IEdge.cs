namespace Dijkstra
{
    public interface IEdge
    {
        INode StartNode { get; }
        INode EndNode { get; }
        float Weight { get; }
    }
}
