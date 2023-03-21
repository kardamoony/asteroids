using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Models
{
    public interface IRotationModel
    {
        void Rotate(IRotatable rotatable, IInputProvider inputProvider, float deltaTime);
    }
}