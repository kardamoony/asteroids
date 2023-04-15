using System;
using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class PlayerSpawner : Entity, ISpawner
    {
        public event Action<string, GameObject> OnSpawned;
        public IEntity SpawnOwner => null;
        public float SpawnDelay { get; private set; }
        public int MaxCount { get; private set; }
        
        public PlayerSpawner(ISettingsProvider settingsProvider, TimeSpan lifeTimeSpan) : base(settingsProvider, lifeTimeSpan)
        {
        }
        
        public void InvokeSpawnedEvent(GameObject gameObject, string assetId)
        {
            OnSpawned?.Invoke(assetId, gameObject);
        }

        protected override void InitializeInternal()
        {
            SpawnDelay = 0f;
            MaxCount = 1;
        }
    }
}