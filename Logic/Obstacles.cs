using System.Drawing;

namespace Logic
{
    public class Obstacles : IGameObject
    {
        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; }
        public GameClass ObjectName => GameClass.Obstacles;

        public Obstacles(Point point, TypeName typeName)
        {
            PositionAndSize = new PositionAndSize(point, new Size(1,1));
            TypeName = typeName;
        }
    }
}