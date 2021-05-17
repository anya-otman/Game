using System.Drawing;
using Game.Properties;

namespace Game
{
    public class Images
    {
        public readonly Image Corn;
        public readonly Image Berries;
        public readonly Image Nut;
        public readonly Image Bush;
        public readonly Image Stone1;
        public readonly Image Stone2;
        public readonly Image Stump;
        public readonly Image AppleCore;
        public readonly Image Mushroom;
        public readonly Image Totoro;
        public readonly Image TotoroGo;
        public readonly Image Bird1;
        public Image Bird2;
        public Image Bird3;
        public Image Bird4;
        public Image Bird5;

        public Images()
        {
            Corn = new Bitmap(Resources.corn, 90, 90);
            Berries = new Bitmap(Resources.corn, 90, 90);
            Nut = new Bitmap(Resources.nut, 90, 90);
            Bush = new Bitmap(Resources.bush, 90, 90);
            Stone1 = new Bitmap(Resources.stone1, 90, 90);
            Stone2 = new Bitmap(Resources.stone2, 90, 90);
            Stump = new Bitmap(Resources.stump, 90, 90);
            AppleCore = new Bitmap(Resources.appleCore, 90, 90);
            Mushroom = new Bitmap(Resources.mushroom, 90, 90);
            Totoro = new Bitmap(Resources.totoro, 90, 180);
            TotoroGo = new Bitmap(Resources.totoroGo, 90, 180);
            Bird1 = new Bitmap(Resources.bird1, 90, 90);
            Bird2 = new Bitmap(Resources.bird2, 90, 90);
            Bird3 = new Bitmap(Resources.bird3, 90, 90);
            Bird4 = new Bitmap(Resources.bird4, 90, 90);
            Bird5 = new Bitmap(Resources.bird5, 90, 90);
        }
    }
}