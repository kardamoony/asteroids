using Asteroids.CoreLayer.Input;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class PlayerRotationInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is IRotatable rotatable && component is RotationComponent movementComponent)
            {
                movementComponent.SetContext(rotatable);
                var inputProvider = IoC.Locator.Instance.Resolver.Resolve<IInputProvider>();
                IoC.Locator.Instance.Resolver.Resolve<RotationSystem>().Register(rotatable, inputProvider);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is IRotatable rotatable)
            {
                IoC.Locator.Instance.Resolver.Resolve<RotationSystem>().Unregister(rotatable);
            }
            
            Next?.HandleDeinitialization(entity);
        }
    }
}