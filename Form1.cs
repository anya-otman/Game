using System;
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
        private readonly int imageSize;
        private int animationCount;
        private readonly int maxAnimationCount;
        private static Images images;

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
            imageSize = 90;
            maxAnimationCount = 26;
            Init();
        }

        private void Init()
        {
            gameController = new GameController();
            images = new Images();
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
                    -imageSize + gameObject.PositionAndSize.Position.X * imageSize - timerCount * 3, 580);
            }

            var playerPhysics = gameController.GetPlayerPhysics();
            var playerImage = gameController.GetPlayerImageName();

            if (playerPhysics.IsCrouching)
                g.DrawImage(GetImage(playerImage), -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    405 + playerPhysics.PositionAndSize.Position.Y * imageSize);

            else if (animationCount < maxAnimationCount / 2)
                g.DrawImage(GetImage(gameController.GetPlayerImageName()),
                    -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * imageSize);
            else
                g.DrawImage(GetImage(gameController.GetPlayerImageNameGo()),
                    -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * imageSize);
        }

        private Image GetImage(TypeName typeName)
        {
            switch (typeName)
            {
                case TypeName.Berries:
                    return images.Berries;
                case TypeName.Corn:
                    return images.Corn;
                case TypeName.Nut:
                    return images.Nut;
                case TypeName.Stone1:
                    return images.Stone1;
                case TypeName.Stone2:
                    return images.Stone2;;
                case TypeName.Stump:
                    return images.Stump;;
                case TypeName.Bush:
                    return images.Bush;;
                case TypeName.Totoro:
                    return images.Totoro;;
                case TypeName.TotoroGo:
                    return images.TotoroGo;;
                case TypeName.AppleCore:
                    return images.AppleCore;;
                case TypeName.Mushroom:
                    return images.Mushroom;;
                case TypeName.Bird1:
                    return images.Bird1;
                default:
                    throw new Exception();
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