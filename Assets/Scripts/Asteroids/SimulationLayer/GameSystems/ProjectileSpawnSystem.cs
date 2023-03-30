using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Strategies;
using UnityEngine;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ProjectileSpawnSystem : SpawnSystem
    {
        public ProjectileSpawnSystem(IObjectsFactory<GameObject> factory, IEntityInitializer initializer) 
            : base(new ProjectileSpawnStrategy(factory, initializer))
        {
        }
    }
}