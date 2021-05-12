using System.Drawing;

namespace Logic
{
    public class Bird
    {
        private PositionAndSize positionAndSize;

        public Bird(Point position, SizeF size)
        {
            positionAndSize = new PositionAndSize(position, size);
        }

    }
}
