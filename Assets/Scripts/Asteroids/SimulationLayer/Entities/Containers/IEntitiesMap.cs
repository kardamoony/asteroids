using System;

namespace Asteroids.SimulationLayer.Entities.Containers
{
    public interface IEntitiesMap<TEntity>
    {
        void Register(TEntity entity);
        void Unregister(TEntity entity);
        void Update(Action<TEntity> action);
        void Foreach(Action<TEntity> action);
    }
}