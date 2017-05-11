using System;

namespace PriorityQueue
{
    public class StartUp
    {
        public static void Main()
        {
            MaxHeap<double> maxHeap = new MaxHeap<double>();
            maxHeap.AddValue(1);
            maxHeap.AddValue(4);
            maxHeap.AddValue(14);
            maxHeap.AddValue(0);
            maxHeap.AddValue(24);
            maxHeap.AddValue(2);
            maxHeap.AddValue(200);
            maxHeap.AddValue(20);
            maxHeap.AddValue(40);
            maxHeap.AddValue(25);
            maxHeap.AddValue(2);
            Console.WriteLine(maxHeap);

            MinHeap<double> minHeap = new MinHeap<double>();
            minHeap.AddValue(1);
            minHeap.AddValue(4);
            minHeap.AddValue(14);
            minHeap.AddValue(0);
            minHeap.AddValue(24);
            minHeap.AddValue(2);
            minHeap.AddValue(200);
            minHeap.AddValue(20);
            minHeap.AddValue(40);
            minHeap.AddValue(25);
            minHeap.AddValue(2);
            Console.WriteLine(minHeap);
        }
    }
}
