using Asteroids.SimulationLayer.Strategies;
using Asteroids.SimulationLayer.Settings;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ThrustMovementSystem : MovementSystem
    {
        public ThrustMovementSystem(float acceleration, float deceleration, float brake) : base(new ThrustMovement(acceleration, deceleration, brake))
        {
            
        }
    }
}
