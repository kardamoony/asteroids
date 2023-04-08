using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;
using Generated;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class AsteroidSpawnSystem : SpawnSystem
    {
        private static readonly string _assetId = AssetId.Asteroid.ToString();
        
        public AsteroidSpawnSystem(IObjectsFactory<IEntity> factory) : base(new AsteroidSpawnStrategy(_assetId, factory))
        {
        }
    }
}