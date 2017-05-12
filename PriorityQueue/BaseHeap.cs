using System;
using System.Collections.Generic;
using System.Linq;

namespace PriorityQueue
{
    public abstract class BaseHeap<T> where T : IComparable
    {
        protected const string Separator = " ";
        protected readonly IList<T> theHeap;

        public int Count
        {
            get
            {
                return this.theHeap.Count;
            }
        }

        public BaseHeap()
        {
            this.theHeap = new List<T>();

            // IGNORING 0th VALUE
            this.theHeap.Add(default(T));
        }

        public T GetTop()
        {
            if (this.theHeap.Count < 2)
            {
                throw new ArgumentOutOfRangeException("The HEAP is empty!");
            }

            return this.theHeap[1];
        }

        public void AddValue(T value)
        {
            this.theHeap.Add(value);
            int index = (this.theHeap.Count() - 1);
            int parentIndex = index / 2;
            while (parentIndex > 0 && this.FirstValueHasPriority(value, this.theHeap[parentIndex]))
            {
                this.SwapValues(index, parentIndex);
                index = parentIndex;
                parentIndex /= 2;
            }
        }

        public void RemoveTop()
        {
            this.SwapValues(1, this.theHeap.Count - 1);
            this.theHeap.RemoveAt(this.theHeap.Count - 1);

            this.Sink(1);
        }

        public override string ToString()
        {
            string value = string.Join(Separator, this.theHeap);

            // IGNORE 0th VALUE
            int index = value.IndexOf(Separator);
            value = value.Substring(index + Separator.Length);

            return value;
        }

        protected void Sink(int startIndex)
        {
            if (startIndex > this.theHeap.Count - 1 || startIndex < 1)
            {
                return;
            }

            int index = startIndex;
            int leftChildIndex = index * 2;
            int rightChildIndex = index * 2 + 1;
            if (rightChildIndex < this.theHeap.Count)
            {
                int maxChildIndex = this.FirstValueHasPriority(this.theHeap[leftChildIndex], this.theHeap[rightChildIndex]) ?
                    leftChildIndex : rightChildIndex;

                while (this.FirstValueHasPriority(this.theHeap[maxChildIndex], this.theHeap[index]))
                {
                    this.SwapValues(index, maxChildIndex);
                    index = maxChildIndex;
                    leftChildIndex = index * 2;
                    rightChildIndex = index * 2 + 1;
                    if (rightChildIndex < this.theHeap.Count)
                    {
                        maxChildIndex = this.FirstValueHasPriority(this.theHeap[leftChildIndex], this.theHeap[rightChildIndex]) ?
                            leftChildIndex : rightChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (leftChildIndex < this.theHeap.Count &&
                this.FirstValueHasPriority(this.theHeap[leftChildIndex], this.theHeap[index]))
            {
                this.SwapValues(index, leftChildIndex);
            }
        }

        protected void SwapValues(int index, int parentIndex)
        {
            T temp = this.theHeap[index];
            this.theHeap[index] = this.theHeap[parentIndex];
            this.theHeap[parentIndex] = temp;
        }

        protected abstract bool FirstValueHasPriority(T firstValue, T secondValue);
    }
}
