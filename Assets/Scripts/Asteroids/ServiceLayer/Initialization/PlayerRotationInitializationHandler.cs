using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;

namespace Asteroids.ServiceLayer.Initialization
{
    public class PlayerRotationInitializationHandler : IInitializationHandler
    {
        public IInitializationHandler Next { get; set; }
        
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is IRotatable rotatable && component is RotationComponent movementComponent)
            {
                movementComponent.SetContext(rotatable);
                var inputProvider = IoC.Instance.Resolver.Resolve<IInputProvider>();
                IoC.Instance.Resolver.Resolve<RotationSystem>().Register(rotatable, inputProvider);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            if (entity is IRotatable rotatable)
            {
                IoC.Instance.Resolver.Resolve<RotationSystem>().Unregister(rotatable);
                return;
            }
            
            Next.HandleDeinitialization(entity);
        }
    }
}