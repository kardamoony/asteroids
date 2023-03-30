using System.Collections.Generic;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class SimpleEntitySystem<TEntity>
    {
        private readonly List<TEntity> _pendingToAdd = new List<TEntity>();
        private readonly List<TEntity> _pendingToRemove = new List<TEntity>();
        
        protected readonly HashSet<TEntity> Entities = new HashSet<TEntity>();
        
        public void Register(TEntity entity)
        {
            _pendingToAdd.Add(entity);
        }

        public void Unregister(TEntity entity)
        {
            _pendingToRemove.Add(entity);
        }

        protected void AddPending()
        {
            foreach (var entity in _pendingToAdd)
            {
                if (Entities.Contains(entity))
                {
                    continue;
                }
                
                Entities.Add(entity);
            }
            
            _pendingToAdd.Clear();
        }

        protected void RemovePending()
        {
            foreach (var entity in _pendingToRemove)
            {
                if (!Entities.Contains(entity))
                {
                    continue;
                }
                
                Entities.Remove(entity);
            }
            
            _pendingToRemove.Clear();
        }
    }
}