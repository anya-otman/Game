using System.Drawing;

namespace Game.Classes
{
    public interface IGameObject
    {
        Transform transform { get; set; }
        bool DoesNeedGetNewRoad();
        Image Image { get; set; }
        gameClasses Name { get; set; }
    }
}