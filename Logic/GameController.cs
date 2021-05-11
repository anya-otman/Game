using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logic.Classes
{
    public class GameController
    {
        private readonly List<IGameObject> gameObjects;
        private int foodCounter;
        private int obstaclesCounter;
        private readonly Player player;
        private readonly List<int> positions;

        public GameController()
        {
            gameObjects = new List<IGameObject>();
            foodCounter = 0;
            obstaclesCounter = 0;
            positions = new List<int> {11, 20, 40, 53, 65};
            player = new Player();
            GetNewRoad();
        }

        public void ChangeState()
        {
            MoveMap();
            player.Physics.ApplyPhysics();
            Collide();
        }

        public void GetFood()
        {
            if (IsPlayerNearFood_GetIndex(out var index))
            {
                gameObjects.RemoveAt(index);
                player.Score += 10;
            }
        }

        public void Jump()
        {
            player.Physics.Jump();
        }

        public void SitDown()
        {
            player.Physics.SitDown();
        }

        public int GetLife()
        {
            return player.Life;
        }

        public int GetScore()
        {
            return player.Score;
        }

        public int GetFoodCounter()
        {
            return foodCounter;
        }
        
        public int GetObstacleCounter()
        {
            return obstaclesCounter;
        }

        public Physics GetPlayerPhysics()
        {
            return player.Physics;
        }

        public TypeName GetPlayerImageName()
        {
            return player.ImgName;
        }

        public List<IGameObject> GetGameObjectList()
        {
            return gameObjects;
        }

        private bool IsPlayerNearFood_GetIndex(out int index)
        {
            index = -1;
            for (var i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].ObjectName != GameClass.Food)
                    continue;
                var foodPosition = gameObjects[i].PositionAndSize.Position;
                var playerPosition = player.Physics.PositionAndSize.Position;
                var playerSize = player.Physics.PositionAndSize.Size;
                playerSize.Width = 2;
                if (!IsObjectInPlayerPosition(playerPosition, foodPosition, playerSize))
                    continue;
                index = i;
                return true;
            }

            return false;
        }

        private void MoveMap()
        {
            for (var i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].PositionAndSize.Position.X -= 1;
                if (gameObjects[i].PositionAndSize.Position.X < -2)
                {
                    gameObjects.RemoveAt(i);
                    GetNewObject();
                }
            }

            if (gameObjects.Count < 2)
                GetNewRoad();
        }

        private void GetNewRoad()
        {
            foreach (var position in positions)
            {
                var obstacle = new Obstacles();
                var food = new Food();
                if (position == 20 || position == 53)
                {
                    obstacle.PositionAndSize.Position.X = position;
                    gameObjects.Add(obstacle);
                }

                if (position == 11 || position == 40 || position == 65)
                {
                    food.PositionAndSize.Position.X = position;
                    gameObjects.Add(food);
                }
            }
        }

        private void GetNewObject()
        {
            var r = new Random();
            var obj = r.Next(0, 2); //c птицами (0, 3)
            switch (obj)
            {
                case 0:
                    var newObstacle = new Obstacles();
                    newObstacle.PositionAndSize.Position.X = 60;
                    gameObjects.Add(newObstacle);
                    obstaclesCounter++;
                    break;
                case 1:
                    var newFood = new Food();
                    newFood.PositionAndSize.Position.X = 60;
                    gameObjects.Add(newFood);
                    foodCounter++;
                    break;
            }
        }

        private void Collide()
        {
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.ObjectName != GameClass.Obstacles)
                    continue;
                var obstaclePosition = gameObject.PositionAndSize.Position;
                var playerPosition = player.Physics.PositionAndSize.Position;
                var playerSize = player.Physics.PositionAndSize.Size;
                if (!IsObjectInPlayerPosition(playerPosition, obstaclePosition, playerSize))
                    continue;
                player.Life -= 1;
            }
        }

        private bool IsObjectInPlayerPosition(PointF playerPosition, PointF foodPosition, Size playerSize)
        {
            return Math.Abs(playerPosition.X + playerSize.Width - 1 - foodPosition.X) < 0.1 &&
                   Math.Abs(playerPosition.Y + playerSize.Height - 1 - foodPosition.Y) < 0.1;
        }
    }
}