using System.Drawing;

namespace Game.Classes
{
    public class Player
    {
        public Physics physics;
        public int score;
        public int life;
        public Image image;

        public Player()
        {
            physics = new Physics(new PointF(3, 0), new Size(1, 2));
            score = 0;
            life = 5;
            image = Properties.Resources.totoro;
        }

    }
}