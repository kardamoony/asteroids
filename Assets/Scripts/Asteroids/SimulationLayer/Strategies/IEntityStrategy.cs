namespace Asteroids.SimulationLayer.Strategies
{
    public interface IEntityStrategy<in TEntity>
    {
        void Execute(TEntity entity, float deltaTime);
    }
}