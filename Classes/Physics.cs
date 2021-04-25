using System;
using System.Drawing;

namespace Game.Classes
{
    public class Physics
    {
        public Transform transform;
        private float gravity;
        private float a;

        public bool isJumping;
        public bool isCrouching;
        public bool DoesTotoroTakeFood;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            a = 0.4f;
            isJumping = false;
            isCrouching = false;
            DoesTotoroTakeFood = false;
        }

        public void ApplyPhisics()
        {
            CalculatePhysics();
        }

        public bool Collide()
        {
            foreach (var obstacle in GameController.obstacles)
            {
                var delta = new PointF
                {
                    X = (transform.position.X + transform.size.Width / 2) -
                        (obstacle.transform.position.X + obstacle.transform.size.Width / 2),
                    Y = (transform.position.Y + transform.size.Height / 2) -
                        (obstacle.transform.position.Y + obstacle.transform.size.Height / 2)
                };

                if (Math.Abs(delta.X * 1.35)<= transform.size.Width / 2 + obstacle.transform.size.Width / 2)
                    if (Math.Abs(delta.Y * 1.35) <= transform.size.Height / 2 + obstacle.transform.size.Height / 2)
                        return true;
            }
            return false;
        }

        public bool CanTotoroTakeFood()
        {
            foreach (var food in GameController.foods)
            {
                var delta = new PointF
                {
                    X = (transform.position.X + transform.size.Width / 2) -
                        (food.transform.position.X + food.transform.size.Width / 2),
                    Y = (transform.position.Y + transform.size.Height / 2) -
                        (food.transform.position.Y + food.transform.size.Height / 2)
                };

                if (Math.Abs(delta.X)<= transform.size.Width / 2 + food.transform.size.Width / 2)
                    if (Math.Abs(delta.Y) <= transform.size.Height / 2 + food.transform.size.Height / 2)
                    {
                        return true;
                    }
            }
            return false;
        }

        public int GetIndexOfFoodNearTotoro()
        {
            for (var i = 0; i < GameController.foods.Count; i++)
            {
                var food = GameController.foods[i];
                var delta = new PointF
                {
                    X = (transform.position.X + transform.size.Width / 2) -
                        (food.transform.position.X + food.transform.size.Width / 2),
                    Y = (transform.position.Y + transform.size.Height / 2) -
                        (food.transform.position.Y + food.transform.size.Height / 2)
                };

                if (Math.Abs(delta.X)<= transform.size.Width / 2 + food.transform.size.Width / 2)
                    if (Math.Abs(delta.Y) <= transform.size.Height / 2 + food.transform.size.Height / 2)
                    {
                        return i;
                    }
                       
            }
            return -1;
        }


        public void AddForce()
        {
            if (!isJumping)
            {
                isJumping = true;
                gravity = -15;
            }
        }

        private void CalculatePhysics()
        {
            if (transform.position.Y < 496 || isJumping)
            {
                transform.position.Y += gravity;
                gravity += a;
            }

            if (transform.position.Y > 496)
                isJumping = false;
        }
    }
}
