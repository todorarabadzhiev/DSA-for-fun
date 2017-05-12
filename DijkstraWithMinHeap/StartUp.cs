using PriorityQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraWithMinHeap
{
    public class StartUp
    {
        public static void Main()
        {
            var graph = CreateGraph();
            int sourceIndex = 0;
            int destinationIndex = 6;
            double cost = FindShortestPathBetween(graph, sourceIndex, destinationIndex);
            INode destination = graph[destinationIndex];
            int prevElementIndex = destination.PreviousElementIndex;
            StringBuilder output = new StringBuilder("Nodes passed in reverse order: ");
            ICollection<int> path = new List<int>() { destination.Index };
            while (prevElementIndex > -1)
            {
                path.Add(prevElementIndex);
                prevElementIndex = graph[prevElementIndex].PreviousElementIndex;
             }

            output.AppendLine(string.Join(", ", path));
            output.AppendLine($"Total cost: {cost}");
            Console.WriteLine(output.ToString());
        }

        private static double FindShortestPathBetween(IList<INode> graph, int firstNodeIndex, int secondNodeIndex)
        {
            var pq = new MinHeap<INode>();
            foreach (INode node in graph)
            {
                pq.AddValue(node);
            }

            INode currentNode = pq.GetTop();
            pq.RemoveTop();
            while (pq.Count > 0 && currentNode.Index != secondNodeIndex)
            {
                foreach (var edge in currentNode.ListOfNeighbors)
                {
                    INode nextNode = graph[edge.Key];
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

            return currentNode.Value;
        }

        private static IList<INode> CreateGraph()
        {
            IList<INode> graph = new List<INode>();
            graph.Add(new Node(0, 0, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(1, 1),
                    new KeyValuePair<int, double>(3, 3),
                    new KeyValuePair<int, double>(4, 7)
                }));
            graph.Add(new Node(1, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(0, 1),
                    new KeyValuePair<int, double>(2, 5),
                    new KeyValuePair<int, double>(4, 6)
                }));
            graph.Add(new Node(2, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(1, 5),
                    new KeyValuePair<int, double>(5, 4),
                }));
            graph.Add(new Node(3, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(0, 3),
                    new KeyValuePair<int, double>(4, 12),
                    new KeyValuePair<int, double>(6, 8)
                }));
            graph.Add(new Node(4, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(0, 7),
                    new KeyValuePair<int, double>(1, 6),
                    new KeyValuePair<int, double>(3, 12),
                    new KeyValuePair<int, double>(5, 10),
                    new KeyValuePair<int, double>(6, 3)
                }));
            graph.Add(new Node(5, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(2, 4),
                    new KeyValuePair<int, double>(4, 10),
                    new KeyValuePair<int, double>(6, 4)
                }));
            graph.Add(new Node(6, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(3, 8),
                    new KeyValuePair<int, double>(4, 3),
                    new KeyValuePair<int, double>(5, 4)
                }));

            return graph;
        }
    }
}
