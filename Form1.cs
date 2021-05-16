using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using System.Drawing;
using Game.Properties;
using Logic;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        private readonly Timer mainTimer;
        private GameController gameController;
        private int timerCount;
        private int imageSize = 90;
        private int animationCount = 0;
        private readonly int maxAnimationCount = 26;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawGame;
            KeyUp += OnKeyBoardSpace;
            KeyUp += OnKeyBoardUp;
            KeyUp += OnKeyBoardDown;
            mainTimer = new Timer {Interval = 2};
            mainTimer.Tick += Update;
            timerCount = 0;
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
            timerCount++;
            if (timerCount == 30)
                timerCount = 0;
            if (timerCount == 0)
                gameController.ChangeState();
            animationCount++;
            if (animationCount == maxAnimationCount)
                animationCount = 0;
            gameController.DoThisMethodEveryGameTick();
            Text = "Totoro - Life: " + gameController.GetLife() +
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
                if (gameObject.PositionAndSize.Position.X > 20)
                    continue;
                g.DrawImage(GetImage(gameObject.TypeName),
                    -90 + gameObject.PositionAndSize.Position.X * 90 - timerCount * 3, 580,
                    gameObject.PositionAndSize.Size.Width * imageSize,
                    gameObject.PositionAndSize.Size.Height * imageSize);
            }

            var playerPhysics = gameController.GetPlayerPhysics();
            var playerImage = gameController.GetPlayerImageName();

            if (playerPhysics.IsCrouching)
                g.DrawImage(GetImage(playerImage), -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    405 + playerPhysics.PositionAndSize.Position.Y * imageSize,
                    playerPhysics.PositionAndSize.Size.Width * imageSize,
                    playerPhysics.PositionAndSize.Size.Height * imageSize);

            else if (animationCount < maxAnimationCount / 2)
                g.DrawImage(GetImage(gameController.GetPlayerImageName()),
                    -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * imageSize,
                    playerPhysics.PositionAndSize.Size.Width * imageSize,
                    playerPhysics.PositionAndSize.Size.Height * imageSize);
            else
                g.DrawImage(GetImage(gameController.GetPlayerImageNameGo()),
                    -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * imageSize,
                    playerPhysics.PositionAndSize.Size.Width * imageSize,
                    playerPhysics.PositionAndSize.Size.Height * imageSize);
        }

        private static Image GetImage(TypeName typeName)
        {
            switch (typeName)
            {
                case TypeName.Berries:
                    return Resources.berries;
                case TypeName.Corn:
                    return Resources.corn;
                case TypeName.Nut:
                    return Resources.nut;
                case TypeName.Stone1:
                    return Resources.stone1;
                case TypeName.Stone2:
                    return Resources.stone2;
                case TypeName.Stump:
                    return Resources.stump;
                case TypeName.Bush:
                    return Resources.bush;
                case TypeName.Totoro:
                    return Resources.totoro;
                case TypeName.TotoroGo:
                    return Resources.totoroGo;
                case TypeName.AppleCore:
                    return Resources.appleCore;
                case TypeName.Mushroom:
                    return Resources.mushroom;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeName), typeName, null);
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