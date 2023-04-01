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

        protected SpawnStrategy(IObjectsFactory<GameObject> factory, IEntityInitializer initializer)
        {
            Factory = factory;
            Initializer = initializer;
        }

        public abstract void Execute(ISpawner entity, float deltaTime);
    }
}