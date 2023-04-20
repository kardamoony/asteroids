using System;
using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class ExplosionSpawner : Entity, ISpawner
    {
        public event Action<string, GameObject> OnSpawned;
        public IEntity SpawnOwner { get; }
        public float SpawnDelay { get; }
        public int MaxCount { get; }
        
        public ExplosionSpawner(ISettingsProvider settingsProvider, TimeSpan lifeTimeSpan) : base(settingsProvider, lifeTimeSpan)
        {
        }

        protected override void InitializeInternal(){}

        public void InvokeSpawnedEvent(GameObject gameObject, string assetId)
        {
            OnSpawned?.Invoke(assetId, gameObject);
        }
    }
}