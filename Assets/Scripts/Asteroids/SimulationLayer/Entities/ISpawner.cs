using System;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface ISpawner
    {
        event Action<string, GameObject> OnSpawned;
        
        AssetId SpawnedAssetId { get; }
        float SpawnDelay { get; }

        void InvokeSpawnedEvent(GameObject gameObject);

    }
}
