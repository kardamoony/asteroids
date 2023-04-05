using Asteroids.SimulationLayer.Entities.Containers;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class ContextSystem<TEntity, TContext>
    {
        protected readonly IEntitiesContextMap<TEntity, TContext> EntitiesContextMap;

        protected ContextSystem()
        {
            //TODO: pass in the constructor
            EntitiesContextMap = new EntitiesContextMap<TEntity, TContext>();
        }
 
        public void Register(TEntity entity, TContext context)
        {
            EntitiesContextMap.Register(entity, context);
        }

        public void Unregister(TEntity entity)
        {
            EntitiesContextMap.Unregister(entity);
        }
    }
}