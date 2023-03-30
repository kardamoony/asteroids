namespace Asteroids.SimulationLayer.Entities
{
    public interface ICollidable
    {
        int Damage { get; }
        
        void HandleCollisionEnter(ICollidable collision);
        void HandleCollisionExit(ICollidable collision);
        void HandleCollisionStay(ICollidable collision);
    }
}