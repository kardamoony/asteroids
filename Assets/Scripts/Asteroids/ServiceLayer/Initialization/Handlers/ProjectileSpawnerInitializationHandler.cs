using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;

namespace Asteroids.ServiceLayer.Initialization.Handlers
{
    public class ProjectileSpawnerInitializationHandler : IInitializationHandler
    {
        public IInitializationHandler Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is IPlayer player && component is SpawnerComponent spawnerComponent)
            {
                spawnerComponent.SetContext(player.Spawner);
                var inputProvider = IoC.Instance.Resolver.Resolve<IInputProvider>();
                IoC.Instance.Resolver.Resolve<ProjectileSpawnSystem>().Register(player.Spawner, inputProvider);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is IPlayer player)
            {
                IoC.Instance.Resolver.Resolve<ProjectileSpawnSystem>().Unregister(player.Spawner);
                return;
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}