using Asteroids.SimulationLayer.Entities.Containers;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class EntityContextSystem<TEntity, TContext>
    {
        protected readonly IEntitiesContextMap<TEntity, TContext> EntitiesContextMap;

        protected EntityContextSystem()
        {
            //TODO: pass in the constructor
            EntitiesContextMap = new EntitiesContextMap<TEntity, TContext>();
        }
 
        public virtual void Register(TEntity entity, TContext context)
        {
            EntitiesContextMap.Register(entity, context);
        }

        public virtual void Unregister(TEntity entity)
        {
            EntitiesContextMap.Unregister(entity);
        }
    }
}