using System;
using System.Collections;
using System.Collections.Generic;

namespace MyAvlTree
{
    public class AvlNode<T> : IAvlNode<T>
        where T : IComparable<T>
    {
        private int size;
        private int balanceFactor;
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
        public int BalanceFactor
        {
            get
            {
                return this.balanceFactor;
            }

            set
            {
                if (value < -1 || 1 < value)
                {
                    throw new ArgumentNullException("BalanceFactor");
                }

                this.balanceFactor = value;
            }
        }
        public T Value { get; set; }
        public IAvlNode<T> Right { get; set; }
        public IAvlNode<T> Left { get; set; }
        public IAvlNode<T> Parent { get; set; }
        public AvlNode(T value)
        {
            this.Value = value;
            this.Size = 1;
            this.BalanceFactor = 0;
            this.Right = null;
            this.Left = null;
            this.Parent = null;
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
