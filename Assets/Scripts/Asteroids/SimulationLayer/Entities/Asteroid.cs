namespace Asteroids.SimulationLayer.Entities
{
    public class Asteroid : Entity, IMovable, ICollidable
    {
        public float Speed { get; }
        public float Velocity { get; set; }

        public Asteroid(float speed)
        {
            Speed = speed;
        }

        public int Damage { get; }
        
        public void HandleCollisionEnter(ICollidable collision)
        {
            
        }

        public void HandleCollisionExit(ICollidable collision){}

        public void HandleCollisionStay(ICollidable collision){}
    }
}
