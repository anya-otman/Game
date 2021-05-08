using System.Drawing;

namespace Logic.Classes
{
    public class Road: IGameObject
    {
        public sealed override PositionAndSize PositionAndSize { get; set; }
        public sealed override ImageName ImageName { get; set; }
        public override GameClass ObjectName => GameClass.Road;

        public Road()
        {
            PositionAndSize = new PositionAndSize(new PointF(2100, 900), new Size(100, 17));
            ImageName = ImageName.Road;
        }
    }
}