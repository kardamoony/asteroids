using System;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class EntityLifespanSystem : SimpleEntitySystem<IEntity>, IUpdateSystem
    {
        private readonly IEntityInitializer _initializer;
        
        public EntityLifespanSystem(IEntityInitializer initializer)
        {
            _initializer = initializer;
        }
        
        public void Update(float deltaTime)
        {
            Entities.Update();
            
            Entities.Foreach(entity =>
            {
                if (entity.LifeTimeSpan.Equals(default))
                {
                    return;
                }
                
                var lifespan = DateTime.Now - entity.InitializationTime;
                
                if (lifespan > entity.LifeTimeSpan)
                {
                    _initializer.DeinitializeEntity(entity);
                }
            });
        }
    }
}