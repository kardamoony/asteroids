using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class DestructableInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is IDestructable destructable && component is DestructableComponent destructableComponent)
            {
                destructableComponent.SetContext(destructable);
                IoC.Locator.Instance.Resolver.Resolve<HealthSystem>().Register(destructable);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is IDestructable destructable)
            {
                IoC.Locator.Instance.Resolver.Resolve<HealthSystem>().Unregister(destructable);
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}