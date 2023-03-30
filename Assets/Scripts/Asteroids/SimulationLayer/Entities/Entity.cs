namespace Asteroids.SimulationLayer.Entities
{
    public abstract class Entity : IEntity
    {
        public IEntityView EntityView { get; set; }
    }
}