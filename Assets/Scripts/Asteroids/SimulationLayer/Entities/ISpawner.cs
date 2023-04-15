using System;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface ISpawner
    {
        event Action<string, GameObject> OnSpawned;
        
        IEntity SpawnOwner { get; }

        float SpawnDelay { get; }
        
        int MaxCount { get; }

        void InvokeSpawnedEvent(GameObject gameObject, string assetId);

    }
}
