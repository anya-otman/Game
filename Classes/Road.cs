using System.Drawing;


namespace Game.Classes
{
    public class Road: IGameObject
    {
        public sealed override Transform Transform { get; set; }
        public sealed override ImageName ImageName { get; set; }
        public override GameClass ObjectName => GameClass.Road;

        public Road()
        {
            Transform = new Transform(new PointF(2100, 900), new Size(100, 17));
            ImageName = Classes.ImageName.Road;
        }
    }
}