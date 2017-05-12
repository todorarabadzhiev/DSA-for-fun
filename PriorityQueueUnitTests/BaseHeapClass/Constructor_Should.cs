using NUnit.Framework;
using PriorityQueue;
using PriorityQueueUnitTests.BaseHeapClass.Mocked;
using System;

namespace PriorityQueueUnitTests.BaseHeapClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateTheHeapListWithASingleElementWithValueDefaultOfTypeT()
        {
            // Arrange, Act
            var heap = new MockedMaxBaseHeap<double>();

            // Assert
            Assert.AreEqual(1, heap.TheHeap.Count);
            Assert.AreEqual(default(double), heap.TheHeap[0]);
        }
    }
}
