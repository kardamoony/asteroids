using Asteroids.CoreLayer.IoC;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;

namespace Asteroids.ServiceLayer.Initialization.Handlers
{
    public class AsteroidSpawnerInitializationHandler : IInitializationHandler
    {
        public IInitializationHandler Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is AsteroidSpawner spawner && component is SpawnerComponent spawnerComponent)
            {
                spawnerComponent.SetContext(spawner);
                IoC.Instance.Resolver.Resolve<AsteroidSpawnSystem>().Register(spawner);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is AsteroidSpawner spawner)
            {
                IoC.Instance.Resolver.Resolve<AsteroidSpawnSystem>().Unregister(spawner);
                return;
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}