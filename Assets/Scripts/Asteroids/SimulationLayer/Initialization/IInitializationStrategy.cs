namespace Asteroids.SimulationLayer.Initialization
{
    public interface IInitializationStrategy
    {
        void Initialize();
        void Deinitialize();
    }
}