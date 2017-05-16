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
            IGraph graph = CreateGraph();
            int sourceIndex = 0;
            int destinationIndex = 5;
            INode destination = graph.FindShortestPathBetweenNodes(sourceIndex, destinationIndex);
            int prevElementIndex = destination.PreviousElementIndex;
            StringBuilder output = new StringBuilder("Nodes passed in reverse order: ");
            ICollection<int> path = new List<int>() { destination.Index };
            while (prevElementIndex > -1)
            {
                path.Add(prevElementIndex);
                prevElementIndex = graph.ElementAt(prevElementIndex).PreviousElementIndex;
            }

            output.AppendLine(string.Join(", ", path));
            output.AppendLine($"Total cost: {destination.Value}");
            Console.WriteLine(output.ToString());
        }

        private static IGraph CreateGraph()
        {
            IList<INode> graphList = new List<INode>();
            graphList.Add(new Node(0, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(1, 1),
                    new KeyValuePair<int, double>(3, 3),
                    new KeyValuePair<int, double>(4, 7)
                }));
            graphList.Add(new Node(1, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(0, 1),
                    new KeyValuePair<int, double>(2, 1),
                    new KeyValuePair<int, double>(4, 6)
                }));
            graphList.Add(new Node(2, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(1, 1),
                    new KeyValuePair<int, double>(5, 1),
                }));
            graphList.Add(new Node(3, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(0, 3),
                    new KeyValuePair<int, double>(4, 12),
                    new KeyValuePair<int, double>(6, 8)
                }));
            graphList.Add(new Node(4, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(0, 7),
                    new KeyValuePair<int, double>(1, 6),
                    new KeyValuePair<int, double>(3, 12),
                    new KeyValuePair<int, double>(5, 10),
                    new KeyValuePair<int, double>(6, 3)
                }));
            graphList.Add(new Node(5, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(2, 1),
                    new KeyValuePair<int, double>(4, 10),
                    new KeyValuePair<int, double>(6, 4)
                }));
            graphList.Add(new Node(6, double.MaxValue, new List<KeyValuePair<int, double>>()
                {
                    new KeyValuePair<int, double>(3, 8),
                    new KeyValuePair<int, double>(4, 3),
                    new KeyValuePair<int, double>(5, 4)
                }));

            BaseHeap<INode> pq = new MinHeap<INode>();
            IGraph graph = new Graph(graphList, pq);

            return graph;
        }
    }
}
