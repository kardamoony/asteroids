using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class SpawnSystem : SimpleEntitySystem<ISpawner>, IUpdateSystem
    {
        private readonly IEntityStrategy<ISpawner> _strategy;
        
        public SpawnSystem(IEntityStrategy<ISpawner> strategy)
        {
            _strategy = strategy;
        }
        
        protected override void OnUpdated(float deltaTime)
        {
            Entities.Update(entity => _strategy.Execute(entity, deltaTime));
        }
    }
}