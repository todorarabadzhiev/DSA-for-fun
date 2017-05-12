using Dijkstra;
using NUnit.Framework;
using System;

namespace DijkstraUnitTests.NodeClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithMessageId_WhenProvidedIdIsNull()
        {
            // Arrange, Act, Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new Node(null));
            StringAssert.Contains("Id", ex.Message);
        }
    }
}