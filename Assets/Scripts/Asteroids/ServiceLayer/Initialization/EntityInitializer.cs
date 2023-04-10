using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Initialization
{
    public class EntityInitializer : IInitializer<IEntity, IEntityView>
    {
        public event Action<IEntity> OnObjectInitialized;
        public event Action<IEntity> OnObjectDenitialized;
        
        private readonly IInitializationHandler<IEntity, IEntityComponent> _handler;
        private readonly IObjectsFactory<GameObject> _factory;

        public EntityInitializer(IInitializationHandler<IEntity, IEntityComponent>[] handlers, IObjectsFactory<GameObject> factory)
        {
            _factory = factory;
            _handler = handlers[0];
            
            for (var i = 0; i < handlers.Length - 1; i++)
            {
                handlers[i].Next = handlers[i + 1];
            }
        }

        public void InitializeObject(IEntity entity, IEntityView entityView)
        {
            if (entity.Initialized)
            {
                return;
            }

            var viewComponents = entityView.GetComponents();
  
            foreach (var component in viewComponents)
            {
                _handler.HandleInitialization(entity, component);
            }
            
            entity.InitializationTime = DateTime.Now;
            entity.Initialize(entityView);
            entityView.GameObject.SetActive(true);

            IoC.Locator.Instance.Resolver.Resolve<EntityLifespanSystem>().Register(entity);
            OnObjectInitialized?.Invoke(entity);
        }

        public void DeinitializeObject(IEntity @object)
        {
            if (!@object.Initialized)
            {
                return;
            }
            
            IoC.Locator.Instance.Resolver.Resolve<EntityLifespanSystem>().Unregister(@object);
            
            _handler.HandleDeinitialization(@object);

            var viewComponents = @object.EntityView.GetComponents();

            foreach (var component in viewComponents)
            {
                component.ClearContext();
            }

            _factory.Release(@object.EntityView.GameObject, false);
            @object.Denitialize();
            OnObjectDenitialized?.Invoke(@object);
        }
    }
}