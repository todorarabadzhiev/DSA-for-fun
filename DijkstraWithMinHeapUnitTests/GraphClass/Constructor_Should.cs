using DijkstraWithMinHeap;
using DijkstraWithMinHeapUnitTests.GraphClass.Mocked;
using NUnit.Framework;
using PriorityQueue;
using System;
using System.Collections.Generic;
using Telerik.JustMock;

namespace DijkstraWithMinHeapUnitTests.GraphClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithMessageGraph_WhenTheProvidedGraphArgumentIsNull()
        {
            // Arrange
            var pq = Mock.Create<BaseHeap<INode>>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new MockedGraph(null, pq));
            StringAssert.Contains("Graph", ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessagePriorityQueue_WhenTheProvidedPqArgumentIsNull()
        {
            // Arrange
            var graphList = Mock.Create<IEnumerable<INode>>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new MockedGraph(graphList, null));
            StringAssert.Contains("Priority Queue", ex.Message);
        }

        [Test]
        public void CreateNewInstanceOfGraphClass_WhenTheProvidedArgumentsAreValid()
        {
            // Arrange
            var graphList = Mock.Create<IEnumerable<INode>>();
            var pq = Mock.Create<BaseHeap<INode>>();

            // Act
            var graph = new Graph(graphList, pq);

            // Assert
            Assert.AreEqual(typeof(Graph), graph.GetType());
        }
    }
}
