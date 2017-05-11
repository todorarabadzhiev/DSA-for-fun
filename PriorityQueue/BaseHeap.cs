using System;
using System.Collections.Generic;
using System.Linq;

namespace PriorityQueue
{
    public abstract class BaseHeap<T> where T : IComparable
    {
        protected const string Separator = " ";
        protected readonly IList<T> TheHeap;
        public BaseHeap()
        {
            this.TheHeap = new List<T>();

            // IGNORING 0th VALUE
            this.TheHeap.Add(default(T));
        }

        public T GetTop()
        {
            if (this.TheHeap.Count < 2)
            {
                throw new ArgumentOutOfRangeException("The HEAP is empty!");
            }

            return this.TheHeap[1];
        }

        public void AddValue(T value)
        {
            this.TheHeap.Add(value);
            int index = (this.TheHeap.Count() - 1);
            int parentIndex = index / 2;
            while (parentIndex > 0 && this.FirstValueHasPriority(value, this.TheHeap[parentIndex]))
            {
                this.SwapValues(index, parentIndex);
                index = parentIndex;
                parentIndex /= 2;
            }
        }

        public void RemoveTop()
        {
            this.SwapValues(1, this.TheHeap.Count - 1);
            this.TheHeap.RemoveAt(this.TheHeap.Count - 1);

            this.Sink(1);
        }

        public override string ToString()
        {
            string value = string.Join(Separator, this.TheHeap);

            // IGNORE 0th VALUE
            int index = value.IndexOf(Separator);
            value = value.Substring(index + Separator.Length);

            return value;
        }

        protected void Sink(int startIndex)
        {
            if (startIndex > this.TheHeap.Count - 1 || startIndex < 1)
            {
                return;
            }

            int index = startIndex;
            int leftChildIndex = index * 2;
            int rightChildIndex = index * 2 + 1;
            if (rightChildIndex < this.TheHeap.Count)
            {
                int maxChildIndex = this.FirstValueHasPriority(this.TheHeap[leftChildIndex], this.TheHeap[rightChildIndex]) ?
                    leftChildIndex : rightChildIndex;

                while (this.FirstValueHasPriority(this.TheHeap[maxChildIndex], this.TheHeap[index]))
                {
                    this.SwapValues(index, maxChildIndex);
                    index = maxChildIndex;
                    leftChildIndex = index * 2;
                    rightChildIndex = index * 2 + 1;
                    if (rightChildIndex < this.TheHeap.Count)
                    {
                        maxChildIndex = this.FirstValueHasPriority(this.TheHeap[leftChildIndex], this.TheHeap[rightChildIndex]) ?
                            leftChildIndex : rightChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (leftChildIndex < this.TheHeap.Count &&
                this.FirstValueHasPriority(this.TheHeap[leftChildIndex], this.TheHeap[index]))
            {
                this.SwapValues(index, leftChildIndex);
            }
        }

        protected void SwapValues(int index, int parentIndex)
        {
            T temp = this.TheHeap[index];
            this.TheHeap[index] = this.TheHeap[parentIndex];
            this.TheHeap[parentIndex] = temp;
        }

        protected abstract bool FirstValueHasPriority(T firstValue, T secondValue);
    }
}
