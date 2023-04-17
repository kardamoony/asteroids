using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Factories;
using Asteroids.IoC;
using Asteroids.UILayer.MVVM;
using Asteroids.UILayer.UISystem;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Initialization.Strategies
{
    public abstract class InitializationStrategy : IInitializationStrategy
    {
        private HashSet<Type> _registeredTypes = new HashSet<Type>();
        private HashSet<Type> _addedViews = new HashSet<Type>();
        
        protected GameObject PoolParent;

        protected IUISystem UISystem => Locator.Instance.Resolver.Resolve<IUISystem>();

        protected InitializationStrategy(GameObject poolParent)
        {
            PoolParent = poolParent;
        }

        public abstract void Initialize();

        public abstract void Deinitialize();
        
        protected void RegisterConstructor<T>(Func<object[], object> constructor)
        {
            Locator.Instance.Container.Register<T>(constructor);
            _registeredTypes.Add(typeof(T));
        }
        
        protected void RegisterInstance<T>(T instance)
        {
            Locator.Instance.Container.RegisterInstance(instance);
            _registeredTypes.Add(typeof(T));
        }

        protected void UnregisterDependencies()
        {
            foreach (var type in _registeredTypes)
            {
                Locator.Instance.Container.Unregister(type);
            }
            
            _registeredTypes.Clear();
        }

        protected void AddUIView<T>(string assetId, bool show) where T : UIView
        {
            Locator.Instance.Resolver.Resolve<IUISystem>().AddView<T>(assetId, show);
            _addedViews.Add(typeof(T));
        }
        
        protected void RemoveViews()
        {
            foreach (var type in _addedViews)
            {
                UISystem.RemoveView(type);
            }
            
            _addedViews.Clear();
        }
        
        protected void ClearPool()
        {
            var factory = Locator.Instance.Resolver.Resolve<IObjectsFactory<GameObject>>();
            var pooledObjects = PoolParent.GetComponentsInChildren<PoolBehaviour>(true);

            foreach (var t in pooledObjects)
            {
                var gameObject = t.gameObject;
                
                if (gameObject.Equals(PoolParent))
                {
                    continue;
                }
                
                factory.Release(gameObject, true);
            }
        }
    }
}