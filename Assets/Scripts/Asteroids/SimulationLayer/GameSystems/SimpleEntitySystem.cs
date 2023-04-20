using Asteroids.SimulationLayer.Entities.Containers;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class SimpleEntitySystem<TEntity>
    {
        protected readonly IEntitiesMap<TEntity> Entities;
        
        public bool Initialized { get; private set; }
        
        protected SimpleEntitySystem()
        {
            //TODO: pass in constructor
            Entities = new EntitiesHashSet<TEntity>();
            Initialized = true;
        }

        public virtual void Register(TEntity entity)
        {
            Entities.Register(entity);
        }

        public virtual void Unregister(TEntity entity)
        {
            Entities.Unregister(entity);
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