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
        private readonly List<int> positionsForSecondRoad;
        private readonly Random random;

        public GameController()
        {
            gameObjects = new List<IGameObject>();
            positionsForFirstRoad = new List<int> {9, 14, 21, 25, 28, 35, 45, 53, 65};
            positionsForSecondRoad = new List<int> {16, 19, 23, 30, 33, 36, 40, 47, 51, 57, 60}; 
            player = new Player(new Point(3, 1), 5);
            random = new Random();
            GetNewFirstRoad();
        }

        public void ChangeState()
        {
            MoveMap();
            player.Physics.ApplyPhysics();
            Collide();
        }

        public void UpdatePlayerPosition(int maxTickInAir)
        {
            player.Physics.UpdatePlayerPosition(maxTickInAir);
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
            
            if (gameObjects.Count < 3)
            {
                var randomNumber = random.Next(0, 2);
                if (randomNumber == 0)
                    GetNewSecondRoad();
                else
                    GetNewFirstRoad();
                
            }
        }

        private void GetNewFirstRoad()
        {
            var delta = 5;
            foreach (var position in positionsForFirstRoad)
            {
                if (position == 14 || position == 35 || position == 53)
                {
                    var obstacle = new Obstacles(new Point(position + delta, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(obstacle);
                }

                if (position == 9 || position == 21 || position == 65 || position == 45)
                {
                    var food = new Food(new Point(position + delta, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
                }

                if (position == 25)
                {
                    var badFood = new BadFood(new Point(position + delta, 2), ChooseRandomBadFoodImage());
                    gameObjects.Add(badFood);
                }

                if (position == 28)
                {
                    var bird = new Bird(new Point(position, 1));
                    gameObjects.Add(bird);
                    var food = new Food(new Point(position + delta, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
                }
            }
        }
        
        private void GetNewSecondRoad()
        {
            foreach (var position in positionsForSecondRoad)
            {
                if (position == 23 || position == 36|| position == 40 || position == 57)
                {
                    var obstacle = new Obstacles(new Point(position, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(obstacle);
                }

                if (position == 16 || position == 19 || position == 30 || position == 47 || position == 60)
                {
                    var food = new Food(new Point(position, 2), ChooseRandomFoodImage());
                    gameObjects.Add(food);
                }

                if (position == 51)
                {
                    var badFood = new BadFood(new Point(position, 2), ChooseRandomBadFoodImage());
                    gameObjects.Add(badFood);
                }

                if (position == 33)
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
            var obj = random.Next(0, 5);
            switch (obj)
            {
                case 0:
                    var newObstacle = new Obstacles(new Point(39, 2), ChooseRandomObstacleImage());
                    gameObjects.Add(newObstacle);
                    break;
                case 1:
                    var newFood = new Food(new Point(26, 2), ChooseRandomFoodImage());
                    gameObjects.Add(newFood);
                    break;
                case 4:
                    gameObjects.Add(new Food(new Point(29, 2), ChooseRandomFoodImage()));
                    break;
                case 5:
                    gameObjects.Add(new Obstacles(new Point(17, 2), ChooseRandomFoodImage()));
                    break;
                case 2:
                    var newBadFood = new BadFood(new Point(22, 2), ChooseRandomBadFoodImage());
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
            var randomNumber = random.Next(1, 4);
            switch (randomNumber)
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
            var randomNumber = random.Next(0, 4);
            switch (randomNumber)
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
            var randomNumber = random.Next(1, 3);
            switch (randomNumber)
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