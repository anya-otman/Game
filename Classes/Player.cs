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
        public int animationCount = 0;
        public int score;
        public int life;
        public bool isInCollision = false;
        Image image;

        public Player(PointF position, Size size)
        {
            physics = new Physics(position, size);
            score = 0;
            life = 5;
            image = new Bitmap("D:\\C# 2\\Totoro-game\\Game\\Images\\totoro.png");
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(image,
                new Rectangle(new Point((int) physics.transform.position.X, (int) physics.transform.position.Y),
                    new Size(physics.transform.size.Width, physics.transform.size.Height)));
        }

    }
}
