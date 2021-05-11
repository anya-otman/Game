using System.Drawing;

namespace Logic.Classes
{
    public class Bird
    {
        private PositionAndSize positionAndSize;

        public Bird(Point position, Size size)
        {
            positionAndSize = new PositionAndSize(position, size);
        }

    }
}
