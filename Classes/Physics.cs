using System;
using System.Drawing;

namespace Game.Classes
{
    public class Physics
    {
        public Transform transform;
        public bool isJumping;
        public bool isCrouching;
        public bool DoesTotoroTakeFood;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            isJumping = false;
            isCrouching = false;
            DoesTotoroTakeFood = false;
        }

        public void ApplyPhisics()
        {
        }

        
    }
}