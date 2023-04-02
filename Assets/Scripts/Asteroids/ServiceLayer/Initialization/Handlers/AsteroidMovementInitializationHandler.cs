using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;

namespace Asteroids.ServiceLayer.Initialization.Handlers
{
    public class AsteroidMovementInitializationHandler : IInitializationHandler
    {
        public IInitializationHandler Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is Asteroid asteroid && component is MovementComponent movementComponent)
            {
                movementComponent.SetContext(asteroid);
                var input = IoC.Instance.Resolver.Resolve<ConstantInputProvider>();
                input.VerticalAxis = 1f;
                
                IoC.Instance.Resolver.Resolve<ConstantMovementSystem>().Register(asteroid, input);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is Asteroid movable)
            {
                IoC.Instance.Resolver.Resolve<ConstantMovementSystem>().Unregister(movable);
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}