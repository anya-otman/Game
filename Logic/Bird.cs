using System.Drawing;

namespace Logic
{
    public class Bird : IGameObject
    {

        public Bird(Point position)
        {
            PositionAndSize = new PositionAndSize(position, new SizeF(1,1));
            TypeName = TypeName.Bird1;
        }

        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; }
        public GameClass ObjectName => GameClass.Bird;
    }
}
