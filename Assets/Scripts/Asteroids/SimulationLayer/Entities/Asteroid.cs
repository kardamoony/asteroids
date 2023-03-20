namespace Asteroids.SimulationLayer.Entities
{
    public class Asteroid : IMovable
    {
        public float Speed { get; }
        public float Velocity { get; set; }

        public Asteroid(float speed)
        {
            Speed = speed;
        }
    }
}
