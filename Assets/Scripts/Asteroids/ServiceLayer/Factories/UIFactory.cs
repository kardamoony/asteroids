using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.MetaLayer.Initialization;
using Asteroids.MetaLayer.MVVM;
using Asteroids.MetaLayer.Views.StartView;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Factories
{
    public class UIFactory : IObjectsFactory<UIView>
    {
        private readonly IObjectsFactory<GameObject> _factory;
        private readonly IInitializer<UIView, IUIContext> _initializer;
        private readonly Transform _uiRoot;

        public UIFactory(IObjectsFactory<GameObject> factory, IInitializer<UIView, IUIContext> initializer, Transform uiRoot)
        {
            _factory = factory;
            _initializer = initializer;
            _uiRoot = uiRoot;
        }
        
        //TODO: fix this
        public void Get<T>(string id, Action<T> callback)
        {
            _factory.Get<T>(id, view =>
            {
                _initializer.InitializeObject(view as UIView, new UIContext{ Parent = _uiRoot });
                callback?.Invoke(view);
            });
        }

        public void Release(UIView obj, bool dispose)
        {
            _initializer.DeinitializeObject(obj);
            _factory.Release(obj.gameObject, dispose);
        }
    }
}