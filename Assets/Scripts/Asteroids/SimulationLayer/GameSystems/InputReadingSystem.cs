using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Entities.Containers;

namespace Asteroids.SimulationLayer.GameSystems
{
    public abstract class InputReadingSystem<TEntity>
    {
        protected readonly IEntitiesInputMap<TEntity> EntitiesInputMap;

        protected InputReadingSystem()
        {
            //TODO: pass in the constructor
            EntitiesInputMap = new EntitiesInputMap<TEntity>();
        }
 
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