using System.Drawing;

namespace Logic
{
    public class Physics
    {
        public PositionAndSize PositionAndSize;
        private bool isJumping;
        private bool isCrouching;
        private int tick;
        private int ticksInAir;
        private int sitTick;

        public bool IsCrouching => isCrouching;
        public bool IsJumping => isJumping;

        public Physics(Point position, Size size)
        {
            PositionAndSize = new PositionAndSize(position, size);
            isJumping = false;
            isCrouching = false;
            tick = 0;
            sitTick = -1;
        }

        public void ApplyPhysics()
        {
            if (!isJumping)
                StandUp();
            tick += 1;
        }

        public void UpdatePlayerPosition()
        {
            if (ticksInAir != 0)
                ticksInAir++;
            if (ticksInAir == 60)
            {
                ticksInAir = 0;
                isJumping = false;
                PositionAndSize = new PositionAndSize(new Point(3, 1), new Size(1, 2));
            }
        }

        public void Jump()
        {
            if (isCrouching) 
                return;
            isJumping = true;
            PositionAndSize = new PositionAndSize(new Point(3, 0), new Size(1, 2));
            ticksInAir = 1;
        }

        public void SitDown()
        {
            if (isJumping)
            {
                isJumping = false;
                ticksInAir = 0;
            }
            isCrouching = true;
            PositionAndSize = new PositionAndSize(new Point(3, 2), new Size(1, 1));
            sitTick = tick;
        }

        private void StandUp()
        {
            if (sitTick == tick || isCrouching == false)
                return;
            isCrouching = false;
            PositionAndSize = new PositionAndSize(new Point(3, 1), new Size(1, 2));
        }
    }
}