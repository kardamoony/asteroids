using UnityEngine;

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

        public Projectile(float speed)
        {
            Speed = speed;
        }

        public void InvokeDestructionEvent(GameObject gameObject, float destructionDelay)
        {
            //TODO: return object to pool
        }
        
        public void HandleCollisionEnter(ICollidable other)
        {
            Health = 0;
        }

        public void HandleCollisionExit(ICollidable other){}

        public void HandleCollisionStay(ICollidable other){}
    }
}