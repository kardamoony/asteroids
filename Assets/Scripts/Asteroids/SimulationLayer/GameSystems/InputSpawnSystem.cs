using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class InputSpawnSystem : EntityContextSystem<ISpawner, IInputProvider>, IUpdateSystem
    {
        private readonly IContextEntityStrategy<ISpawner, IInputProvider> _strategy;

        public InputSpawnSystem(IContextEntityStrategy<ISpawner, IInputProvider> strategy)
        {
            _strategy = strategy;
        }
        
        protected override void OnUpdated(float deltaTime)
        {
            EntitiesContextMap.Update((spawner, input) => _strategy.Execute(spawner, input, deltaTime));
        }
    }
}