using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public abstract class SpawnStrategy : IEntityStrategy<ISpawner>
    {
        protected readonly IObjectsFactory<GameObject> Factory;
        protected readonly IEntityInitializer Initializer;

        protected readonly string AssetId;

        protected SpawnStrategy(string assetId, IObjectsFactory<GameObject> factory, IEntityInitializer initializer)
        {
            AssetId = assetId;
            Factory = factory;
            Initializer = initializer;
        }

        public abstract void Execute(ISpawner entity, float deltaTime);
    }
}