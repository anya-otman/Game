using System.Drawing;
using NUnit.Framework;
using Logic.Classes;

namespace TestProject2
{
    [TestFixture]
    public class PhysicsTests
    {
        [Test]
        public void Jump_True()
        {
            var physics = new Physics(new PointF(3, 1), new Size(1, 2));
            
            physics.Jump();
            var positionAndSize = physics.positionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new PointF(3, 0), new Size(1, 2));
            Assert.AreEqual(positionAndSize, expectedPositionAndSize);
        }
    }
}