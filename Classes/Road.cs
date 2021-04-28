using System.Drawing;


namespace Game.Classes
{
    public class Road : IGameObject
    {
        public Transform transform;
        public Image roadImage = Properties.Resources.road;

        public Road(PointF position, Size size)
        {
            transform = new Transform(position, size);
        }

        public Transform Transform { get; set; }
    }
}