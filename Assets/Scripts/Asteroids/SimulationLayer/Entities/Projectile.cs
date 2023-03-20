using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class Projectile : IMovable
    {
        public Vector3 Translation { get; set; }
        public float Speed { get; }
        public float Velocity { get; set; }

        public Projectile(float speed)
        {
            Speed = speed;
        }
    }
}