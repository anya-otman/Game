﻿using System;
using System.Windows.Forms;
using System.Drawing;
using Game.Properties;
using Logic.Classes;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        private readonly Timer mainTimer;
        private GameController gameController;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawGame;
            KeyUp += OnKeyBoardSpace;
            KeyUp += OnKeyBoardUp;
            KeyUp += OnKeyBoardDown;
            mainTimer = new Timer {Interval = 300};
            mainTimer.Tick += Update;

            Init();
        }

        private void Init()
        {
            gameController = new GameController();
            mainTimer.Start();
            Invalidate();
        }

        private void Update(object sender, EventArgs e)
        {
            gameController.ChangeState();
            Text = "Totoro - Life: " + gameController.GetLife() +
                   //" timeInterval: " + mainTimer.Interval + 
                   " Score" + gameController.GetScore() +
                   " foodCounter " + gameController.GetFoodCounter() +
                   " obstaclesCounter " + gameController.GetObstacleCounter();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawObjects(g);
        }

        private void DrawObjects(Graphics g)
        {
            foreach (var gameObject in gameController.GetGameObjectList())
            {
                g.DrawImage(GetImage(gameObject.ImageName), -265 + gameObject.PositionAndSize.Position.X * 180, 690,
                        gameObject.PositionAndSize.Size.Width * 90, gameObject.PositionAndSize.Size.Height * 90);
            }

            var playerPhysics = gameController.GetPlayerPhysics();
            var playerImage = gameController.GetPlayerImageName();

            if (playerPhysics.IsCrouching)
                g.DrawImage(GetImage(playerImage), -265 + playerPhysics.PositionAndSize.Position.X * 180,
                    455 + playerPhysics.PositionAndSize.Position.Y * 90,
                    playerPhysics.PositionAndSize.Size.Width * 180, playerPhysics.PositionAndSize.Size.Height * 140);

            else
                g.DrawImage(GetImage(playerImage), -265 + playerPhysics.PositionAndSize.Position.X * 180,
                    405 + playerPhysics.PositionAndSize.Position.Y * 90,
                    playerPhysics.PositionAndSize.Size.Width * 180, playerPhysics.PositionAndSize.Size.Height * 140);
        }

        private static Image GetImage(ImageName imageName)
        {
            switch (imageName)
            {
                case ImageName.Berries:
                    return Resources.berries;
                case ImageName.Corn:
                    return Resources.corn;
                case ImageName.Nut:
                    return Resources.nut;
                case ImageName.Stone1:
                    return Resources.stone1;
                case ImageName.Stone2:
                    return Resources.stone2;
                case ImageName.Stump:
                    return Resources.stump;
                case ImageName.Bush:
                    return Resources.bush;
                case ImageName.Totoro:
                    return Resources.totoro;
                default:
                    throw new ArgumentOutOfRangeException(nameof(imageName), imageName, null);
            }
        }

        private void OnKeyBoardDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    gameController.SitDown();
                    break;
            }
        }

        private void OnKeyBoardUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    gameController.Jump();
                    break;
            }
        }

        private void OnKeyBoardSpace(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    gameController.GetFood();
                    break;
            }
        }
    }
}