using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class MovementSystem : EntityContextSystem<IMovable, IInputProvider>, IFixedUpdateSystem
    {
        private readonly IContextEntityStrategy<IMovable, IInputProvider> _strategy;

        public MovementSystem( IContextEntityStrategy<IMovable, IInputProvider> strategy)
        {
            _strategy = strategy;
        }

        protected override void OnUpdated(float fixedDeltaTime)
        {
            EntitiesContextMap.Update((entity, input) => _strategy.Execute(entity, input, fixedDeltaTime));
        }
    }
}