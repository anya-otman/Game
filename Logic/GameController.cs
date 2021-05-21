using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logic
{
    public class GameController
    {
        private readonly List<IGameObject> gameObjects;
        private readonly Player player;
        private readonly List<int> positionsForFirstRoad;
        private readonly List<int> positionsForSecondRoad; //для второй дороги


        public GameController()
        {
            gameObjects = new List<IGameObject>();
            positionsForFirstRoad = new List<int> {11, 14, 21, 25, 30, 35, 45, 53, 65};
            positionsForSecondRoad = new List<int> {13, 15, 19, 24, 28, 34, 36, 40, 45, 51, 57, 60}; //для второй дороги
            player = new Player(new Point(3, 1), 5);
            GetNewRoad();
            //GetNewSecondRoad();
        }

        
        //это я пишу метод, который будет генерировать другую дорогу
        private void GetNewSecondRoad()
        {
            foreach (var position in positionsForSecondRoad)
            {
                if (position == 13 || position == 24 || position == 36|| position == 40 || position == 57)
                {
                    var obstacle = new Obstacles(new Point(position, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(obstacle);
                }

                if (position == 15 || position == 19 || position == 28 || position == 45 || position == 60)
                {
                    var food = new Food(new Point(position, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
                }

                if (position == 51)
                {
                    var badFood = new BadFood(new Point(position, 2), ChooseRandomBadFoodImage());
                    gameObjects.Add(badFood);
                }

                if (position == 34)
                {
                    var bird = new Bird(new Point(position, 1));
                    gameObjects.Add(bird);
                    var food = new Food(new Point(position, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
                }
            }
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
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.ObjectName == GameClass.Bird)
                    gameObject.PositionAndSize.Position.X -= 2;
                else
                    gameObject.PositionAndSize.Position.X -= 1;
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].PositionAndSize.Position.X < 0)
                {
                    gameObjects.Remove(gameObjects[i]);
                    GetNewObject();
                }
            }

            if (gameObjects.Count < 2)
                GetNewRoad();
        }

        private void GetNewRoad()
        {
            foreach (var position in positionsForFirstRoad)
            {
                if (position == 14 || position == 35 || position == 53)
                {
                    var obstacle = new Obstacles(new Point(position, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(obstacle);
                }

                if (position == 11 || position == 21 || position == 65 || position == 45)
                {
                    var food = new Food(new Point(position, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
                }

                if (position == 25)
                {
                    var badFood = new BadFood(new Point(position, 2), ChooseRandomBadFoodImage());
                    gameObjects.Add(badFood);
                }

                if (position == 30)
                {
                    var bird = new Bird(new Point(position, 1));
                    gameObjects.Add(bird);
                    var food = new Food(new Point(position, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
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
                    var newObstacle = new Obstacles(new Point(25, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(newObstacle);
                    break;
                case 1:
                    var newFood = new Food(new Point(19, 2), ChooseRandomFoodImage());
                    gameObjects.Add(newFood);
                    break;
                case 2:
                    var newBadFood = new BadFood(new Point(25, 2), ChooseRandomBadFoodImage());
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
                    if (gameObject.ObjectName == GameClass.Obstacles &&
                        IsObstacleInPlayerPosition(playerPosition, objectPosition)
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

        private bool IsObstacleInPlayerPosition(Point playerPosition, Point obstaclePosition)
        {
            return playerPosition.X == obstaclePosition.X &&
                   playerPosition.Y == 1;
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

        public List<IGameObject> GetGameObjectList()
        {
            return gameObjects;
        }
    }
}