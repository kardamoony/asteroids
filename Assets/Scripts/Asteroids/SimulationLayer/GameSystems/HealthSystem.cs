using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class HealthSystem : SimpleEntitySystem<IDestructable>, IUpdateSystem
    {
        private readonly IEntityInitializer _initializer;
        
        public HealthSystem(IEntityInitializer initializer)
        {
            _initializer = initializer;
        }
        
        public void Update(float deltaTime)
        {
            Entities.Update();
            
            Entities.Foreach(e =>
            {
                if (e.Health <= 0)
                {
                    _initializer.DeinitializeEntity((IEntity)e);
                }
            });
        }
    }
}