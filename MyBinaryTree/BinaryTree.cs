using System;

namespace MyBinaryTree
{
    public class BinaryTree<T> where T : IComparable<T>
    {
        private int size;
        public int Size
        {
            get
            {
                return this.size;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Size");
                }

                this.size = value;
            }
        }

        public INode<T> Root { get; set; }
        public BinaryTree()
        {
            this.Size = 0;
            this.Root = null;
        }

        public bool Contains(T value)
        {
            INode<T> node = this.Root;
            while (node != null)
            {
                int cmp = value.CompareTo(node.Value);
                if (cmp == 0)
                {
                    return true;
                }

                if (cmp < 0)
                {
                    if (node.Left == null)
                    {
                        return false;
                    }

                    node = node.Left;
                }
                else
                {
                    if (node.Right == null)
                    {
                        return false;
                    }

                    node = node.Right;
                }
            }

            return false;
        }

        public void Add(T value)
        {
            this.Root = this.Add(this.Root, value);
        }

        public void Remove(T value)
        {
            this.Root = this.Remove(this.Root, value);
        }

        private INode<T> Remove(INode<T> node, T value)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Node doesn't exist");
            }

            int cmp = value.CompareTo(node.Value);
            if (cmp == 0)
            {
                this.Size--;
                if (node.Left == null)
                {
                    return node.Right;
                }

                if (node.Right == null)
                {
                    return node.Left;
                }

                // node has Left && Right children
                INode<T> maxLeft = node.Left;
                INode<T> parent = null;
                while (maxLeft.Right != null)
                {
                    parent = maxLeft;
                    maxLeft = maxLeft.Right;
                }

                if (parent != null)
                {
                    parent.Right = null;
                    maxLeft.Left = node.Left;
                }

                maxLeft.Right = node.Right;
                return maxLeft;
            }

            if (cmp < 0)
            {
                node.Left = this.Remove(node.Left, value);
            }
            else
            {
                node.Right = this.Remove(node.Right, value);
            }

            return node;
        }
        private INode<T> Add(INode<T> node, T value)
        {
            if (node == null)
            {
                node = new Node<T>(value);
                this.Size++;
                return node;
            }

            int cmp = value.CompareTo(node.Value);
            if (cmp == 0)
            {
                // value already exists. Skip it!
                return node;
            }

            if (cmp < 0)
            {
                node.Left = this.Add(node.Left, value);
            }
            else
            {
                node.Right = this.Add(node.Right, value);
            }

            return node;
        }
    }
}
