using System.Drawing;

namespace Logic.Classes
{
    public class PositionAndSize
    {
        public PointF Position;
        public Size Size;

        public PositionAndSize(Point position, Size size)
        {
            Position = position;
            Size = size;
        }
    }
}
