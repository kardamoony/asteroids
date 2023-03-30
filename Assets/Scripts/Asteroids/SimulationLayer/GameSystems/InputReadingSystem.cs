using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class InputReadingSystem<TEntity>
    {
        protected IEntitiesInputMap<TEntity> EntitiesInputMap;
 
        public void Register(TEntity entity, IInputProvider inputProvider)
        {
            EntitiesInputMap.Register(entity, inputProvider);
        }

        public void Unregister(TEntity entity)
        {
            EntitiesInputMap.Unregister(entity);
        }
    }
}