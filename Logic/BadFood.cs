using System;
using System.Drawing;

namespace Logic
{
    public class BadFood : IGameObject
    {
        public PositionAndSize PositionAndSize { get; }
        public TypeName TypeName { get; private set; }
        public GameClass ObjectName => GameClass.BadFood;

        public BadFood(Point point, TypeName typeName)
        {
            PositionAndSize = new PositionAndSize(point, new SizeF(0.9f, 0.9f));
            TypeName = typeName;
        }
    }
}