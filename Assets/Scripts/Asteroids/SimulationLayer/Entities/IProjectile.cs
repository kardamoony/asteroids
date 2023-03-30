namespace Asteroids.SimulationLayer.Entities
{
    public interface IProjectile
    {
        IMovable Movable { get; }
        ICollidable Collidable { get; }
        IDestructable Destructable { get; }
    }
}