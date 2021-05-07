using System;
using System.Drawing;


namespace Game.Classes
{
    public class Obstacles : IGameObject
    {
        public override PositionAndSize PositionAndSize { get; set; }
        public override ImageName ImageName { get; set; }
        public override GameClass ObjectName => GameClass.Obstacles;

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
                    ImageName = Classes.ImageName.Bush;
                    break;
                case 1:
                    ImageName = Classes.ImageName.Stump;
                    break;
                case 2:
                    ImageName = Classes.ImageName.Stone1;
                    break;
                case 3:
                    ImageName = Classes.ImageName.Stone2;
                    break;
            }
        }
    }
}