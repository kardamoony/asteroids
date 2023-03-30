using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ProjectileSpawnStrategy : IEntityStrategy<ISpawner>
    {
        private readonly IObjectsFactory<GameObject> _factory;
        private readonly IEntityInitializer _initializer;

        private float _timeUntilSpawn;

        public ProjectileSpawnStrategy(IObjectsFactory<GameObject> factory, IEntityInitializer initializer)
        {
            _factory = factory;
            _initializer = initializer;
        }

        public void Execute(ISpawner entity, IInputProvider inputProvider, float deltaTime)
        {
            _timeUntilSpawn -= deltaTime;
            
            if (!inputProvider.Fire || _timeUntilSpawn > 0) return;

            _factory.Get<IEntityView>(entity.SpawnedAssetId.ToString(), o =>
            {
                var projectile = IoC.Instance.Resolver.Resolve<IProjectile>();
                _initializer.InitializeEntity((IEntity)projectile, o);
                entity.InvokeSpawnedEvent(o.GameObject);
            });
            
            _timeUntilSpawn = entity.SpawnDelay;
        }
    }
}