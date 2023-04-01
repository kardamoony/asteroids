using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.IoC;
using Asteroids.ServiceLayer.Initialization.Handlers;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Initialization
{
    public class EntityInitializer : IEntityInitializer
    {
        public event Action<IEntity> OnEntityInitialized;
        public event Action<IEntity> OnEntityDenitialized;
        
        private readonly IInitializationHandler _handler;
        private readonly IObjectsFactory<GameObject> _factory;

        public EntityInitializer(IInitializationHandler[] handlers, IObjectsFactory<GameObject> factory)
        {
            _factory = factory;
            _handler = handlers[0];
            
            for (var i = 0; i < handlers.Length - 1; i++)
            {
                handlers[i].Next = handlers[i + 1];
            }
        }

        public void InitializeEntity(IEntity entity, IEntityView entityView)
        {
            if (entity.Initialized)
            {
                return;
            }
            
            entity.Initialize(entityView);
            
            var viewComponents = entityView.GetComponents();
  
            foreach (var component in viewComponents)
            {
                _handler.HandleInitialization(entity, component);
            }
            
            entity.InitializationTime = DateTime.Now;

            IoC.Instance.Resolver.Resolve<EntityLifespanSystem>().Register(entity);
            OnEntityInitialized?.Invoke(entity);
        }

        public void DeinitializeEntity(IEntity entity)
        {
            IoC.Instance.Resolver.Resolve<EntityLifespanSystem>().Unregister(entity);

            if (!entity.Initialized)
            {
                return;
            }
            
            var viewComponents = entity.EntityView.GetComponents();

            foreach (var component in viewComponents)
            {
                component.ClearContext();
            }
            
            _handler.HandleDeinitialization(entity);
            _factory.Release(entity.EntityView.GameObject);
            
            entity.Denitialize();
            OnEntityDenitialized?.Invoke(entity);
        }
    }
}