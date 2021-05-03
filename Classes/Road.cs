using System.Drawing;


namespace Game.Classes
{
    public class Road 
    {
        public Transform transform { get; set; }
        public Image roadImage = Properties.Resources.road;

        public Road(PointF position)
        {
            transform = new Transform(new PointF(position.X, 750), new Size(100, 17));
        }

        public Transform Transform { get; set; }
        public bool DoesNeedGetNewRoad()
        {
            return true;
        }
    }
}