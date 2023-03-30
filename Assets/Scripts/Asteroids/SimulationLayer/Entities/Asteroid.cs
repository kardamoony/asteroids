namespace Asteroids.SimulationLayer.Entities
{
    public class Asteroid : Entity, IMovable
    {
        public float Speed { get; }
        public float Velocity { get; set; }

        public Asteroid(float speed)
        {
            Speed = speed;
        }
    }
}
