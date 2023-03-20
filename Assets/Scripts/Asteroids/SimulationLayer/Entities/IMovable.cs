namespace Asteroids.SimulationLayer.Entities
{
    public interface IMovable
    {
        float Speed { get; }
        float Velocity { get; set; }
    }
}