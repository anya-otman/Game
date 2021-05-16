using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logic
{
    public class GameController
    {
        private List<IGameObject> gameObjects;
        private int foodCounter;
        private int obstaclesCounter;
        private readonly Player player;
        private readonly List<int> positions;

        public GameController()
        {
            gameObjects = new List<IGameObject>();
            foodCounter = 0;
            obstaclesCounter = 0;
            positions = new List<int> {11, 20, 35, 53, 65};
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
                if (gameObjects[index].ObjectName == GameClass.Food)
                    player.Score += 10;
                if (gameObjects[index].ObjectName == GameClass.BadFood)
                    player.Life -= 1;
                gameObjects.RemoveAt(index);
            }
        }

        public void AddObject(IGameObject gameObject)
        {
            gameObjects.Add(gameObject);
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
                if (gameObjects[i].ObjectName == GameClass.Food || gameObjects[i].ObjectName == GameClass.BadFood)
                {
                    var foodPosition = gameObjects[i].PositionAndSize.Position;
                    var playerPosition = player.Physics.PositionAndSize.Position;
                    var playerSize = player.Physics.PositionAndSize.Size;
                    if (!IsObjectInPlayerPosition_MethodForFood(playerPosition, foodPosition, playerSize))
                        continue;
                    index = i;
                    return true;
                }
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
                var obstacle = new Obstacles(new Point(11, 2), ChooseRandomObstacleImage());
                var food = new Food(new Point(11, 2), ChooseRandomFoodImage());
                var badFood = new BadFood(new Point(11, 2), ChooseRandomBadFoodImage());
                if (position == 35 || position == 53)
                {
                    obstacle.PositionAndSize.Position.X = position;
                    gameObjects.Add(obstacle);
                }

                if (position == 11 || position == 65)
                {
                    food.PositionAndSize.Position.X = position;
                    gameObjects.Add(food);
                }

                if (position == 20)
                {
                    badFood.PositionAndSize.Position.X = position;
                    gameObjects.Add(badFood);
                }
            }
        }

        private void GetNewObject()
        {
            var r = new Random();
            var obj = r.Next(0, 3); //c птицами (0, 4)
            switch (obj)
            {
                case 0:
                    var newObstacle = new Obstacles(new Point(60, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(newObstacle);
                    obstaclesCounter++;
                    break;
                case 1:
                    var newFood = new Food(new Point(60, 2), ChooseRandomFoodImage());
                    gameObjects.Add(newFood);
                    foodCounter++;
                    break;
                case 2:
                    var newBadFood = new BadFood(new Point(60, 2), ChooseRandomBadFoodImage());
                    gameObjects.Add(newBadFood);
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
                if (!IsObjectInPlayerPosition_MethodForObstacles(playerPosition, obstaclePosition, playerSize))
                    continue;
                player.Life -= 1;
            }
        }

        private bool IsObjectInPlayerPosition_MethodForFood(PointF playerPosition, Point foodPosition, SizeF playerSize)
        {
            return playerPosition.X <= foodPosition.X &&
                   playerPosition.X + 1 >= foodPosition.X &&
                   Math.Abs(playerPosition.Y + playerSize.Height - 1 - foodPosition.Y) < 0.1;
        }
        
        private bool IsObjectInPlayerPosition_MethodForObstacles(PointF playerPosition, Point foodPosition, SizeF playerSize)
        {
            return Math.Abs(playerPosition.X + playerSize.Width - 1 - foodPosition.X) < 0.1 &&
                   Math.Abs(playerPosition.Y + playerSize.Height - 1 - foodPosition.Y) < 0.1;
        }
        
        private TypeName ChooseRandomFoodImage()
        {
            var r = new Random();
            var rnd = r.Next(1, 4);
            switch (rnd)
            {
                case 1:
                    return TypeName.Corn;
                case 2:
                    return TypeName.Berries;
                case 3:
                    return TypeName.Nut;
            }
            throw new Exception();
        }
        
        private TypeName ChooseRandomObstacleImage()
        {
            var r = new Random();
            var rnd = r.Next(0, 4);
            switch (rnd)
            {
                case 0:
                    return TypeName.Bush;
                case 1:
                    return TypeName.Stump;
                case 2:
                    return TypeName.Stone1;
                case 3:
                    return TypeName.Stone2;
            }
            throw new Exception();
        }
        
        private TypeName ChooseRandomBadFoodImage()
        {
            var r = new Random();
            var rnd = r.Next(1, 3);
            switch (rnd)
            {
                case 1:
                    return TypeName.AppleCore;
                case 2:
                    return TypeName.Mushroom;
            }
            throw new Exception();
        }
    }
}