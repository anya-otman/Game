using System.Drawing;


namespace Game.Classes
{
    public class Road
    {
        public Transform transform;

        public Road(PointF position, Size size)
        {
            transform = new Transform(position, size);
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(Properties.Resources.road, 2100, 112, 100, 17);
        }
    }
}