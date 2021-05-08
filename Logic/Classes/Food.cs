using System;
using System.Drawing;

namespace Logic.Classes
{
    public class Food : IGameObject
    {
        public sealed override PositionAndSize PositionAndSize { get; set; }
        public override ImageName ImageName { get; set; }
        public override GameClass ObjectName => GameClass.Food;

        public Food()
        {
            PositionAndSize = new PositionAndSize(new PointF(11, 2), new Size(1,1));
            ChooseRandomImage();
        }
        
        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 3);
            switch (rnd)
            {
                case 0:
                    ImageName = ImageName.Berries;
                    break;
                case 1:
                    ImageName = ImageName.Corn;
                    break;
                case 2:
                    ImageName = ImageName.Nut;
                    break;
            }
        }
    }
}