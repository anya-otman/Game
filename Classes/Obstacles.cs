using System;
using System.Drawing;


namespace Game.Classes
{
    public class Obstacles : IGameObject
    {
        public Transform Transform { get; set; }
        public Image Image => _image;
        public string ObjectName => "obstacle";

        private Image _image;

        public Obstacles()
        {
            Transform = new Transform(new PointF(1800, 668), new Size(100, 110));
            ChooseRandomImage();
        }

        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 4);
            switch (rnd)
            {
                case 0:
                    _image = Properties.Resources.bush;
                    break;
                case 1:
                    _image = Properties.Resources.stump;
                    break;
                case 2:
                    _image = Properties.Resources.stone1;
                    break;
                case 3:
                    _image = Properties.Resources.stone2;
                    break;
            }
        }
    }
}