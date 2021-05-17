using System.Drawing;

namespace Logic
{
    public class Food : IGameObject
    {
        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; private set; }
        public GameClass ObjectName => GameClass.Food;

        public Food(Point point, TypeName typeName)
        {
            PositionAndSize = new PositionAndSize(point, new SizeF(1,1));
            TypeName = typeName;
        }
    }
}