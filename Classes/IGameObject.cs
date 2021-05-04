using System.Drawing;

namespace Game.Classes
{
    public abstract class IGameObject
    {
        public abstract Transform Transform { get; set; }
        public abstract ImageName ImageName { get; set; }
        public abstract GameClass ObjectName { get; }
    }
}