using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Input;

namespace Asteroids.SimulationLayer.Entities.Containers
{
    public sealed class EntitiesInputMap<TEntity> : IEntitiesInputMap<TEntity>
    {
        private readonly Dictionary<TEntity, IInputProvider> _entities = new Dictionary<TEntity, IInputProvider>();
        private readonly List<KeyValuePair<TEntity, IInputProvider>> _pendingToAdd = new List<KeyValuePair<TEntity, IInputProvider>>();
        private readonly List<TEntity> _pendingToRemove = new List<TEntity>();

        public void Register(TEntity entity, IInputProvider inputProvider)
        {
            _pendingToAdd.Add(new KeyValuePair<TEntity, IInputProvider>(entity, inputProvider));
        }

        public void Unregister(TEntity entity)
        {
            _pendingToRemove.Add(entity);
        }
        
        public void Foreach(Action<TEntity, IInputProvider> action)
        {
            foreach (var pair in _entities)
            {
                action.Invoke(pair.Key, pair.Value);
            }
        }

        public void Update()
        {
            AddPending();
            RemovePending();
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