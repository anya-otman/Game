using System.Drawing;

namespace Logic
{
    public class Player
    {
        public readonly Physics Physics;
        public int Score;
        public int Life;
        public readonly TypeName ImgName;

        public Player()
        {
            Physics = new Physics(new Point(3, 1), new SizeF(1, 2));
            Score = 0;
            Life = 5;
            ImgName = TypeName.Totoro;
        }

    }
}