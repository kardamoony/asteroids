namespace Asteroids.SimulationLayer.Entities
{
    public interface IPlayer
    {
        IMovable Movable { get; }
        IRotatable Rotatable { get; }
        ICollidable Collidable { get; }
        ISpawner Spawner { get; }
        IDestructable Destructable { get; }
    }
}