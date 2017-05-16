using PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraWithMinHeap
{
    public class Graph : IGraph
    {
        protected readonly IEnumerable<INode> graph;
        protected readonly BaseHeap<INode> pq;

        public int Count
        {
            get
            {
                return this.graph.Count();
            }
        }

        public Graph(IEnumerable<INode> graph, BaseHeap<INode> pq)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("Graph");
            }

            if (pq == null)
            {
                throw new ArgumentNullException("Priority Queue");
            }

            this.graph = graph;
            this.pq = pq;
        }

        public INode ElementAt(int index)
        {
            if (0 > index || index > this.Count)
            {
                throw new ArgumentOutOfRangeException("Graph Index");
            }

            INode element = this.graph.ElementAt(index);

            return element;
        }

        public INode FindShortestPathBetweenNodes(int sourceIndex, int destinationIndex)
        {
            if (this.Count < sourceIndex)
            {
                throw new ArgumentException("Source Index");
            }

            if (this.Count < destinationIndex)
            {
                throw new ArgumentException("Destination Index");
            }

            this.graph.ElementAt(sourceIndex).Value = 0;
            foreach (INode node in graph)
            {
                pq.AddValue(node);
            }

            INode currentNode = pq.GetTop();
            pq.RemoveTop();
            while (pq.Count > 0 && currentNode.Index != destinationIndex)
            {
                foreach (var edge in currentNode.ListOfNeighbors)
                {
                    INode nextNode = this.graph.ElementAt(edge.Key);
                    var altValue = currentNode.Value + edge.Value;
                    if (altValue < nextNode.Value)
                    {
                        nextNode.Value = altValue;
                        nextNode.PreviousElementIndex = currentNode.Index;
                        pq.AddValue(nextNode);
                    }
                }

                currentNode = pq.GetTop();
                pq.RemoveTop();
            }

            return currentNode;
        }
    }
}
