using System.Collections.Generic;
using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class AsteroidSpawnStrategy : SpawnStrategy
    {
        private readonly HashSet<IEntity> _spawnedEntities = new HashSet<IEntity>();
        
        private float _timeUntilSpawn;
        private bool _spawnStarted;

        public AsteroidSpawnStrategy(string assetId, IObjectsFactory<IEntity> factory) : base(assetId, factory)
        {
        }

        public override void Execute(ISpawner entity, float deltaTime)
        {
            _timeUntilSpawn -= deltaTime;
            
            if (!CanSpawn(entity)) return;

            _spawnStarted = true;
            
            Factory.Get<AsteroidEntity>(AssetId, spawnedEntity =>
            {
                _spawnedEntities.Add(spawnedEntity);
                spawnedEntity.OnDeinitialized += HandleEntityDeinitialized;
                entity.InvokeSpawnedEvent(spawnedEntity.EntityView.GameObject, AssetId);
                _spawnStarted = false;
            });

            _timeUntilSpawn = entity.SpawnDelay;
        }

        private bool CanSpawn(ISpawner entity)
        {
            if (_spawnStarted || _timeUntilSpawn > 0 && _spawnedEntities.Count != 0)
            {
                return false;
            }

            return _spawnedEntities.Count < entity.MaxCount;
        }

        private void HandleEntityDeinitialized(IEntity entity, GameObject gameObject)
        {
            if (!_spawnedEntities.Contains(entity))
            {
                return;
            }
            
            _spawnedEntities.Remove(entity);
            entity.OnDeinitialized -= HandleEntityDeinitialized;
        }
    }
}
