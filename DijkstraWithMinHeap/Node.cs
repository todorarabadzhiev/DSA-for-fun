using System;
using System.Collections.Generic;

namespace DijkstraWithMinHeap
{
    public class Node : INode, IComparable
    {
        public double Value { get; set; }
        public IEnumerable<KeyValuePair<int, double>> ListOfNeighbors { get; set; }
        public int Index { get; set; }
        public int PreviousElementIndex { get; set; }

        public Node(int index, double value, IEnumerable<KeyValuePair<int, double>> listOfNeighbors)
        {
            if (listOfNeighbors == null)
            {
                throw new ArgumentNullException("List of Neighbors");
            }

            this.Index = index;
            this.Value = value;
            this.PreviousElementIndex = -1;
            this.ListOfNeighbors = listOfNeighbors;
        }

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is INode))
            {
                throw new ArgumentException("Object is not INode");
            }

            INode otherNode = (INode)obj;
            
            return this.Value.CompareTo(otherNode.Value);
        }
    }
}
