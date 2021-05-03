using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Game.Classes
{
    
    

    public static class GameController
    {
        public static List<IGameObject> gameObjects;

        public static List<Road> roads;
        public static List<Obstacles> obstacles;
        public static List<Food> foods;
        public static Transform food;

        public static Transform road;

        public static int foodCounter;
        //еще птицы

        private static int dangerSpawn = 7;
        private static int countDangerSpawn = 2;

        public static void Init(Transform f, Transform r)
        {
            gameObjects = new List<IGameObject>();
            roads = new List<Road>();
            obstacles = new List<Obstacles>();
            foods = new List<Food>();
            food = f;
            road = r;
            foodCounter = 0;
            //еще птицы
            GenerateRoad();
        }

        public static void MoveMap()
        {
            for (var i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].transform.position.X -= 4;
                if (gameObjects[i].transform.position.X + gameObjects[i].transform.size.Width < 0)
                {
                    gameObjects.RemoveAt(i);
                }
            }
            for (var i = 0; i < roads.Count; i++)
            {
                roads[i].transform.position.X -= 4;
                if (roads[i].transform.position.X + roads[i].transform.size.Width < 0)
                {
                    roads.RemoveAt(i);
                    GetNewRoad();
                }
            }

            /*for (var i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].transform.position.X -= 4;
                if (obstacles[i].transform.position.X + obstacles[i].transform.size.Width < 0)
                {
                    obstacles.RemoveAt(i);
                }
            }

            for (var i = 0; i < foods.Count; i++)
            {
                foods[i].transform.position.X -= 4;
                if (foods[i].transform.position.X + foods[i].transform.size.Width < 0)
                {
                    foods.RemoveAt(i);
                }
            }*/
        }

        /*private static void NewMethod(List<T> list)
        {
            for (var i = 0; i < roads.Count; i++)
            {
                roads[i].transform.position.X -= 4;
                if (roads[i].transform.position.X + roads[i].transform.size.Width < 0)
                {
                    roads.RemoveAt(i);
                    GetNewRoad();
                }
            }
        }*/
        

        private static void GetNewRoad()
        {
            var road = new Road(new PointF(0 + 200 * 9, 750));
            roads.Add(road);
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
                        //obstacles.Add(new Obstacles());
                        break;
                    case 1:
                        gameObjects.Add(new Food());
                        //foods.Add(new Food());
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
                var newRoad = new Road(new PointF(road.position.X * i, 750));
                //gameObjects.Add(new Road(new PointF(road.position.X * i, 750)));
                roads.Add(newRoad);
                countDangerSpawn++;
            }
        }
    }
}