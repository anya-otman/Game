using System.Drawing;

namespace Game.Classes
{
    public class Player
    {
        public readonly Physics Physics;
        public int Score;
        public int Life;
        public ImageName ImageName;

        public Player()
        {
            Physics = new Physics(new PointF(3, 1), new Size(1, 2));
            Score = 0;
            Life = 5;
            ImageName = ImageName.Totoro;
        }

    }
}