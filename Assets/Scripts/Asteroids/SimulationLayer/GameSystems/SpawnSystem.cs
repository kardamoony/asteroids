using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class SpawnSystem : InputReadingSystem<ISpawner>, IUpdateSystem
    {
        private readonly IEntityStrategy<ISpawner> _strategy;

        public SpawnSystem(IEntityStrategy<ISpawner> strategy)
        {
            _strategy = strategy;
            EntitiesInputMap = new EntitiesInputMap<ISpawner>();
        }
        
        public void Update(float deltaTime)
        {
            EntitiesInputMap.AddPending();
            EntitiesInputMap.RemovePending();

            EntitiesInputMap.Foreach((spawner, input) => _strategy.Execute(spawner, input, deltaTime));
        }
    }
}