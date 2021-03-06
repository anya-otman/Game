using System;
using System.Windows.Forms;
using System.Drawing;
using Logic;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        private const int maxBirdAnimationCount = 25;
        private const int imageSize = 90;
        private const int maxTickInAir = 60;

        private GameController gameController;
        private int maxTimerCount;
        private readonly Timer mainTimer;
        private int timerCount;
        private int animationCount;
        private int birdAnimationCount;
        private static Images images;
        private int maxAnimationCount;
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
            maxAnimationCount = 30;
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
            {
                maxTimerCount = 25;
                maxAnimationCount = 26;
            }

            if (gameController.GetScore() == 100)
            {
                maxTimerCount = 20;
                maxAnimationCount = 22;
            }

            timerCount++;
            if (timerCount == maxTimerCount)
            {
                gameController.ChangeState();
                timerCount = 0;
            }

            birdAnimationCount++;
            if (birdAnimationCount == maxBirdAnimationCount)
                birdAnimationCount = 0;
            animationCount++;
            if (animationCount == maxAnimationCount)
                animationCount = 0;
            gameController.UpdatePlayerPosition(maxTickInAir);
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            if (gameController.GetLife() == 0)
                DrawFinalState(g);
            DrawObjects(g);
            DrawPlayer(g);
            DrawScore(g);
        }

        private void DrawFinalState(Graphics e)
        {
            mainTimer.Stop();
            var point = new Point(480, 30);
            e.DrawString("Game Over",
                new Font("Thintel", 100, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Chartreuse, point);
            CreateRestartButton();
        }

        private void CreateRestartButton()
        {
            var button = new Button {Location = new Point(450, 300), Size = new Size(230, 100)};
            button.Click += ButtonClick;
            button.Text = @"Restart";
            button.ForeColor = Color.Chartreuse;
            button.Font = new Font("Thintel", 80, FontStyle.Bold, GraphicsUnit.Pixel);
            button.BackColor = Color.Peru;
            Controls.Add(button);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            gameController = new GameController();
            maxTimerCount = 30;
            mainTimer.Start();
            Controls.Clear();
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
                if (gameObject.PositionAndSize.Position.X > 1800 / imageSize)
                    continue;
                if (gameObject.TypeName == TypeName.Bird)
                    g.DrawImage(GetImage(gameObject.TypeName),
                        -imageSize + gameObject.PositionAndSize.Position.X * imageSize -
                        180 / maxTimerCount * timerCount,
                        400 + imageSize * gameObject.PositionAndSize.Position.Y);
                else
                    g.DrawImage(GetImage(gameObject.TypeName),
                        -imageSize + gameObject.PositionAndSize.Position.X * imageSize -
                        90 / maxTimerCount * timerCount,
                        400 + imageSize * gameObject.PositionAndSize.Position.Y);
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
                g.DrawImage(GetImage(playerImage), -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    405 + playerPhysics.PositionAndSize.Position.Y * imageSize,
                    imageSize, imageSize);
            else if (playerPhysics.IsJumping)
                g.DrawImage(GetImage(gameController.GetPlayerImageName()),
                    -imageSize + playerPhysics.PositionAndSize.Position.X * imageSize,
                    400 + playerPhysics.PositionAndSize.Position.Y * imageSize);
            else
                g.DrawImage(GetImage(gameController.GetPlayerImageName()),
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
                    return images.Stone2;
                case TypeName.Stump:
                    return images.Stump;
                case TypeName.Bush:
                    return images.Bush;
                case TypeName.Totoro:
                    if (animationCount < maxAnimationCount / 2)
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