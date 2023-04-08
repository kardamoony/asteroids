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
        private readonly IEntityInitializer _initializer;
        private readonly IObjectsFactory<GameObject> _factory;

        public EntityFactory(IObjectsFactory<GameObject> factory, IEntityInitializer initializer)
        {
            _factory = factory;
            _initializer = initializer;
        }
        
        public void Get<T>(string id, Action<T> callback)
        {
            _factory.Get<IEntityView>(id, view =>
            {
                var product = Locator.Instance.Resolver.Resolve<T>();
                var entity = product as IEntity;
                _initializer.InitializeEntity(entity, view);
                callback?.Invoke(product);
            });
        }

        public void Release(IEntity obj)
        {
            _initializer.DeinitializeEntity(obj);
            //TODO: entity pool
        }
    }
}