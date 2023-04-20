using Asteroids.SimulationLayer.Entities.Containers;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class EntityContextSystem<TEntity, TContext>
    {
        protected readonly IEntitiesContextMap<TEntity, TContext> EntitiesContextMap;
        
        public bool Initialized { get; private set; }

        protected EntityContextSystem()
        {
            //TODO: pass in the constructor
            EntitiesContextMap = new EntitiesContextMap<TEntity, TContext>();
            Initialized = true;
        }
 
        public virtual void Register(TEntity entity, TContext context)
        {
            EntitiesContextMap.Register(entity, context);
        }

        public virtual void Unregister(TEntity entity)
        {
            EntitiesContextMap.Unregister(entity);
        }

        public void Update(float deltaTime)
        {
            if (!Initialized) return;
            OnUpdated(deltaTime);
        }

        public void Deinitialize()
        {
            Initialized = false;
            OnDeinitialized();
        }
        
        protected virtual void OnUpdated(float deltaTime){}
        protected virtual void OnDeinitialized(){}
    }
}