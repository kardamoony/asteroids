using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ThrustMovementSystem : MovementSystem
    {
        public ThrustMovementSystem(float acceleration, float deceleration, float brake) : base(new ThrustMovement(acceleration, deceleration, brake))
        {
        }
    }
}
