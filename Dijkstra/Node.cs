using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public class Node
    {
        private const string boundaryString = "--------------------";

        public int NodeId { get; set; }
        public float Value { get; set; }

        public ICollection<Edge> Connections { get; set; }
        public ICollection<Node> NeighbourNodes { get; set; }
        public ICollection<Node> CurrentPathToNode { get; set; }

        public Node(int id, float value)
        {
            this.NodeId = id;
            this.Value = value;
            this.Connections = new List<Edge>();
            this.NeighbourNodes = new List<Node>();
            this.CurrentPathToNode = new List<Node>();
        }

        public void AddConnection(Edge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("Edge");
            }

            if (edge.StartNode != this && edge.EndNode != this)
            {
                throw new ArgumentException("This connection does not contain this node!");
            }

            if (!this.Connections.Contains(edge))
            {
                this.Connections.Add(edge);
            }

            if (edge.StartNode != this && !this.NeighbourNodes.Contains(edge.StartNode))
            {
                this.NeighbourNodes.Add(edge.StartNode);
            }
            else if (!this.NeighbourNodes.Contains(edge.EndNode))
            {
                this.NeighbourNodes.Add(edge.EndNode);
            }
        }

        public void RemoveConnection(Edge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("Edge");
            }

            if (edge.StartNode != this && edge.EndNode != this)
            {
                throw new ArgumentException("This connection does not contain this node!");
            }

            if (this.Connections.Contains(edge))
            {
                this.Connections.Remove(edge);
            }

            if (edge.EndNode == this)
            {
                this.NeighbourNodes.Remove(edge.StartNode);
                edge.StartNode.NeighbourNodes.Remove(this);
            }
            else
            {
                this.NeighbourNodes.Remove(edge.EndNode);
                edge.EndNode.NeighbourNodes.Remove(this);
            }
        }

        public void UpdateNeighbourValuesInCollection(ICollection<Node> searchNodes)
        {
            foreach (Edge edge in this.Connections)
            {
                if (searchNodes.Contains(edge.EndNode) || searchNodes.Contains(edge.StartNode))
                {
                    if (edge.EndNode == this)
                    {
                        edge.StartNode.UpdateCurrentPathToNode(this, edge.Weight);
                    }
                    else
                    {
                        edge.EndNode.UpdateCurrentPathToNode(this, edge.Weight);
                    }
                }
            }
        }

        private void UpdateCurrentPathToNode(Node updatingNode, float weight)
        {
            float newValue = updatingNode.Value + weight;
            if (this.Value > newValue)
            {
                this.Value = newValue;
                this.CurrentPathToNode.Clear();
                foreach (Node node in updatingNode.CurrentPathToNode)
                {
                    this.CurrentPathToNode.Add(node);
                }

                this.CurrentPathToNode.Add(updatingNode);
            }
        }

        public override string ToString()
        {
            StringBuilder nodeInfo = new StringBuilder();
            nodeInfo.AppendLine(boundaryString);
            nodeInfo.AppendLine($"Id: {this.NodeId}");
            nodeInfo.AppendLine($"Value: {this.Value}");
            nodeInfo.Append("Neighbour Nodes: ");
            foreach (Node node in this.NeighbourNodes)
            {
                nodeInfo.Append($"{node.NodeId}; ");
            }
            nodeInfo.AppendLine();
            nodeInfo.AppendLine("Connections: ");
            foreach (Edge edge in this.Connections)
            {
                nodeInfo.AppendLine(edge.ToString());
            }

            nodeInfo.AppendLine(boundaryString);
            return nodeInfo.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(Node))
            {
                return false;
            }

            Node node = (Node)obj;
            if (node.NodeId == this.NodeId)
            {
                return true;
            }

            return false;
        }
    }
}