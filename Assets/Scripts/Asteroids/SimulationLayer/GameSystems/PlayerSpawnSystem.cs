using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class PlayerSpawnSystem : SpawnSystem
    {
        public PlayerSpawnSystem(int tries, string assetId, IObjectsFactory<IEntity> factory) 
            : base(new PlayerSpawnStrategy(tries, assetId, factory))
        {
        }
    }
}