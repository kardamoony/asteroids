using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public class PlayerSpawnStrategy : SpawnStrategy
    {
        private bool _playerSpawned;
        private int _triesCount;
        
        public PlayerSpawnStrategy(int triesCount, string assetId, IObjectsFactory<IEntity> factory) : base(assetId, factory)
        {
            _triesCount = triesCount + 1;
        }

        public override void Execute(ISpawner entity, float deltaTime)
        {
            if (_playerSpawned || _triesCount < 1)
            {
                return;
            }
            
            _playerSpawned = true;
            _triesCount -= 1;
            
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