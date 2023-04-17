using System;
using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Providers;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class PlayerSpawnStrategy : SpawnStrategy, IPlayerAttemptsProvider
    {
        private readonly uint _playerId;
        
        private bool _playerSpawned;
        private bool _firstSpawnOccured;
        private IEntity _player;

        public event Action<uint> OnPlayerGameOver;
        public int Attempts { get; private set; }

        public PlayerSpawnStrategy(uint playerId, int attempts, string assetId, IObjectsFactory<IEntity> factory) : base(assetId, factory)
        {
            _playerId = playerId;
            Attempts = attempts;
        }

        public override void Execute(ISpawner entity, float deltaTime)
        {
            if (_playerSpawned)
            {
                return;
            }
            
            if (_firstSpawnOccured)
            {
                Attempts = Mathf.Max(0, Attempts - 1);
            }

            _firstSpawnOccured = true;

            if (Attempts < 1)
            {
                OnPlayerGameOver?.Invoke(_playerId);
                return;
            }
            
            _playerSpawned = true;
            
            //TODO: add small respawn delay
            Factory.Get<IPlayer>(AssetId, player =>
            {
                _player= (IEntity)player;
                _player.OnDeinitialized += HandlePlayerDeinitialized;
                entity.InvokeSpawnedEvent(_player.EntityView.GameObject, AssetId);
            }, _playerId);
        }
        
        private void HandlePlayerDeinitialized(IEntity entity, GameObject gameObject)
        {
            entity.OnDeinitialized -= HandlePlayerDeinitialized;
            _playerSpawned = false;
        }
    }
}