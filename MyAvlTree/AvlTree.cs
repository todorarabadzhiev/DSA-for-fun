using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyAvlTree
{
    public class AvlTree<T> : IAvlTree<T>
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

        public IAvlNode<T> Root { get; set; }
        public AvlTree()
        {
            this.Size = 0;
            this.Root = null;
        }

        public bool Contains(T value)
        {
            IAvlNode<T> node = this.Root;
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

        public IAvlNode<T> this[int index]
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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(string.Join("<->", this));

            return result.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IAvlNode<T> GetElementByIndex(IAvlNode<T> node, int index)
        {
            if (node.Left == null)
            {
                if (node.Right == null || index == 0)
                {
                    return node;
                }

                return this.GetElementByIndex(node.Right, index - 1);
            }

            if (node.Left.Size == index)
            {
                return node;
            }

            if (node.Left.Size < index)
            {
                return this.GetElementByIndex(node.Right, index - node.Left.Size - 1);
            }

            return this.GetElementByIndex(node.Left, index);
        }

        private IAvlNode<T> Add(IAvlNode<T> node, T value)
        {
            if (node == null)
            {
                node = new AvlNode<T>(value);
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
                if (node.BalanceFactor < -1)
                {
                    if (node.Left.BalanceFactor > 0)
                    {
                        node.Left = node.Left.RotateLeft();
                    }

                    node = node.RotateRight();
                }

                if (node.Left != null)
                {
                    node.Left.Parent = node;
                }
            }
            else
            {
                node.Right = this.Add(node.Right, value);
                if (node.BalanceFactor > 1)
                {
                    if (node.Right.BalanceFactor < 0)
                    {
                        node.Right = node.Right.RotateRight();
                    }

                    node = node.RotateLeft();
                }

                if (node.Right != null)
                {
                    node.Right.Parent = node;
                }
            }

            node.Update();

            return node;
        }

        private IAvlNode<T> Remove(IAvlNode<T> node, T value)
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
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }

                if (node.Left == null)
                {
                    node.Right.Parent = node.Parent;
                    return node.Right;
                }

                if (node.Right == null)
                {
                    node.Left.Parent = node.Parent;
                    return node.Left;
                }

                IAvlNode<T> nextInOrder;
                if (node.BalanceFactor > 0)
                {
                    node.Right = RemoveLeftmost(node.Right, out nextInOrder);
                }
                else
                {
                    node.Left = RemoveRightmost(node.Left, out nextInOrder);
                }

                nextInOrder.Left = node.Left;
                nextInOrder.Right = node.Right;
                if (nextInOrder.Left != null)
                {
                    nextInOrder.Left.Parent = nextInOrder;
                }

                if (nextInOrder.Right != null)
                {
                    nextInOrder.Right.Parent = nextInOrder;
                }

                nextInOrder.Parent = node.Parent;

                nextInOrder.Update();

                return nextInOrder;
            }

            if (cmp < 0)
            {
                node.Left = this.Remove(node.Left, value);
                if (node.BalanceFactor > 1)
                {
                    if (node.Right.BalanceFactor < 0)
                    {
                        node.Right = node.Right.RotateRight();
                    }
                    node = node.RotateLeft();
                }
            }
            else
            {
                node.Right = this.Remove(node.Right, value);
                if (node.BalanceFactor < -1)
                {
                    if (node.Left.BalanceFactor > 0)
                    {
                        node.Left = node.Left.RotateLeft();
                    }
                    node = node.RotateRight();
                }
            }

            node.Update();

            return node;
        }

        private IAvlNode<T> RemoveRightmost(IAvlNode<T> root, out IAvlNode<T> nextInOrder)
        {
            if (root.Right == null)
            {
                nextInOrder = root;
                if (root.Left != null)
                {
                    root.Left.Parent = root.Parent;
                }

                return root.Left;
            }

            root.Right = this.RemoveRightmost(root.Right, out nextInOrder);
            if (root.BalanceFactor > 1)
            {
                if (root.Left.BalanceFactor < 0)
                {
                    root.Left = root.Left.RotateLeft();
                }
                root = root.RotateRight();
            }

            root.Update();
            return root;
        }

        private IAvlNode<T> RemoveLeftmost(IAvlNode<T> root, out IAvlNode<T> nextInOrder)
        {
            if (root.Left == null)
            {
                nextInOrder = root;
                if (root.Right != null)
                {
                    root.Right.Parent = root.Parent;
                }

                return root.Right;
            }

            root.Left = this.RemoveLeftmost(root.Left, out nextInOrder);
            if (root.BalanceFactor < -1)
            {
                if (root.Right.BalanceFactor > 0)
                {
                    root.Right = root.Right.RotateRight();
                }
                root = root.RotateLeft();
            }

            root.Update();
            return root;
        }
    }
}
