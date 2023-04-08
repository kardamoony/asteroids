﻿using System;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class AsteroidSpawner : Entity, ISpawner
    {
        public event Action<string, GameObject> OnSpawned;
        public string SpawnedAssetId { get; private set; }
        public float SpawnDelay { get; private set;}
        public int MaxCount { get; private set;}
        
        public AsteroidSpawner(ISettingsProvider settingsProvider, TimeSpan lifeTimeSpan) : base(settingsProvider, lifeTimeSpan)
        {
        }

        public void InvokeSpawnedEvent(GameObject gameObject)
        {
            OnSpawned?.Invoke(SpawnedAssetId, gameObject);
        }

        protected override void InitializeInternal()
        {
            SpawnDelay = SettingsProvider.GetValue<float>(Asteroid.SpawnDelay);
            MaxCount = SettingsProvider.GetValue<int>(Asteroid.MaxCount);
            SpawnedAssetId = SettingsProvider.GetValue<string>(Asteroid.AsteroidAssetId);
        }
    }
}