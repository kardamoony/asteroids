using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ProjectileSpawnStrategy : InputSpawnStrategy
    {
        private float _timeUntilSpawn;
        
        public ProjectileSpawnStrategy(string assetId, IObjectsFactory<IEntity> factory) : base(assetId, factory)
        {
        }
        
        public override void Execute(ISpawner entity, IInputProvider context, float deltaTime)
        {
            _timeUntilSpawn -= deltaTime;
            
            if (!context.Fire || _timeUntilSpawn > 0) return;
            
            Factory.Get<ProjectileEntity>(AssetId, spawnedEntity =>
            {
                entity.InvokeSpawnedEvent(spawnedEntity.EntityView.GameObject, AssetId);
            });

            _timeUntilSpawn = entity.SpawnDelay;
        }
    }
}