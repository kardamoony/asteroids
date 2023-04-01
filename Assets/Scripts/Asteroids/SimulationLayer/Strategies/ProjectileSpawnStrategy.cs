using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.CoreLayer.IoC;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ProjectileSpawnStrategy : InputSpawnStrategy
    {
        private float _timeUntilSpawn;
        
        public ProjectileSpawnStrategy(IObjectsFactory<GameObject> factory, IEntityInitializer initializer) : base(factory, initializer)
        {
        }
        
        public override void Execute(ISpawner entity, IInputProvider inputProvider, float deltaTime)
        {
            _timeUntilSpawn -= deltaTime;
            
            if (!inputProvider.Fire || _timeUntilSpawn > 0) return;

            Factory.Get<IEntityView>(entity.SpawnedAssetId.ToString(), o =>
            {
                var projectile = IoC.Instance.Resolver.Resolve<IProjectile>();
                Initializer.InitializeEntity((IEntity)projectile, o);
                entity.InvokeSpawnedEvent(o.GameObject);
            });
            
            _timeUntilSpawn = entity.SpawnDelay;
        }
    }
}