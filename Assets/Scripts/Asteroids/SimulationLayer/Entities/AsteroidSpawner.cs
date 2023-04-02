using System;
using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class AsteroidSpawner : Entity, ISpawner
    {
        public event Action<string, GameObject> OnSpawned;
        public string SpawnedAssetId { get; }
        public float SpawnDelay { get; }
        public int MaxCount { get; }

        public AsteroidSpawner(IAsteroidSettings asteroidSettings)
        {
            SpawnDelay = asteroidSettings.SpawnDelay;
            MaxCount = asteroidSettings.MaxCount;
            SpawnedAssetId = asteroidSettings.AsteroidAssetId;
        }
        
        public void InvokeSpawnedEvent(GameObject gameObject)
        {
            OnSpawned?.Invoke(SpawnedAssetId, gameObject);
        }
    }
}