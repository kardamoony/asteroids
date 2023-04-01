using System;
using Asteroids.CoreLayer.Input;

namespace Asteroids.SimulationLayer.Entities.Containers
{
    public interface IEntitiesInputMap<TEntity>
    {
        void Register(TEntity entity, IInputProvider inputProvider);
        void Unregister(TEntity entity);
        void Foreach(Action<TEntity, IInputProvider> action);
        void Update();
    }
}