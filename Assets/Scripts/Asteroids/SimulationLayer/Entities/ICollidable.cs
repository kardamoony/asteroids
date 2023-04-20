using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface ICollidable
    {
        int Damage { get; }
        
        Collision Collision{ get; set; }
        
        void HandleCollisionEnter(ICollidable collision);
        void HandleCollisionExit(ICollidable collision);
        void HandleCollisionStay(ICollidable collision);
    }
}