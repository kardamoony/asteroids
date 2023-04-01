using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Strategies;
using UnityEngine;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class AsteroidSpawnSystem : SpawnSystem
    {
        public AsteroidSpawnSystem(IObjectsFactory<GameObject> factory, IEntityInitializer initializer) 
            : base(new AsteroidSpawnStrategy(factory, initializer))
        {
        }
    }
}