using Asteroids.CoreLayer.Input;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class AsteroidMovementInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is AsteroidEntity asteroid && component is MovementComponent movementComponent)
            {
                movementComponent.SetContext(asteroid);
                var input = IoC.Locator.Instance.Resolver.Resolve<ConstantInputProvider>();
                input.VerticalAxis = 1f;
                
                IoC.Locator.Instance.Resolver.Resolve<ConstantMovementSystem>().Register(asteroid, input);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is AsteroidEntity movable)
            {
                IoC.Locator.Instance.Resolver.Resolve<ConstantMovementSystem>().Unregister(movable);
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}