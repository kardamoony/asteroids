using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Strategies;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ProjectileSpawnSystem : InputSpawnSystem
    {
        private static readonly string _assetId = AssetId.Projectile.ToString();
        
        public ProjectileSpawnSystem(IObjectsFactory<GameObject> factory, IEntityInitializer initializer) 
            : base(new ProjectileSpawnStrategy(_assetId, factory, initializer))
        {
        }
    }
}