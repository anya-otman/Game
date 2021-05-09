namespace Logic.Classes
{
    public interface IGameObject
    {
        PositionAndSize PositionAndSize { get; }
        ImageName ImageName { get; }
        GameClass ObjectName { get; }
    }
}