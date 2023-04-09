using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.Services;
using Asteroids.IoC;
using Asteroids.ServiceLayer.Factories;
using Asteroids.ServiceLayer.Initialization.Handlers;
using Asteroids.ServiceLayer.Initialization.Handlers.Gameplay;
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
using Object = UnityEngine.Object;

namespace Asteroids.ServiceLayer.Initialization.Strategies
{
    public class GameplayInitializationStrategy : IInitializationStrategy
    {
        private readonly ISettingsProvider _settings;
        private GameObject _root;
        private IInputProvider _playerInput;
        
        public GameplayInitializationStrategy(GameplaySettings gameplaySettings)
        {
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
        
        public void Initialize()
        {
            CreateRoot();
            RegisterDependencies();
            CreateSpawners();
            CreateSceneDirector();
            
            Locator.Instance.Resolver.Resolve<IAddressableService>().Initialize();
        }

        public void Deinitialize()
        {
            //TODO: proper deinitialization and asset release
            Object.Destroy(_root);
        }

        private void CreateRoot()
        {
            _root = new GameObject("GameplayRoot");
            _playerInput = _root.AddComponent<RawInputProvider>();
        }

        private void RegisterDependencies()
        {
            var container = Locator.Instance.Container;
            var resolver = Locator.Instance.Resolver;

            var gameObjectsFactory = resolver.Resolve<IObjectsFactory<GameObject>>();
            
            var initializer = CreateEntityInitializer(gameObjectsFactory);
            var entityFactory = new EntityFactory(gameObjectsFactory, initializer);
            
            //settings
            container.RegisterInstance(_settings);
            
            //factories
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
            var triesCount = _settings.GetValue<int>(Player.Tries);

            container.RegisterInstance(new ThrustMovementSystem(acceleration, deceleration, brake));
            
            container.RegisterInstance(new RotationSystem(new Rotation()));
            container.RegisterInstance(new ProjectileSpawnSystem(entityFactory));
            container.RegisterInstance(new AsteroidSpawnSystem(entityFactory));
            container.RegisterInstance(new EntityLifespanSystem(entityFactory));
            container.RegisterInstance(new HealthSystem(entityFactory));
            container.RegisterInstance(new PlayerSpawnSystem(triesCount, AssetId.Player.ToString(), entityFactory));

            //entities
            var projectileLifeTime = _settings.GetValue<TimeSpan>(Projectile.LifeTime);
            
            container.Register<IPlayer>(_ => new PlayerEntity(_settings, TimeSpan.Zero));
            container.Register<IProjectile>(_ => new ProjectileEntity(_settings, projectileLifeTime));
            
            //TODO: IAsteroid
            container.Register<AsteroidEntity>(_ => new AsteroidEntity(_settings, TimeSpan.Zero));
            
            container.Register<AsteroidSpawner>(_ => new AsteroidSpawner(_settings, TimeSpan.Zero));
            container.Register<PlayerSpawner>(_ => new PlayerSpawner(_settings, TimeSpan.Zero));
        }

        private void CreateSpawners()
        {
            Locator.Instance.Resolver.Resolve<IObjectsFactory<IEntity>>()
                .Get<AsteroidSpawner>(AssetId.AsteroidSpawner.ToString(), null);
            
            Locator.Instance.Resolver.Resolve<IObjectsFactory<IEntity>>()
                .Get<PlayerSpawner>(AssetId.PlayerSpawner.ToString(), null);
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
                Locator.Instance.Resolver.Resolve<PlayerSpawnSystem>()
            };

            var fixedUpdateSystems = new List<IFixedUpdateSystem>
            {
                Locator.Instance.Resolver.Resolve<ThrustMovementSystem>(),
                Locator.Instance.Resolver.Resolve<ConstantMovementSystem>(),
            };

            director.Initialize(updateSystems, fixedUpdateSystems);
        }
        
        private IInitializer<IEntity, IEntityView> CreateEntityInitializer(IObjectsFactory<GameObject> factory)
        {
            return new EntityInitializer(new IInitializationHandler<IEntity, IEntityComponent>[]
            {
                new PlayerMovementInitializationHandler(),
                new PlayerRotationInitializationHandler(),
                new PlayerSpawnerInitializationHandler(),
                
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