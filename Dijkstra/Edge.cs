using System;
using System.Text;

namespace Dijkstra
{
    public class Edge
    {
        // NO LOOPS AND ONLY SINGLE CONNECTIONS BETWEEN NODES
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }
        public float Weight { get; set; }

        public Edge(Node startNode, Node endNode, float weight)
        {
            if (startNode == null)
            {
                throw new ArgumentNullException("Start Node");
            }

            if (endNode == null)
            {
                throw new ArgumentNullException("End Node");
            }

            if (startNode == endNode)
            {
                throw new ArgumentException("Loop attempt");
            }

            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Weight = weight;

            this.StartNode.AddConnection(this);
            this.EndNode.AddConnection(this);
        }

        public override string ToString()
        {
            StringBuilder edgeInfo = new StringBuilder();
            edgeInfo.Append($"Start Node: {this.StartNode.NodeId}; ");
            edgeInfo.Append($"End Node: {this.EndNode.NodeId}; ");
            edgeInfo.Append($"Weight: {this.Weight}");

            return edgeInfo.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(Edge))
            {
                return false;
            }

            Edge edge = (Edge)obj;
            if (edge.EndNode == this.EndNode && 
                edge.StartNode == this.StartNode)
            {
                return true;
            }

            return false;
        }
    }
}