using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public abstract class InputSpawnStrategy : IInputBasedEntityStrategy<ISpawner>
    {
        protected readonly IObjectsFactory<GameObject> Factory;
        protected readonly IEntityInitializer Initializer;

        protected InputSpawnStrategy(IObjectsFactory<GameObject> factory, IEntityInitializer initializer)
        {
            Factory = factory;
            Initializer = initializer;
        }
        
        public abstract void Execute(ISpawner entity, IInputProvider inputProvider, float deltaTime);
    }
}