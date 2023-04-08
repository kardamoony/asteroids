using Asteroids.CoreLayer.Input;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;

namespace Asteroids.ServiceLayer.Initialization.Handlers
{
    public class PlayerMovementInitializationHandler : IInitializationHandler
    {
        public IInitializationHandler Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is IPlayer player && component is MovementComponent movementComponent)
            {
                movementComponent.SetContext(player.Movable);
                var inputProvider = IoC.Locator.Instance.Resolver.Resolve<IInputProvider>();
                IoC.Locator.Instance.Resolver.Resolve<ThrustMovementSystem>().Register(player.Movable, inputProvider);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is IPlayer player)
            {
                IoC.Locator.Instance.Resolver.Resolve<ThrustMovementSystem>().Unregister(player.Movable);
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}