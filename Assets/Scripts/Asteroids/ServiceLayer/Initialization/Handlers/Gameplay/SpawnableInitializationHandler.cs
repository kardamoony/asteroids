using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class SpawnableInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity @object, IEntityComponent context)
        {
            if (@object is ISpawnable spawnable && context is SpawnableComponent spawnableComponent)
            {
                spawnableComponent.SetContext(spawnable);
                return;
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(IEntity @object)
        {
            Next?.HandleDeinitialization(@object);
        }
    }
}