using System;
using System.Windows.Forms;
using Game.Classes;

namespace Game
{
    public sealed partial class Form1 : Form
    {
        private Timer mainTimer;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawGame;
            mainTimer = new Timer();
            mainTimer.Interval = 5;
            mainTimer.Tick += Update;

            Init();
        }

        private void Init()
        {
            GameController.Init();
            mainTimer.Start();
            Invalidate();
        }

        private void Update(object sender, EventArgs e)
        {
            GameController.MoveMap();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            GameController.DrawObjects(g);
        }
    }
}

