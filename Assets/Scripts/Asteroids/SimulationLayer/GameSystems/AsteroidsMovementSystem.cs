using System.Collections.Generic;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class AsteroidsMovementSystem : IFixedUpdateSystem
    {
        private HashSet<IMovable> _movables = new HashSet<IMovable>();

        public void RegisterObject(IMovable movable)
        {
            _movables.Add(movable);
        }

        public void UnregisterObject(IMovable movable)
        {
            _movables.Remove(movable);
        }
        
        public void FixedUpdate(float deltaTime)
        {
            Move(deltaTime);
        }

        private void Move(float deltaTime)
        {
            foreach (var movable in _movables)
            {
                movable.Velocity = movable.Speed;
            }
        }
    }
}