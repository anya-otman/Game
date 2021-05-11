using System;
using System.Drawing;

namespace Logic.Classes
{
    public class Obstacles : IGameObject
    {
        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; private set; }
        public GameClass ObjectName => GameClass.Obstacles;

        public Obstacles()
        {
            PositionAndSize = new PositionAndSize(new Point(11, 2), new Size(1,1));
            ChooseRandomImage();
        }

        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 4);
            switch (rnd)
            {
                case 0:
                    TypeName = TypeName.Bush;
                    break;
                case 1:
                    TypeName = TypeName.Stump;
                    break;
                case 2:
                    TypeName = TypeName.Stone1;
                    break;
                case 3:
                    TypeName = TypeName.Stone2;
                    break;
            }
        }
    }
}