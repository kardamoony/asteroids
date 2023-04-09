using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.IoC;
using Asteroids.MetaLayer.MVVM;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Factories
{
    public class UIFactory : IObjectsFactory<UIView>
    {
        private readonly IObjectsFactory<GameObject> _factory;
        private readonly IInitializer<UIView, UIModel> _initializer;
        private readonly Transform _uiRoot;

        public UIFactory(IObjectsFactory<GameObject> factory, IInitializer<UIView, UIModel> initializer, Transform uiRoot)
        {
            _factory = factory;
            _initializer = initializer;
            _uiRoot = uiRoot;
        }
        
        public void Get<T>(string id, Action<T> callback)
        {
            _factory.Get<UIView>(id, o =>
            {
                o.SetParent(_uiRoot);

                var model = Locator.Instance.Resolver.Resolve<T>();
                _initializer.InitializeObject(o, model as UIModel);
                callback?.Invoke(model);
            });
        }

        public void Release(UIView obj, bool dispose)
        {
            
        }
    }
}