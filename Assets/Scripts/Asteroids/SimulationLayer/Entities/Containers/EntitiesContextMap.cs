using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Input;

namespace Asteroids.SimulationLayer.Entities.Containers
{
    public sealed class EntitiesContextMap<TEntity, TContext> : IEntitiesContextMap<TEntity, TContext>
    {
        private readonly Dictionary<TEntity, TContext> _entities = new Dictionary<TEntity, TContext>();
        private readonly List<KeyValuePair<TEntity, TContext>> _pendingToAdd = new List<KeyValuePair<TEntity, TContext>>();
        private readonly List<TEntity> _pendingToRemove = new List<TEntity>();

        public void Register(TEntity entity, TContext context)
        {
            _pendingToAdd.Add(new KeyValuePair<TEntity, TContext>(entity, context));
        }

        public void Unregister(TEntity entity)
        {
            _pendingToRemove.Add(entity);
        }
        
        public void Foreach(Action<TEntity, TContext> action)
        {
            foreach (var pair in _entities)
            {
                action.Invoke(pair.Key, pair.Value);
            }
        }

        public void Update(Action<TEntity, TContext> action)
        {
            AddPending();
            RemovePending();
            
            Foreach(action);
        }

        private void AddPending()
        {
            foreach (var pair in _pendingToAdd)
            {
                if (_entities.ContainsKey(pair.Key))
                {
                    return;
                }
            
                _entities.Add(pair.Key, pair.Value);
            }
            
            _pendingToAdd.Clear();
        }
        
        private void RemovePending()
        {
            foreach (var entity in _pendingToRemove)
            {
                if (!_entities.ContainsKey(entity))
                {
                    return;
                }

                _entities.Remove(entity);
            }
            
            _pendingToRemove.Clear();
            
        }
    }
}