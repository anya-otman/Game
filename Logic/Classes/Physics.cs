using System.Drawing;

namespace Logic.Classes
{
    public class Physics
    {
        public PositionAndSize PositionAndSize;
        private bool isJumping;
        private bool isCrouching;
        private int tick;
        private int jumpTick;
        private int sitTick;

        public bool IsCrouching => isCrouching;

        public Physics(PointF position, Size size)
        {
            PositionAndSize = new PositionAndSize(position, size);
            isJumping = false;
            isCrouching = false;
            tick = 0;
            jumpTick = -1;
            sitTick = -1;
        }

        public void ApplyPhysics()
        {
            FallDown();
            if (!isJumping)
                StandUp();
            tick += 1;
        }
        
        public void Jump()
        {
            isJumping = true;
            PositionAndSize = new PositionAndSize(new PointF(3, 0), new Size(1, 2));
            jumpTick = tick;
        }

        public void SitDown()
        {
            if (isJumping) return;
            isCrouching = true;
            PositionAndSize = new PositionAndSize(new PointF(3, 2), new Size(1, 1));
            sitTick = tick;
        }

        private void FallDown()
        {
            if (jumpTick == tick || isJumping == false)
                return;
            isJumping = false;
            PositionAndSize = new PositionAndSize(new PointF(3, 1), new Size(1, 2));
        }

        private void StandUp()
        {
            if (sitTick == tick || isCrouching == false)
                return;
            isCrouching = false;
            PositionAndSize = new PositionAndSize(new PointF(3, 1), new Size(1, 2));
        }
    }
}