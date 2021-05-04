using System.Drawing;

namespace Game.Classes
{
    public class Player
    {
        public readonly Physics Physics;
        public int Score;
        public int Life;
        public Image Image;

        public Player()
        {
            Physics = new Physics(new PointF(3, 0), new Size(1, 2));
            Score = 0;
            Life = 5;
            Image = Properties.Resources.totoro;
        }

    }
}