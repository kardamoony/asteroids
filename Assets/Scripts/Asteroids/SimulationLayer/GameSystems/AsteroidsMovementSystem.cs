using Asteroids.SimulationLayer.Models;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class AsteroidsMovementSystem : MovementSystem
    {
        public AsteroidsMovementSystem() : base(new ConstantMovement())
        {
            
        }
    }
}