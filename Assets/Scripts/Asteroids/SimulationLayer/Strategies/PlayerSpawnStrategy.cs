using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Providers;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class PlayerSpawnStrategy : SpawnStrategy, IPlayerAttemptsProvider
    {
        private bool _playerSpawned;
        private bool _initialSpawnOccured;
        
        public int Attempts { get; private set; }
        
        public PlayerSpawnStrategy(int attempts, string assetId, IObjectsFactory<IEntity> factory) : base(assetId, factory)
        {
            Attempts = attempts;
        }

        public override void Execute(ISpawner entity, float deltaTime)
        {
            if (_playerSpawned)
            {
                return;
            }
            
            if (_initialSpawnOccured)
            {
                Attempts = Mathf.Max(0, Attempts - 1);
            }

            _initialSpawnOccured = true;
            
            if (Attempts < 1) return;
            
            _playerSpawned = true;
            
            //TODO: add small respawn delay
            Factory.Get<IPlayer>(AssetId, player =>
            {
                var e = (IEntity)player;
                e.OnDeinitialized += HandlePlayerDeinitialized;
                entity.InvokeSpawnedEvent(e.EntityView.GameObject, AssetId);
            });
        }

        private void HandlePlayerDeinitialized(IEntity entity)
        {
            entity.OnDeinitialized -= HandlePlayerDeinitialized;
            _playerSpawned = false;
        }
    }
}