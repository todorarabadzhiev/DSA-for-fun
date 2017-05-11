using System;

namespace PriorityQueue
{
    public class MinHeap<T> : BaseHeap<T> where T : IComparable
    {
        protected override bool FirstValueHasPriority(T firstValue, T secondValue)
        {
            bool result = firstValue.CompareTo(secondValue) < 0;

            return result;
        }
    }
}