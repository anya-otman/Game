using System;
using System.Drawing;

namespace Game.Classes
{
    public class Physics
    {
        public readonly PositionAndSize positionAndSize;
        public bool isJumping;
        public bool isCrouching;
        public bool DoesTotoroTakeFood;

        public Physics(PointF position, Size size)
        {
            positionAndSize = new PositionAndSize(position, size);
            isJumping = false;
            isCrouching = false;
            DoesTotoroTakeFood = false;
        }

        public void ApplyPhisics()
        {
            
        }


        public void GetFood()
        {
            if (TryGetFoodIndex(out var index))
            {
                GameController.gameObjects.RemoveAt(index);
                GameController.player.Score += 10;
            }
        }

        public bool TryGetFoodIndex(out int index)
        {
            index = -1;
            for (int i = 0; i < GameController.gameObjects.Count; i++)
            {
                if (GameController.gameObjects[i].ObjectName != GameClass.Food)
                    continue;
                var foodPosition = GameController.gameObjects[i].PositionAndSize.position;
                var playerPosition = positionAndSize.position;
                var playerSize = positionAndSize.size;
                if (!IsFoodInPlayerPosition(playerPosition, foodPosition, playerSize)) 
                    continue;
                index = i;
                return true;
            }
            return false;
        }

        private static bool IsFoodInPlayerPosition(PointF playerPosition, PointF foodPosition, Size playerSize)
        {
            return playerPosition.X == foodPosition.X &&
                   foodPosition.Y == 2;
        }
    }
}