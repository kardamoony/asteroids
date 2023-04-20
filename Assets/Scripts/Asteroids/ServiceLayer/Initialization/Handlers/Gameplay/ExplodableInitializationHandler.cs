using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class ExplodableInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity @object, IEntityComponent context)
        {
            if (@object is IExplodable explodable && context is ExplodableComponent)
            {
                IoC.Locator.Instance.Resolver.Resolve<ExplosionSpawnSystem>().Register(explodable);
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(IEntity @object)
        {
            if (@object is IExplodable explodable)
            {
                IoC.Locator.Instance.Resolver.Resolve<ExplosionSpawnSystem>().Unregister(explodable);
            }
            
            Next?.HandleDeinitialization(@object);
        }
    }
}