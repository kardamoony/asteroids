using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class CollisionInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        public void HandleInitialization(IEntity entity, IEntityComponent component)
        {
            if (entity is ICollidable collidable && component is CollisionComponent collisionComponent)
            {
                collisionComponent.SetContext(collidable);
                return;
            }
            
            Next?.HandleInitialization(entity, component);
        }

        public void HandleDeinitialization(IEntity entity)
        {
            Next?.HandleDeinitialization(entity);
        }
    }
}