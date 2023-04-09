using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class PlayerSpawnerInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is PlayerSpawner spawner && component is SpawnerComponent spawnerComponent)
            {
                spawnerComponent.SetContext(spawner);
                IoC.Locator.Instance.Resolver.Resolve<PlayerSpawnSystem>().Register(spawner);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is PlayerSpawner spawner)
            {
                IoC.Locator.Instance.Resolver.Resolve<PlayerSpawnSystem>().Unregister(spawner);
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}