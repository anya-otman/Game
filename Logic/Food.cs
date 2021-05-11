using System;
using System.Drawing;

namespace Logic.Classes
{
    public class Food : IGameObject
    {
        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; private set; }
        public GameClass ObjectName => GameClass.Food;

        public Food()
        {
            PositionAndSize = new PositionAndSize(new Point(11, 2), new Size(1,1));
            ChooseRandomImage();
        }
        
        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(1, 4);
            switch (rnd)
            {
                case 1:
                    TypeName = TypeName.Corn;
                    break;
                case 2:
                    TypeName = TypeName.Berries;
                    break;
                case 3:
                    TypeName = TypeName.Nut;
                    break;
            }
        }
    }
}