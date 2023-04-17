using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.IoC;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Factories
{
    public class EntityFactory : IObjectsFactory<IEntity>
    {
        private readonly IInitializer<IEntity, IEntityView> _initializer;
        private readonly IObjectsFactory<GameObject> _factory;

        public EntityFactory(IObjectsFactory<GameObject> factory, IInitializer<IEntity, IEntityView> initializer)
        {
            _factory = factory;
            _initializer = initializer;
        }
        
        public void Get<T>(string id, Action<T> callback, params object[] args)
        {
            _factory.Get<IEntityView>(id, view =>
            {
                var product = Locator.Instance.Resolver.Resolve<T>(args);
                var entity = product as IEntity;
                _initializer.InitializeObject(entity, view);
                callback?.Invoke(product);
            });
        }

        public void Release(IEntity obj, bool dispose)
        {
            _initializer.DeinitializeObject(obj, dispose);
        }
    }
}