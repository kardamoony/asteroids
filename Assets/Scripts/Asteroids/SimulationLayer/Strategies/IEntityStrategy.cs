using Asteroids.CoreLayer.Input;

namespace Asteroids.SimulationLayer.Strategies
{
    public interface IEntityStrategy<in TEntity>
    {
        void Execute(TEntity entity, IInputProvider inputProvider, float deltaTime);
    }
}