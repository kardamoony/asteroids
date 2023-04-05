using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public abstract class InputSpawnStrategy : IContextEntityStrategy<ISpawner, IInputProvider>
    {
        protected readonly string AssetId;
        protected readonly IObjectsFactory<GameObject> Factory;
        protected readonly IEntityInitializer Initializer;

        protected InputSpawnStrategy(string assetId, IObjectsFactory<GameObject> factory, IEntityInitializer initializer)
        {
            AssetId = assetId;
            Factory = factory;
            Initializer = initializer;
        }
        
        public abstract void Execute(ISpawner entity, IInputProvider context, float deltaTime);
    }
}