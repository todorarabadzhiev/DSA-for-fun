using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyBinaryTree
{
    public class BinaryTree<T> : IBinaryTree<T>, IEnumerable<T>
        where T : IComparable<T>
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

        public IEnumerator<T> GetEnumerator()
        {
            if (this.Root != null)
            {
                foreach (var item in this.Root)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || this.Size <= index)
                {
                    throw new ArgumentOutOfRangeException("Index");
                }

                return this.GetElementByIndex(this.Root, index);
            }
        }

        private T GetElementByIndex(INode<T> node, int index)
        {
            if (node.Left == null)
            {
                if (node.Right == null || index == 0)
                {
                    return node.Value;
                }

                return this.GetElementByIndex(node.Right, index - 1);
            }

            if (node.Left.Size == index)
            {
                return node.Value;
            }

            if (node.Left.Size < index)
            {
                return this.GetElementByIndex(node.Right, index - node.Left.Size - 1);
            }

            return this.GetElementByIndex(node.Left, index);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(string.Join("<->", this));

            return result.ToString();
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

            this.UpdateNodeSize(node);

            return node;
        }

        private INode<T> Remove(INode<T> node, T value)
        {
            if (node == null)
            {
                //Node doesn't exist
                return null;
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
                    parent.Right = maxLeft.Left;
                    maxLeft.Left = node.Left;
                    parent.Size -= 1;
                }

                maxLeft.Right = node.Right;
                maxLeft.Size = node.Size - 1;

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

            this.UpdateNodeSize(node);

            return node;
        }

        private void UpdateNodeSize(INode<T> node)
        {
            node.Size = 1;
            if (node.Left != null)
            {
                node.Size += node.Left.Size;
            }

            if (node.Right != null)
            {
                node.Size += node.Right.Size;
            }
        }
    }
}
