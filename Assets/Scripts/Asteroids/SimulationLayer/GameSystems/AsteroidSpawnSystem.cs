using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Strategies;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class AsteroidSpawnSystem : SpawnSystem
    {
        private static readonly string _assetId = AssetId.Asteroid.ToString();
        
        public AsteroidSpawnSystem(IObjectsFactory<GameObject> factory, IEntityInitializer initializer) 
            : base(new AsteroidSpawnStrategy(_assetId, factory, initializer))
        {
        }
    }
}