using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Game.Classes
{
    
    public interface IGameObject
    {
        Transform Transform { get; set; }
    }

    public static class GameController
    {
        public static List<IGameObject> road1;

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
            for (var i = 0; i < roads.Count; i++)
            {
                roads[i].transform.position.X -= 4;
                if (roads[i].transform.position.X + roads[i].transform.size.Width < 0)
                {
                    roads.RemoveAt(i);
                    GetNewRoad();
                }
            }

            for (var i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].transform.position.X -= 4;
                if (obstacles[i].transform.position.X + obstacles[i].transform.size.Width < 0)
                {
                    obstacles.RemoveAt(i);
                }
            }

            for (var i = 0; i < foods.Count; i++)
            {
                foods[i].transform.position.X -= 1;
                if (foods[i].transform.position.X < 0)
                    foods.RemoveAt(i);
                /*foods[i].transform.position.X -= 4;
                if (foods[i].transform.position.X + foods[i].transform.size.Width < 0)
                {
                    foods.RemoveAt(i);
                }*/
            }
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
            var road = new Road(new PointF(0 + 200 * 9, 750), new Size(100, 17));
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
                        var obstacle = new Obstacles(new PointF(0 + 200 * 9, 668), new Size(100, 110));
                        obstacles.Add(obstacle);
                        break;
                    case 1:
                        //var food = new Food(new PointF(GameController.food.position.X, 690), new Size(90, 90));
                        foods.Add(new Food());
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
                var newRoad = new Road(new PointF(road.position.X * i, 750), new Size(100, 17));
                roads.Add(newRoad);
                countDangerSpawn++;
            }
        }
    }
}