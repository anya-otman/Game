using System.Drawing;

namespace Logic
{
    public class Player
    {
        public readonly Physics Physics;
        public int Score;
        public int Life;
        public readonly TypeName ImageName;
        public readonly TypeName ImageNameGo;

        public Player(Point point, int maxLife)
        {
            Physics = new Physics(point, new SizeF(1, 2));
            Score = 0;
            Life = maxLife;
            ImageName = TypeName.Totoro;
            ImageNameGo = TypeName.TotoroGo;
        }

    }
}