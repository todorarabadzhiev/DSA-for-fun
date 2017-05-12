using PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueueUnitTests.BaseHeapClass.Mocked
{
    public class MockedMaxBaseHeap<T> : MaxHeap<T> where T : IComparable
    {
        public IList<T> TheHeap
        {
            get
            {
                return this.theHeap;
            }
        }
    }
}
