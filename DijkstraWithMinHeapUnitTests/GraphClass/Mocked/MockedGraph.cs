using DijkstraWithMinHeap;
using PriorityQueue;
using System.Collections.Generic;

namespace DijkstraWithMinHeapUnitTests.GraphClass.Mocked
{
    public class MockedGraph : Graph
    {
        public IEnumerable<INode> Graph
        {
            get
            {
                return this.graph;
            }
        }

        public BaseHeap<INode> Pq
        {
            get
            {
                return this.pq;
            }
        }

        public MockedGraph(IEnumerable<INode> graph, BaseHeap<INode> pq)
            : base(graph, pq)
        {
        }
    }
}
