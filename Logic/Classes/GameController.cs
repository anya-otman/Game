using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logic.Classes
{
    public class GameController
    {
        private List<IGameObject> gameObjects;
        private int foodCounter;
        private int obstaclesCounter;
        private readonly Player player;
        private int dangerSpawn = 7;
        private int countDangerSpawn = 2;

        public GameController()
        {
            gameObjects = new List<IGameObject>();
            foodCounter = 0;
            obstaclesCounter = 0;
            player = new Player();
            GenerateRoad();
        }

        public void ChangeState()
        {
            MoveMap();
            player.physics.ApplyPhysics();
            Collide();
        }
        private void MoveMap()
        {
            for (var i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].PositionAndSize.position.X -= 1;
                if (gameObjects[i].PositionAndSize.position.X < -2)
                {
                    if (gameObjects[i].ObjectName == GameClass.Road)
                        GetNewRoad();
                    gameObjects[i].PositionAndSize.position.X = 70;
                    //gameObjects.RemoveAt(i);
                }
            }
        }
        public void GetFood()
        {
            if (TryGetFoodIndex(out var index))
            {
                gameObjects.RemoveAt(index);
                player.score += 10;
            }
        }

        public void Jump()
        {
            player.physics.Jump();
        }

        public void SitDown()
        {
            player.physics.SitDown();
        }

        public int GetLife()
        {
            return player.life;
        }

        public int GetScore()
        {
            return player.score;
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
            return player.physics;
        }

        public ImageName GetPlayerImageName()
        {
            return player.imageName;
        }

        public List<IGameObject> GetGameObjectList()
        {
            return gameObjects;
        }
        private bool TryGetFoodIndex(out int index)
        {
            index = -1;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].ObjectName != GameClass.Food)
                    continue;
                var foodPosition = gameObjects[i].PositionAndSize.position;
                var playerPosition = player.physics.positionAndSize.position;
                var playerSize = player.physics.positionAndSize.size;
                if (!IsObjectInPlayerPosition(playerPosition, foodPosition, playerSize))
                    continue;
                index = i;
                return true;
            }

            return false;
        }
        private void GetNewRoad()
        {
            var road = new Road();
            gameObjects.Add(road);
            countDangerSpawn++;

            if (countDangerSpawn >= dangerSpawn)
            {
                var r = new Random();
                dangerSpawn = r.Next(2, 6);
                countDangerSpawn = 0;
                var obj = r.Next(0, 2); //c птицами (0, 3)
                switch (obj)
                {
                    case 0:
                        gameObjects.Add(new Obstacles());
                        obstaclesCounter++;
                        break;
                    case 1:
                        gameObjects.Add(new Food());
                        foodCounter++;
                        break;
                }
            }
        }

        private void GenerateRoad()
        {
            for (var i = 0; i < 10; i++)
            {
                var newRoad = new Road();
                newRoad.PositionAndSize.position.X *= i;
                gameObjects.Add(newRoad);
                countDangerSpawn++;
            }
        }

        private void Collide()
        {
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.ObjectName != GameClass.Obstacles)
                    continue;
                var obstaclePosition = gameObject.PositionAndSize.position;
                var playerPosition = player.physics.positionAndSize.position;
                var playerSize = player.physics.positionAndSize.size;
                if (!IsObjectInPlayerPosition(playerPosition, obstaclePosition, playerSize))
                    continue;
                player.life -= 1;
            }
        }

        private bool IsObjectInPlayerPosition(PointF playerPosition, PointF foodPosition, Size playerSize)
        {
            return Math.Abs(playerPosition.X - foodPosition.X) < 0.1 &&
                   Math.Abs(playerPosition.Y + playerSize.Height - 1 - foodPosition.Y) < 0.1;
        }
        
        
    }
}