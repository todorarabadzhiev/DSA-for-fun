using System;
using System.Collections.Generic;

namespace MyAvlTree
{
    public interface IAvlNode<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        T Value { get; set; }
        int Size { get; set; }
        int Height { get; set; }
        int BalanceFactor { get; }
        IAvlNode<T> Right { get; set; }
        IAvlNode<T> Left { get; set; }
        IAvlNode<T> Parent { get; set; }
        void Update();
        IAvlNode<T> RotateRight();
        IAvlNode<T> RotateLeft();
    }
}
