using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class MovementSystem : InputReadingSystem<IMovable>, IFixedUpdateSystem
    {
        private readonly IInputBasedEntityStrategy<IMovable> _strategy;

        public MovementSystem(IInputBasedEntityStrategy<IMovable> strategy)
        {
            _strategy = strategy;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            EntitiesInputMap.Update();
            EntitiesInputMap.Foreach((entity, input) => _strategy.Execute(entity, input, fixedDeltaTime));
        }
    }
}