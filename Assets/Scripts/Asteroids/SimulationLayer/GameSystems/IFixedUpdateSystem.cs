namespace Asteroids.SimulationLayer.GameSystems
{
    public interface IFixedUpdateSystem
    {
        void Update(float fixedDeltaTime);
        void Deinitialize();
    }
}