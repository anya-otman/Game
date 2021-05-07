using System;
using System.Windows.Forms;
using System.Drawing;
using Game.Properties;
using Game.Classes;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        static Player player;
        private Timer mainTimer;
        private int time;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawGame;
            KeyUp += OnKeyBoardSpace;
            mainTimer = new Timer();
            mainTimer.Interval = 300;
            mainTimer.Tick += Update;

            Init();
        }

        private void Init()
        {
            GameController.Init();
            player = new Player();
            time = 0;
            mainTimer.Start();
            Invalidate();
        }

        private void Update(object sender, EventArgs e)
        {
            time += mainTimer.Interval;
            Text = "Totoro - life: " + player.Life + " time interval: " + mainTimer.Interval + " score" + player.Score + " foodCounter " + GameController.foodCounter +
                " obstaclesCounter " + GameController.obstaclesCounter;
            GameController.MoveMap();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawObjects(g);
        }

        private void DrawObjects(Graphics g)
        {
            foreach (var gameObject in GameController.gameObjects)
            {
                //нужно убратьб дорогу из логики и тогда вот этот if не будет нужен
                if (gameObject.ObjectName == GameClass.Road)
                    g.DrawImage(GetImage(gameObject.ImageName), gameObject.PositionAndSize.position.X,gameObject.PositionAndSize.position.Y,
                        gameObject.PositionAndSize.size.Width, gameObject.PositionAndSize.size.Height);
                else
                    g.DrawImage(GetImage(gameObject.ImageName), -265 + gameObject.PositionAndSize.position.X*180,690,
                        gameObject.PositionAndSize.size.Width*90, gameObject.PositionAndSize.size.Height*90);
            }

            g.DrawImage(GetImage(player.ImageName), -265 + player.Physics.positionAndSize.position.X * 180, 20 + player.Physics.positionAndSize.position.Y * 475,
                player.Physics.positionAndSize.size.Width*180, player.Physics.positionAndSize.size.Height*140);
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
        private void ShouldGameStop()
        {
            if (player.Life == 0)
                mainTimer.Stop();
        }

        /*private void UpdateCollision()
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

            if (player.physics.CanTotoroTakeFood(player) == false && player.physics.DoesTotoroTakeFood)
                player.physics.DoesTotoroTakeFood = false;
        }*/

        /*private void OnKeyBoardDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    player.physics.SitDown();
                    break;
            }
        }

        private void OnKeyBoardUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.physics.Jump();
                    break;
                case Keys.Down:
                    player.physics.isCrouching = false;
                    player.physics.transform.size.Height = 280;
                    player.physics.transform.position.Y = 496.2f;
                    break;
            }
        }*/

        private void OnKeyBoardSpace(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    GameController.player.Physics.GetFood();
                    break;
            }
        }
    }
}