using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public abstract class SpawnStrategy : IEntityStrategy<ISpawner>
    {
        protected readonly IObjectsFactory<IEntity> Factory;
        protected readonly string AssetId;

        protected SpawnStrategy(string assetId, IObjectsFactory<IEntity> factory)
        {
            AssetId = assetId;
            Factory = factory;
        }

        public abstract void Execute(ISpawner entity, float deltaTime);
    }
}