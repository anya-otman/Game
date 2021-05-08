using System.Drawing;
using NUnit.Framework;
using Logic.Classes;

namespace TestProject2
{
    [TestFixture]
    public class PhysicsTests
    {
        [Test]
        public void Jump()
        {
            var physics = new Physics(new PointF(3, 1), new Size(1, 2));
            
            physics.Jump();
            var positionAndSize = physics.positionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new PointF(3, 0), new Size(1, 2));
            Assert.AreEqual(expectedPositionAndSize.position, positionAndSize.position);
            Assert.AreEqual(expectedPositionAndSize.size,positionAndSize.size);
        }

        [Test]
        public void SitDown()
        {
            var physics = new Physics(new PointF(3, 1), new Size(1, 2));
            
            physics.SitDown();
            var positionAndSize = physics.positionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new PointF(3, 2), new Size(1, 1));
            Assert.AreEqual(expectedPositionAndSize.position, positionAndSize.position);
            Assert.AreEqual(expectedPositionAndSize.size,positionAndSize.size);
        }
        
        [Test]
        public void SitDown_WhenIsJumping()
        {
            var physics = new Physics(new PointF(3, 1), new Size(1, 2));

            physics.Jump();
            physics.SitDown();
            var positionAndSize = physics.positionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new PointF(3, 0), new Size(1, 2));
            Assert.AreEqual(expectedPositionAndSize.position, positionAndSize.position);
            Assert.AreEqual(expectedPositionAndSize.size,positionAndSize.size);
        }
    }
}