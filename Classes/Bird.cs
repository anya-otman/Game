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
        public Transform transform;
        int frameCount = 0;
        int animationCount = 0;

        public Bird(PointF position, Size size)
        {
            transform = new Transform(position, size);
        }

        public void DrawSprite(Graphics g)
        {
            frameCount++;
            //Разобраться с анимацией

            g.DrawImage(Properties.Resources.bush, 2100, 112, 100, 17);
        }
    }
}
