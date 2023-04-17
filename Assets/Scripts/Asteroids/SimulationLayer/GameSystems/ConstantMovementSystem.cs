using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ConstantMovementSystem : MovementSystem
    {
        public ConstantMovementSystem() : base(new ConstantMovement())
        {
        }
    }
}