using System.Drawing;

namespace Game.Classes
{
    public class Player
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
            image = Properties.Resources.totoro;
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(image, physics.transform.position.X, physics.transform.position.Y, physics.transform.size.Width, physics.transform.size.Height);
        }

    }
}
