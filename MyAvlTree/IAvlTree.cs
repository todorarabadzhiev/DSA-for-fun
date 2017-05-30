using System;
using System.Collections.Generic;

namespace MyAvlTree
{
    public interface IAvlTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        int Size { get; }
        bool Contains(T value);
        void Add(T value);
        void Remove(T value);
        IAvlNode<T> this[int index] { get; }
    }
}
