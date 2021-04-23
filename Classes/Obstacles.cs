using System;
using System.Drawing;


namespace Game.Classes
{
    public class Obstacles
    {
        public Transform transform;
        Image image;

        public Obstacles(PointF position, Size size)
        {
            transform = new Transform(position, size);
            ChooseRandomImage();
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(image, transform.position.X, transform.position.Y, transform.size.Width, transform.size.Height);
        }

        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 4);
            switch (rnd)
            {
                case 0:
                    image = Properties.Resources.bush;
                    break;
                case 1:
                    image = Properties.Resources.stump;
                    break;
                case 2:
                    image = Properties.Resources.stone1;
                    break;
                case 3:
                    image = Properties.Resources.stone2;
                    break;
            }
        }
    }
}