using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ExplosionSpawnSystem : SimpleEntitySystem<IExplodable>, IUpdateSystem
    {
        private readonly IObjectsFactory<IEntity> _factory;

        public ExplosionSpawnSystem(IObjectsFactory<IEntity> factory)
        {
            _factory = factory;
        }

        protected override void OnUpdated(float deltaTime)
        {
            Entities.Update(e =>
            {
                if (!e.Explode) return;

                e.Explode = false;
                
                _factory.Get<ISpawnable>(e.ExplosionAssetId, explosion =>
                {
                    explosion.SetPosition(e.ExplosionPosition);
                });
            });
        }
    }
}