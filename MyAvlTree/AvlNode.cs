using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyAvlTree
{
    public class AvlNode<T> : IAvlNode<T>
        where T : IComparable<T>
    {
        private int size;
        private int height;
        public int Height
        {
            get
            {
                return this == null ? 0 : this.height;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Height");
                }

                this.height = value;
            }
        }
        public int Size
        {
            get
            {
                return this == null ? 0 : this.size;
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
                return this.GetBalance();
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
            this.Height = 1;
            this.Right = null;
            this.Left = null;
            this.Parent = null;
        }
        public void Update()
        {
            int leftSize = this.Left?.Size ?? 0;
            int rightSize = this.Right?.Size ?? 0;
            this.Size = leftSize + rightSize + 1;

            int leftHeight = this.Left?.Height ?? 0;
            int rightHeight = this.Right?.Height ?? 0;
            this.Height = Math.Max(leftHeight, rightHeight) + 1;
        }
        public IAvlNode<T> RotateRight()
        {
            IAvlNode<T> newRoot = this.Left;
            IAvlNode<T> leftRight = newRoot.Right;
            if (leftRight != null)
            {
                leftRight.Parent = this;
            }

            newRoot.Right = this;
            newRoot.Parent = this.Parent;
            this.Left = leftRight;
            this.Parent = newRoot;

            this.Update();
            newRoot.Update();

            return newRoot;
        }
        public IAvlNode<T> RotateLeft()
        {
            IAvlNode<T> newRoot = this.Right;
            IAvlNode<T> rightLeft = newRoot.Left;
            if (rightLeft != null)
            {
                rightLeft.Parent = this;
            }

            newRoot.Left = this;
            newRoot.Parent = this.Parent;
            this.Right = rightLeft;
            this.Parent = newRoot;

            this.Update();
            newRoot.Update();

            return newRoot;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.Value);
            result.Append($"_blns:{this.BalanceFactor}");
            result.Append($"_size:{this.Size}");
            result.Append($"_height:{this.Height}_");
            if (this.Left != null)
            {
                result.Append($"[L:{this.Left.Value}]");
            }
            else
            {
                result.Append("[L:x]");
            }

            if (this.Right != null)
            {
                result.Append($"[R:{this.Right.Value}]");
            }
            else
            {
                result.Append("[R:x]");
            }

            if (this.Parent != null)
            {
                result.Append($"[P:{this.Parent.Value}]");
            }
            else
            {
                result.Append("[P:*]");
            }

            return result.ToString(); 
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

        private int GetBalance()
        {
            int leftHeight = this.Left?.Height ?? 0;
            int rightHeight = this.Right?.Height ?? 0;

            int result = this == null ? 0 : rightHeight - leftHeight;

            return result;
        }
    }
}
