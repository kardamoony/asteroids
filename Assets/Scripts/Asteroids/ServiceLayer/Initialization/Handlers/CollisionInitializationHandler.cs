using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.ServiceLayer.Initialization.Handlers
{
    public class CollisionInitializationHandler : IInitializationHandler
    {
        public IInitializationHandler Next { get; set; }
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
            
        }
    }
}