using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Input;

namespace Asteroids.SimulationLayer.Entities
{
    public sealed class EntitiesInputMap<TEntity> : IEntitiesInputMap<TEntity>
    {
        private readonly Dictionary<TEntity, IInputProvider> _entities = new Dictionary<TEntity, IInputProvider>();

        public void Register(TEntity entity, IInputProvider inputProvider)
        {
            if (_entities.ContainsKey(entity))
            {
                return;
            }
            
            _entities.Add(entity, inputProvider);
        }

        public void Unregister(TEntity entity)
        {
            if (!_entities.ContainsKey(entity))
            {
                return;
            }

            _entities.Remove(entity);
        }

        public void Foreach(Action<TEntity, IInputProvider> action)
        {
            foreach (var pair in _entities)
            {
                action.Invoke(pair.Key, pair.Value);
            }
        }
    }
}