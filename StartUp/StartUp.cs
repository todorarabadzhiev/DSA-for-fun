using MyAvlTree;
using MyBinaryTree;
using PriorityQueue;
using System;

namespace StartUp
{
    public class StartUp
    {
        private static double[] heapVaues = new double[] { 1, 4, 14, 0, 24, 2, 200, 20, 40, 25, 2 };
        private static int[] binaryTreeValues = new int[] { 1, 4, 14, 0, 24, 2, 200, 20, 40, 25, 2, 100, 28, -10 };
        //private static int[] binaryTreeValues = new int[] { 1, 3, 2, -1, 6, -136, -12, 8886, 0 };

        public static void Main()
        {
            //TestMaxHeap();
            //TestMinHeap();
            //TestBinaryTree();
            TestAvlTree();
        }

        private static void TestAvlTree()
        {
            IAvlTree<int> tree = new AvlTree<int>();
            foreach (var value in binaryTreeValues)
            {
                tree.Add(value);
            }

            Console.WriteLine(tree);
            for (int i = 0; i < tree.Size; i++)
            {
                Console.WriteLine(tree[i]);
            }

            //tree.Remove(binaryTreeValues[3]);
            //tree.Remove(binaryTreeValues[0]);
            //tree.Remove(binaryTreeValues[1]);

            //Console.WriteLine(tree);
            //for (int i = 0; i < tree.Size; i++)
            //{
            //    Console.WriteLine(tree[i]);
            //}
        }

        private static void TestBinaryTree()
        {
            IBinaryTree<int> tree = new BinaryTree<int>();
            foreach (var value in binaryTreeValues)
            {
                tree.Add(value);
            }

            Console.WriteLine(tree);
            for (int i = 0; i < tree.Size; i++)
            {
                Console.WriteLine(tree[i]);
            }

            tree.Remove(binaryTreeValues[3]);
            tree.Remove(binaryTreeValues[0]);
            tree.Remove(binaryTreeValues[1]);

            Console.WriteLine(tree);
            for (int i = 0; i < tree.Size; i++)
            {
                Console.WriteLine(tree[i]);
            }
        }

        private static void TestMaxHeap()
        {
            BaseHeap<double> maxHeap = new MaxHeap<double>();
            foreach (var value in heapVaues)
            {
                maxHeap.AddValue(value);
            }

            Console.WriteLine(maxHeap);
        }

        private static void TestMinHeap()
        {
            BaseHeap<double> minHeap = new MinHeap<double>();
            foreach (var value in heapVaues)
            {
                minHeap.AddValue(value);
            }

            Console.WriteLine(minHeap);
        }
    }
}
