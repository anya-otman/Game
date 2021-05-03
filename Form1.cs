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
            GameController.Init();
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
            for (var i = 0; i < GameController.gameObjects.Count; i++)
            {
                var gameObjects = GameController.gameObjects[i];
                g.DrawImage(gameObjects.Image, gameObjects.Transform.position.X, gameObjects.Transform.position.Y,
                    gameObjects.Transform.size.Width, gameObjects.Transform.size.Height);
            }

            //еще птицы
        }

        private void OnKeyBoardDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    player.physics.SitDown();
                    break;
            }
        }

        private void OnKeyBoardUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.physics.Jump();
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
                        GameController.gameObjects.RemoveAt(player.physics.GetIndexOfFoodNearTotoro(player));
                    }

                    break;
            }
        }
    }
}