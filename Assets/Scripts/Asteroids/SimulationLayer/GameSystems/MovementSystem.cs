using System.Collections.Generic;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Models;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class MovementSystem : IFixedUpdateSystem
    {
        private readonly IMovementModel _model;
        private readonly Dictionary<IMovable, IInputProvider> _movingEntities = new Dictionary<IMovable, IInputProvider>();

        public MovementSystem(IMovementModel model)
        {
            _model = model;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (var pair in _movingEntities)
            {
                _model.Move(pair.Key, pair.Value, fixedDeltaTime);
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