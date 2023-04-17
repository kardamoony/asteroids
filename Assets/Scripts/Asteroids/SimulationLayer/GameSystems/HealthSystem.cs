using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class HealthSystem : SimpleEntitySystem<IDestructable>, IUpdateSystem
    {
        private readonly IObjectsFactory<IEntity> _factory;

        public HealthSystem(IObjectsFactory<IEntity> factory)
        {
            _factory = factory;
        }
        
        protected override void OnUpdated(float deltaTime)
        {
            Entities.Update(e =>
            {
                if (e.Health <= 0)
                {
                    _factory.Release((IEntity)e, false);
                }
            });
        }
    }
}