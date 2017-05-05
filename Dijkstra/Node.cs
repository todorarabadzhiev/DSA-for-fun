using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public class Node : INode, IPathToNode
    {
        private const string boundaryString = "--------------------";

        public string NodeId { get; set; }
        public float Value { get; set; }

        public ICollection<IEdge> Connections { get; set; }
        public ICollection<INode> NeighbourNodes { get; set; }
        public ICollection<INode> CurrentPathToNode { get; set; }

        public Node(string id)
        {
            this.NodeId = id;
            this.Connections = new List<IEdge>();
            this.NeighbourNodes = new List<INode>();
            this.CurrentPathToNode = new List<INode>();
        }

        public Node(string id, float value)
            : this(id)
        {
            this.Value = value;
        }

        public void AddConnection(IEdge edge)
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

        public void RemoveConnection(IEdge edge)
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

        public void UpdateNeighbourValuesInCollection(ICollection<INode> searchNodes)
        {
            foreach (IEdge edge in this.Connections)
            {
                if (searchNodes.Contains(edge.EndNode) || searchNodes.Contains(edge.StartNode))
                {
                    if (edge.EndNode == this)
                    {
                        (edge.StartNode as Node).UpdateCurrentPathToNode(this, edge.Weight);
                    }
                    else
                    {
                        (edge.EndNode as Node).UpdateCurrentPathToNode(this, edge.Weight);
                    }
                }
            }
        }

        public string PrintPathToNode()
        {
            StringBuilder pathInfo = new StringBuilder();
            pathInfo.AppendLine(boundaryString);
            pathInfo.AppendLine($"Total Value: {this.Value}");
            foreach (INode node in this.CurrentPathToNode)
            {
                pathInfo.AppendLine($"{node.NodeId} passed");
            }

            pathInfo.AppendLine($"{this.NodeId} reached");
            pathInfo.AppendLine(boundaryString);

            return pathInfo.ToString();
        }

        private void UpdateCurrentPathToNode(IPathToNode updatingNode, float weight)
        {
            float newValue = updatingNode.Value + weight;
            if (this.Value > newValue)
            {
                this.Value = newValue;
                this.CurrentPathToNode.Clear();
                foreach (INode node in updatingNode.CurrentPathToNode)
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
            foreach (INode node in this.NeighbourNodes)
            {
                nodeInfo.Append($"{node.NodeId}; ");
            }
            nodeInfo.AppendLine();
            nodeInfo.AppendLine("Connections: ");
            foreach (IEdge edge in this.Connections)
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

            INode node = (INode)obj;
            if (node.NodeId == this.NodeId)
            {
                return true;
            }

            return false;
        }
    }
}