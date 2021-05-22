using System;
using System.Windows.Forms;
using System.Drawing;
using Logic;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        private const int MaxBirdAnimationCount = 25;
        private const int MaxAnimationCount = 30;
        private const int ImageSize = 90;

        private GameController gameController;
        private int maxTimerCount;
        private readonly Timer mainTimer;
        private int timerCount;
        private int animationCount;
        private int birdAnimationCount;
        private static Images images;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawGame;
            KeyUp += OnKeyBoardSpace;
            KeyUp += OnKeyBoardUp;
            KeyUp += OnKeyBoardDown;
            mainTimer = new Timer {Interval = 10};
            mainTimer.Tick += Update;
            timerCount = -1;
            birdAnimationCount = 0;
            maxTimerCount = 30;
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
            if (gameController.GetScore() == 50)
                maxTimerCount = 25;
            if (gameController.GetScore() == 100)
                maxTimerCount = 20;
            timerCount++;
            if (timerCount == maxTimerCount)
            {
                gameController.ChangeState();
                timerCount = 0;
            }
            birdAnimationCount++;
            if (birdAnimationCount == MaxBirdAnimationCount)
                birdAnimationCount = 0;
            animationCount++;
            if (animationCount == MaxAnimationCount)
                animationCount = 0;
            gameController.UpdatePlayerPosition();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawFinalState(g);
            DrawObjects(g);
            DrawPlayer(g);
            DrawScore(g);
        }

        private void DrawFinalState(Graphics e)
        {
            if (gameController.GetLife() == 0)
            {
                mainTimer.Stop();
                var point = new Point(400, 45);
                e.DrawString("Game Over",
                    new Font("Thintel", 120, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Chartreuse, point);
            }
            
        }
        
        private void DrawScore(Graphics e)
        {
            var point = new Point(50, 45);
            e.DrawString("Score " + gameController.GetScore(),
                new Font("Thintel", 80, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Chartreuse, point);
        }

        private void DrawObjects(Graphics g)
        {
            foreach (var gameObject in gameController.GetGameObjectList())
            {
                if (gameObject.PositionAndSize.Position.X > 1800 / ImageSize)
                    continue;
                if (gameObject.TypeName == TypeName.Bird)
                    g.DrawImage(GetImage(gameObject.TypeName),
                        -ImageSize + gameObject.PositionAndSize.Position.X * ImageSize - 180 / maxTimerCount * timerCount,
                        400 + ImageSize * gameObject.PositionAndSize.Position.Y);
                else
                    g.DrawImage(GetImage(gameObject.TypeName),
                    -ImageSize + gameObject.PositionAndSize.Position.X * ImageSize - 90 / maxTimerCount * timerCount,
                    400 + ImageSize * gameObject.PositionAndSize.Position.Y);
            }

            for (int i = 0; i < gameController.GetLife(); i++)
            {
                g.DrawImage(images.Life, 750 + 70 * i, 60);
            }
        }

        private void DrawPlayer(Graphics g)
        {
            var playerPhysics = gameController.GetPlayerPhysics();
            var playerImage = gameController.GetPlayerImageName();

            if (playerPhysics.IsCrouching)
                g.DrawImage(GetImage(playerImage), -ImageSize + playerPhysics.PositionAndSize.Position.X * ImageSize,
                    405 + playerPhysics.PositionAndSize.Position.Y * ImageSize,
                    ImageSize, ImageSize);
            else if (playerPhysics.IsJumping)
                g.DrawImage(GetImage(gameController.GetPlayerImageName()),
                    -ImageSize + playerPhysics.PositionAndSize.Position.X * ImageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * ImageSize);
            else
                g.DrawImage(GetImage(gameController.GetPlayerImageName()),
                    -ImageSize + playerPhysics.PositionAndSize.Position.X * ImageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * ImageSize);
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
                    return images.Stone2;
                case TypeName.Stump:
                    return images.Stump;
                case TypeName.Bush:
                    return images.Bush;
                case TypeName.Totoro:
                    if (animationCount < MaxAnimationCount / 2)
                        return images.Totoro;
                    return images.TotoroGo;
                case TypeName.AppleCore:
                    return images.AppleCore;
                case TypeName.Mushroom:
                    return images.Mushroom;
                case TypeName.Bird:
                    if (birdAnimationCount < 5)
                        return images.Bird1;
                    if (birdAnimationCount < 10)
                        return images.Bird2;
                    if (birdAnimationCount < 15)
                        return images.Bird3;
                    if (birdAnimationCount < 20)
                        return images.Bird4;
                    return images.Bird5;
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