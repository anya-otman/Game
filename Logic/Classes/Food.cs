using System;
using System.Drawing;

namespace Logic.Classes
{
    public class Food : IGameObject
    {
        public PositionAndSize PositionAndSize { get; }
        public ImageName ImageName { get; private set; }
        public GameClass ObjectName => GameClass.Food;

        public Food()
        {
            PositionAndSize = new PositionAndSize(new PointF(11, 2), new Size(1,1));
            ChooseRandomImage();
        }
        
        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(1, 4);
            switch (rnd)
            {
                case 1:
                    ImageName = ImageName.Corn;
                    break;
                case 2:
                    ImageName = ImageName.Berries;
                    break;
                case 3:
                    ImageName = ImageName.Nut;
                    break;
            }
        }
    }
}