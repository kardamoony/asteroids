using System;
using System.Collections.Generic;

namespace Asteroids.SimulationLayer.Entities.Containers
{
    public class EntitiesHashSet<TEntity> : IEntitiesMap<TEntity>
    {
        private readonly HashSet<TEntity> _entities = new HashSet<TEntity>();
        private readonly List<TEntity> _entitiesToAdd = new List<TEntity>();
        private readonly List<TEntity> _entitiesToRemove= new List<TEntity>();

        public void Register(TEntity entity)
        {
            _entitiesToAdd.Add(entity);
        }

        public void Unregister(TEntity entity)
        {
            _entitiesToRemove.Remove(entity);
        }

        public void Update(Action<TEntity> action)
        {
            AddPending();
            RemovePending();
            
            Foreach(action);
        }
        
        private void AddPending()
        {
            foreach (var entity in _entitiesToAdd)
            {
                if (_entities.Contains(entity))
                {
                    return;
                }
            
                _entities.Add(entity);
            }
            
            _entitiesToAdd.Clear();
        }
        
        private void RemovePending()
        {
            foreach (var entity in _entitiesToRemove)
            {
                if (!_entities.Contains(entity))
                {
                    return;
                }

                _entities.Remove(entity);
            }
            
            _entitiesToRemove.Clear();
            
        }

        public void Foreach(Action<TEntity> action)
        {
            foreach (var entity in _entities)
            {
                action.Invoke(entity);
            }
        }
    }
}