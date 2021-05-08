namespace Logic.Classes
{
    public abstract class IGameObject
    {
        public abstract PositionAndSize PositionAndSize { get; set; }
        public abstract ImageName ImageName { get; set; }
        public abstract GameClass ObjectName { get; }
    }
}