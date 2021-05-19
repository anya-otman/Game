using System;
using System.Windows.Forms;
using System.Drawing;
using Logic;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        private readonly Timer mainTimer;
        private GameController gameController;
        private int timerCount;
        private const int ImageSize = 90;
        private int animationCount;
        private readonly int maxAnimationCount;
        private static Images images;
        private const int MaxTimerCount = 30;

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
            if (timerCount == MaxTimerCount)
            {
                gameController.ChangeState();
                timerCount = 0;
            }
            animationCount++;
            if (animationCount == maxAnimationCount)
                animationCount = 0;
            gameController.DoThisMethodEveryGameTick();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawObjects(g);
            DrawString(g);
        }
        
        private void DrawString(Graphics e)
        {
            var point = new Point(50, 45);
            e.DrawString("Score " + gameController.GetScore(), new Font("Thintel", 100, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Chartreuse, point);
        }

        private void DrawObjects(Graphics g)
        {
            foreach (var gameObject in gameController.GetGameObjectList())
            {
                if (gameObject.PositionAndSize.Position.X > 1800 / ImageSize)
                    continue;
                g.DrawImage(GetImage(gameObject.TypeName),
                    -ImageSize + gameObject.PositionAndSize.Position.X * ImageSize - timerCount * ImageSize / MaxTimerCount, 580);
            }

            for (int i = 0; i < gameController.GetLife(); i++)
            {
                g.DrawImage(images.Life, 750 + 70 * i, 60);
            }

            var playerPhysics = gameController.GetPlayerPhysics();
            var playerImage = gameController.GetPlayerImageName();

            if (playerPhysics.IsCrouching)
                g.DrawImage(GetImage(playerImage), -ImageSize + playerPhysics.PositionAndSize.Position.X * ImageSize,
                    405 + playerPhysics.PositionAndSize.Position.Y * ImageSize);

            else if (animationCount < maxAnimationCount / 2)
                g.DrawImage(GetImage(gameController.GetPlayerImageName()),
                    -ImageSize + playerPhysics.PositionAndSize.Position.X * ImageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * ImageSize);
            else
                g.DrawImage(GetImage(gameController.GetPlayerImageNameGo()),
                    -ImageSize + playerPhysics.PositionAndSize.Position.X * ImageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * ImageSize);
        }

        private static Image GetImage(TypeName typeName)
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
                    return images.Stone2;
                case TypeName.Stump:
                    return images.Stump;
                case TypeName.Bush:
                    return images.Bush;
                case TypeName.Totoro:
                    return images.Totoro;
                case TypeName.TotoroGo:
                    return images.TotoroGo;
                case TypeName.AppleCore:
                    return images.AppleCore;
                case TypeName.Mushroom:
                    return images.Mushroom;
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