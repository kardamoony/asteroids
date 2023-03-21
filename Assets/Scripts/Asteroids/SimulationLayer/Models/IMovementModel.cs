using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Models
{
    public interface IMovementModel
    {
        void Move(IMovable movable, IInputProvider input, float deltaTime);
    }
}