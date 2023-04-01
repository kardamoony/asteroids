using Asteroids.SimulationLayer.Entities.Containers;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class SimpleEntitySystem<TEntity>
    {
        protected readonly IEntitiesMap<TEntity> Entities;

        protected SimpleEntitySystem()
        {
            //TODO: pass in constructor
            Entities = new EntitiesHashSet<TEntity>();
        }

        public void Register(TEntity entity)
        {
            Entities.Register(entity);
        }

        public void Unregister(TEntity entity)
        {
            Entities.Unregister(entity);
        }
    }
}