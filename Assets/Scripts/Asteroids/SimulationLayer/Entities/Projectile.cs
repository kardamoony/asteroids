using System;

namespace Asteroids.SimulationLayer.Entities
{
    public class Projectile : Entity, IProjectile, IMovable, ICollidable, IDestructable
    {
        public IMovable Movable => this;
        public ICollidable Collidable => this;
        public IDestructable Destructable => this;
        public float Speed { get; }
        public float Velocity { get; set; }
        public int Health { get; set; } = 1;

        public int Damage { get; } = 1;

        public Projectile(float speed) : base(new TimeSpan(0, 0, 3))
        {
            Speed = speed;
        }

        public void HandleCollisionEnter(ICollidable other)
        {
            Health = 0;
        }

        public void HandleCollisionExit(ICollidable other){}

        public void HandleCollisionStay(ICollidable other){}
    }
}