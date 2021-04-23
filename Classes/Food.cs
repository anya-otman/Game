﻿using System;
using System.Drawing;


namespace Game.Classes
{
    public class Food
    {
        public Transform transform;
        Image image;

        public Food(PointF position, Size size)
        {
            transform = new Transform(position, size);
            ChooseRandomImage();
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(image, transform.position.X, transform.position.Y, transform.size.Width, transform.size.Height);
        }

        private void ChooseRandomImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 3);
            switch (rnd)
            {
                case 0:
                    image = Properties.Resources.berries;
                    break;
                case 1:
                    image = Properties.Resources.corn;
                    break;
                case 2:
                    image = Properties.Resources.nut;
                    break;
            }
        }
    }
}