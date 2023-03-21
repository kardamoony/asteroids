using System.Collections.Generic;
using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Models;
using Asteroids.SimulationLayer.Settings;

using UnityEngine;

namespace Asteroids.SimulationLayer.Scene
{
    public class SimpleSceneInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _playerGo;
        [SerializeField] private AssetsMap _assetsMap;
        [SerializeField] private GameObject _asteroidPrefab;

        private void RegisterDependencies()
        {
            var container = new MinimalisticIoCContainer();
            IoC.Instance.SetContainer(container).SetResolver(container);
            
            var rotation = new Rotation();
            var playerInputProvider = GetComponentInChildren<IInputProvider>();

            container.Register<IPlayer>((args) => new Player((IPlayerSettings)args[0]));
            container.Register<Asteroid>((args) => new Asteroid((float)args[0]));
            container.Register<ThrustMovement>((args) 
                => new ThrustMovement((float)args[0], (float)args[1], (float)args[2]));

            container.RegisterInstance(playerInputProvider);
            container.RegisterInstance(new ConstantInputProvider{VerticalAxis = 1f});
            container.RegisterInstance<IPlayerSettings>(_playerSettings);
            container.RegisterInstance(new AsteroidsMovementSystem());
            container.RegisterInstance(new PlayerMovementSystem(_playerSettings));
            
            container.RegisterInstance(rotation);

            container.RegisterInstance(new RotationSystem(rotation));
        }

        private void CreatePlayer()
        {
            var settings = IoC.Instance.Resolver.Resolve<IPlayerSettings>();
            var player = IoC.Instance.Resolver.Resolve<IPlayer>(new object[]{ settings });

            var playerMovement = _playerGo.GetComponent<MovementComponent>();
            var playerRotation = _playerGo.GetComponent<LocalRotationComponent>();
            var playerCollision = _playerGo.GetComponent<CollisionComponent>();
            
            playerMovement.SetContext(player.Movable);
            playerRotation.SetContext(player.Rotatable);
            playerCollision.SetContext(player.Collidable);

            var inputProvider = IoC.Instance.Resolver.Resolve<IInputProvider>();
            
            IoC.Instance.Resolver.Resolve<PlayerMovementSystem>().Register(player.Movable, inputProvider);
            IoC.Instance.Resolver.Resolve<RotationSystem>().Register(player.Rotatable, inputProvider);
        }

        [ContextMenu("CreateAsteroid")]
        private void CreateAsteroid()
        {
            var randomPos = new Vector3(Random.Range(-1, 1) * _camera.rect.width / 2, 0, Random.Range(-1, 1) * _camera.rect.height / 2);

            var rotation = Quaternion.AngleAxis(Random.Range(-360f, 360f), Vector3.up);
            
            var asteroid = Instantiate(_asteroidPrefab, randomPos, rotation);

            var movement = asteroid.GetComponent<MovementComponent>();
            var movable = new Asteroid(2);
            
            movement.SetContext(movable);
            
            var inputProvider = IoC.Instance.Resolver.Resolve<ConstantInputProvider>();
            
            IoC.Instance.Resolver.Resolve<AsteroidsMovementSystem>().Register(movable, inputProvider);
        }

        private void CreateSceneDirector()
        {
            var director = gameObject.AddComponent<SceneDirector>();

            var updateSystems = new List<IUpdateSystem>()
            {
                IoC.Instance.Resolver.Resolve<RotationSystem>()
            };

            var fixedUpdateSystems = new List<IFixedUpdateSystem>
            {
                IoC.Instance.Resolver.Resolve<PlayerMovementSystem>(),
                IoC.Instance.Resolver.Resolve<AsteroidsMovementSystem>(),
            };

            director.Initialize(updateSystems, fixedUpdateSystems);
        }
       
        private void Awake()
        {
            RegisterDependencies();
            CreatePlayer();
            CreateSceneDirector();
        }
    }
}
