using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.Services;
using Asteroids.IoC;
using Asteroids.ServiceLayer.Factories;
using Asteroids.ServiceLayer.Initialization.Handlers;
using Asteroids.ServiceLayer.Settings;
using Asteroids.ServiceLayer.Settings.Converters;
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
        private readonly ISettingsProvider _settings;

        private GameObject _root;
        private Transform _rootTransform;
        private IInputProvider _playerInput;
        
        public InitializationStrategy(IAssetsMap assetsMap, GameplaySettings gameplaySettings)
        {
            _assetsMap = assetsMap;
            _settings = new SettingsProvider(gameplaySettings, new ISettingsConverter[]
            {
                new StringSettingsConverter(),
                new FloatSettingsConverter(),
                new BoolSettingsConverter(),
                new IntSettingsConverter(),
                new Vector2SettingsConverter(),
                new TimeSpanSettingConverter()
            });
        }
        
        public void InitializeGameplay()
        {
            CreateRoot();
            RegisterDependencies();
            CreatePlayer();
            CreateSpawners();
            CreateSceneDirector();
            
            Locator.Instance.Resolver.Resolve<IAddressableService>().Initialize();
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
            var entityFactory = new EntityFactory(gameObjectsFactory, initializer);
            
            Locator.Instance.SetContainer(container).SetResolver(container);
            
            //settings
            container.RegisterInstance(_settings);
            
            //assets management
            container.RegisterInstance(_assetsMap);
            container.RegisterInstance<IAddressableService>(addressableService);
            container.RegisterInstance<IObjectsFactory<GameObject>>(gameObjectsFactory);
            container.RegisterInstance<IObjectsFactory<IEntity>>(entityFactory);
            
            //initialization
            container.RegisterInstance(initializer);
            
            //input
            container.RegisterInstance(_playerInput);
            container.RegisterInstance(new ConstantInputProvider{VerticalAxis = 1f});

            //systems
            container.RegisterInstance(new ConstantMovementSystem());

            var acceleration = _settings.GetValue<float>(Player.Acceleration);
            var deceleration = _settings.GetValue<float>(Player.Deceleration);
            var brake = _settings.GetValue<float>(Player.Brake);

            container.RegisterInstance(new ThrustMovementSystem(acceleration, deceleration, brake));
            
            container.RegisterInstance(new RotationSystem(new Rotation()));
            container.RegisterInstance(new ProjectileSpawnSystem(entityFactory));
            container.RegisterInstance(new AsteroidSpawnSystem(entityFactory));
            container.RegisterInstance(new EntityLifespanSystem(entityFactory));
            container.RegisterInstance(new HealthSystem(entityFactory));

            //entities
            container.Register<IPlayer>(_ => new PlayerEntity(_settings, TimeSpan.Zero));

            var projectileLifeTime = _settings.GetValue<TimeSpan>(Projectile.LifeTime);
            container.Register<IProjectile>(_ => new ProjectileEntity(_settings, projectileLifeTime));
            container.Register<AsteroidEntity>(_ => new AsteroidEntity(_settings, TimeSpan.Zero));
            container.Register<AsteroidSpawner>(_ => new AsteroidSpawner(_settings, projectileLifeTime));
        }
        
        private void CreatePlayer()
        {
            Locator.Instance.Resolver.Resolve<IObjectsFactory<IEntity>>().Get<IPlayer>(AssetId.Player.ToString(), null);
        }

        private void CreateSpawners()
        {
            Locator.Instance.Resolver.Resolve<IObjectsFactory<IEntity>>()
                .Get<AsteroidSpawner>(AssetId.AsteroidSpawner.ToString(), null);
        }
        
        private void CreateSceneDirector()
        {
            var director = _root.AddComponent<SceneDirector>();

            var updateSystems = new List<IUpdateSystem>()
            {
                Locator.Instance.Resolver.Resolve<RotationSystem>(),
                Locator.Instance.Resolver.Resolve<ProjectileSpawnSystem>(),
                Locator.Instance.Resolver.Resolve<AsteroidSpawnSystem>(),
                Locator.Instance.Resolver.Resolve<EntityLifespanSystem>(),
                Locator.Instance.Resolver.Resolve<HealthSystem>(),
            };

            var fixedUpdateSystems = new List<IFixedUpdateSystem>
            {
                Locator.Instance.Resolver.Resolve<ThrustMovementSystem>(),
                Locator.Instance.Resolver.Resolve<ConstantMovementSystem>(),
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
                
                new ProjectileMovementInitializationHandler(),
                new ProjectileSpawnerInitializationHandler(),
                
                new AsteroidMovementInitializationHandler(),
                new AsteroidSpawnerInitializationHandler(),
                
                new DestructableInitializationHandler()
            }, factory);
        }
    }
}