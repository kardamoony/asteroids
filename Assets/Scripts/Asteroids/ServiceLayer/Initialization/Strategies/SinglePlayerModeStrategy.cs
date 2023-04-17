using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.IoC;
using Asteroids.UILayer.Views.AttemptsView;
using Asteroids.UILayer.Views.GameoverView;
using Asteroids.UILayer.Views.ScoreView;
using Asteroids.UILayer.Views.StartView;
using Asteroids.ServiceLayer.Factories;
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
    public class SinglePlayerModeStrategy : InitializationStrategy
    {
        private readonly ISettingsProvider _settings;
        private GameObject _root;
        private IInputProvider _playerInput;
        private bool _initialized;
        
        public SinglePlayerModeStrategy(GameplaySettings gameplaySettings, GameObject poolParent) : base(poolParent)
        {
            _settings = new SettingsProvider(gameplaySettings, new ISettingsConverter[]
            {
                new StringSettingsConverter(),
                new FloatSettingsConverter(),
                new BoolSettingsConverter(),
                new IntSettingsConverter(),
                new Vector2SettingsConverter(),
                new TimeSpanSettingConverter(),
                new UintSettingsConverter()
            });
        }
        
        public override void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            
            CreateRoot();
            RegisterDependencies();
            CreateSpawners();
            CreateSceneDirector();
            AddUIViews();
        }

        public override void Deinitialize()
        {
            if (!_initialized) return;
            _initialized = false;
            
            var sceneDirector = _root.GetComponent<SceneDirector>();
            sceneDirector.Deinitialize();

            Object.Destroy(_root);

            RemoveViews();
            ClearPool();
            UnregisterDependencies();

            if (UISystem.TryGetView<StartView>(out var startView))
            {
                startView.Show();
            }
        }

        private void CreateRoot()
        {
            _root = new GameObject("GameplayRoot");
            _playerInput = _root.AddComponent<RawInputProvider>();
        }

        private void RegisterDependencies()
        {
            var resolver = Locator.Instance.Resolver;
            
            var playerId = 1U;

            var gameObjectsFactory = resolver.Resolve<IObjectsFactory<GameObject>>();
            var initializer = CreateEntityInitializer(gameObjectsFactory);
            var entityFactory = new EntityFactory(gameObjectsFactory, initializer);
            
            //settings
            RegisterInstance(_settings);
  
            //factories
            RegisterInstance<IObjectsFactory<IEntity>>(entityFactory);

            //initialization
            RegisterInstance(initializer);
            
            //input
            RegisterInstance(_playerInput);
            RegisterInstance(new ConstantInputProvider{VerticalAxis = 1f});

            //systems
            RegisterInstance(new ConstantMovementSystem());

            var acceleration = _settings.GetValue<float>(Player.Acceleration);
            var deceleration = _settings.GetValue<float>(Player.Deceleration);
            var brake = _settings.GetValue<float>(Player.Brake);
            var triesCount = _settings.GetValue<int>(Player.Tries);

            RegisterInstance(new ThrustMovementSystem(acceleration, deceleration, brake));
            
            RegisterInstance(new RotationSystem(new Rotation()));
            RegisterInstance(new ProjectileSpawnSystem(entityFactory));
            RegisterInstance(new AsteroidSpawnSystem(entityFactory));
            RegisterInstance(new EntityLifespanSystem(entityFactory));
            RegisterInstance(new HealthSystem(entityFactory));

            var scoreStrategy = new ScoreCountingStrategy(playerId);
            RegisterInstance(new ScoreCountingSystem(scoreStrategy));

            var playerSpawnStrategy = new PlayerSpawnStrategy(playerId, triesCount, AssetId.Player.ToString(), entityFactory);
            RegisterInstance(new PlayerSpawnSystem(playerSpawnStrategy));

            //entities
            var projectileLifeTime = _settings.GetValue<TimeSpan>(Projectile.LifeTime);

            RegisterConstructor<IPlayer>(args => new PlayerEntity((uint)args[0], _settings, TimeSpan.Zero));
            RegisterConstructor<IProjectile>(_ => new ProjectileEntity(_settings, projectileLifeTime));
            
            //TODO: IAsteroid
            RegisterConstructor<AsteroidEntity>(_ => new AsteroidEntity(_settings, TimeSpan.Zero));
            RegisterConstructor<AsteroidSpawner>(_ => new AsteroidSpawner(_settings, TimeSpan.Zero));
            RegisterConstructor<PlayerSpawner>(_ => new PlayerSpawner(_settings, TimeSpan.Zero));
            
            //UI
            RegisterInstance(new AttemptsModel(gameObjectsFactory, playerSpawnStrategy));
            RegisterInstance(new ScoreModel(scoreStrategy, playerSpawnStrategy));
            RegisterInstance(new GameoverModel(playerSpawnStrategy, scoreStrategy, this));
        }

        private void AddUIViews()
        {
            AddUIView<AttemptsView>(AssetId.AttemptsView.ToString(), true);
            AddUIView<ScoreView>(AssetId.ScoreView.ToString(), true);
            AddUIView<GameoverView>(AssetId.GameoverView.ToString(), false);
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
                Locator.Instance.Resolver.Resolve<ScoreCountingSystem>(),
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
                
                new DestructableInitializationHandler(),
                new ScoreProducerInitializationHandler(),
            }, factory);
        }
    }
}