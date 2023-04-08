using Asteroids.CoreLayer.Input;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;

namespace Asteroids.ServiceLayer.Initialization.Handlers
{
    public class ProjectileMovementInitializationHandler : IInitializationHandler
    {
        public IInitializationHandler Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is IProjectile projectile && component is MovementComponent movementComponent)
            {
                movementComponent.SetContext(projectile.Movable);
                var input = IoC.Locator.Instance.Resolver.Resolve<ConstantInputProvider>();
                input.VerticalAxis = 1f;
                
                IoC.Locator.Instance.Resolver.Resolve<ConstantMovementSystem>().Register(projectile.Movable, input);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is IProjectile projectile)
            {
                IoC.Locator.Instance.Resolver.Resolve<ConstantMovementSystem>().Unregister(projectile.Movable);
            }

            Next.HandleDeinitialization(entity);
        }
    }
}