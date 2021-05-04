using System;
using System.Drawing;


namespace Game.Classes
{
    public class Food : IGameObject
    {
        public Transform Transform { get; set; }
        public Image Image { get; private set; }
        public GameClass ObjectName => GameClass.Food;

        public Food()
        {
            Transform = new Transform(new PointF(11, 2), new Size(1,1));
            ChooseRandomImage();
        }
        
        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 3);
            switch (rnd)
            {
                case 0:
                    Image = Properties.Resources.berries;
                    break;
                case 1:
                    Image = Properties.Resources.corn;
                    break;
                case 2:
                    Image = Properties.Resources.nut;
                    break;
            }
        }
    }
}