using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Models
{
    public class ConstantMovement : IMovementModel
    {
        public void Move(IMovable movable, IInputProvider input, float deltaTime)
        {
            movable.Velocity = movable.Speed * input.VerticalAxis;
        }
    }
}