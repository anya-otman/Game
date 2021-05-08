using System.Drawing;

namespace Logic.Classes
{
    public class Player
    {
        public readonly Physics physics;
        public int score;
        public int life;
        public readonly ImageName imageName;

        public Player()
        {
            physics = new Physics(new PointF(3, 1), new Size(1, 2));
            score = 0;
            life = 5;
            imageName = ImageName.Totoro;
        }

    }
}