using System;
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
            Text = "Totoro - life: " + gameController.GetLife() +
                   //" timeInterval: " + mainTimer.Interval + 
                   " score" + gameController.GetScore() +
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
                //нужно убратьб дорогу из логики и тогда вот этот if не будет нужен
                if (gameObject.ObjectName == GameClass.Road)
                    g.DrawImage(GetImage(gameObject.ImageName), gameObject.PositionAndSize.position.X,
                        gameObject.PositionAndSize.position.Y,
                        gameObject.PositionAndSize.size.Width, gameObject.PositionAndSize.size.Height);
                else
                    g.DrawImage(GetImage(gameObject.ImageName), -265 + gameObject.PositionAndSize.position.X * 180, 690,
                        gameObject.PositionAndSize.size.Width * 90, gameObject.PositionAndSize.size.Height * 90);
            }

            var playerPhysics = gameController.GetPlayerPhysics();
            var playerImage = gameController.GetPlayerImageName();
            if (playerPhysics.IsCrouching)

                g.DrawImage(GetImage(playerImage), -265 + playerPhysics.positionAndSize.position.X * 180,
                    455 + playerPhysics.positionAndSize.position.Y * 90,
                    playerPhysics.positionAndSize.size.Width * 180, playerPhysics.positionAndSize.size.Height * 140);

            else
                g.DrawImage(GetImage(playerImage), -265 + playerPhysics.positionAndSize.position.X * 180,
                    405 + playerPhysics.positionAndSize.position.Y * 90,
                    playerPhysics.positionAndSize.size.Width * 180, playerPhysics.positionAndSize.size.Height * 140);
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
                case ImageName.Road:
                    return Resources.road;
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