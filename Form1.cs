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
                if (gameObject.PositionAndSize.Position.X > 11)
                    continue;
                g.DrawImage(GetImage(gameObject.TypeName), -265 + gameObject.PositionAndSize.Position.X * 180 - timerCount*6, 580,
                        gameObject.PositionAndSize.Size.Width * 90, gameObject.PositionAndSize.Size.Height * 90);
            }

            var playerPhysics = gameController.GetPlayerPhysics();
            var playerImage = gameController.GetPlayerImageName();

            if (playerPhysics.IsCrouching)
                g.DrawImage(GetImage(playerImage), -265 + playerPhysics.PositionAndSize.Position.X * 180,
                    355 + playerPhysics.PositionAndSize.Position.Y * 90,
                    playerPhysics.PositionAndSize.Size.Width * 180, playerPhysics.PositionAndSize.Size.Height * 140);

            else
                g.DrawImage(GetImage(playerImage), -265 + playerPhysics.PositionAndSize.Position.X * 180,
                    300 + playerPhysics.PositionAndSize.Position.Y * 90,
                    playerPhysics.PositionAndSize.Size.Width * 180, playerPhysics.PositionAndSize.Size.Height * 140);
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