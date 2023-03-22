using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class AsteroidsMovementSystem : MovementSystem
    {
        public AsteroidsMovementSystem() : base(new ConstantMovement())
        {
            
        }
    }
}