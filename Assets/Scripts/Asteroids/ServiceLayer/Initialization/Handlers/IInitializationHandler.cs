using Asteroids.SimulationLayer.Entities;

namespace Asteroids.ServiceLayer.Initialization.Handlers
{
    public interface IInitializationHandler
    {
        IInitializationHandler Next { set; }

        void HandleInitialization(IEntity entity, IEntityComponent component);
        void HandleDeinitialization(IEntity entity);
    }
}