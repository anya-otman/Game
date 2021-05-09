namespace Logic.Classes
{
    public interface IGameObject
    {
        PositionAndSize PositionAndSize { get; set; }
        ImageName ImageName { get; set; }
        GameClass ObjectName { get; }
    }
}