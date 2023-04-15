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

        public virtual void Register(TEntity entity)
        {
            Entities.Register(entity);
        }

        public virtual void Unregister(TEntity entity)
        {
            Entities.Unregister(entity);
        }
    }
}