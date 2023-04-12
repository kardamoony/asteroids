using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class PlayerSpawnSystem : SpawnSystem
    {
        public PlayerSpawnSystem(SpawnStrategy strategy) 
            : base(strategy)
        {
        }
    }
}