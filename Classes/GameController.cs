using System;
using System.Collections.Generic;

namespace Game.Classes
{
    public static class GameController
    {
        public static List<IGameObject> gameObjects;

        public static int foodCounter;
        //еще птицы

        private static int dangerSpawn = 7;
        private static int countDangerSpawn = 2;

        public static void Init()
        {
            gameObjects = new List<IGameObject>();
            foodCounter = 0;
            //еще птицы
            GenerateRoad();
        }

        public static void MoveMap()
        {
            for (var i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Transform.position.X -= 4;
                if (gameObjects[i].Transform.position.X + gameObjects[i].Transform.size.Width < 0)
                {
                    if (gameObjects[i].ObjectName == "road")
                        GetNewRoad();
                    gameObjects.RemoveAt(i);
                }
            }
        }


        private static void GetNewRoad()
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
                        break;
                    case 1:
                        gameObjects.Add(new Food());
                        foodCounter++;
                        break;
                    //еще птицы
                }
            }
        }

        private static void GenerateRoad()
        {
            for (var i = 0; i < 10; i++)
            {
                var newRoad = new Road();
                newRoad.Transform.position.X *= i;
                gameObjects.Add(newRoad);
                countDangerSpawn++;
            }
        }
    }
}