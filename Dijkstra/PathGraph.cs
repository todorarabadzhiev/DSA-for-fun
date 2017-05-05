using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public class PathGraph
    {
        public ICollection<INode> PathNodes { get; set; }
        public ICollection<IEdge> PathEdges { get; set; }
        public PathGraph(ICollection<IEdge> edges)
        {
            if (edges == null)
            {
                throw new ArgumentNullException("Edges");
            }

            this.PathEdges = edges;
            this.PathNodes = new List<INode>();
            this.GetNodesFromEdges(edges);
        }

        public void AddEdge(IEdge edge)
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
            this.GetNodesFromEdges(new List<IEdge>() { edge });
            edge.StartNode.AddConnection(edge);
            edge.EndNode.AddConnection(edge);
        }

        public void RemoveEdge(IEdge edge)
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

        public void RemoveNode(INode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Node");
            }

            if (!this.PathNodes.Contains(node))
            {
                throw new ArgumentException("Node not in Path");
            }

            foreach (IEdge connection in node.Connections)
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
        
        public IPathToNode FindShortestPathWithDijkstra(IPathToNode node_start, IPathToNode node_end)
        {
            if (!this.PathNodes.Contains(node_start))
            {
                throw new ArgumentException($"No such node {node_start.NodeId}!");
            }

            if (!this.PathNodes.Contains(node_end))
            {
                throw new ArgumentException($"No such node {node_end.NodeId}!");
            }

            ICollection<INode> activeNodes = new List<INode>();
            foreach (IPathToNode node in this.PathNodes)
            {
                node.Value = int.MaxValue;
                activeNodes.Add(node);
            }

            node_start.Value = 0;
            IPathToNode currentNode = node_start;

            while (currentNode != node_end)
            {
                activeNodes.Remove(currentNode);
                currentNode.UpdateNeighbourValuesInCollection(activeNodes);

                float minValue = int.MaxValue;
                foreach (IPathToNode node in activeNodes)
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
            }

            return currentNode;
        }

        public override string ToString()
        {
            StringBuilder graphInfo = new StringBuilder();
            foreach (INode node in this.PathNodes)
            {
                graphInfo.Append(node);
            }

            return graphInfo.ToString();
        }

        private void GetNodesFromEdges(IEnumerable<IEdge> edges)
        {
            foreach (IEdge edge in edges)
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