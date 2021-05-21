using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logic
{
    public class GameController
    {
        private readonly List<IGameObject> gameObjects;
        private readonly Player player;
        private readonly List<int> positions;


        public GameController()
        {
            gameObjects = new List<IGameObject>();
            positions = new List<int> {11, 21, 23, 35, 53, 65};
            player = new Player(new Point(3, 1), 5);
            GetNewRoad();
        }

        public void ChangeState()
        {
            MoveMap();
            player.Physics.ApplyPhysics();
            Collide();
        }

        public void DoThisMethodEveryGameTick()
        {
            player.Physics.DoThisMethodEveryGameTick();
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
                    if (!IsFoodInPlayerPosition(playerPosition, foodPosition, playerSize))
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
                if (gameObjects[i].ObjectName == GameClass.Bird)
                    gameObjects[i].PositionAndSize.Position.X -= 2;
                else
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
                if (position == 35 || position == 53)
                {
                    var obstacle = new Obstacles(new Point(position, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(obstacle);
                }

                if (position == 21)
                {
                    var bird = new Bird(new Point(position, 1));
                    gameObjects.Add(bird);
                }

                if (position == 11 || position == 65)
                {
                    var food = new Food(new Point(position, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
                }

                if (position == 23)
                {
                    var badFood = new BadFood(new Point(position, 2), ChooseRandomBadFoodImage());
                    gameObjects.Add(badFood);
                }
            }
        }

        private void GetNewObject()
        {
            var r = new Random();
            var obj = r.Next(0, 4);
            switch (obj)
            {
                case 0:
                    var newObstacle = new Obstacles(new Point(60, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(newObstacle);
                    break;
                case 1:
                    var newFood = new Food(new Point(60, 2), ChooseRandomFoodImage());
                    gameObjects.Add(newFood);
                    break;
                case 2:
                    var newBadFood = new BadFood(new Point(60, 2), ChooseRandomBadFoodImage());
                    gameObjects.Add(newBadFood);
                    break;
                case 3:
                    var newBird = new Bird(new Point(59, 1));
                    gameObjects.Add(newBird);
                    break;
            }
        }

        private void Collide()
        {
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.ObjectName == GameClass.Obstacles || gameObject.ObjectName == GameClass.Bird)
                {
                    var objectPosition = gameObject.PositionAndSize.Position;
                    var playerPosition = player.Physics.PositionAndSize.Position;
                    var playerSize = player.Physics.PositionAndSize.Size;
                    if (gameObject.ObjectName == GameClass.Obstacles &&
                        IsObstacleInPlayerPosition(playerPosition, objectPosition, playerSize)
                        || gameObject.ObjectName == GameClass.Bird &&
                        IsBirdInPlayerPosition(playerPosition, objectPosition))
                        player.Life -= 1;
                }
            }
        }

        private bool IsFoodInPlayerPosition(Point playerPosition, Point foodPosition, Size playerSize)
        {
            return playerPosition.X <= foodPosition.X &&
                   playerPosition.X + 1 >= foodPosition.X &&
                   Math.Abs(playerPosition.Y + playerSize.Height - 1 - foodPosition.Y) < 0.1;
        }

        private bool IsObstacleInPlayerPosition(Point playerPosition, Point obstaclePosition,
            Size playerSize)
        {
            return Math.Abs(playerPosition.X + 1 - obstaclePosition.X) < 0.1 &&
                   Math.Abs(playerPosition.Y + playerSize.Height - 1 - obstaclePosition.Y) < 0.1;
        }

        private bool IsBirdInPlayerPosition(Point playerPosition, Point birdPosition)
        {
            return playerPosition.X == birdPosition.X &&
                   playerPosition.Y <= birdPosition.Y;
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

        public Physics GetPlayerPhysics()
        {
            return player.Physics;
        }

        public TypeName GetPlayerImageName()
        {
            return player.ImageName;
        }

        public TypeName GetPlayerImageNameGo()
        {
            return player.ImageNameGo;
        }

        public List<IGameObject> GetGameObjectList()
        {
            return gameObjects;
        }
        
    }
}