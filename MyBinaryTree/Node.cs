using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBinaryTree
{
    internal class Node<T> : INode<T>
        where T : IComparable<T>
    {
        public T Value { get; set; }
        public INode<T> Right { get; set; }
        public INode<T> Left { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Right = null;
            this.Left = null;
        }
    }
}
