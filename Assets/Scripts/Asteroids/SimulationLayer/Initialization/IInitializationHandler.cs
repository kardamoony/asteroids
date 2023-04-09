namespace Asteroids.SimulationLayer.Initialization
{
    public interface IInitializationHandler<TObject, TContext>
    {
        IInitializationHandler<TObject, TContext> Next { set; }

        void HandleInitialization(TObject @object, TContext context);
        void HandleDeinitialization(TObject @object);
    }
}