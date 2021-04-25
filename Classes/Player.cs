using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Game.Properties;

namespace Game.Classes
{
    class Player
    {
        public Physics physics;
        public int framesCount = 0; //для синхронизации с основным таймером
        public int animationCount = 0;
        public int score = 0;
        public int life = 5;
        public bool isInCollision = false;
        Image image;

        public Player(PointF position, Size size)
        {
            physics = new Physics(position, size);
            framesCount = 0;
            score = 0;
            image = new Bitmap("D:\\C# 2\\Totoro-game\\Game\\Images\\totoro.png");
        }

        public void DrawImage(Graphics g)
        {
            /*if (physics.isCrouching)
            {
                g.DrawImage(image,
                    new Rectangle(new Point((int) physics.transform.position.X, (int) physics.transform.position.Y),
                        new Size(physics.transform.size.Width, physics.transform.size.Height)));
            }
            else
            {
                g.DrawImage(image,
                    new Rectangle(new Point((int) physics.transform.position.X, (int) physics.transform.position.Y),
                        new Size(physics.transform.size.Width, physics.transform.size.Height)));
            }*/

            g.DrawImage(image,
                new Rectangle(new Point((int) physics.transform.position.X, (int) physics.transform.position.Y),
                    new Size(physics.transform.size.Width, physics.transform.size.Height)));
        }

    }
}
