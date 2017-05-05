using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public class StartUp
    {
        public static void Main()
        {
            // TEST NODE AND EDGE Classes
            PathGraph map = CreateMap();

            // TEST 1 DIJKSTRA
            IPathToNode testOneFinalNode = TestOne();
            Console.WriteLine(testOneFinalNode.PrintPathToNode());

            // TEST 2 DIJKSTRA
            IPathToNode testTwoFinalNode = TestTwo();
            Console.WriteLine(testTwoFinalNode.PrintPathToNode());
        }

        private static IPathToNode TestOne()
        {
            INode node_A = new Node("A");
            INode node_B = new Node("B");
            INode node_C = new Node("C");
            INode node_D = new Node("D");
            INode node_E = new Node("E");
            INode node_Z = new Node("Z");

            IEdge edge_AB = new Edge(node_A, node_B, 4);
            IEdge edge_AC = new Edge(node_A, node_C, 2);
            IEdge edge_BC = new Edge(node_B, node_C, 1);
            IEdge edge_BD = new Edge(node_B, node_D, 5);
            IEdge edge_CD = new Edge(node_C, node_D, 8);
            IEdge edge_CE = new Edge(node_C, node_E, 10);
            IEdge edge_DE = new Edge(node_D, node_E, 2);
            IEdge edge_DZ = new Edge(node_D, node_Z, 6);
            IEdge edge_EZ = new Edge(node_E, node_Z, 3);
            ICollection<IEdge> pathEdges = new List<IEdge>()
            {
                edge_AB, edge_AC, edge_BC, edge_BD, edge_CD, edge_CE, edge_DE, edge_DZ, edge_EZ
            };

            PathGraph graph = new PathGraph(pathEdges);

            IPathToNode result = graph.FindShortestPathWithDijkstra((IPathToNode)node_A, (IPathToNode)node_Z);

            return result;
        }

        private static IPathToNode TestTwo()
        {
            INode node_A = new Node("A");
            INode node_B = new Node("B");
            INode node_C = new Node("C");
            INode node_D = new Node("D");
            INode node_E = new Node("E");
            INode node_F = new Node("F");
            INode node_G = new Node("G");
            INode node_H = new Node("H");
            INode node_I = new Node("I");
            INode node_J = new Node("J");
            INode node_K = new Node("K");
            INode node_L = new Node("L");
            INode node_M = new Node("M");
            INode node_N = new Node("N");
            INode node_O = new Node("O");
            INode node_P = new Node("P");
            INode node_Q = new Node("Q");
            INode node_R = new Node("R");

            IEdge edge_AB = new Edge(node_A, node_B, 10);
            IEdge edge_AD = new Edge(node_A, node_D, 5);
            IEdge edge_AE = new Edge(node_A, node_E, 7);
            IEdge edge_BC = new Edge(node_B, node_C, 1);
            IEdge edge_BF = new Edge(node_B, node_F, 4);
            IEdge edge_CF = new Edge(node_C, node_F, 16);
            IEdge edge_DG = new Edge(node_D, node_G, 10);
            IEdge edge_EI = new Edge(node_E, node_I, 4);
            IEdge edge_FH = new Edge(node_F, node_H, 8);
            IEdge edge_GH = new Edge(node_G, node_H, 1);
            IEdge edge_GJ = new Edge(node_G, node_J, 7);
            IEdge edge_HK = new Edge(node_H, node_K, 3);
            IEdge edge_IK = new Edge(node_I, node_K, 2);
            IEdge edge_JK = new Edge(node_J, node_K, 2);
            IEdge edge_JM = new Edge(node_J, node_M, 11);
            IEdge edge_KL = new Edge(node_K, node_L, 5);
            IEdge edge_LO = new Edge(node_L, node_O, 7);
            IEdge edge_MP = new Edge(node_M, node_P, 2);
            IEdge edge_MQ = new Edge(node_M, node_Q, 3);
            IEdge edge_NO = new Edge(node_N, node_O, 3);
            IEdge edge_NQ = new Edge(node_N, node_Q, 8);
            IEdge edge_OR = new Edge(node_O, node_R, 4);
            IEdge edge_PQ = new Edge(node_P, node_Q, 6);
            IEdge edge_QR = new Edge(node_Q, node_R, 10);

            ICollection<IEdge> pathEdges = new List<IEdge>()
            {
                edge_AB, edge_AD, edge_AE, edge_BC, edge_BF, edge_CF, edge_DG,
                edge_EI, edge_FH, edge_GH, edge_HK, edge_IK, edge_JK, edge_JM,
                edge_KL, edge_LO, edge_MP, edge_MQ, edge_NO, edge_NQ, edge_OR,
                edge_PQ, edge_QR
            };

            PathGraph graph = new PathGraph(pathEdges);

            IPathToNode result = graph.FindShortestPathWithDijkstra((IPathToNode)node_A, (IPathToNode)node_Q);

            return result;
        }

        private static PathGraph CreateMap()
        {
            INode node_1 = new Node("1");
            INode node_2 = new Node("2");
            INode node_3 = new Node("3");
            INode node_4 = new Node("4");
            INode node_5 = new Node("5");
            INode node_6 = new Node("6");

            IEdge edge_12 = new Edge(node_1, node_2, 7);
            IEdge edge_13 = new Edge(node_1, node_3, 9);
            IEdge edge_16 = new Edge(node_1, node_6, 14);
            IEdge edge_23 = new Edge(node_2, node_3, 10);
            IEdge edge_24 = new Edge(node_2, node_4, 15);
            IEdge edge_34 = new Edge(node_3, node_4, 11);
            IEdge edge_36 = new Edge(node_3, node_6, 2);
            IEdge edge_45 = new Edge(node_4, node_5, 6);
            IEdge edge_56 = new Edge(node_5, node_6, 9);
            ICollection<IEdge> pathEdges = new List<IEdge>()
            {
                edge_12, edge_13, edge_16, edge_23, edge_24, edge_34, edge_36, edge_45, edge_56
            };

            PathGraph graph = new PathGraph(pathEdges);

            Console.WriteLine("INITIAL");
            Console.Write(graph);

            graph.RemoveEdge(edge_56);
            Console.WriteLine();
            Console.WriteLine("Removed edge 5-6");
            Console.Write(graph);

            graph.RemoveNode(node_1);
            Console.WriteLine();
            Console.WriteLine("Removed node 1");
            Console.Write(graph);

            graph.AddEdge(edge_56);
            Console.WriteLine();
            Console.WriteLine("Added edge 5-6");
            Console.Write(graph);

            graph.AddEdge(edge_16);
            Console.WriteLine();
            Console.WriteLine("Added edge 1-6");
            Console.Write(graph);

            graph.AddEdge(edge_12);
            Console.WriteLine();
            Console.WriteLine("Added edge 1-2");
            Console.Write(graph);

            graph.AddEdge(edge_13);
            Console.WriteLine();
            Console.WriteLine("Added edge 1-3");
            Console.Write(graph);

            return graph;
        }
    }
}