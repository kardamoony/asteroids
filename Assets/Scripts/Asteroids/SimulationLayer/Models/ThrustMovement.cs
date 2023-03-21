
using System;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Models
{
    public class ThrustMovement : IMovementModel
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
        
        public void Move(IMovable movable, IInputProvider input, float deltaTime)
        {
            var currentVelocity = movable.Velocity;
            var verticalInput = input.VerticalAxis;
            
            if (verticalInput > 0 && currentVelocity < movable.Speed)
            {
                movable.Velocity = Math.Min(currentVelocity + _acceleration * deltaTime, movable.Speed);
                return;
            }
            
            if (currentVelocity > 0)
            {
                var decelerationRate = verticalInput < 0 ? _brake : _deceleration;
                movable.Velocity = Math.Max(0f, currentVelocity - decelerationRate * deltaTime);
            }
        }
    }
}