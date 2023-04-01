using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class InputSpawnSystem : InputReadingSystem<ISpawner>, IUpdateSystem
    {
        private readonly IInputBasedEntityStrategy<ISpawner> _strategy;

        public InputSpawnSystem(IInputBasedEntityStrategy<ISpawner> strategy)
        {
            _strategy = strategy;
        }
        
        public void Update(float deltaTime)
        {
            EntitiesInputMap.Update();
            EntitiesInputMap.Foreach((spawner, input) => _strategy.Execute(spawner, input, deltaTime));
        }
    }
}