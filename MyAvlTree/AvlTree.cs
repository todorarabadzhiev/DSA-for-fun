using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.BalanceUpFrom(addedNode);
        }

        private void BalanceUpFrom(IAvlNode<T> addedNode)
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
                            parent.BalanceFactor = 0;
                            break;
                        }

                        parent.BalanceFactor = -1;
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
                            parent.BalanceFactor = 0;
                            break;
                        }

                        parent.BalanceFactor = 1;
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

            this.UpdateNodeSize(node);

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
                if (node.Left == null)
                {
                    if (node == this.Root)
                    {
                        node.Right.Parent = null;
                    }

                    return node.Right;
                }

                if (node.Right == null)
                {
                    if (node == this.Root)
                    {
                        node.Left.Parent = null;
                    }

                    return node.Left;
                }

                // node has Left && Right children
                IAvlNode<T> maxLeft = node.Left;
                IAvlNode<T> parent = null;
                while (maxLeft.Right != null)
                {
                    parent = maxLeft;
                    maxLeft = maxLeft.Right;
                }

                if (parent != null)
                {
                    parent.Right = maxLeft.Left;
                    maxLeft.Left = node.Left;
                    maxLeft.Left.Parent = maxLeft;
                    parent.Size -= 1;
                }

                maxLeft.Right = node.Right;
                maxLeft.Right.Parent = maxLeft;
                maxLeft.Size = node.Size - 1;
                if (node == this.Root)
                {
                    maxLeft.Parent = null;
                }

                return maxLeft;
            }

            if (cmp < 0)
            {
                node.Left = this.Remove(node.Left, value);
                if (node.Left != null)
                {
                    node.Left.Parent = node;
                }
            }
            else
            {
                node.Right = this.Remove(node.Right, value);
                if (node.Right != null)
                {
                    node.Right.Parent = node;
                }
            }

            this.UpdateNodeSize(node);

            return node;
        }

        private void UpdateNodeSize(IAvlNode<T> node)
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

            rightChild.Parent = root.Parent;
            root.Parent = rightChild;
            root.Right = rightChild.Left;
            rightChild.Left = root;
            if (rightChild.Left != null)
            {
                rightChild.Left.Parent = root;
            }

            if (rightChild.BalanceFactor == 0)
            {
                root.BalanceFactor = 1;
                rightChild.BalanceFactor = -1;
            }
            else
            {
                root.BalanceFactor = 0;
                rightChild.BalanceFactor = 0;
            }

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

            leftChild.Parent = root.Parent;
            root.Parent = leftChild;
            root.Left = leftChild.Right;
            leftChild.Right = root;
            if (leftChild.Right != null)
            {
                leftChild.Right.Parent = root;
            }

            if (leftChild.BalanceFactor == 0)
            {
                root.BalanceFactor = -1;
                leftChild.BalanceFactor = 1;
            }
            else
            {
                root.BalanceFactor = 0;
                leftChild.BalanceFactor = 0;
            }

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

            if (newRoot.BalanceFactor > 0)
            {
                root.BalanceFactor = -1;
                rightChild.BalanceFactor = 0;
            }
            else if (newRoot.BalanceFactor == 0)
            {
                root.BalanceFactor = 0;
                rightChild.BalanceFactor = 0;
            }
            else
            {
                root.BalanceFactor = 0;
                rightChild.BalanceFactor = 1;
            }

            newRoot.BalanceFactor = 0;

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

            if (newRoot.BalanceFactor > 0)
            {
                root.BalanceFactor = 0;
                leftChild.BalanceFactor = -1;
            }
            else if (newRoot.BalanceFactor == 0)
            {
                root.BalanceFactor = 0;
                leftChild.BalanceFactor = 0;
            }
            else
            {
                root.BalanceFactor = -1;
                leftChild.BalanceFactor = 0;
            }

            newRoot.BalanceFactor = 0;

            return newRoot;
        }
    }
}
