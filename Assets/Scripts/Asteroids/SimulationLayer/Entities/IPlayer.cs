using Asteroids.SimulationLayer.Settings;

namespace Asteroids.SimulationLayer.Entities
{
    public interface IPlayer
    {
        IPlayerSettings Settings { get; }
        IMovable Movable { get; }
        IRotatable Rotatable { get; }
        ICollidable Collidable { get; }
    }
}