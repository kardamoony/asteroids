using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.UILayer.Initialization;
using Asteroids.UILayer.MVVM;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Factories
{
    public class UIFactory : IObjectsFactory<UIView>
    {
        private readonly UIContext _defaultContext;
        private readonly IObjectsFactory<GameObject> _factory;
        private readonly IInitializer<UIView, IUIContext> _initializer;
        private readonly Transform _uiRoot;

        public UIFactory(IObjectsFactory<GameObject> factory, IInitializer<UIView, IUIContext> initializer, Transform uiRoot)
        {
            _factory = factory;
            _initializer = initializer;
            _uiRoot = uiRoot;
            _defaultContext = new UIContext { Parent = _uiRoot };
        }
        
        public void Get<T>(string id, Action<T> callback, params object[] args)
        {
            _factory.Get<T>(id, view =>
            {
                if (args.Length > 0 && args[0] is IUIContext context)
                {
                    context.Parent = _uiRoot;
                }
                else
                {
                    context = _defaultContext;
                }
                
                _initializer.InitializeObject(view as UIView, context);
                callback?.Invoke(view);
            });
        }

        public void Release(UIView obj, bool dispose)
        {
            _initializer.DeinitializeObject(obj, dispose);
            _factory.Release(obj.gameObject, dispose);
        }
    }
}