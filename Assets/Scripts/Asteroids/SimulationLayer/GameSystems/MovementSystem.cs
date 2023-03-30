using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class MovementSystem : InputReadingSystem<IMovable>, IFixedUpdateSystem
    {
        private readonly IEntityStrategy<IMovable> _strategy;

        public MovementSystem(IEntityStrategy<IMovable> strategy)
        {
            _strategy = strategy;
            EntitiesInputMap = new EntitiesInputMap<IMovable>();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            EntitiesInputMap.AddPending();
            EntitiesInputMap.RemovePending();
            
            EntitiesInputMap.Foreach((entity, input) => _strategy.Execute(entity, input, fixedDeltaTime));
        }
    }
}