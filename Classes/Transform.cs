﻿using System.Drawing;


namespace Game.Classes
{
    public class Transform
    {
        public PointF position;
        public Size size;

        public Transform(PointF position, Size size)
        {
            this.position = position;
            this.size = size;
        }
    }
}
