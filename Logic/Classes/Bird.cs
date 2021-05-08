using System.Drawing;

namespace Logic.Classes
{
    public class Bird
    {
        private PositionAndSize positionAndSize;
        private int frameCount = 0;
        private int animationCount = 0;

        public Bird(PointF position, Size size)
        {
            positionAndSize = new PositionAndSize(position, size);
        }

    }
}
