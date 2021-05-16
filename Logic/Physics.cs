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

        public Physics(Point position, SizeF size)
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

        public void DoThisMethodEveryGameTick()
        {
            if (ticksInAir != 0)
                ticksInAir++;
            if (ticksInAir == 60)
            {
                ticksInAir = 0;
                isJumping = false;
                PositionAndSize = new PositionAndSize(new Point(3, 1), new SizeF(1, 2));
            }
        }
        
        public void Jump()
        {
            isJumping = true;
            PositionAndSize = new PositionAndSize(new Point(3, 0), new SizeF(1, 2));
            ticksInAir = 1;
        }

        public void SitDown()
        {
            if (isJumping) return;
            isCrouching = true;
            PositionAndSize = new PositionAndSize(new Point(3, 2), new SizeF(1, 1));
            sitTick = tick;
        }

        private void StandUp()
        {
            if (sitTick == tick || isCrouching == false)
                return;
            isCrouching = false;
            PositionAndSize = new PositionAndSize(new Point(3, 1), new SizeF(1, 2));
        }
    }
}