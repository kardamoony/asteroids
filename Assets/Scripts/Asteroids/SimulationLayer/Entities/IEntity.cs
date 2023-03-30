namespace Asteroids.SimulationLayer.Entities
{
    public interface IEntity
    {
        IEntityView EntityView { get; set; }
    }
}