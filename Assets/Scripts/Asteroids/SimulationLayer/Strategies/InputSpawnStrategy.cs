using Asteroids.CoreLayer.Factories;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public abstract class InputSpawnStrategy : IContextEntityStrategy<ISpawner, IInputProvider>
    {
        protected readonly string AssetId;
        protected readonly IObjectsFactory<IEntity> Factory;

        protected InputSpawnStrategy(string assetId, IObjectsFactory<IEntity> factory)
        {
            AssetId = assetId;
            Factory = factory;
        }
        
        public abstract void Execute(ISpawner entity, IInputProvider context, float deltaTime);
    }
}