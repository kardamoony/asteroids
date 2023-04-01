using System.Collections.Generic;
using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.CoreLayer.Services;
using Asteroids.ServiceLayer.Initialization.Handlers;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Scene;
using Asteroids.SimulationLayer.Settings;
using Asteroids.SimulationLayer.Strategies;
using Generated;
using UnityEngine;

namespace Asteroids.ServiceLayer.Initialization.Strategies
{
    public class InitializationStrategy : IInitializationStrategy
    {
        private readonly IAssetsMap _assetsMap;
        private readonly IPlayerSettings _playerSettings;

        private GameObject _root;
        private Transform _rootTransform;
        private IInputProvider _playerInput;
        
        public InitializationStrategy(IAssetsMap assetsMap, IPlayerSettings playerSettings)
        {
            _assetsMap = assetsMap;
            _playerSettings = playerSettings;
        }
        
        public void InitializeGameplay()
        {
            CreateRoot();
            RegisterDependencies();
            CreatePlayer();
            CreateSpawners();
            CreateSceneDirector();
            
            IoC.Instance.Resolver.Resolve<IAddressableService>().Initialize();
        }

        private void CreateRoot()
        {
            _root = new GameObject("GameplayRoot");
            _rootTransform = _root.transform;
            _playerInput = _root.AddComponent<RawInputProvider>();
        }

        private void RegisterDependencies()
        {
            var container = new MinimalisticIoCContainer();
            
            var addressableService = new AddressableService(_assetsMap);
            var gameObjectsFactory = new GameObjectsFactory(addressableService, _rootTransform);

            var initializer = CreateEntityInitializer(gameObjectsFactory);
            
            IoC.Instance.SetContainer(container).SetResolver(container);
            
            //assets management
            container.RegisterInstance(_assetsMap);
            container.RegisterInstance<IAddressableService>(addressableService);
            container.RegisterInstance<IObjectsFactory<GameObject>>(gameObjectsFactory);
            
            //initialization
            container.RegisterInstance(initializer);
            
            //input
            container.RegisterInstance(_playerInput);
            container.RegisterInstance(new ConstantInputProvider{VerticalAxis = 1f});
            
            //settings
            container.RegisterInstance(_playerSettings);
            
            //systems
            container.RegisterInstance(new ConstantMovementSystem());
            container.RegisterInstance(new ThrustMovementSystem(_playerSettings));
            container.RegisterInstance(new RotationSystem(new Rotation()));
            container.RegisterInstance(new ProjectileSpawnSystem(gameObjectsFactory, initializer));
            container.RegisterInstance(new AsteroidSpawnSystem(gameObjectsFactory, initializer));
            container.RegisterInstance(new EntityLifespanSystem(initializer));

            //entities
            container.Register<IPlayer>(args => new Player((IPlayerSettings)args[0]));
            container.Register<IProjectile>(args => new Projectile(20f));
            container.Register<Asteroid>(args => new Asteroid((float)args[0]));
            container.Register<AsteroidSpawner>(args => new AsteroidSpawner());
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

        private void CreateSpawners()
        {
            IoC.Instance.Resolver.Resolve<IObjectsFactory<GameObject>>().Get<IEntityView>(
                AssetId.AsteroidSpawner.ToString(),
                o =>
                {
                    var spawner = IoC.Instance.Resolver.Resolve<AsteroidSpawner>();
                    IoC.Instance.Resolver.Resolve<IEntityInitializer>().InitializeEntity(spawner, o);
                });
        }
        
        private void CreateSceneDirector()
        {
            var director = _root.AddComponent<SceneDirector>();

            var updateSystems = new List<IUpdateSystem>()
            {
                IoC.Instance.Resolver.Resolve<RotationSystem>(),
                IoC.Instance.Resolver.Resolve<ProjectileSpawnSystem>(),
                IoC.Instance.Resolver.Resolve<AsteroidSpawnSystem>(),
                IoC.Instance.Resolver.Resolve<EntityLifespanSystem>()
            };

            var fixedUpdateSystems = new List<IFixedUpdateSystem>
            {
                IoC.Instance.Resolver.Resolve<ThrustMovementSystem>(),
                IoC.Instance.Resolver.Resolve<ConstantMovementSystem>(),
            };

            director.Initialize(updateSystems, fixedUpdateSystems);
        }
        
        private IEntityInitializer CreateEntityInitializer(IObjectsFactory<GameObject> factory)
        {
            return new EntityInitializer(new IInitializationHandler[]
            {
                new PlayerMovementInitializationHandler(),
                new PlayerRotationInitializationHandler(),
                new CollisionInitializationHandler(),
                new AsteroidMovementInitializationHandler(),
                new ProjectileMovementInitializationHandler(),
                new ProjectileSpawnerInitializationHandler(),
                new AsteroidSpawnerInitializationHandler(),
            }, factory);
        }
    }
}