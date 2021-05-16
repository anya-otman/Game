using System.Drawing;

namespace Logic
{
    public class Bird : IGameObject
    {
        private PositionAndSize positionAndSize;

        public Bird(Point position)
        {
            positionAndSize = new PositionAndSize(position, new SizeF(1,1));
            TypeName = TypeName.Bird1;
            TypeName2 = TypeName.Bird2;
            TypeName3 = TypeName.Bird3;
            TypeName4 = TypeName.Bird4;
            TypeName5 = TypeName.Bird5;
        }

        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; }
        public TypeName TypeName2 { get; }
        public TypeName TypeName3 { get; }
        public TypeName TypeName4 { get; }
        public TypeName TypeName5 { get; }
        public GameClass ObjectName => GameClass.Bird;
    }
}
