namespace Asteroids.SimulationLayer.Initialization
{
    public interface IInitializer<TObject, in TContext>
    {
        void InitializeObject(TObject @object, TContext context);
        void DeinitializeObject(TObject @object, bool dispose);
    }
}