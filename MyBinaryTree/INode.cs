using System;
using System.Collections.Generic;

namespace MyBinaryTree
{
    public interface INode<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        T Value { get; set; }
        int Size { get; set; }
        INode<T> Right { get; set; }
        INode<T> Left { get; set; }
    }
}
