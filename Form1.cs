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
            mainTimer.Interval = 10;
            mainTimer.Tick += Update;
            
            Init();
        }

        private void Init()
        {
            GameController.Init();
            time = 0;
            player = new Player(new PointF(275, 569), new Size(150, 200));
            mainTimer.Start();
            Invalidate();
            }

        private void Update(object sender, EventArgs e)
        {
            time += mainTimer.Interval;
            UpdateTimer();
            UpdateCollision();
            Text = "Totoro - life: " + player.life + " time interval: " + mainTimer.Interval + " score" + player.score;
            ShouldGameStop();
            player.physics.ApplyPhisics();
            GameController.MoveMap();
            Invalidate();
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

            if (player.physics.CanTotoroTakeFood() == false && player.physics.DoesTotoroTakeFood)
                player.physics.DoesTotoroTakeFood = false;
        }

        private void UpdateTimer()
        {
            if (time > 10000)
                mainTimer.Interval = 3;
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            GameController.DrawObjects(g);
            player.DrawImage(g);
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
                        player.physics.transform.size.Height = 125;
                        player.physics.transform.position.Y = 650;
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
                    player.physics.transform.size.Height = 200;
                    player.physics.transform.position.Y = 570.2f;
                    break;
            }
        }

        private void OnKeyBoardSpace(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (player.physics.CanTotoroTakeFood() && !player.physics.DoesTotoroTakeFood)
                    {
                        player.score += 10;
                        GameController.foods.RemoveAt(player.physics.GetIndexOfFoodNearTotoro());
                        player.physics.DoesTotoroTakeFood = true;
                    }

                    break;
            }
        }
    }
}