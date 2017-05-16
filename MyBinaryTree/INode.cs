using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBinaryTree
{
    public interface INode<T>
        where T : IComparable<T>
    {
        T Value { get; set; }
        INode<T> Right { get; set; }
        INode<T> Left { get; set; }
    }
}
