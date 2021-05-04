using System.Drawing;

namespace Game.Classes
{
    public interface IGameObject
    {
        Transform Transform { get; set; }
        Image Image { get; }
        GameClass ObjectName { get; }
    }
}