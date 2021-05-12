using System.Drawing;

namespace Logic
{
    public class PositionAndSize
    {
        public Point Position;
        public SizeF Size;

        public PositionAndSize(Point position, SizeF size)
        {
            Position = position;
            Size = size;
        }
    }
}
