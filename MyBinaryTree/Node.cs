using System;
using System.Collections;
using System.Collections.Generic;

namespace MyBinaryTree
{
    public class Node<T> : INode<T>, IEnumerable<T>
        where T : IComparable<T>
    {
        private int size;

        public int Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Size");
                }

                this.size = value;
            }
        }

        public T Value { get; set; }
        public INode<T> Right { get; set; }
        public INode<T> Left { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Size = 1;
            this.Right = null;
            this.Left = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            // IN-ORDER IMPLEMENTATION
            if (this.Left != null)
            {
                foreach (var item in this.Left)
                {
                    yield return item;
                }
            }

            yield return this.Value;

            if (this.Right != null)
            {
                foreach (var item in this.Right)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
