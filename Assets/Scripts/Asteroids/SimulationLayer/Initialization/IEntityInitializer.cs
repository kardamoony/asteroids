using System;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Initialization
{
    public interface IEntityInitializer
    {
        event Action<IEntity> OnEntityInitialized;
        event Action<IEntity> OnEntityDenitialized;
        
        void InitializeEntity(IEntity entity, IEntityView entityView);
        void DeinitializeEntity(IEntity entity);
    }
}