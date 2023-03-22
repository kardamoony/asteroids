using System.Collections.Generic;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class MovementSystem : IFixedUpdateSystem
    {
        private readonly IEntityStrategy<IMovable> _strategy;
        private readonly Dictionary<IMovable, IInputProvider> _movingEntities = new Dictionary<IMovable, IInputProvider>();

        public MovementSystem(IEntityStrategy<IMovable> strategy)
        {
            _strategy = strategy;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (var pair in _movingEntities)
            {
                _strategy.Execute(pair.Key, pair.Value, fixedDeltaTime);
            }
        }

        public void Register(IMovable movable, IInputProvider inputProvider)
        {
            if (_movingEntities.ContainsKey(movable)) return;
            _movingEntities.Add(movable, inputProvider);
        }

        public void Unregister(IMovable movable)
        {
            if (!_movingEntities.ContainsKey(movable)) return;
            _movingEntities.Remove(movable);
        }
    }
}