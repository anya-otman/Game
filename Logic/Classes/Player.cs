using System.Drawing;

namespace Logic.Classes
{
    public class Player
    {
        public readonly Physics Physics;
        public int Score;
        public int Life;
        public readonly ImageName ImgName;

        public Player()
        {
            Physics = new Physics(new PointF(3, 1), new Size(1, 2));
            Score = 0;
            Life = 5;
            ImgName = ImageName.Totoro;
        }

    }
}