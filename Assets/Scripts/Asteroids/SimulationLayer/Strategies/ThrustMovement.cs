using System;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ThrustMovement : IEntityStrategy<IMovable>
    {
        private readonly float _acceleration;
        private readonly float _deceleration;
        private readonly float _brake;
        
        public ThrustMovement(float acceleration, float deceleration, float brake)
        {
            _acceleration = acceleration;
            _deceleration = deceleration;
            _brake = brake;
        }
        
        public void Execute(IMovable entity, IInputProvider inputProvider, float deltaTime)
        {
            var currentVelocity = entity.Velocity;
            var verticalInput = inputProvider.VerticalAxis;
            
            if (verticalInput > 0 && currentVelocity < entity.Speed)
            {
                entity.Velocity = Math.Min(currentVelocity + _acceleration * deltaTime, entity.Speed);
                return;
            }
            
            if (currentVelocity > 0)
            {
                var decelerationRate = verticalInput < 0 ? _brake : _deceleration;
                entity.Velocity = Math.Max(0f, currentVelocity - decelerationRate * deltaTime);
            }
        }
    }
}