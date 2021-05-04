using System.Drawing;


namespace Game.Classes
{
    public class Road: IGameObject
    {
        public Transform Transform { get; set; }
        public Image Image => Properties.Resources.road;
        public GameClass ObjectName => GameClass.Road;

        public Road()
        {
            Transform = new Transform(new PointF(2100, 900), new Size(100, 17));
        }
    }
}