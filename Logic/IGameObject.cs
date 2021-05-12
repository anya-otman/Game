namespace Logic
{
    public interface IGameObject
    {
        PositionAndSize PositionAndSize { get; }
        TypeName TypeName { get; }
        GameClass ObjectName { get; }
    }
}