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

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawGame;
            KeyUp += OnKeyBoardUp;
            KeyDown += OnKeyBoardDown;
            mainTimer = new Timer();
            mainTimer.Interval = 5;
            mainTimer.Tick += Update;

            Init();
        }

        private void Init()
        {
            GameController.Init();
            player = new Player(new PointF(275, 570), new Size(150, 200));
            mainTimer.Start();
            Invalidate();
        }

        private void Update(object sender, EventArgs e)
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

            Text = "Totoro - life: " + player.life;
            if (player.life == 0)
                mainTimer.Stop();
            player.physics.ApplyPhisics();
            GameController.MoveMap();
            Invalidate();
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
                    player.physics.transform.position.Y = 570;
                    break;
            }
        }
    }
}