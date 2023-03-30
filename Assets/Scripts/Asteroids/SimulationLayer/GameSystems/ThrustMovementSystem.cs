using Asteroids.SimulationLayer.Strategies;
using Asteroids.SimulationLayer.Settings;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ThrustMovementSystem : MovementSystem
    {
        public ThrustMovementSystem(IPlayerSettings settings) : base(new ThrustMovement(settings.Acceleration, settings.Deceleration, settings.Brake))
        {
            
        }
    }
}
