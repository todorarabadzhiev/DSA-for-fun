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
            IAvlNode<T> addedNode;
            this.Root = this.Add(this.Root, value, out addedNode);
            this.BalanceAfterAddition(addedNode);
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

        private IAvlNode<T> Add(IAvlNode<T> node, T value, out IAvlNode<T> addedNode)
        {
            addedNode = null;
            if (node == null)
            {
                node = new AvlNode<T>(value);
                this.Size++;
                addedNode = node;
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
                node.Left = this.Add(node.Left, value, out addedNode);
                node.Left.Parent = node;
            }
            else
            {
                node.Right = this.Add(node.Right, value, out addedNode);
                node.Right.Parent = node;
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

        private void BalanceAfterAddition(IAvlNode<T> addedNode)
        {
            IAvlNode<T> node = addedNode;
            IAvlNode<T> parent = node?.Parent ?? null;
            IAvlNode<T> grandParent;
            IAvlNode<T> newRoot;
            while (parent != null)
            {
                if (parent.Left == node)
                {
                    if (parent.BalanceFactor < 0)
                    {
                        grandParent = parent.Parent;
                        if (node.BalanceFactor > 0)
                        {
                            newRoot = this.RotateLeftRight(parent, node);
                        }
                        else
                        {
                            newRoot = this.RotateRight(parent, node);
                        }
                    }
                    else
                    {
                        if (parent.BalanceFactor > 0)
                        {
                            parent.Update();
                            break;
                        }

                        parent.Update();
                        node = node.Parent;
                        parent = node.Parent;
                        continue;
                    }
                }
                else
                {
                    if (parent.BalanceFactor > 0)
                    {
                        grandParent = parent.Parent;
                        if (node.BalanceFactor < 0)
                        {
                            newRoot = this.RotateRightLeft(parent, node);
                        }
                        else
                        {
                            newRoot = this.RotateLeft(parent, node);
                        }
                    }
                    else
                    {
                        if (parent.BalanceFactor < 0)
                        {
                            parent.Update();
                            break;
                        }

                        parent.Update();
                        node = node.Parent;
                        parent = node.Parent;
                        continue;
                    }
                }

                newRoot.Parent = grandParent;
                if (grandParent != null)
                {
                    if (parent == grandParent.Left)
                    {
                        grandParent.Left = newRoot;
                    }
                    else
                    {
                        grandParent.Right = newRoot;
                    }

                    break;
                }
                else
                {
                    this.Root = newRoot;
                    break;
                }
            }
        }

        private IAvlNode<T> RotateLeft(IAvlNode<T> root, IAvlNode<T> rightChild)
        {
            if (root == null)
            {
                throw new ArgumentNullException("AVL Root");
            }

            if (rightChild == null)
            {
                throw new ArgumentNullException("AVL Root RightChild");
            }

            IAvlNode<T> midNode = rightChild.Left;
            root.Right = midNode;
            if (midNode != null)
            {
                midNode.Parent = root;
            }
            rightChild.Parent = root.Parent;
            root.Parent = rightChild;
            rightChild.Left = root;

            root.Update();
            rightChild.Update();

            return rightChild;
        }

        private IAvlNode<T> RotateRight(IAvlNode<T> root, IAvlNode<T> leftChild)
        {
            if (root == null)
            {
                throw new ArgumentNullException("AVL Root");
            }

            if (leftChild == null)
            {
                throw new ArgumentNullException("AVL Root LefttChild");
            }

            IAvlNode<T> midNode = leftChild.Right;
            root.Left = midNode;
            if (midNode != null)
            {
                midNode.Parent = root;
            }
            leftChild.Parent = root.Parent;
            root.Parent = leftChild;
            leftChild.Right = root;

            //if (leftChild.BalanceFactor == 0)
            //{
            //    root.BalanceFactor = -1;
            //    leftChild.BalanceFactor = 1;
            //}
            //else
            //{
            //    root.BalanceFactor = 0;
            //    leftChild.BalanceFactor = 0;
            //}
            root.Update();
            leftChild.Update();

            return leftChild;
        }

        private IAvlNode<T> RotateRightLeft(IAvlNode<T> root, IAvlNode<T> rightChild)
        {
            IAvlNode<T> newRoot = rightChild.Left;
            IAvlNode<T> midRight = newRoot.Right;
            rightChild.Left = midRight;
            if (midRight != null)
            {
                midRight.Parent = rightChild;
            }

            newRoot.Right = rightChild;
            rightChild.Parent = newRoot;

            IAvlNode<T> midLeft = newRoot.Left;
            root.Right = midLeft;
            if (midLeft != null)
            {
                midLeft.Parent = root;
            }

            newRoot.Left = root;
            root.Parent = newRoot;

            //if (newRoot.BalanceFactor > 0)
            //{
            //    root.BalanceFactor = -1;
            //    rightChild.BalanceFactor = 0;
            //}
            //else if (newRoot.BalanceFactor == 0)
            //{
            //    root.BalanceFactor = 0;
            //    rightChild.BalanceFactor = 0;
            //}
            //else
            //{
            //    root.BalanceFactor = 0;
            //    rightChild.BalanceFactor = 1;
            //}

            //newRoot.BalanceFactor = 0;
            root.Update();
            rightChild.Update();
            newRoot.Update();

            return newRoot;
        }

        private IAvlNode<T> RotateLeftRight(IAvlNode<T> root, IAvlNode<T> leftChild)
        {
            IAvlNode<T> newRoot = leftChild.Right;
            IAvlNode<T> midLeft = newRoot.Left;
            leftChild.Right = midLeft;
            if (midLeft != null)
            {
                midLeft.Parent = leftChild;
            }

            newRoot.Left = leftChild;
            leftChild.Parent = newRoot;

            IAvlNode<T> midRight = newRoot.Right;
            root.Left = midRight;
            if (midRight != null)
            {
                midRight.Parent = root;
            }

            newRoot.Right = root;
            root.Parent = newRoot;

            //if (newRoot.BalanceFactor > 0)
            //{
            //    root.BalanceFactor = 0;
            //    leftChild.BalanceFactor = -1;
            //}
            //else if (newRoot.BalanceFactor == 0)
            //{
            //    root.BalanceFactor = 0;
            //    leftChild.BalanceFactor = 0;
            //}
            //else
            //{
            //    root.BalanceFactor = -1;
            //    leftChild.BalanceFactor = 0;
            //}

            //newRoot.BalanceFactor = 0;
            root.Update();
            leftChild.Update();
            newRoot.Update();

            return newRoot;
        }
    }
}
