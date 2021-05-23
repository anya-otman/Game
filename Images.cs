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
        public Image Life;

        public Images()
        {
            var imageSize = 90;
            Corn = new Bitmap(Resources.corn, imageSize, imageSize);
            Berries = new Bitmap(Resources.berries, imageSize, imageSize);
            Nut = new Bitmap(Resources.nut, imageSize, imageSize);
            Bush = new Bitmap(Resources.bush, imageSize, imageSize);
            Stone1 = new Bitmap(Resources.stone1, imageSize, imageSize);
            Stone2 = new Bitmap(Resources.stone2, imageSize, imageSize);
            Stump = new Bitmap(Resources.stump, imageSize, imageSize);
            AppleCore = new Bitmap(Resources.appleCore, imageSize, imageSize);
            Mushroom = new Bitmap(Resources.mushroom, imageSize, imageSize);
            Totoro = new Bitmap(Resources.totoro, imageSize, 2 * imageSize);
            TotoroGo = new Bitmap(Resources.totoroGo, imageSize, 2 * imageSize);
            Bird1 = new Bitmap(Resources.bird1, imageSize, imageSize);
            Bird2 = new Bitmap(Resources.bird2, imageSize, imageSize);
            Bird3 = new Bitmap(Resources.bird3, imageSize, imageSize);
            Bird4 = new Bitmap(Resources.bird4, imageSize, imageSize);
            Bird5 = new Bitmap(Resources.bird5, imageSize, imageSize);
            Life = new Bitmap(Resources.life, 2 * imageSize / 3, 2 * imageSize / 3);
        }
    }
}