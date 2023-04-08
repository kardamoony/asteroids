using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;
using Generated;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ProjectileSpawnSystem : InputSpawnSystem
    {
        private static readonly string _assetId = AssetId.Projectile.ToString();
        
        public ProjectileSpawnSystem(IObjectsFactory<IEntity> factory) : base(new ProjectileSpawnStrategy(_assetId, factory))
        {
        }
    }
}