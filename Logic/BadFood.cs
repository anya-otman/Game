using System;
using System.Drawing;

namespace Logic
{
    public class BadFood: IGameObject
    {
        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; private set; }
        public GameClass ObjectName => GameClass.BadFood;

        public BadFood()
        {
            PositionAndSize = new PositionAndSize(new Point(11, 2), new SizeF(0.9f, 0.9f));
            ChooseRandomImage();
        }

        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(1, 3);
            switch (rnd)
            {
                case 1:
                    TypeName = TypeName.AppleCore;
                    break;
                case 2:
                    TypeName = TypeName.Mushroom;
                    break;
            }
        }
    }
}
