using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Factories
{
    public class EntityFactory : IObjectsFactory<IEntity>
    {
        private readonly IEntityInitializer _initializer;
        
        public EntityFactory(IEntityInitializer initializer)
        {
            _initializer = initializer;
        }
        
        public void Get<T>(string id, Action<T> callback)
        {
            
        }

        public void Release(IEntity obj)
        {
            
        }
    }
}