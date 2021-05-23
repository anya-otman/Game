using System.Drawing;
using Logic;
using NUnit.Framework;

namespace TestProject2
{
    [TestFixture]
    public class PhysicsTests
    {
        [Test]
        public void Jump()
        {
            var physics = new Physics(new Point(3, 1), new Size(1, 2));
            
            physics.Jump();
            var positionAndSize = physics.PositionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new Point(3, 0), new Size(1, 2));
            Assert.AreEqual(expectedPositionAndSize.Position, positionAndSize.Position);
            Assert.AreEqual(expectedPositionAndSize.Size,positionAndSize.Size);
        }

        [Test]
        public void SitDown()
        {
            var physics = new Physics(new Point(3, 1), new Size(1, 2));
            
            physics.SitDown();
            var positionAndSize = physics.PositionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new Point(3, 2), new Size(1, 1));
            Assert.AreEqual(expectedPositionAndSize.Position, positionAndSize.Position);
            Assert.AreEqual(expectedPositionAndSize.Size,positionAndSize.Size);
        }
        
        [Test]
        public void SitDown_WhenIsJumping()
        {
            var physics = new Physics(new Point(3, 1), new Size(1, 2));

            physics.Jump();
            physics.SitDown();
            var positionAndSize = physics.PositionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new Point(3, 2), new Size(1, 1));
            Assert.AreEqual(expectedPositionAndSize.Position, positionAndSize.Position);
            Assert.AreEqual(expectedPositionAndSize.Size,positionAndSize.Size);
        }

        [Test]
        public void JumpWhenIsSitting()
        {
            var physics = new Physics(new Point(3, 1), new Size(1, 2));
            
            physics.SitDown();
            physics.Jump();
            var positionAndSize = physics.PositionAndSize;
            
            var expectedPositionAndSize = new PositionAndSize(new Point(3, 2), new Size(1, 2));
            Assert.AreEqual(expectedPositionAndSize.Position, positionAndSize.Position);
        }
    }
}