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
        
        public void Update(float deltaTime)
        {
            Entities.Update();
            
            Entities.Foreach(e =>
            {
                if (e.Health <= 0)
                {
                    _factory.Release((IEntity)e, false);
                }
            });
        }
    }
}