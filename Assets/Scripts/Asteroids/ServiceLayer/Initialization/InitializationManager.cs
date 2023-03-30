using System.Collections.Generic;
using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.CoreLayer.Services;
using Asteroids.ServiceLayer.Initialization;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Strategies;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Scene
{
    public class InitializationManager : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private AssetsMap _assetsMap;

        private void RegisterDependencies()
        {
            var container = new MinimalisticIoCContainer();
            
            var addressableService = new AddressableService(_assetsMap);
            var rotation = new Rotation();
            var playerInputProvider = GetComponentInChildren<IInputProvider>();
            var gameObjectsFactory = new GameObjectsFactory(addressableService, transform);
            
            IoC.Instance.SetContainer(container).SetResolver(container);
            
            //assets management
            container.RegisterInstance<IAssetsMap>(_assetsMap);
            container.RegisterInstance<IAddressableService>(addressableService);
            container.RegisterInstance<IObjectsFactory<GameObject>>(gameObjectsFactory);
            
            //initialization
            var initializer = new EntityInitializer(new IInitializationHandler[]
            {
                new PlayerMovementInitializationHandler(),
                new PlayerRotationInitializationHandler(),
                new CollisionInitializationHandler(),
                new AsteroidMovementInitializationHandler(),
                new ProjectileMovementInitializationHandler(),
                new ProjectileSpawnerInitializationHandler()
            });
            
            container.RegisterInstance<IEntityInitializer>(initializer);
            
            //input
            container.RegisterInstance(playerInputProvider);
            container.RegisterInstance(new ConstantInputProvider{VerticalAxis = 1f});
            
            //settings
            container.RegisterInstance<IPlayerSettings>(_playerSettings);
            
            //systems
            container.RegisterInstance(new ConstantMovementSystem());
            container.RegisterInstance(new ThrustMovementSystem(_playerSettings));
            container.RegisterInstance(new RotationSystem(rotation));
            container.RegisterInstance(new ProjectileSpawnSystem(gameObjectsFactory, initializer));

            //entities
            container.Register<IPlayer>(args => new Player((IPlayerSettings)args[0]));
            container.Register<IProjectile>(args => new Projectile(20f));
            container.Register<Asteroid>(args => new Asteroid((float)args[0]));
            
            //entity strategies
            container.Register<ThrustMovement>(args => new ThrustMovement((float)args[0], (float)args[1], (float)args[2]));
        }
        
        private void CreatePlayer()
        {
            IoC.Instance.Resolver.Resolve<IObjectsFactory<GameObject>>().Get<IEntityView>(AssetId.Player.ToString(), o =>
            {
                var settings = IoC.Instance.Resolver.Resolve<IPlayerSettings>();
                var player = IoC.Instance.Resolver.Resolve<IPlayer>(settings);

                IoC.Instance.Resolver.Resolve<IEntityInitializer>().InitializeEntity((IEntity)player, o);
            });
        }

        [ContextMenu("CreateAsteroid")]
        private void CreateAsteroid()
        {
            IoC.Instance.Resolver.Resolve<IObjectsFactory<GameObject>>()
                .Get<IEntityView>(AssetId.Asteroid.ToString(), o =>
                {
                    var asteroid = new Asteroid(2);
                    IoC.Instance.Resolver.Resolve<IEntityInitializer>().InitializeEntity(asteroid, o);
                });
        }

        private void CreateSceneDirector()
        {
            var director = gameObject.AddComponent<SceneDirector>();

            var updateSystems = new List<IUpdateSystem>()
            {
                IoC.Instance.Resolver.Resolve<RotationSystem>(),
                IoC.Instance.Resolver.Resolve<ProjectileSpawnSystem>()
            };

            var fixedUpdateSystems = new List<IFixedUpdateSystem>
            {
                IoC.Instance.Resolver.Resolve<ThrustMovementSystem>(),
                IoC.Instance.Resolver.Resolve<ConstantMovementSystem>(),
            };

            director.Initialize(updateSystems, fixedUpdateSystems);
        }
       
        private void Start()
        {
            RegisterDependencies();
            CreatePlayer();
            CreateSceneDirector();
            
            IoC.Instance.Resolver.Resolve<IAddressableService>().Initialize();
        }
    }
}
