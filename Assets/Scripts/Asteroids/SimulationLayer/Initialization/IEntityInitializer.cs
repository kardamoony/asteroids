using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Initialization
{
    public interface IEntityInitializer
    {
        void InitializeEntity(IEntity entity, IEntityView entityView);
        void DeinitializeEntity(IEntity entity);
    }
}