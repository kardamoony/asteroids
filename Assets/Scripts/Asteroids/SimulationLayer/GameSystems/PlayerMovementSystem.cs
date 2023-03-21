using Asteroids.SimulationLayer.Models;
using Asteroids.SimulationLayer.Settings;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class PlayerMovementSystem : MovementSystem
    {
        public PlayerMovementSystem(IPlayerSettings settings) : base(new ThrustMovement(settings.Acceleration, settings.Deceleration, settings.Brake))
        {
            
        }
    }
}
