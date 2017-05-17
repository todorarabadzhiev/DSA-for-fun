using System;
using System.Collections.Generic;

namespace MyBinaryTree
{
    public interface IBinaryTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        int Size { get; }
        bool Contains(T value);
        void Add(T value);
        void Remove(T value);
        T this[int index] { get; }
    }
}
