using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Bird
    {
        public PositionAndSize positionAndSize;
        int frameCount = 0;
        int animationCount = 0;

        public Bird(PointF position, Size size)
        {
            positionAndSize = new PositionAndSize(position, size);
        }

    }
}
