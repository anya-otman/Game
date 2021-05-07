using System.Drawing;


namespace Game.Classes
{
    public class PositionAndSize
    {
        public PointF position;
        public Size size;

        public PositionAndSize(PointF position, Size size)
        {
            this.position = position;
            this.size = size;
        }
    }
}
