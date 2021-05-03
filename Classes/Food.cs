using System;
using System.Drawing;


namespace Game.Classes
{
    public class Food : IGameObject
    {
        public Transform Transform { get; set; }
        public Image Image => _image;
        public string ObjectName => "food";

        private Image _image;

        public Food()
        {
            Transform = new Transform(new PointF(1800, 690), new Size(90,90));
            ChooseRandomImage();
        }
        
        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 3);
            switch (rnd)
            {
                case 0:
                    _image = Properties.Resources.berries;
                    break;
                case 1:
                    _image = Properties.Resources.corn;
                    break;
                case 2:
                    _image = Properties.Resources.nut;
                    break;
            }
        }
    }
}
