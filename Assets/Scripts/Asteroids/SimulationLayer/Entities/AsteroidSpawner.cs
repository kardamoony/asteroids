using System;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class AsteroidSpawner : Entity, ISpawner
    {
        public event Action<string, GameObject> OnSpawned;
        public AssetId SpawnedAssetId => AssetId.Asteroid;
        public float SpawnDelay => 5f; //TODO: from settings
        public int EntitesMaxCount => 5; //TODO: from settings
        
        public void InvokeSpawnedEvent(GameObject gameObject)
        {
            OnSpawned?.Invoke(SpawnedAssetId.ToString(), gameObject);
        }
    }
}