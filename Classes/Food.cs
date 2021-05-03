using System;
using System.Drawing;


namespace Game.Classes
{
    public class Food : IGameObject
    {
        public Transform transform { get; set; }
        public Image Image { get; set; }
        public gameClasses Name { get; set; }

        public Food()
        {
            transform = new Transform(new PointF(1800, 690), new Size(90,90));
            Name = gameClasses.Food;
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

        public bool DoesNeedGetNewRoad()
        {
            return false;
        }
    }
}
