using Asteroids.CoreLayer.Factories;
using Asteroids.IoC;
using Asteroids.MetaLayer.MVVM;
using Asteroids.MetaLayer.Views.StartView;
using Asteroids.ServiceLayer.Factories;
using Asteroids.ServiceLayer.Initialization.Handlers.Meta;
using Asteroids.SimulationLayer.Initialization;
using Generated;
using UnityEngine;

namespace Asteroids.ServiceLayer.Initialization.Strategies
{
    public class MetaInitializationStrategy : IInitializationStrategy
    {
        private Transform _uiRoot;
        
        public MetaInitializationStrategy(Transform uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        public void Initialize()
        {
            RegisterDependencies();
            CreateStartView();
        }

        public void Deinitialize()
        {
            //TODO: deinitialization
        }

        private void RegisterDependencies()
        {
            var gameObjectsFactory = Locator.Instance.Resolver.Resolve<IObjectsFactory<GameObject>>();
            var gameplayInitStrategy = Locator.Instance.Resolver.Resolve<GameplayInitializationStrategy>();

            var uiInitializer = new UIInitializer(new[]
            {
                new StartViewInitializationHandler(),
            });

            var uiFactory = new UIFactory(gameObjectsFactory, uiInitializer, _uiRoot);
            
            Locator.Instance.Container.RegisterInstance<IObjectsFactory<UIView>>(uiFactory);
            Locator.Instance.Container.RegisterInstance(new StartModel(gameplayInitStrategy));
        }

        private void CreateStartView()
        {
            var factory = Locator.Instance.Resolver.Resolve<IObjectsFactory<UIView>>();
            factory.Get<StartView>(AssetId.StartView.ToString(), view => { view.Show(); });
        }
    }
}