namespace Asteroids.SimulationLayer.Entities
{
    public class Asteroid : Entity, IMovable, ICollidable, IDestructable
    {
        public float Speed { get; }
        public float Velocity { get; set; }
        
        public int Health { get; set; }
        
        public int Damage { get; }

        public Asteroid(float speed)
        {
            Speed = speed;
            Health = 1; //TODO: from settings
            Damage = 1; //TODO: from settings
        }
        
        public void HandleCollisionEnter(ICollidable collision)
        {
            Health -= collision.Damage;
        }

        public void HandleCollisionExit(ICollidable collision){}

        public void HandleCollisionStay(ICollidable collision){}
        
    }
}
