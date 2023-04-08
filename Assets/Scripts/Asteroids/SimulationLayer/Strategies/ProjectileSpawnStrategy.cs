using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ProjectileSpawnStrategy : InputSpawnStrategy
    {
        private float _timeUntilSpawn;
        
        public ProjectileSpawnStrategy(string assetId, IObjectsFactory<GameObject> factory, IEntityInitializer initializer) 
            : base(assetId, factory, initializer)
        {
        }
        
        public override void Execute(ISpawner entity, IInputProvider context, float deltaTime)
        {
            _timeUntilSpawn -= deltaTime;
            
            if (!context.Fire || _timeUntilSpawn > 0) return;

            Factory.Get<IEntityView>(AssetId, o =>
            {
                //TODO: remove ioc call
                var projectile = IoC.Locator.Instance.Resolver.Resolve<IProjectile>();
                Initializer.InitializeEntity((IEntity)projectile, o);
                entity.InvokeSpawnedEvent(o.GameObject);
            });
            
            _timeUntilSpawn = entity.SpawnDelay;
        }
    }
}