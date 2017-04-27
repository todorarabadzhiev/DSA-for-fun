using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public class StartUp
    {
        public static void Main()
        {
            //PathGraph map = CreateMap();

            Node node_A = new Node(1, 1);
            Node node_B = new Node(2, 2);
            Node node_C = new Node(3, 3);
            Node node_D = new Node(4, 4);
            Node node_E = new Node(5, 5);
            Node node_Z = new Node(6, 6);

            Edge edge_AB = new Edge(node_A, node_B, 4);
            Edge edge_AC = new Edge(node_A, node_C, 2);
            Edge edge_BC = new Edge(node_B, node_C, 1);
            Edge edge_BD = new Edge(node_B, node_D, 5);
            Edge edge_CD = new Edge(node_C, node_D, 8);
            Edge edge_CE = new Edge(node_C, node_E, 10);
            Edge edge_DE = new Edge(node_D, node_E, 2);
            Edge edge_DZ = new Edge(node_D, node_Z, 6);
            Edge edge_EZ = new Edge(node_E, node_Z, 3);
            ICollection<Edge> pathEdges = new List<Edge>()
            {
                edge_AB, edge_AC, edge_BC, edge_BD, edge_CD, edge_CE, edge_DE, edge_DZ, edge_EZ
            };

            PathGraph graph = new PathGraph(pathEdges);

            graph.FindShortestPathWithDijkstra(node_A, node_Z);
        }

        private static PathGraph CreateMap()
        {
            Node node_1 = new Node(1, 1);
            Node node_2 = new Node(2, 2);
            Node node_3 = new Node(3, 3);
            Node node_4 = new Node(4, 4);
            Node node_5 = new Node(5, 5);
            Node node_6 = new Node(6, 6);

            Edge edge_12 = new Edge(node_1, node_2, 7);
            Edge edge_13 = new Edge(node_1, node_3, 9);
            Edge edge_16 = new Edge(node_1, node_6, 14);
            Edge edge_23 = new Edge(node_2, node_3, 10);
            Edge edge_24 = new Edge(node_2, node_4, 15);
            Edge edge_34 = new Edge(node_3, node_4, 11);
            Edge edge_36 = new Edge(node_3, node_6, 2);
            Edge edge_45 = new Edge(node_4, node_5, 6);
            Edge edge_56 = new Edge(node_5, node_6, 9);
            ICollection<Edge> pathEdges = new List<Edge>()
            {
                edge_12, edge_13, edge_16, edge_23, edge_24, edge_34, edge_36, edge_45, edge_56
            };

            PathGraph graph = new PathGraph(pathEdges);


            //// TESTING Node & Edge Classes
            //Console.WriteLine("INITIAL");
            //Console.Write(graph);

            //graph.RemoveEdge(edge_56);
            //Console.WriteLine();
            //Console.WriteLine("Removed edge 5-6");
            //Console.Write(graph);

            //graph.RemoveNode(node_1);
            //Console.WriteLine();
            //Console.WriteLine("Removed node 1");
            //Console.Write(graph);

            //graph.AddEdge(edge_56);
            //Console.WriteLine();
            //Console.WriteLine("Added edge 5-6");
            //Console.Write(graph);

            //graph.AddEdge(edge_16);
            //Console.WriteLine();
            //Console.WriteLine("Added edge 1-6");
            //Console.Write(graph);

            //graph.AddEdge(edge_12);
            //Console.WriteLine();
            //Console.WriteLine("Added edge 1-2");
            //Console.Write(graph);

            //graph.AddEdge(edge_13);
            //Console.WriteLine();
            //Console.WriteLine("Added edge 1-3");
            //Console.Write(graph);

            return graph;
        }
    }
}
