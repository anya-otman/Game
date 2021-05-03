using System;
using System.Windows.Forms;
using Game.Classes;
using System.Drawing;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        Player player;
        private Timer mainTimer;
        private int time;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawGame;
            KeyUp += OnKeyBoardUp;
            KeyDown += OnKeyBoardDown;
            KeyUp += OnKeyBoardSpace;
            mainTimer = new Timer();
            mainTimer.Interval = 5;
            mainTimer.Tick += Update;

            Init();
        }

        private void Init()
        {
            GameController.Init(new Transform(new PointF(1800, 200), new Size(50, 50)),
                new Transform(new PointF(200, 200), new Size(50, 50)));
            //player = new Player(new PointF(275, 495), new Size(180, 280));
            player = new Player();
            time = 0;
            mainTimer.Start();
            Invalidate();
        }

        private void Update(object sender, EventArgs e)
        {
            time += mainTimer.Interval;
            //UpdateTimer();
            UpdateCollision();
            Text = "Totoro - life: " + player.life + " time interval: " + mainTimer.Interval + " score" + player.score + " foodCounter " + GameController.foodCounter;
            //ShouldGameStop();
            player.physics.ApplyPhisics();
            GameController.MoveMap();
            Invalidate();
        }

        public void DrawArrayOfTotoro(Graphics g)
        {
            var x = -265;
            var y = 495;
            for (int i = 0; i < 10; i++)
            {
                g.DrawImage(player.image, x + i*180, player.physics.transform.position.Y,
                    player.physics.transform.size.Width, player.physics.transform.size.Height);
            }
        }

        private void ShouldGameStop()
        {
            if (player.life == 0)
                mainTimer.Stop();
        }

        private void UpdateCollision()
        {
            var isCollide = player.physics.Collide();
            if (isCollide && !player.isInCollision)
            {
                player.life -= 1;
                player.isInCollision = true;
            }

            if (!isCollide)
            {
                player.isInCollision = false;
            }

            if (player.physics.CanTotoroTakeFood(player) == false && player.physics.DoesTotoroTakeFood)
                player.physics.DoesTotoroTakeFood = false;
        }

        private void UpdateTimer()
        {
            if (time > 10000)
                mainTimer.Interval = 5;
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawObjects(g);
            DrawPlayer(g);
            //DrawArrayOfTotoro(g);
        }

        public void DrawPlayer(Graphics g)
        {
            /*g.DrawImage(player.image, -265 + player.physics.transform.position.X * 180, 495 + player.physics.transform.position.Y * 475,
                player.physics.transform.size.Width*180, player.physics.transform.size.Height*140);*/
            g.DrawImage(player.image, player.physics.transform.position.X * 1, player.physics.transform.position.Y * 1,
                player.physics.transform.size.Width*1, player.physics.transform.size.Height*1);
        }

        public static void DrawObjects(Graphics g)
        {
            for (var i = 0; i < GameController.roads.Count; i++)
            {
                var road = GameController.roads[i];
                g.DrawImage(road.roadImage,
                    2100, 112, 100, 17);
            }

            for (var i = 0; i < GameController.obstacles.Count; i++)
            {
                var obstacle = GameController.obstacles[i];
                g.DrawImage(obstacle.image, obstacle.transform.position.X, obstacle.transform.position.Y,
                    obstacle.transform.size.Width, obstacle.transform.size.Height);
            }

            for (var i = 0; i < GameController.foods.Count; i++)
            {
                var food = GameController.foods[i];
                g.DrawImage(food.image, food.transform.position.X * 1, food.transform.position.Y,
                    food.transform.size.Width, food.transform.size.Height);
                /*g.DrawImage(food.image, -265 + food.transform.position.X * 180, 690,
                    90, 90);*/
            }

            //еще птицы
        }

        private void OnKeyBoardDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (!player.physics.isJumping)
                    {
                        player.physics.isCrouching = true;
                        player.physics.isJumping = false;
                        player.physics.transform.size.Height = 200;
                        player.physics.transform.position.Y = 580;
                    }

                    break;
            }
        }

        private void OnKeyBoardUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (!player.physics.isJumping)
                    {
                        player.physics.isCrouching = false;
                        player.physics.AddForce();
                    }

                    break;
                case Keys.Down:
                    player.physics.isCrouching = false;
                    player.physics.transform.size.Height = 280;
                    player.physics.transform.position.Y = 496.2f;
                    break;
            }
        }

        private void OnKeyBoardSpace(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (player.physics.CanTotoroTakeFood(player))
                    {
                        player.score += 10;
                        GameController.foods.RemoveAt(player.physics.GetIndexOfFoodNearTotoro(player));
                    }

                    break;
            }
        }
    }
}