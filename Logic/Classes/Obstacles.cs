using System;
using System.Drawing;

namespace Logic.Classes
{
    public class Obstacles : IGameObject
    {
        public PositionAndSize PositionAndSize { get; set; }
        public ImageName ImageName { get; set; }
        public GameClass ObjectName => GameClass.Obstacles;

        public Obstacles()
        {
            PositionAndSize = new PositionAndSize(new PointF(11, 2), new Size(1,1));
            ChooseRandomImage();
        }

        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 4);
            switch (rnd)
            {
                case 0:
                    ImageName = ImageName.Bush;
                    break;
                case 1:
                    ImageName = ImageName.Stump;
                    break;
                case 2:
                    ImageName = ImageName.Stone1;
                    break;
                case 3:
                    ImageName = ImageName.Stone2;
                    break;
            }
        }
    }
}