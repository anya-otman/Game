using System;
using System.Drawing;


namespace Game.Classes
{
    public class Obstacles : IGameObject
    {
        public Transform transform { get; set; }
        public Image Image { get; set; }
        public gameClasses Name { get; set; }

        public Obstacles()
        {
            transform = new Transform(new PointF(0 + 200 * 9, 668), new Size(100, 110));
            Name = gameClasses.Obstacles;
            ChooseRandomImage();
        }

        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 4);
            switch (rnd)
            {
                case 0:
                    Image = Properties.Resources.bush;
                    break;
                case 1:
                    Image = Properties.Resources.stump;
                    break;
                case 2:
                    Image = Properties.Resources.stone1;
                    break;
                case 3:
                    Image = Properties.Resources.stone2;
                    break;
            }
        }
        public bool DoesNeedGetNewRoad()
        {
            return false;
        }
    }
}