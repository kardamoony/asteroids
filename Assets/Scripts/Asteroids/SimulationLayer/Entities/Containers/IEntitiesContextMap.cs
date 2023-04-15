using System;
using Asteroids.CoreLayer.Input;

namespace Asteroids.SimulationLayer.Entities.Containers
{
    public interface IEntitiesContextMap<TEntity, TContext>
    {
        void Register(TEntity entity, TContext context);
        void Unregister(TEntity entity);
        void Foreach(Action<TEntity, TContext> action);
        void Update(Action<TEntity, TContext> action);
    }
}