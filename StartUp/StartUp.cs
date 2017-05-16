using MyBinaryTree;
using PriorityQueue;
using System;

namespace StartUp
{
    public class StartUp
    {
        public static void Main()
        {
            //TestMaxHeap();
            //TestMinHeap();
            TestBinaryTree();
        }

        private static void TestBinaryTree()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //Console.WriteLine(tree.Size);
            //Console.WriteLine(tree.Contains(1));

            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(-1);
            tree.Add(6);
            tree.Add(-136);
            tree.Add(-12);
            tree.Add(8886);
            tree.Add(0);
            //Console.WriteLine(tree.Size);
            //Console.WriteLine(tree.Contains(1));
            //Console.WriteLine(tree.Contains(2));
            //Console.WriteLine(tree.Contains(3));
            //Console.WriteLine(tree.Contains(0));
            //Console.WriteLine(tree.Contains(6));
            //Console.WriteLine(tree.Contains(-136));
            //Console.WriteLine(tree.Contains(-12));
            //Console.WriteLine(tree.Contains(8886));

            tree.Remove(-1);
            tree.Remove(8886);
            Console.WriteLine(tree.Size);
            Console.WriteLine(tree.Contains(1));
            Console.WriteLine(tree.Contains(2));
            Console.WriteLine(tree.Contains(3));
            Console.WriteLine(tree.Contains(-1));
            Console.WriteLine(tree.Contains(6));
            Console.WriteLine(tree.Contains(-136));
            Console.WriteLine(tree.Contains(-12));
            Console.WriteLine(tree.Contains(8886));
            Console.WriteLine(tree.Contains(0));
        }

        private static void TestMaxHeap()
        {
            BaseHeap<double> maxHeap = new MaxHeap<double>();
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
        }

        private static void TestMinHeap()
        {
            BaseHeap<double> minHeap = new MinHeap<double>();
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
