using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization
{
    public class EntityInitializer : IEntityInitializer
    {
        private readonly IInitializationHandler _handler;

        public EntityInitializer(IInitializationHandler[] handlers)
        {
            _handler = handlers[0];
            
            for (var i = 0; i < handlers.Length - 1; i++)
            {
                handlers[i].Next = handlers[i + 1];
            }
        }
        
        public void InitializeEntity(IEntity entity, IEntityView entityView)
        {
            entity.EntityView = entityView;
            var viewComponents = entityView.GetComponents();
  
            foreach (var component in viewComponents)
            {
                _handler.HandleInitialization(entity, component);
            }
        }

        public void DeinitializeEntity(IEntity entity)
        {
            var viewComponents = entity.EntityView.GetComponents();

            foreach (var component in viewComponents)
            {
                component.ClearContext();
            }
            
            _handler.HandleDeinitialization(entity);
            entity.EntityView = null;
        }
    }
}