namespace Asteroids.SimulationLayer.GameSystems
{
    public interface IUpdateSystem
    {
        void Update(float deltaTime);
        void Deinitialize();
    }
}