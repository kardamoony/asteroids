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
        
        public void Update(float deltaTime)
        {
            Entities.Update();
            Entities.Foreach(entity => _strategy.Execute(entity, deltaTime));
        }
    }
}