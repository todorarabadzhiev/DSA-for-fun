// http://codeforces.com/gym/100513/problem/C

using System;
using System.Collections.Generic;
using System.Runtime;

namespace ComponentTree
{
    class Program
    {
        static void Main()
        {
            GCSettings.LatencyMode = GCLatencyMode.LowLatency;

            int n = int.Parse(Console.ReadLine());

            var parents = new int[n + 1];

            var compressedKey = new Dictionary<string, int>();

            var properties = new Node[n + 1];
            properties[0] = new Node();

            for (int i = 1; i <= n; ++i)
            {
                var strs = Console.ReadLine().Split(' ');
                int p = int.Parse(strs[0]);
                parents[i] = p;

                int k = int.Parse(strs[1]);
                //properties[i] = new Dictionary<int, string>(properties[p]);
                properties[i] = properties[p];

                for (int j = 0; j < k; ++j)
                {
                    var keyValue = Console.ReadLine().Split('=');
                    int keyIndex;
                    if (!compressedKey.TryGetValue(keyValue[0], out keyIndex))
                    {
                        keyIndex = compressedKey.Count;
                        compressedKey.Add(keyValue[0], keyIndex);
                    }

                    // Save new root
                    properties[i] = properties[i].SetAtIndex(keyIndex, keyValue[1]);
                }
            }

            int q = int.Parse(Console.ReadLine());
            for (int i = 0; i < q; ++i)
            {
                var strs = Console.ReadLine().Split(' ');
                int component = int.Parse(strs[0]);

                int keyIndex;

                if (!compressedKey.TryGetValue(strs[1], out keyIndex))
                {
                    Console.WriteLine("N/A");
                }
                else
                {
                    string value;
                    properties[component].TryGetAtIndex(keyIndex, out value);
                    Console.WriteLine(value ?? "N/A");
                }
            }
        }

        //static string Query(int[] parents, Dictionary<int, string>[] properties,
        //    int keyIndex, int component)
        //{
        //    if (component == 0)
        //    {
        //        return "N/A";
        //    }

        //    string value;
        //    if (properties[component].TryGetValue(keyIndex, out value))
        //    {
        //        return value;
        //    }

        //    value = Query(parents, properties, keyIndex, parents[component]);
        //    properties[component][keyIndex] = value;
        //    return value;
        //}
    }

    class Node
    {
        private const int HEIGHT = 19;

        private string value;

        private Node left;
        private Node right;

        public Node()
        {
            left = null;
            right = null;

            value = null;
        }
        private static Node Set(Node node, int index, string newValue, int bit)
        {
            var newNode = new Node();

            if (bit == -1)
            {
                newNode.value = newValue;
            }
            else if (((index >> bit) & 1) == 0)
            {
                newNode.left = Set(node.left ?? newNode, index, newValue, bit - 1);
                newNode.right = node.right;
            }
            else
            {
                newNode.right = Set(node.right ?? newNode, index, newValue, bit - 1);
                newNode.left = node.left;
            }

            return newNode;
        }

        public Node SetAtIndex(int index, string newValue)
        {
            return Set(this, index, newValue, HEIGHT - 1);
        }

        public bool TryGetAtIndex(int index, out string value)
        {
            Node node = this;
            for (int i = HEIGHT - 1; i >= 0; --i)
            {
                if (((index >> i) & 1) == 0)
                {
                    node = node.left;
                }
                else
                {
                    node = node.right;
                }

                if (node == null)
                {
                    value = null;
                    return false;
                }
            }

            value = node.value;
            return true;
        }
    }
}
