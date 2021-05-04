using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Game.Classes;
using System.Drawing;
using Game.Properties;

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
            //KeyUp += OnKeyBoardSpace;
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
                    g.DrawImage(GetImage(gameObject.ImageName), gameObject.Transform.position.X,gameObject.Transform.position.Y,
                        gameObject.Transform.size.Width, gameObject.Transform.size.Height);
                else
                    g.DrawImage(GetImage(gameObject.ImageName), -265 + gameObject.Transform.position.X*180,690,
                        gameObject.Transform.size.Width*90, gameObject.Transform.size.Height*90);
            }

            g.DrawImage(player.Image, -265 + player.Physics.transform.position.X * 180, 495 + player.Physics.transform.position.Y * 475,
                player.Physics.transform.size.Width*180, player.Physics.transform.size.Height*140);
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

        /*private void OnKeyBoardSpace(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (player.physics.CanTotoroTakeFood(player))
                    {
                        player.score += 10;
                        GameController.gameObjects.RemoveAt(player.physics.GetIndexOfFoodNearTotoro(player));
                    }

                    break;
            }
        }*/
    }
}