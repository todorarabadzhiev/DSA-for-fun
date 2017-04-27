using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public class PathGraph
    {
        public ICollection<Node> PathNodes { get; set; }
        public ICollection<Edge> PathEdges { get; set; }
        public PathGraph(ICollection<Edge> edges)
        {
            if (edges == null)
            {
                throw new ArgumentNullException("Edges");
            }

            this.PathEdges = edges;
            this.PathNodes = new List<Node>();
            this.GetNodesFromEdges(edges);
        }

        public void AddEdge(Edge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("Edge");
            }

            if (this.PathEdges.Contains(edge))
            {
                return;
            }

            this.PathEdges.Add(edge);
            this.GetNodesFromEdges(new List<Edge>() { edge });
            edge.StartNode.AddConnection(edge);
            edge.EndNode.AddConnection(edge);
        }

        public void RemoveEdge(Edge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("Edge");
            }

            if (!this.PathEdges.Contains(edge))
            {
                return;
            }

            edge.StartNode.RemoveConnection(edge);
            edge.EndNode.RemoveConnection(edge);
            this.PathEdges.Remove(edge);
        }

        public void RemoveNode(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Node");
            }

            if (!this.PathNodes.Contains(node))
            {
                throw new ArgumentException("Node not in Path");
            }

            foreach (Edge connection in node.Connections)
            {
                if (connection.StartNode == node)
                {
                    connection.EndNode.RemoveConnection(connection);
                }
                else
                {
                    connection.StartNode.RemoveConnection(connection);
                }

                this.PathEdges.Remove(connection);
            }

            node.Connections.Clear();
            this.PathNodes.Remove(node);
        }

        public void FindShortestPathWithDijkstra(Node node_start, Node node_end)
        {
            if (!this.PathNodes.Contains(node_start))
            {
                throw new ArgumentException($"No such node {node_start.NodeId}!");
            }

            if (!this.PathNodes.Contains(node_end))
            {
                throw new ArgumentException($"No such node {node_end.NodeId}!");
            }

            ICollection<Node> activeNodes = new List<Node>();// Nodes To Pass
            ICollection<Node> passedNodes = new List<Node>();// Passed Nodes
            float totalValue = 0;
            foreach (Node node in this.PathNodes)
            {
                node.Value = int.MaxValue;
                activeNodes.Add(node);
            }

            node_start.Value = 0;
            Node currentNode = node_start;

            while (currentNode != node_end)
            {
                passedNodes.Add(currentNode);
                activeNodes.Remove(currentNode);
                currentNode.UpdateNeighbourValuesInCollection(activeNodes);

                float minValue = int.MaxValue;
                foreach (Node node in activeNodes)
                {
                    if (node.Value == minValue && node == node_end)
                    {
                        currentNode = node_end;
                    }

                    if (node.Value < minValue)
                    {
                        minValue = node.Value;
                        currentNode = node;
                    }
                }

                totalValue = currentNode.Value;
            }

            Console.WriteLine($"Total Value: {totalValue}");
            foreach (Node node in currentNode.CurrentPathToNode)
            {
                Console.WriteLine($"{node.NodeId} passed");
            }

            Console.WriteLine($"{currentNode.NodeId} reached");
        }

        public override string ToString()
        {
            StringBuilder graphInfo = new StringBuilder();
            foreach (Node node in this.PathNodes)
            {
                graphInfo.Append(node);
            }

            return graphInfo.ToString();
        }

        private void GetNodesFromEdges(IEnumerable<Edge> edges)
        {
            foreach (Edge edge in edges)
            {
                if (!this.PathNodes.Contains(edge.StartNode))
                {
                    this.PathNodes.Add(edge.StartNode);
                }

                if (!this.PathNodes.Contains(edge.EndNode))
                {
                    this.PathNodes.Add(edge.EndNode);
                }
            }
        }
    }
}