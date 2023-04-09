using Asteroids.CoreLayer.Input;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class ProjectileSpawnerInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is IPlayer player && component is SpawnerComponent spawnerComponent)
            {
                spawnerComponent.SetContext(player.Spawner);
                var inputProvider = IoC.Locator.Instance.Resolver.Resolve<IInputProvider>();
                IoC.Locator.Instance.Resolver.Resolve<ProjectileSpawnSystem>().Register(player.Spawner, inputProvider);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is IPlayer player)
            {
                IoC.Locator.Instance.Resolver.Resolve<ProjectileSpawnSystem>().Unregister(player.Spawner);
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}