using System;
using System.Drawing;
using Game.Classes;

namespace Logic.Classes
{
    public class Physics
    {
        public PositionAndSize positionAndSize;
        private bool isJumping;
        public bool isCrouching;
        private int tick;
        private int jumpTick;
        private int sitTick;

        public Physics(PointF position, Size size)
        {
            positionAndSize = new PositionAndSize(position, size);
            isJumping = false;
            isCrouching = false;
            tick = 0;
            jumpTick = -1;
            sitTick = -1;
        }

        public void ApplyPhysics()
        {
            Collide();
            FallDown();
            if (!isJumping)
                StandUp();
            tick += 1;
        }

        private void Collide()
        {
            foreach (var t in GameController.gameObjects)
            {
                if (t.ObjectName != GameClass.Obstacles)
                    continue;
                var obstaclePosition = t.PositionAndSize.position;
                var playerPosition = positionAndSize.position;
                var playerSize = positionAndSize.size;
                if (!IsObjectInPlayerPosition(playerPosition, obstaclePosition, playerSize))
                    continue;
                GameController.player.Life -= 1;
            }
        }


        public void Jump()
        {
            isJumping = true;
            positionAndSize = new PositionAndSize(new PointF(3, 0), new Size(1, 2));
            jumpTick = tick;
        }

        public void SitDown()
        {
            if (isJumping) return;
            isCrouching = true;
            positionAndSize = new PositionAndSize(new PointF(3, 2), new Size(1, 1));
            sitTick = tick;
        }

        private void FallDown()
        {
            if (jumpTick == tick || isJumping == false)
                return;
            isJumping = false;
            positionAndSize = new PositionAndSize(new PointF(3, 1), new Size(1, 2));
        }

        private void StandUp()
        {
            if (sitTick == tick || isCrouching == false)
                return;
            isCrouching = false;
            positionAndSize = new PositionAndSize(new PointF(3, 1), new Size(1, 2));
        }

        public void GetFood()
        {
            if (TryGetFoodIndex(out var index))
            {
                GameController.gameObjects.RemoveAt(index);
                GameController.player.Score += 10;
            }
        }

        private bool TryGetFoodIndex(out int index)
        {
            index = -1;
            for (int i = 0; i < GameController.gameObjects.Count; i++)
            {
                if (GameController.gameObjects[i].ObjectName != GameClass.Food)
                    continue;
                var foodPosition = GameController.gameObjects[i].PositionAndSize.position;
                var playerPosition = positionAndSize.position;
                var playerSize = positionAndSize.size;
                if (!IsObjectInPlayerPosition(playerPosition, foodPosition, playerSize))
                    continue;
                index = i;
                return true;
            }

            return false;
        }

        private static bool IsObjectInPlayerPosition(PointF playerPosition, PointF foodPosition, Size playerSize)
        {
            return Math.Abs(playerPosition.X - foodPosition.X) < 0.1 &&
                   Math.Abs(playerPosition.Y + playerSize.Height - 1 - foodPosition.Y) < 0.1;
        }
    }
}