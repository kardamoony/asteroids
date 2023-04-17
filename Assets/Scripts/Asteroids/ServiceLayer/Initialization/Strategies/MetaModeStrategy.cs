using Asteroids.CoreLayer.Factories;
using Asteroids.IoC;
using Asteroids.UILayer.Initialization;
using Asteroids.UILayer.MVVM;
using Asteroids.UILayer.UISystem;
using Asteroids.UILayer.Views.StartView;
using Asteroids.ServiceLayer.Factories;
using Asteroids.ServiceLayer.Initialization.Handlers.Meta;
using Asteroids.SimulationLayer.Initialization;
using Generated;
using UnityEngine;

namespace Asteroids.ServiceLayer.Initialization.Strategies
{
    public class MetaModeStrategy : InitializationStrategy
    {
        private Transform _uiRoot;
        
        public MetaModeStrategy(Transform uiRoot, GameObject poolParent) : base(poolParent)
        {
            _uiRoot = uiRoot;
        }
        
        public override void Initialize()
        {
            RegisterDependencies();
            CreateStartView();
        }

        public override void Deinitialize()
        {
        }

        private void RegisterDependencies()
        {
            var gameObjectsFactory = Locator.Instance.Resolver.Resolve<IObjectsFactory<GameObject>>();
            var gameplayInitStrategy = Locator.Instance.Resolver.Resolve<SinglePlayerModeStrategy>();

            var uiInitializer = new UIInitializer(new IInitializationHandler<UIView, IUIContext>[]
            {
                new StartViewInitializationHandler(),
                new AttemptsViewInitializationHandler(), 
                new ScoreViewInitializationHandler(),
                new GameoverViewInitializationHandler()
            });

            var uiFactory = new UIFactory(gameObjectsFactory, uiInitializer, _uiRoot);
            
            RegisterInstance<IUISystem>(new UISystem(uiFactory));
            RegisterInstance(new StartModel(gameplayInitStrategy));
        }

        private void CreateStartView()
        {
            AddUIView<StartView>(AssetId.StartView.ToString(), true);
        }
    }
}