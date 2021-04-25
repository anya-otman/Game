using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game.Classes
{
    class Physics
    {
        public Transform transform;
        private float gravity;
        private float a;

        public bool isJumping;
        public bool isCrouching;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            a = 0.4f;
            isJumping = false;
            isCrouching = false;
        }

        public void ApplyPhisics()
        {
            CalculatePhysics();
        }
        public void CalculatePhysics()
        {
            if (transform.position.Y < 570 || isJumping)
            {
                transform.position.Y += gravity;
                gravity += a;
            }

            if (transform.position.Y > 570)
                isJumping = false;
        }

        public bool Collide()
        {
            for (var i = 0; i < GameController.obstacles.Count; i++)
            {
                var obstacle = GameController.obstacles[i];
                PointF delta = new PointF();
                delta.X = (transform.position.X + transform.size.Width / 2) -
                          (obstacle.transform.position.X + obstacle.transform.size.Width / 2);
                delta.Y = (transform.position.Y + transform.size.Height / 2) -
                          (obstacle.transform.position.Y + obstacle.transform.size.Height / 2);
                
                if (Math.Abs(delta.X * 1.3)<= transform.size.Width / 2 + obstacle.transform.size.Width / 2)
                    if (Math.Abs(delta.Y * 1.3) <= transform.size.Height / 2 + obstacle.transform.size.Height / 2)
                        return true;
            }

            return false;
        }

        
        public void AddForce()
        {
            if (!isJumping)
            {
                isJumping = true;
                gravity = -17;
            }
        }
    }
}
