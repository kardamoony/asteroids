using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class EntityLifespanSystem : SimpleEntitySystem<IEntity>, IUpdateSystem
    {
        private readonly IObjectsFactory<IEntity> _factory;

        public EntityLifespanSystem(IObjectsFactory<IEntity> factory)
        {
            _factory = factory;
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
                    _factory.Release(entity, false);
                }
            });
        }
    }
}