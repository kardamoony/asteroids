using System.Collections.Generic;
using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.IoC;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class AsteroidSpawnStrategy : SpawnStrategy
    {
        private float _timeUntilSpawn;
        private readonly HashSet<IEntity> _spawnedEntities = new HashSet<IEntity>();

        public AsteroidSpawnStrategy(IObjectsFactory<GameObject> factory, IEntityInitializer initializer) : base(factory, initializer)
        {
            //TODO: unsubscribe
            initializer.OnEntityDenitialized += HandleEntityDeinitialized;
        }

        public override void Execute(ISpawner entity, float deltaTime)
        {
            _timeUntilSpawn -= deltaTime;
            
            if (!CanSpawn(entity)) return;
            
            Factory.Get<IEntityView>(entity.SpawnedAssetId, view =>
            {
                var asteroidSettings = IoC.Instance.Resolver.Resolve<IAsteroidSettings>();
                var asteroid = IoC.Instance.Resolver.Resolve<Asteroid>(asteroidSettings.Speed);
                Initializer.InitializeEntity(asteroid, view);
                entity.InvokeSpawnedEvent(view.GameObject);
                _spawnedEntities.Add(asteroid);
            });

            _timeUntilSpawn = entity.SpawnDelay;
        }

        private bool CanSpawn(ISpawner entity)
        {
            if (_timeUntilSpawn > 0 && _spawnedEntities.Count != 0)
            {
                return false;
            }

            return _spawnedEntities.Count < entity.MaxCount;
        }

        private void HandleEntityDeinitialized(IEntity entity)
        {
            if (!_spawnedEntities.Contains(entity))
            {
                return;
            }

            _spawnedEntities.Remove(entity);
        }
    }
}
