namespace Asteroids.SimulationLayer.Strategies
{
    public interface IContextEntityStrategy<in TEntity, in TContext>
    {
        void Execute(TEntity entity, TContext context, float deltaTime);
    }
}