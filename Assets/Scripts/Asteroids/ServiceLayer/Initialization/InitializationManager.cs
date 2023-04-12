using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Services;
using Asteroids.IoC;
using Asteroids.ServiceLayer.Initialization.Strategies;
using Asteroids.ServiceLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Scene
{
    public class InitializationManager : MonoBehaviour
    {
        [SerializeField] private AssetsMap _assetsMap;
        [SerializeField] private GameplaySettings _gameplaySettings;
        
        [Space]
        [SerializeField] private Transform _poolParent;
        [SerializeField] private Transform _uiRoot;

        private void InitializeIoc()
        {
            var container = new MinimalisticIoCContainer();
            Locator.Instance.SetContainer(container).SetResolver(container);
        }
        
        private void Start()
        {
            InitializeIoc();
            
            var addressableService = new AddressableService(_assetsMap);
            var gameObjectsFactory = new GameObjectsFactory(addressableService, _poolParent);
            var gameplayInitStrategy = new GameplayInitializationStrategy(_gameplaySettings);
            
            Locator.Instance.Container.RegisterInstance<IAddressableService>(addressableService);
            Locator.Instance.Container.RegisterInstance<IObjectsFactory<GameObject>>(gameObjectsFactory);
            Locator.Instance.Container.RegisterInstance(gameplayInitStrategy);
            
            new MetaInitializationStrategy(_uiRoot).Initialize();
        }
    }
}
